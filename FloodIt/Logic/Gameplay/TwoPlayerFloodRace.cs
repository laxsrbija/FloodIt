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

        public TwoPlayerFloodRace(Game game) : base("Two Player Flood Race", game)
        {

            p1Start = new Tuple<int, int>(0, 0);
            p2Start = new Tuple<int, int>(13, 13);

            turn = 1;

            Tile p1Origin = game.GameGrid[0, 0];
            p1Origin.Owner = TileOwner.Player1;
            p1Tiles = 1 + game.GameGrid.FloodFill(p1Start, p1Origin.TileColor, TileOwner.Player1);

            Tile p2Origin = game.GameGrid[13, 13];
            p2Origin.Owner = TileOwner.Player2;
            p2Tiles = 1 + game.GameGrid.FloodFill(p2Start, p2Origin.TileColor, TileOwner.Player2);

            game.Painter.Repaint();
            UpdateScoreboard();

        }

        public override void FloodToColor(Color color)
        {

            if (!running || game.GameGrid[0, 0].TileColor == color || game.GameGrid[13, 13].TileColor == color) // TODO izbaciti upozorenje
            {
                return;
            }

            if (turn > 0)
            {
                p1Tiles += game.GameGrid.FloodFill(p1Start, color, TileOwner.Player1);
            } else
            {
                p2Tiles += game.GameGrid.FloodFill(p2Start, color, TileOwner.Player2);
            }

            turn = -turn;

            game.Painter.Repaint();
            UpdateScoreboard();

            if (HasEnded())
            {
                if (p1Tiles > p2Tiles)
                {
                    game.Screen.DisplayMessage("Player 1 has won!", View.GameScreen.MessageType.SUCCESS);
                } else if (p2Tiles > p1Tiles)
                {
                    game.Screen.DisplayMessage("Player 2 has won!", View.GameScreen.MessageType.SUCCESS);
                } else
                {
                    game.Screen.DisplayMessage("The game is a tie!", View.GameScreen.MessageType.SUCCESS);
                }

                running = false;
            }

        }

        public override bool HasEnded()
        {
            return p1Tiles + p2Tiles == Math.Pow(game.GameGrid.GridSize, 2);
        }

        public override void UpdateScoreboard()
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
