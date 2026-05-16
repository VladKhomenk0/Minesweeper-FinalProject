using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1.Core
{
    public partial class Form1 : Form
    {
        private Game game;
        private Difficulty selectedDifficulty;
        private TimeManager timeManager;
        private int totalMines = 0;
        private bool isPanelExpanded = true;
        private Timer panelTimer;
        private Dictionary<string, Image> cellImages;
        private int cellSize;
        private GameRenderer renderer;
        private Label customTooltip;
        private bool isPaused = false;
        private bool gameStarted = false;
        private Panel pauseOverlayPanel;
        private Label pauseLabel;
        private Button resumeButton;
        private Difficulty currentLevel;
        private GameMemento _loadedSave;
        private ReplayManager replayManager;
        // Техніка: Replace Magic Numbers with Constants
        private const int GamePanelDefaultWidth = 660;
        private const int GamePanelDefaultHeight = 480;
        private const int MenuPanelMaxHeight = 150;
        private const int MenuPanelMinHeight = 0;
        private const int AnimationStep = 10;

        public Form1(GameMemento saveToLoad = null)
        {
            _loadedSave = saveToLoad;
            InitializeComponent();
            SetupUI();

            timeManager = new TimeManager(labelTimer);

            btnSafeCell.Click += BtnSafeCell_Click;
            this.borderForAdditionInfo.Paint += new PaintEventHandler(this.borderForAdditionInfo_Paint);
            this.pausePanel.Click += new System.EventHandler(this.pausePanel_Click);

            SetupPauseOverlay();
        }

        private void SetupUI()
        {
            this.Text = "Minesweeper";

            panelGame.Visible = true;
            buttonNewGame.Visible = false;
            buttonStartGame.Enabled = true;

            labelTimer.Text = "Час: 0 с";

            radioEasy.CheckedChanged += RadioDifficulty_CheckedChanged;
            radioMedium.CheckedChanged += RadioDifficulty_CheckedChanged;
            radioHard.CheckedChanged += RadioDifficulty_CheckedChanged;

            buttonStartGame.Click += (s, e) => StartGame();
            buttonNewGame.Click += (s, e) => StartGame();
            buttonNewGame.Enabled = false;

            groupDifficulty.Height = MenuPanelMaxHeight;

            panelTimer = new Timer();
            panelTimer.Interval = 15;
            panelTimer.Tick += PanelTimer_Tick;

            buttonTogglePanel.Click += (s, e) =>
            {
                panelTimer.Start();
            };

            SetupCustomTooltip();
            LoadCellImages();
        }

        // Техніка: Extract Method (Виділення методу)
        private void StartGame()
        {
            InitializeGameData();
            ConfigureUIForGame();
            UpdateGameStatsUI();
        }

        private void InitializeGameData()
        {
            if (_loadedSave != null)
            {
                selectedDifficulty = _loadedSave.GameDifficulty;
                game = new Game(selectedDifficulty);
                game.RestoreState(_loadedSave);
                timeManager.Reset(_loadedSave.ElapsedSeconds);

                if (selectedDifficulty == Difficulty.Easy) radioEasy.Checked = true;
                else if (selectedDifficulty == Difficulty.Medium) radioMedium.Checked = true;
                else if (selectedDifficulty == Difficulty.Hard) radioHard.Checked = true;

                _loadedSave = null;
            }
            else
            {
                UpdateSelectedDifficulty();
                game = new Game(selectedDifficulty);
                game.ResetFirstClick();
                timeManager.Reset(0);
            }

            totalMines = game.Board.TotalMines;
            gameStarted = true;
            currentLevel = selectedDifficulty;
        }

        private void ConfigureUIForGame()
        {
            panelGame.Visible = true;
            buttonNewGame.Visible = true;
            buttonNewGame.Enabled = true;
            buttonStartGame.Visible = false;
            btnSafeCell.Visible = true;
            btnSafeCell.Enabled = true;
            panelGame.Enabled = true;

            panelGame.Width = GamePanelDefaultWidth;
            panelGame.Height = GamePanelDefaultHeight;

            cellSize = Math.Min(panelGame.Width / game.Board.Columns, panelGame.Height / game.Board.Rows);

            timeManager.Stop();
            timeManager.Start();

            renderer = new GameRenderer(panelGame, game, cellSize, cellImages);
            renderer.DrawBoard(Cell_Click);
            replayManager = new ReplayManager(game, renderer, labelTimer);
        }

        private void UpdateGameStatsUI()
        {
            lblSafeOpensRemaining.Text = $"Залишилось:\n{renderer.GetRemainingHighlights()}";
            labelProgress.Text = $"Відкрито:\n{game.GetRevealedPercentage()}%";
            lvlNow.Text = $"Рівень: {GetDifficultyDisplayName(currentLevel)}";

            int best = ProfileManager.Instance.CurrentProfile.BestTimes[currentLevel];
            topRecord.Text = best == int.MaxValue
                ? "Найкращий час:---"
                : $"Найкращий час: {best} с";

            UpdateMinesLeft();
        }

        private void RadioDifficulty_CheckedChanged(object sender, EventArgs e)
        {
            if (radioEasy.Checked) selectedDifficulty = Difficulty.Easy;
            else if (radioMedium.Checked) selectedDifficulty = Difficulty.Medium;
            else if (radioHard.Checked) selectedDifficulty = Difficulty.Hard;
        }

        private void UpdateSelectedDifficulty()
        {
            if (radioEasy.Checked) selectedDifficulty = Difficulty.Easy;
            else if (radioMedium.Checked) selectedDifficulty = Difficulty.Medium;
            else if (radioHard.Checked) selectedDifficulty = Difficulty.Hard;
        }

        private void UpdateMinesLeft()
        {
            int flagged = game.Board.Cells.Cast<Cell>().Count(c => c.IsFlagged);
            int remaining = totalMines - flagged;
            labelMinesLeft.Text = $"Міни: {remaining}";
        }

        private async void Cell_Click(object sender, MouseEventArgs e)
        {
            if (sender is not PictureBox pic || game.State != GameState.Playing) return;
            if (replayManager != null && replayManager.IsReplaying) return; // Блокування кліків під час реплею

            Point p = (Point)pic.Tag;
            List<Cell> changedCells = new();
            bool wasFirstClick = game.IsFirstClick;

            ICommand cmd = null;

            // Замість прямих викликів генерування команди
            if (e.Button == MouseButtons.Left)
            {
                cmd = new RevealCommand(game, p.X, p.Y);
            }
            else if (e.Button == MouseButtons.Right)
            {
                cmd = new FlagCommand(game, p.X, p.Y);
            }

            if (cmd != null)
            {
                changedCells = cmd.Execute();

                // Якщо це був перший клік, міни щойно розставились. Робиться чистий знімок для реплею
                if (wasFirstClick)
                {
                    replayManager.SetInitialState(game.GetCleanState());
                }

                // Записування команд в історію
                replayManager.AddCommand(cmd);
            }

            renderer.RedrawCells(changedCells);
            UpdateMinesLeft();
            labelProgress.Text = $"Відкрито:\n{game.GetRevealedPercentage()}%";

            // Техніка: Decompose Conditional
            if (game.State == GameState.Lost)
            {
                await HandleGameLossAsync();
            }
            else if (game.State == GameState.Won)
            {
                await HandleGameWinAsync();
            }
        }

        private async System.Threading.Tasks.Task HandleGameLossAsync()
        {
            timeManager.Stop();
            ProfileManager.Instance.CurrentProfile.GamesPlayed++;
            ProfileManager.Instance.SaveProfiles();
            SaveManager.DeleteSave(ProfileManager.Instance.CurrentProfile.Name);

            var replayResult = MessageBox.Show("Гра закінчилась. Ви програли!\n\nБажаєте переглянути повтор Вашої гри?", "Поразка", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (replayResult == DialogResult.Yes)
            {
                await replayManager.PlayReplayAsync();
            }

            var result = MessageBox.Show("Спробувати ще раз?", "Нова гра", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) StartGame();
        }

        private async System.Threading.Tasks.Task HandleGameWinAsync()
        {
            timeManager.Stop();
            SaveManager.DeleteSave(ProfileManager.Instance.CurrentProfile.Name);

            var currentProfile = ProfileManager.Instance.CurrentProfile;
            currentProfile.GamesPlayed++;
            currentProfile.GamesWon++;

            if (timeManager.ElapsedSeconds < currentProfile.BestTimes[selectedDifficulty])
            {
                currentProfile.BestTimes[selectedDifficulty] = timeManager.ElapsedSeconds;
                MessageBox.Show($"Новий рекорд: {timeManager.ElapsedSeconds} секунд для рівня {GetDifficultyDisplayName(selectedDifficulty)}!", "Рекорд");
            }
            ProfileManager.Instance.SaveProfiles();

            var replayResult = MessageBox.Show("Перемога!\n\nБажаєте переглянути повтор Вашої ідеальної гри?", "Перемога", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (replayResult == DialogResult.Yes)
            {
                await replayManager.PlayReplayAsync();
            }

            if (selectedDifficulty == Difficulty.Easy)
            {
                var result = MessageBox.Show("Вітаємо, Ви пройшли легкий рівень!\n\nПерейти до середнього рівня?", "Перемога", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) { radioMedium.Checked = true; StartGame(); labelProgress.Text = $"Відкрито:\n{game.GetRevealedPercentage()}%"; }
            }
            else if (selectedDifficulty == Difficulty.Medium)
            {
                var result = MessageBox.Show("Чудово! Ви пройшли середній рівень.\n\nПерейти до складного рівня?", "Перемога", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) { radioHard.Checked = true; StartGame(); labelProgress.Text = $"Відкрито:\n{game.GetRevealedPercentage()}%"; }
            }
            else if (selectedDifficulty == Difficulty.Hard)
            {
                MessageBox.Show("Вітаємо, Вам вдалось пройти останній рівень складності!", "Перемога", MessageBoxButtons.OK);
                var result = MessageBox.Show("Грати ще раз?", "Нова гра", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) StartGame();
            }
        }

        private void PanelTimer_Tick(object sender, EventArgs e)
        {
            if (!isPanelExpanded)
            {
                groupDifficulty.Height += AnimationStep;
                if (groupDifficulty.Height >= MenuPanelMaxHeight)
                {
                    groupDifficulty.Height = MenuPanelMaxHeight;
                    panelTimer.Stop();
                    isPanelExpanded = true;
                }
            }
            else
            {
                groupDifficulty.Height -= AnimationStep;
                if (groupDifficulty.Height <= MenuPanelMinHeight)
                {
                    groupDifficulty.Height = MenuPanelMinHeight;
                    panelTimer.Stop();
                    isPanelExpanded = false;
                }
            }
        }

        private void LoadCellImages()
        {
            cellImages = new Dictionary<string, Image>
            {
        { "field_0", Properties.Resources.field_0 },
        { "ground", Properties.Resources.ground },
        { "mine", Properties.Resources.mine },
        { "flag", Properties.Resources.flag },
        { "field_1", Properties.Resources.field_1 },
        { "field_2", Properties.Resources.field_2 },
        { "field_3", Properties.Resources.field_3 },
        { "field_4", Properties.Resources.field_4 },
        { "field_5", Properties.Resources.field_5 },
        { "field_6", Properties.Resources.field_6 },
        };
        }

        private void BtnSafeCell_Click(object sender, EventArgs e)
        {
            var safeCell = game.GetSafeCell();
            if (safeCell != null)
            {
                renderer.HighlightCell(safeCell);
            }
            lblSafeOpensRemaining.Text = $"Залишилось:\n{renderer.GetRemainingHighlights()}";

            if (renderer.IsHighlightsLimitReached())
            {
                btnSafeCell.Enabled = false;
            }
        }

        private void SetupCustomTooltip()
        {
            customTooltip = new Label();
            customTooltip.BackColor = Color.OliveDrab;
            customTooltip.ForeColor = Color.Black;
            customTooltip.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            customTooltip.BorderStyle = BorderStyle.FixedSingle;
            customTooltip.AutoSize = true;
            customTooltip.Padding = new Padding(5);
            customTooltip.Visible = false;
            this.Controls.Add(customTooltip);

            btnSafeCell.MouseEnter += (s, e) =>
            {
                var btn = s as Control;
                customTooltip.Text = "Це безпечне відкриття." +
                    "\nВоно допоможе знайти клітинки без мін." +
                    "\nДля рівней складності є своя кількість:" +
                    "\n      Для легкого - 3 безпечних відкриттів;" +
                    "\n      Для середнього - 5 безпечних відкриттів;" +
                    "\n      Для складного - 7 безпечних відкриттів;" +
                    "\nКористуйся з розумом!";

                customTooltip.Visible = true;
                customTooltip.Update();

                var btnScreenPos = btn.PointToScreen(Point.Empty);
                var btnClientPos = this.PointToClient(btnScreenPos);

                int tooltipX = btnClientPos.X;
                int tooltipY = btnClientPos.Y - customTooltip.Height - 5;

                if (tooltipY < 0)
                    tooltipY = btnClientPos.Y + btn.Height + 5;

                customTooltip.Location = new Point(tooltipX, tooltipY);

                customTooltip.BringToFront();
            };

            btnSafeCell.MouseLeave += (s, e) =>
            {
                customTooltip.Visible = false;
            };
        }

        private void borderForAdditionInfo_Paint(object sender, PaintEventArgs e)
        {
            Control p = (Control)sender;
            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.DrawRectangle(pen, 1, 1, p.Width - 2, p.Height - 2);
            }

            this.Controls.SetChildIndex(this.borderForAdditionInfo, 4);
        }

        private void SetupPauseOverlay()
        {
            pauseOverlayPanel = new Panel
            {
                Size = new Size(831, 489),
                Location = new Point((this.ClientSize.Width - 658) / 2, (this.ClientSize.Height - 534) / 2),
                BackColor = Color.FromArgb(0, 0, 0),
                Visible = false
            };

            pauseLabel = new Label
            {
                Text = "Гру призупинено",
                AutoSize = true,
                Font = new Font("Consolas", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(90, 140, 59),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point((pauseOverlayPanel.Width - 280) / 2 , 100)
            };

            resumeButton = new Button
            {
                Text = "Продовжити",
                Font = new Font("Consolas", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(90, 140, 59),
                BackColor = Color.Black,
                Size = new Size(150, 40),
                FlatStyle = FlatStyle.Flat,
                Location = new Point((pauseOverlayPanel.Width - 136) / 2, 300)
            };

            resumeButton.Click += (s, e) =>
            {
                // РЕФАКТОРИНГ: Замість дублювання коду викликаємо метод
                ResumeGame();
            };

            Button saveExitButton = new Button
            {
                Text = "Зберегти та вийти",
                Font = new Font("Consolas", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(90, 140, 59),
                BackColor = Color.Black,
                Size = new Size(250, 40),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(((pauseOverlayPanel.Width - 250) / 2) + 7, 360)
            };

            saveExitButton.Click += (s, e) =>
            {
                var memento = game.CreateMemento(timeManager.ElapsedSeconds);
                SaveManager.SaveGame(memento);
                MessageBox.Show("Гру успішно збережено!", "Збереження", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            };

            pauseOverlayPanel.Controls.Add(pauseLabel);
            pauseOverlayPanel.Controls.Add(resumeButton);
            pauseOverlayPanel.Controls.Add(saveExitButton);
            this.Controls.Add(pauseOverlayPanel);
        }


        // --- ДОДАНО: Виділені методи для інкапсуляції логіки паузи (Extract Method) ---
        private void PauseGame()
        {
            isPaused = true;
            timeManager.Stop();

            panelGame.Enabled = false;
            buttonNewGame.Enabled = false;
            buttonStartGame.Enabled = false;

            radioEasy.Enabled = false;
            radioMedium.Enabled = false;
            radioHard.Enabled = false;

            pauseOverlayPanel.Visible = true;
            pauseOverlayPanel.BringToFront();
        }

        private void ResumeGame()
        {
            isPaused = false;
            timeManager.Start();

            panelGame.Enabled = true;
            buttonNewGame.Enabled = true;
            buttonStartGame.Enabled = true;

            radioEasy.Enabled = true;
            radioMedium.Enabled = true;
            radioHard.Enabled = true;

            pauseOverlayPanel.Visible = false;

            lblSafeOpensRemaining.BringToFront();
            labelProgress.BringToFront();
            customTooltip.BringToFront();
            btnSafeCell.BringToFront();
        }
        // ---------------------------------------------------------------------------------

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (!gameStarted || game.State != GameState.Playing)
                    return true;

                if (!isPaused)
                {
                    // РЕФАКТОРИНГ
                    PauseGame();
                }
                else
                {
                    // РЕФАКТОРИНГ
                    ResumeGame();
                }

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void pausePanel_Click(object sender, EventArgs e)
        {
            if (!gameStarted || game.State != GameState.Playing)
                return;

            if (isPaused) return;

            // РЕФАКТОРИНГ
            PauseGame();
        }

        private string GetDifficultyDisplayName(Difficulty diff)
        {
            return diff switch
            {
                Difficulty.Easy => "Легкий",
                Difficulty.Medium => "Середній",
                Difficulty.Hard => "Складний",
                _ => "Невідомо"
            };
        }
    }
}