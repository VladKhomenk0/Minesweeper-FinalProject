using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1.Core
{
    public class Game
    {
        public Board Board { get; private set; }
        public Difficulty Difficulty { get; private set; }
        public GameState State { get; private set; }

        // Нове поле для фабрики
        private readonly IBoardFactory _boardFactory;

        public Game(Difficulty difficulty)
        {
            // Ініціалізуємо фабрику в конструкторі
            _boardFactory = new BoardFactory();
            Difficulty = difficulty;
            StartNewGame();
        }

        public void StartNewGame()
        {
            State = GameState.Playing;
            // Використовуємо фабрику замість старого приватного методу
            Board = _boardFactory.CreateBoard(Difficulty);
        }

        public event Action<GameState> GameEnded;

        private bool isFirstClick = true;

        // --- СТАРИЙ МЕТОД CreateBoard ДЕЛЕТНУТО ЗВІДСИ ---

        public List<Cell> RevealCell(int row, int col)
        {
            var changedCells = new List<Cell>();

            if (isFirstClick)
            {
                Board.PlaceMinesAvoiding(row, col);
                isFirstClick = false;
            }

            if (State != GameState.Playing)
                return changedCells;

            var cell = Board.Cells[row, col];

            if (cell.IsRevealed || cell.IsFlagged)
                return changedCells;

            if (cell.IsMine)
            {
                cell.IsRevealed = true;
                changedCells.Add(cell);
                State = GameState.Lost;

                foreach (var c in Board.Cells)
                {
                    if (c.IsMine)
                    {
                        c.IsRevealed = true;
                        changedCells.Add(c);
                    }
                }

                GameEnded?.Invoke(State);
                return changedCells;
            }

            if (cell.NeighborMineCount == 0)
            {
                changedCells.AddRange(Board.RevealEmptyArea(row, col));
            }
            else
            {
                cell.IsRevealed = true;
                changedCells.Add(cell);
            }

            CheckWinCondition();
            return changedCells;
        }

        public void ToggleFlag(int row, int col)
        {
            var cell = Board.Cells[row, col];

            if (cell.IsRevealed)
                return;

            cell.IsFlagged = !cell.IsFlagged;
        }

        private void CheckWinCondition()
        {
            foreach (var cell in Board.Cells)
            {
                if (!cell.IsMine && !cell.IsRevealed)
                    return;
            }

            State = GameState.Won;
            GameEnded?.Invoke(State);
        }

        public void ResetFirstClick()
        {
            isFirstClick = true;
        }

        public Cell GetSafeCell()
        {
            foreach (var cell in Board.Cells)
            {
                if (!cell.IsRevealed && !cell.IsMine)
                    return cell;
            }
            return null;
        }

        public int GetRevealedPercentage()
        {
            if (State == GameState.Won)
            {
                return 100;
            }

            int total = Board.Cells.Length;
            int revealed = Board.Cells.Cast<Cell>().Count(c => c.IsRevealed);
            return (int)((revealed / (double)total) * 100);
        }

        // Створення та відновлення стану 
        public GameMemento CreateMemento(int currentElapsedSeconds)
        {
            var memento = new GameMemento
            {
                PlayerName = ProfileManager.Instance.CurrentProfile.Name,
                GameDifficulty = this.Difficulty,
                ElapsedSeconds = currentElapsedSeconds,
                CurrentState = this.State,
                IsFirstClick = this.isFirstClick
            };

            foreach (var cell in Board.Cells)
            {
                memento.Cells.Add(new CellMemento
                {
                    X = cell.X,
                    Y = cell.Y,
                    IsRevealed = cell.IsRevealed,
                    IsMine = cell.IsMine,
                    IsFlagged = cell.IsFlagged,
                    NeighborMineCount = cell.NeighborMineCount
                });
            }
            return memento;
        }

        public void RestoreState(GameMemento memento)
        {
            this.Difficulty = memento.GameDifficulty;
            this.State = memento.CurrentState;
            this.isFirstClick = memento.IsFirstClick;

            // Використовуємо фабрику для перестворення путого поля потрібного розміру
            this.Board = _boardFactory.CreateBoard(this.Difficulty);

            // Відновлення стану кожної клітинки
            foreach (var savedCell in memento.Cells)
            {
                var cell = Board.Cells[savedCell.X, savedCell.Y];
                cell.IsRevealed = savedCell.IsRevealed;
                cell.IsMine = savedCell.IsMine;
                cell.IsFlagged = savedCell.IsFlagged;
                cell.NeighborMineCount = savedCell.NeighborMineCount;
            }
        }

        public bool IsFirstClick => isFirstClick;

        // Зберігання чистого стану поля з розставленими мінами
        public GameMemento GetCleanState()
        {
            var memento = new GameMemento
            {
                PlayerName = ProfileManager.Instance.CurrentProfile.Name,
                GameDifficulty = this.Difficulty,
                ElapsedSeconds = 0,
                CurrentState = GameState.Playing,
                IsFirstClick = false
            };

            foreach (var cell in Board.Cells)
            {
                memento.Cells.Add(new CellMemento
                {
                    X = cell.X,
                    Y = cell.Y,
                    IsMine = cell.IsMine,
                    NeighborMineCount = cell.NeighborMineCount,
                    IsRevealed = false,
                    IsFlagged = false
                });
            }
            return memento;
        }
    }
}