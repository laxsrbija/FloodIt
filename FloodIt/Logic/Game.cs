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

namespace FloodIt.Logic
{

    public class Game
    {

        private int gridSize = 14;

        public IGameplay Strategy { get; private set; }
        public Grid GameGrid { get; private set; }
        public Painting Painter { get; private set; }
        public GameScreen Screen { get; private set; }

        public Game(GameScreen gameScreen, GameType gameType)
        {

            Screen = gameScreen;

            GameGrid = new Grid(gridSize);
            Painter = new Painting(this);

            switch (gameType)
            {
                case GameType.Singleplayer:
                    Strategy = new CasualGameplay(this);
                    break;
                case GameType.FloodRace:
                    Strategy = new ComputerFloodRaceGameplay(this);
                    break;
            }

        }

        public void HandleClick(Point point)
        {

            var tuple = Painter.IdentifyTile(point);
            Tile tile = GameGrid[tuple.Item1, tuple.Item2];

            Strategy.FloodToColor(tile.TileColor);

        }

        public void PrintGrid()
        {
            for (var i = 0; i < gridSize; i++)
            {
                for (var j = 0; j < gridSize; j++)
                {
                    Console.WriteLine(GameGrid[i, j]);
                }
            }
        }

    }
}
