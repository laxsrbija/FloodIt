using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FloodIt.Logic.Gameplay
{

    public class Classic : IGameplay
    {

        private int tiles;
        private int steps;
        private int maxSteps;

        private Tuple<int, int> floodStart;

        public Classic(Game game) : base("Classic", game)
        {

            floodStart = new Tuple<int, int>(0, 0);
            steps = 0;

            maxSteps = (int)Math.Floor((double)(25 * ((game.GameGrid.GridSize * 2) * 6) / ((14 + 14) * 6))); // TODO Ceil?

            Tile origin = game.GameGrid[0, 0];
            origin.Owner = TileOwner.Player1;

            tiles = 1 + game.GameGrid.FloodFill(floodStart, origin.TileColor, TileOwner.Player1);
            game.Painter.Repaint();

            UpdateScoreboard();

        }

        public override void OnColorSelect(Color color)
        {

            if (!running || game.GameGrid[0, 0].TileColor == color)
            {
                return;
            }

            tiles += game.GameGrid.FloodFill(floodStart, color, TileOwner.Player1);
            game.Painter.Repaint();

            ++steps;
            UpdateScoreboard();

            if (running && steps > maxSteps)
            {
                game.Screen.DisplayMessage("You failed to flood the board in " + maxSteps + " steps", View.GameScreen.MessageType.SUCCESS);
                running = false;
            }

            if (running && HasEnded())
            {
                game.Screen.DisplayMessage("You did it in " + steps + " steps!", View.GameScreen.MessageType.SUCCESS);
                running = false;
            }

        }

        public override bool HasEnded()
        {
            return tiles == Math.Pow(game.GameGrid.GridSize, 2);
        }

        public override void UpdateScoreboard()
        {
            Scoreboard = steps + " / " + maxSteps;
        }

    }

}
