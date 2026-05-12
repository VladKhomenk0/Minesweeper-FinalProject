using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1.Core
{
    public class GameRenderer
    {
        private readonly Panel _panel;
        private readonly Game _game;
        private readonly int _cellSize;
        private readonly int _maxHighlights;
        private readonly CellImageResolver _imageResolver;
        private readonly List<Cell> _highlightedCells = new List<Cell>();
        private int _highlightCount = 0;

        public GameRenderer(Panel panel, Game game, int cellSize,
                            Dictionary<string, Image> cellImages)
        {
            _panel = panel;
            _game = game;
            _cellSize = cellSize;
            _imageResolver = new CellImageResolver(cellImages);

            _maxHighlights = game.Difficulty switch
            {
                Difficulty.Easy => 3,
                Difficulty.Medium => 5,
                Difficulty.Hard => 7,
                _ => 0
            };
        }

        public void DrawBoard(MouseEventHandler cellClickHandler)
        {
            _panel.Controls.Clear();

            for (int row = 0; row < _game.Board.Rows; row++)
            {
                for (int col = 0; col < _game.Board.Columns; col++)
                {
                    Cell cell = _game.Board.GetCell(row, col);

                    var pic = new PictureBox
                    {
                        Width = _cellSize,
                        Height = _cellSize,
                        Left = col * _cellSize,
                        Top = row * _cellSize,
                        Tag = new Point(row, col),
                        Margin = new Padding(0),
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };

                    pic.Paint += PictureBox_Paint;
                    pic.MouseUp += cellClickHandler;
                    UpdateCellAppearance(pic, cell);
                    _panel.Controls.Add(pic);
                }
            }
        }

        public void RedrawBoard()
        {
            foreach (Control control in _panel.Controls)
            {
                if (control is PictureBox pic && pic.Tag is Point p)
                {
                    UpdateCellAppearance(pic, _game.Board.GetCell(p.X, p.Y));
                    pic.Invalidate();
                }
            }
        }

        public void RedrawCells(List<Cell> cells)
        {
            foreach (Control control in _panel.Controls)
            {
                if (control is PictureBox pic && pic.Tag is Point p)
                {
                    Cell cell = cells.Find(c => c.X == p.X && c.Y == p.Y);
                    if (cell != null)
                    {
                        UpdateCellAppearance(pic, cell);
                        pic.Invalidate();
                    }
                }
            }
        }
        private void UpdateCellAppearance(PictureBox pictureBox, Cell cell)
        {
            pictureBox.Image = _imageResolver.Resolve(cell);
        }

        public void HighlightCell(Cell cell)
        {
            if (_highlightCount >= _maxHighlights)
            {
                MessageBox.Show(
                    $"Ви використали всі {_maxHighlights} підсвічування для цього рівня складності.",
                    "Увага", MessageBoxButtons.OK);
                return;
            }

            if (_highlightedCells.Contains(cell))
                return;

            _highlightedCells.Add(cell);
            _highlightCount++;

            foreach (Control ctrl in _panel.Controls)
            {
                if (ctrl is PictureBox pic && pic.Tag is Point p &&
                    p.X == cell.X && p.Y == cell.Y)
                {
                    pic.Invalidate();
                    break;
                }
            }
        }

        public string GetRemainingHighlights() =>
            $"{_maxHighlights - _highlightCount}/{_maxHighlights}";

        public bool IsHighlightsLimitReached() =>
            _highlightCount >= _maxHighlights;

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (sender is PictureBox pic && pic.Tag is Point p)
            {
                if (_highlightedCells.Contains(_game.Board.GetCell(p.X, p.Y)))
                {
                    using (var pen = new Pen(Color.LimeGreen, 2))
                        e.Graphics.DrawRectangle(pen, 0, 0, pic.Width - 1, pic.Height - 1);
                }
            }
        }
    }
}