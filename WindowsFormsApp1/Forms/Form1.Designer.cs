using System.Windows.Forms;

namespace WindowsFormsApp1.Core
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupDifficulty = new System.Windows.Forms.GroupBox();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.buttonNewGame = new System.Windows.Forms.Button();
            this.radioHard = new System.Windows.Forms.RadioButton();
            this.radioMedium = new System.Windows.Forms.RadioButton();
            this.radioEasy = new System.Windows.Forms.RadioButton();
            this.panelGame = new System.Windows.Forms.Panel();
            this.labelTimer = new System.Windows.Forms.Label();
            this.labelMinesLeft = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pausePanel = new System.Windows.Forms.PictureBox();
            this.lvlNow = new System.Windows.Forms.Label();
            this.pictureClock = new System.Windows.Forms.PictureBox();
            this.topRecord = new System.Windows.Forms.Label();
            this.buttonTogglePanel = new System.Windows.Forms.Button();
            this.pictureRedmine = new System.Windows.Forms.PictureBox();
            this.btnSafeCell = new System.Windows.Forms.Button();
            this.labelProgress = new System.Windows.Forms.Label();
            this.lblSafeOpensRemaining = new System.Windows.Forms.Label();
            this.borderForAdditionInfo = new System.Windows.Forms.Panel();
            this.bgPanelGame = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupDifficulty.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pausePanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureClock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureRedmine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgPanelGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupDifficulty
            // 
            this.groupDifficulty.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupDifficulty.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.groupDifficulty.Controls.Add(this.buttonStartGame);
            this.groupDifficulty.Controls.Add(this.buttonNewGame);
            this.groupDifficulty.Controls.Add(this.radioHard);
            this.groupDifficulty.Controls.Add(this.radioMedium);
            this.groupDifficulty.Controls.Add(this.radioEasy);
            this.groupDifficulty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupDifficulty.Font = new System.Drawing.Font("Consolas", 13F, System.Drawing.FontStyle.Bold);
            this.groupDifficulty.ForeColor = System.Drawing.Color.Black;
            this.groupDifficulty.Location = new System.Drawing.Point(-2, 82);
            this.groupDifficulty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupDifficulty.Name = "groupDifficulty";
            this.groupDifficulty.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupDifficulty.Size = new System.Drawing.Size(484, 237);
            this.groupDifficulty.TabIndex = 0;
            this.groupDifficulty.TabStop = false;
            this.groupDifficulty.Text = "  Оберіть рівень складності:";
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.BackColor = System.Drawing.Color.OliveDrab;
            this.buttonStartGame.FlatAppearance.BorderSize = 0;
            this.buttonStartGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStartGame.Font = new System.Drawing.Font("Consolas", 13F, System.Drawing.FontStyle.Bold);
            this.buttonStartGame.Location = new System.Drawing.Point(255, 131);
            this.buttonStartGame.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(184, 82);
            this.buttonStartGame.TabIndex = 3;
            this.buttonStartGame.TabStop = false;
            this.buttonStartGame.Text = "Почати гру";
            this.buttonStartGame.UseVisualStyleBackColor = false;
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.BackColor = System.Drawing.Color.OliveDrab;
            this.buttonNewGame.Enabled = false;
            this.buttonNewGame.FlatAppearance.BorderSize = 0;
            this.buttonNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNewGame.Font = new System.Drawing.Font("Consolas", 13F, System.Drawing.FontStyle.Bold);
            this.buttonNewGame.Location = new System.Drawing.Point(255, 131);
            this.buttonNewGame.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.Size = new System.Drawing.Size(184, 82);
            this.buttonNewGame.TabIndex = 2;
            this.buttonNewGame.TabStop = false;
            this.buttonNewGame.Text = "Нова гра";
            this.buttonNewGame.UseVisualStyleBackColor = false;
            // 
            // radioHard
            // 
            this.radioHard.AutoSize = true;
            this.radioHard.BackColor = System.Drawing.Color.Transparent;
            this.radioHard.Location = new System.Drawing.Point(38, 180);
            this.radioHard.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioHard.Name = "radioHard";
            this.radioHard.Size = new System.Drawing.Size(151, 35);
            this.radioHard.TabIndex = 1;
            this.radioHard.Text = "Складний";
            this.radioHard.UseVisualStyleBackColor = false;
            // 
            // radioMedium
            // 
            this.radioMedium.AutoSize = true;
            this.radioMedium.BackColor = System.Drawing.Color.Transparent;
            this.radioMedium.Location = new System.Drawing.Point(38, 122);
            this.radioMedium.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioMedium.Name = "radioMedium";
            this.radioMedium.Size = new System.Drawing.Size(151, 35);
            this.radioMedium.TabIndex = 1;
            this.radioMedium.Text = "Середній";
            this.radioMedium.UseVisualStyleBackColor = false;
            // 
            // radioEasy
            // 
            this.radioEasy.AutoSize = true;
            this.radioEasy.BackColor = System.Drawing.Color.Transparent;
            this.radioEasy.Checked = true;
            this.radioEasy.Location = new System.Drawing.Point(38, 66);
            this.radioEasy.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioEasy.Name = "radioEasy";
            this.radioEasy.Size = new System.Drawing.Size(123, 35);
            this.radioEasy.TabIndex = 0;
            this.radioEasy.TabStop = true;
            this.radioEasy.Text = "Легкий";
            this.radioEasy.UseVisualStyleBackColor = false;
            // 
            // panelGame
            // 
            this.panelGame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGame.AutoScroll = true;
            this.panelGame.AutoSize = true;
            this.panelGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(105)))), ((int)(((byte)(59)))));
            this.panelGame.Location = new System.Drawing.Point(550, 175);
            this.panelGame.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelGame.Name = "panelGame";
            this.panelGame.Size = new System.Drawing.Size(580, 562);
            this.panelGame.TabIndex = 3;
            // 
            // labelTimer
            // 
            this.labelTimer.AutoSize = true;
            this.labelTimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(105)))), ((int)(((byte)(59)))));
            this.labelTimer.Font = new System.Drawing.Font("Consolas", 25F, System.Drawing.FontStyle.Bold);
            this.labelTimer.ForeColor = System.Drawing.Color.Black;
            this.labelTimer.Location = new System.Drawing.Point(638, 15);
            this.labelTimer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(295, 59);
            this.labelTimer.TabIndex = 4;
            this.labelTimer.Text = "Час: 00:00";
            // 
            // labelMinesLeft
            // 
            this.labelMinesLeft.AutoSize = true;
            this.labelMinesLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(105)))), ((int)(((byte)(59)))));
            this.labelMinesLeft.Font = new System.Drawing.Font("Consolas", 25F, System.Drawing.FontStyle.Bold);
            this.labelMinesLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelMinesLeft.Location = new System.Drawing.Point(1527, 15);
            this.labelMinesLeft.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMinesLeft.Name = "labelMinesLeft";
            this.labelMinesLeft.Size = new System.Drawing.Size(214, 59);
            this.labelMinesLeft.TabIndex = 4;
            this.labelMinesLeft.Text = "Міни: 0";
            // 
            // timer1
            // 
            this.timer1.Interval = 15;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(105)))), ((int)(((byte)(59)))));
            this.panel1.Controls.Add(this.pausePanel);
            this.panel1.Controls.Add(this.lvlNow);
            this.panel1.Controls.Add(this.pictureClock);
            this.panel1.Controls.Add(this.topRecord);
            this.panel1.Controls.Add(this.buttonTogglePanel);
            this.panel1.Controls.Add(this.labelTimer);
            this.panel1.Controls.Add(this.labelMinesLeft);
            this.panel1.Controls.Add(this.pictureRedmine);
            this.panel1.Location = new System.Drawing.Point(-6, -3);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2079, 85);
            this.panel1.TabIndex = 9;
            // 
            // pausePanel
            // 
            this.pausePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(100)))), ((int)(((byte)(50)))));
            this.pausePanel.Image = global::WindowsFormsApp1.Properties.Resources.pause;
            this.pausePanel.Location = new System.Drawing.Point(93, 5);
            this.pausePanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pausePanel.Name = "pausePanel";
            this.pausePanel.Size = new System.Drawing.Size(87, 80);
            this.pausePanel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pausePanel.TabIndex = 14;
            this.pausePanel.TabStop = false;
            this.pausePanel.Click += new System.EventHandler(this.pausePanel_Click);
            // 
            // lvlNow
            // 
            this.lvlNow.AutoSize = true;
            this.lvlNow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(105)))), ((int)(((byte)(59)))));
            this.lvlNow.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Bold);
            this.lvlNow.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lvlNow.Location = new System.Drawing.Point(1020, 6);
            this.lvlNow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlNow.Name = "lvlNow";
            this.lvlNow.Size = new System.Drawing.Size(127, 36);
            this.lvlNow.TabIndex = 11;
            this.lvlNow.Text = "Рівень:";
            this.lvlNow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureClock
            // 
            this.pictureClock.BackColor = System.Drawing.Color.Transparent;
            this.pictureClock.Image = global::WindowsFormsApp1.Properties.Resources.clock;
            this.pictureClock.Location = new System.Drawing.Point(555, 6);
            this.pictureClock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureClock.Name = "pictureClock";
            this.pictureClock.Size = new System.Drawing.Size(74, 77);
            this.pictureClock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureClock.TabIndex = 6;
            this.pictureClock.TabStop = false;
            // 
            // topRecord
            // 
            this.topRecord.AutoSize = true;
            this.topRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(105)))), ((int)(((byte)(59)))));
            this.topRecord.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold);
            this.topRecord.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.topRecord.Location = new System.Drawing.Point(1020, 45);
            this.topRecord.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.topRecord.Name = "topRecord";
            this.topRecord.Size = new System.Drawing.Size(225, 33);
            this.topRecord.TabIndex = 11;
            this.topRecord.Text = "Найкращий час:";
            this.topRecord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonTogglePanel
            // 
            this.buttonTogglePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(100)))), ((int)(((byte)(50)))));
            this.buttonTogglePanel.FlatAppearance.BorderSize = 0;
            this.buttonTogglePanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTogglePanel.Image = ((System.Drawing.Image)(resources.GetObject("buttonTogglePanel.Image")));
            this.buttonTogglePanel.Location = new System.Drawing.Point(6, 5);
            this.buttonTogglePanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonTogglePanel.Name = "buttonTogglePanel";
            this.buttonTogglePanel.Size = new System.Drawing.Size(78, 77);
            this.buttonTogglePanel.TabIndex = 4;
            this.buttonTogglePanel.UseVisualStyleBackColor = false;
            // 
            // pictureRedmine
            // 
            this.pictureRedmine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(105)))), ((int)(((byte)(59)))));
            this.pictureRedmine.Image = global::WindowsFormsApp1.Properties.Resources.redmine;
            this.pictureRedmine.Location = new System.Drawing.Point(1436, 5);
            this.pictureRedmine.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureRedmine.Name = "pictureRedmine";
            this.pictureRedmine.Size = new System.Drawing.Size(82, 80);
            this.pictureRedmine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureRedmine.TabIndex = 5;
            this.pictureRedmine.TabStop = false;
            // 
            // btnSafeCell
            // 
            this.btnSafeCell.BackColor = System.Drawing.Color.OliveDrab;
            this.btnSafeCell.FlatAppearance.BorderSize = 0;
            this.btnSafeCell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSafeCell.Font = new System.Drawing.Font("Consolas", 13F, System.Drawing.FontStyle.Bold);
            this.btnSafeCell.Location = new System.Drawing.Point(1566, 392);
            this.btnSafeCell.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSafeCell.Name = "btnSafeCell";
            this.btnSafeCell.Size = new System.Drawing.Size(210, 83);
            this.btnSafeCell.TabIndex = 10;
            this.btnSafeCell.TabStop = false;
            this.btnSafeCell.Text = "Безпечне відкриття";
            this.btnSafeCell.UseVisualStyleBackColor = false;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(105)))), ((int)(((byte)(59)))));
            this.labelProgress.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold);
            this.labelProgress.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelProgress.Location = new System.Drawing.Point(1599, 700);
            this.labelProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(150, 33);
            this.labelProgress.TabIndex = 11;
            this.labelProgress.Text = "Відкрито:";
            this.labelProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSafeOpensRemaining
            // 
            this.lblSafeOpensRemaining.AutoSize = true;
            this.lblSafeOpensRemaining.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(105)))), ((int)(((byte)(59)))));
            this.lblSafeOpensRemaining.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold);
            this.lblSafeOpensRemaining.Location = new System.Drawing.Point(1584, 508);
            this.lblSafeOpensRemaining.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSafeOpensRemaining.Name = "lblSafeOpensRemaining";
            this.lblSafeOpensRemaining.Size = new System.Drawing.Size(165, 33);
            this.lblSafeOpensRemaining.TabIndex = 12;
            this.lblSafeOpensRemaining.Text = "Залишилось";
            this.lblSafeOpensRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // borderForAdditionInfo
            // 
            this.borderForAdditionInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(105)))), ((int)(((byte)(59)))));
            this.borderForAdditionInfo.Location = new System.Drawing.Point(1557, 175);
            this.borderForAdditionInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.borderForAdditionInfo.Name = "borderForAdditionInfo";
            this.borderForAdditionInfo.Size = new System.Drawing.Size(228, 738);
            this.borderForAdditionInfo.TabIndex = 13;
            this.borderForAdditionInfo.Paint += new System.Windows.Forms.PaintEventHandler(this.borderForAdditionInfo_Paint);
            // 
            // bgPanelGame
            // 
            this.bgPanelGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(105)))), ((int)(((byte)(59)))));
            this.bgPanelGame.Location = new System.Drawing.Point(544, 168);
            this.bgPanelGame.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bgPanelGame.Name = "bgPanelGame";
            this.bgPanelGame.Size = new System.Drawing.Size(1246, 752);
            this.bgPanelGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bgPanelGame.TabIndex = 8;
            this.bgPanelGame.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-6, -3);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(2074, 1155);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(105)))), ((int)(((byte)(59)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(2052, 1040);
            this.Controls.Add(this.btnSafeCell);
            this.Controls.Add(this.lblSafeOpensRemaining);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.borderForAdditionInfo);
            this.Controls.Add(this.panelGame);
            this.Controls.Add(this.bgPanelGame);
            this.Controls.Add(this.groupDifficulty);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Minesweeper";
            this.groupDifficulty.ResumeLayout(false);
            this.groupDifficulty.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pausePanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureClock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureRedmine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgPanelGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupDifficulty;
        private System.Windows.Forms.RadioButton radioHard;
        private System.Windows.Forms.RadioButton radioMedium;
        private System.Windows.Forms.RadioButton radioEasy;
        private System.Windows.Forms.Button buttonNewGame;
        private System.Windows.Forms.Panel panelGame;
        private System.Windows.Forms.Label labelMinesLeft;
        private System.Windows.Forms.Label labelTimer;
        private System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.Button buttonTogglePanel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureRedmine;
        private System.Windows.Forms.PictureBox pictureClock;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Panel panel1;
        private PictureBox bgPanelGame;
        private Button btnSafeCell;
        private Label labelProgress;
        private Label lblSafeOpensRemaining;
        private Panel borderForAdditionInfo;
        private PictureBox pausePanel;
        private Label lvlNow;
        private Label topRecord;
    }
}

