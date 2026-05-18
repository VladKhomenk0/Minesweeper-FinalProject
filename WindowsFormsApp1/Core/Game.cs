using System;
using System.Collections.Generic;
using System.Linq;

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
                HandleFirstClick(row, col);
            }

            if (State != GameState.Playing)
                return changedCells;

            var cell = Board.Cells[row, col];

            if (cell.IsRevealed || cell.IsFlagged)
                return changedCells;

            if (cell.IsMine)
            {
                return HandleLoss(changedCells);
            }

            HandleSafeReveal(cell, row, col, changedCells);

            CheckWinCondition();
            return changedCells;
        }

        private void HandleFirstClick(int row, int col)
        {
            Board.PlaceMinesAvoiding(row, col);
            isFirstClick = false;
        }

        private List<Cell> HandleLoss(List<Cell> changedCells)
        {
            State = GameState.Lost;

            foreach (var c in Board.Cells)
            {
                if (c.IsMine && !c.IsRevealed)
                {
                    c.IsRevealed = true;
                    changedCells.Add(c);
                }
            }

            GameEnded?.Invoke(State);
            return changedCells;
        }

        private void HandleSafeReveal(Cell cell, int row, int col, List<Cell> changedCells)
        {
            if (cell.NeighborMineCount == 0)
            {
                changedCells.AddRange(Board.RevealEmptyArea(row, col));
            }
            else
            {
                cell.IsRevealed = true;
                changedCells.Add(cell);
            }
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
                return 100;

            int total = Board.Cells.Length;
            int revealed = Board.Cells.Cast<Cell>().Count(c => c.IsRevealed);
            return (int)((revealed / (double)total) * 100);
        }

        public GameMemento CreateMemento(int currentElapsedSeconds)
        {
            return BuildMemento(
                elapsedSeconds: currentElapsedSeconds,
                state: this.State,
                isFirstClick: this.isFirstClick,
                saveRevealState: true
            );
        }

        public GameMemento GetCleanState()
        {
            return BuildMemento(
                elapsedSeconds: 0,
                state: GameState.Playing,
                isFirstClick: false,
                saveRevealState: false
            );
        }

        private GameMemento BuildMemento(int elapsedSeconds, GameState state, bool isFirstClick, bool saveRevealState)
        {
            var memento = new GameMemento
            {
                PlayerName = ProfileManager.Instance.CurrentProfile.Name,
                GameDifficulty = this.Difficulty,
                ElapsedSeconds = elapsedSeconds,
                CurrentState = state,
                IsFirstClick = isFirstClick
            };

            memento.Cells.AddRange(CreateCellMementos(false));
            return memento;
        }

        public void RestoreState(GameMemento memento)
        {
            this.Difficulty = memento.GameDifficulty;
            this.State = memento.CurrentState;
            this.isFirstClick = memento.IsFirstClick;

            // Використовуємо фабрику для перестворення путого поля потрібного розміру
            this.Board = _boardFactory.CreateBoard(this.Difficulty);

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

            memento.Cells.AddRange(CreateCellMementos(true));
            return memento;
        }

        private List<CellMemento> CreateCellMementos(bool asCleanState)
        {
            var mementos = new List<CellMemento>();
            foreach (var cell in Board.Cells)
            {
                mementos.Add(new CellMemento
                {
                    X = cell.X,
                    Y = cell.Y,
                    IsMine = cell.IsMine,
                    NeighborMineCount = cell.NeighborMineCount,
                    IsRevealed = !asCleanState && cell.IsRevealed,
                    IsFlagged = !asCleanState && cell.IsFlagged
                });
            }
            return mementos;
        }
    }
}