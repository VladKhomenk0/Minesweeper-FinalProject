using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1.Core
{
    public class GameRenderer
    {
        private readonly Panel panel;
        private readonly Game game;
        private readonly int cellSize;
        private readonly Dictionary<string, Image> cellImages;
        private readonly List<Cell> highlightedCells = new List<Cell>();

        public GameRenderer(Panel panel, Game game, int cellSize, Dictionary<string, Image> cellImages)
        {
            this.panel = panel;
            this.game = game;
            this.cellSize = cellSize;
            this.cellImages = cellImages;
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

        // Тепер метод просто додає клітинку до списку підсвічених та оновлює її на екрані
        public void HighlightCell(Cell cell)
        {
            if (!highlightedCells.Contains(cell))
            {
                highlightedCells.Add(cell);

            foreach (Control ctrl in _panel.Controls)
            {
                if (ctrl is PictureBox pic && pic.Tag is Point p &&
                    p.X == cell.X && p.Y == cell.Y)
                {
                    if (ctrl is PictureBox pic && pic.Tag is Point p && p.X == cell.X && p.Y == cell.Y)
                    {
                        pic.Invalidate();
                        break;
                    }
                }
            }
        }

        // Перевірка підсвічування для зовнішнього використання
        public bool IsCellHighlighted(Cell cell)
        {
            return highlightedCells.Contains(cell);
        }

        // Очищення підсвічувань при перезапуску гри
        public void ClearHighlights()
        {
            highlightedCells.Clear();
        }

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