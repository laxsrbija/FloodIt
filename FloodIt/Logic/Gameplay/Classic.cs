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

        public Classic() : base("Classic") { }

        public override void OnGameInit(Game game)
        {

            SetGameInstance(game);

            floodStart = new Tuple<int, int>(0, 0);
            steps = 0;

            maxSteps = (int)Math.Ceiling((double)(25 * ((game.GameGrid.GridDimension * 2) * 6) / ((14 + 14) * 6))) + 2;

            Tile origin = game.GameGrid[0, 0];
            origin.Owner = TileOwner.Player1;

            tiles = 1 + game.GameGrid.FloodFill(floodStart, origin.TileColor, TileOwner.Player1);
            game.Painter.Repaint();

            OnScoreboardChanged();

        }

        public override void OnColorSelect(Color color)
        {

            if (game.GameGrid[0, 0].TileColor == color)
            {
                game.Screen.DisplayMessage("Your tiles are already flooded with that color", View.GameScreen.MessageType.HINT);
            }

            tiles += game.GameGrid.FloodFill(floodStart, color, TileOwner.Player1);
            game.Painter.Repaint();

            ++steps;
            OnScoreboardChanged();

            if (HasEnded())
            {
                OnGameEnded();
            }

        }

        public override void OnGameEnded()
        {
            if (steps >= maxSteps)
            {
                game.Screen.DisplayMessage("You failed to flood the board in " + maxSteps + " steps", View.GameScreen.MessageType.INFO, true);
            } else
            {
                game.Screen.DisplayMessage("You did it in " + steps + " steps!", View.GameScreen.MessageType.SUCCESS, true);
            }
        }

        public override bool HasEnded()
        {
            return tiles == Math.Pow(game.GameGrid.GridDimension, 2) || steps > maxSteps;
        }

        public override void OnScoreboardChanged()
        {
            Scoreboard = steps + " / " + maxSteps;
        }

    }

}
