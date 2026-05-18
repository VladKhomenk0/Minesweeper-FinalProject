using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Core.Commands
{
    public class RevealCommand : BaseCellCommand
    {
        public RevealCommand(Game game, int row, int col) : base(game, row, col) { }

        public override List<Cell> Execute()
        {
            return _game.RevealCell(Row, Col);
        }
    }
}
