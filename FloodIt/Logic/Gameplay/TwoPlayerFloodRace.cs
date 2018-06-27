using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FloodIt.Logic.Gameplay
{

    public class TwoPlayerFloodRace : IGameplay
    {

        private int p1Tiles;
        private int p2Tiles;

        private Tuple<int, int> p1Start;
        private Tuple<int, int> p2Start;

        private int turn;

        private int gridSize;

        public TwoPlayerFloodRace() : base("Two Player Flood Race") { }

        public override void OnGameInit(Game game)
        {

            SetGameInstance(game);

            gridSize = game.GameGrid.GridDimension;

            p1Start = new Tuple<int, int>(0, 0);
            p2Start = new Tuple<int, int>(gridSize - 1, gridSize - 1);

            turn = 1;

            Tile p1Origin = game.GameGrid[0, 0];
            p1Origin.Owner = TileOwner.Player1;
            p1Tiles = 1 + game.GameGrid.FloodFill(p1Start, p1Origin.TileColor, TileOwner.Player1);

            Tile p2Origin = game.GameGrid[gridSize - 1, gridSize - 1];
            p2Origin.Owner = TileOwner.Player2;
            p2Tiles = 1 + game.GameGrid.FloodFill(p2Start, p2Origin.TileColor, TileOwner.Player2);

            game.Painter.Repaint();
            OnScoreboardChanged();

        }

        public override void OnColorSelect(Color color)
        {

            if (turn > 0)
            {
                if (game.GameGrid[gridSize - 1, gridSize - 1].TileColor == color)
                {
                    game.Screen.DisplayMessage("Can't select the same color as the other player", View.GameScreen.MessageType.HINT);
                } else {
                    p1Tiles += game.GameGrid.FloodFill(p1Start, color, TileOwner.Player1);
                }
            } else
            {
                if (game.GameGrid[0, 0].TileColor == color)
                {
                    game.Screen.DisplayMessage("Can't select the same color as the other player", View.GameScreen.MessageType.HINT);
                } else
                {
                    p2Tiles += game.GameGrid.FloodFill(p2Start, color, TileOwner.Player2);
                }
            }

            turn = -turn;

            game.Painter.Repaint();
            OnScoreboardChanged();

            if (HasEnded())
            {
                OnGameEnded();
            }

        }

        public override void OnGameEnded()
        {

            if (p1Tiles > p2Tiles)
            {
                game.Screen.DisplayMessage("Player 1 has won!", View.GameScreen.MessageType.SUCCESS, true);
            }
            else if (p2Tiles > p1Tiles)
            {
                game.Screen.DisplayMessage("Player 2 has won!", View.GameScreen.MessageType.SUCCESS, true);
            }
            else
            {
                game.Screen.DisplayMessage("The game is a tie!", View.GameScreen.MessageType.INFO, true);
            }

        }

        public override bool HasEnded()
        {
            return p1Tiles + p2Tiles == Math.Pow(game.GameGrid.GridDimension, 2);
        }

        public override void OnScoreboardChanged()
        {
            if (turn > 0)
            {
                Scoreboard = "> First Player - " + p1Tiles + " | " + p2Tiles + " - Second player  ";
            } else
            {
                Scoreboard = "  First Player - " + p1Tiles + " | " + p2Tiles + " - Second player <";
            }
        }

    }

}
