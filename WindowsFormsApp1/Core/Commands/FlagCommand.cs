using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WindowsFormsApp1.Core.Commands
{
    public class FlagCommand : BaseCellCommand
    {
        public FlagCommand(Game game, int row, int col) : base(game, row, col) { }

        public override List<Cell> Execute()
        {
            _game.ToggleFlag(Row, Col);
            return new List<Cell> { _game.Board.GetCell(Row, Col) };
        }
    }
}
