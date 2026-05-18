using System;

namespace WindowsFormsApp1.Core
{
    public interface IBoardFactory
    {
        Board CreateBoard(Difficulty difficulty);
    }

    public class BoardFactory : IBoardFactory
    {
        public Board CreateBoard(Difficulty difficulty)
        {
            return difficulty switch
            {
                Difficulty.Easy => new Board(8, 10, 10),
                Difficulty.Medium => new Board(12, 16, 40),
                Difficulty.Hard => new Board(16, 22, 70),
                _ => throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null)
            };
        }
    }
}