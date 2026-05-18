using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Core.Commands
{
    public abstract class BaseCellCommand : ICellCommand
    {
        protected readonly Game _game;
        public int Row { get; }
        public int Col { get; }
        protected BaseCellCommand(Game game, int row, int col)
        {
            _game = game;
            Row = row;
            Col = col;
        }

        public abstract List<Cell> Execute();
    }
}
