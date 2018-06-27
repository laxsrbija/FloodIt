using FloodIt.Logic.Gameplay;
using FloodIt.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static FloodIt.Logic.Gameplay.IGameplay;
using static FloodIt.Logic.Grid;

namespace FloodIt.Logic
{

    public class Game
    {

        public IGameplay Gameplay { get; private set; }
        public Grid GameGrid { get; private set; }
        public Painting Painter { get; private set; }
        public GameScreen Screen { get; private set; }

        public Game(GameScreen gameScreen, IGameplay gameplay, GridSize gridType)
        {

            Gameplay = gameplay;
            Screen = gameScreen;

            GameGrid = new Grid(gridType);
            Painter = new Painting(this, gridType);

            gameplay.OnGameInit(this);

        }

        public void HandleClick(Point point)
        {

            var tuple = Painter.IdentifyTile(point);
            Tile tile = GameGrid[tuple.Item1, tuple.Item2];

            Gameplay.OnColorSelect(tile.TileColor);

        }

    }
}
