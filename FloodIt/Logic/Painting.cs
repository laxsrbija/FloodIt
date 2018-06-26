using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FloodIt.Logic
{
    public class Painting
    {

        private int tileSize = 35;

        private Grid grid;
        private Canvas canvas;

        public Painting(Game game)
        {
            this.grid = game.GameGrid;
            this.canvas = game.Screen.GameCanvas;
        }

        public void Repaint()
        {

            canvas.Children.Clear();
            
            for (var i = 0; i < grid.GridSize; i++)
            {
                for (var j = 0; j < grid.GridSize; j++)
                {

                    Rectangle rect = new Rectangle();
                    rect.Fill = new SolidColorBrush(grid[i, j].TileColor);
                    rect.Height = tileSize;
                    rect.Width = tileSize;

                    Canvas.SetLeft(rect, tileSize * i);
                    Canvas.SetTop(rect, tileSize * j);

                    canvas.Children.Add(rect);

                }
            }

        }

        public Tuple<int, int> IdentifyTile(Point point)
        {
            int x = (int) (point.X / tileSize);
            int y = (int) (point.Y / tileSize);
            return new Tuple<int, int>(x, y);
        }

    }
}
