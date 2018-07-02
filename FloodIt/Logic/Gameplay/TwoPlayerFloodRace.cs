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
                    return;
                } else {
                    p1Tiles += game.GameGrid.FloodFill(p1Start, color, TileOwner.Player1);
                }
            } else
            {
                if (game.GameGrid[0, 0].TileColor == color)
                {
                    game.Screen.DisplayMessage("Can't select the same color as the other player", View.GameScreen.MessageType.HINT);
                    return;
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
                OnScoreboardChanged();
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
            if (p1Tiles + p2Tiles == Math.Pow(game.GameGrid.GridDimension, 2))
            {
                return true;
            }

            if (PlayerStuck(TileOwner.Player1))
            {
                p1Tiles += (int)Math.Pow(game.GameGrid.GridDimension, 2) - (p1Tiles + p2Tiles);
                return true;
            }

            if (PlayerStuck(TileOwner.Player2))
            {
                p2Tiles += (int)Math.Pow(game.GameGrid.GridDimension, 2) - (p1Tiles + p2Tiles);
                return true;
            }

            return false;
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

        private bool PlayerStuck(TileOwner owner)
        {

            var startColor = game.GameGrid[p1Start.Item1, p1Start.Item2].TileColor;
            var oponentColor = game.GameGrid[p2Start.Item1, p2Start.Item2].TileColor;
            if (owner == TileOwner.Player2)
            {
                oponentColor = game.GameGrid[p1Start.Item1, p1Start.Item2].TileColor;
                startColor = game.GameGrid[p2Start.Item1, p2Start.Item2].TileColor;
            }


            for (var i = 0; i < game.GameGrid.GridDimension; i++)
            {
                for (var j = 0; j < game.GameGrid.GridDimension; j++)
                {
                    Tile tile = game.GameGrid[i, j];
                    if ((tile.TileColor != startColor && tile.TileColor != oponentColor)
                        || (tile.Owner == TileOwner.None && HasSameOwnerTileNeighbor(owner, i, j)))
                    {
                        return false;
                    }
                }
            }

            return true;

        }

        private bool HasSameOwnerTileNeighbor(TileOwner owner, int i, int j)
        {

            if (game.GameGrid[i - 1, j] != null && game.GameGrid[i - 1, j].Owner == owner)
            {
                return true;
            }

            if (game.GameGrid[i + 1, j] != null && game.GameGrid[i + 1, j].Owner == owner)
            {
                return true;
            }

            if (game.GameGrid[i, j + 1] != null && game.GameGrid[i, j + 1].Owner == owner)
            {
                return true;
            }

            if (game.GameGrid[i, j - 1] != null && game.GameGrid[i, j - 1].Owner == owner)
            {
                return true;
            }

            return false;

        }

    }

}
