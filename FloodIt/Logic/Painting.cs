﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using static FloodIt.Logic.Grid;

namespace FloodIt.Logic
{
    public class Painting
    {

        private Grid grid;
        private Canvas canvas;
        private int tileSize;

        public Painting(Game game, GridSize gridType)
        {
            this.grid = game.GameGrid;
            this.canvas = game.Screen.GameCanvas;

            this.tileSize = DetermineTileSize(gridType);
        }

        public void Repaint()
        {

            canvas.Children.Clear();
            
            for (var i = 0; i < grid.GridDimension; i++)
            {
                for (var j = 0; j < grid.GridDimension; j++)
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

        private int DetermineTileSize(GridSize gridType)
        {
            switch (gridType)
            {
                case GridSize.SMALL:
                    return 36;
                case GridSize.MEDIUM:
                    return 24;
                case GridSize.LARGE:
                    return 18;
            }
            return -1;
        }

    }
}
