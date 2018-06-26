using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FloodIt.Logic.Gameplay
{
    public class ComputerFloodRaceGameplay : IGameplay
    {

        private Tuple<int, int> playerStart;
        private Tuple<int, int> cpuStart;

        private int playerTiles;
        private int cpuTiles;

        public ComputerFloodRaceGameplay(Game game) : base("Computer Flood Race", game)
        {

            playerStart = new Tuple<int, int>(0, 0);
            cpuStart = new Tuple<int, int>(13, 13);

            Tile playerOrigin = game.GameGrid[0, 0];
            playerOrigin.Owner = TileOwner.Player;

            playerTiles = 1 + game.GameGrid.FloodFill(playerStart, playerOrigin.TileColor, TileOwner.Player);

            Tile cpuOrigin = game.GameGrid[13, 13];
            cpuOrigin.Owner = TileOwner.Computer; // TODO Dodati player 1 i 2

            cpuTiles = 1 + game.GameGrid.FloodFill(cpuStart, cpuOrigin.TileColor, TileOwner.Computer);

            UpdateScoreboard();
            game.Painter.Repaint();

        }

        public override void FloodToColor(Color color)
        {

            if (!running || game.GameGrid[0, 0].TileColor == color)
            {
                return;
            }

            if (color == game.GameGrid[13, 13].TileColor)
            {
                game.Screen.DisplayMessage("Cannot select the same color as Computer!", View.GameScreen.MessageType.INFO);
                return;
            }

            playerTiles += game.GameGrid.FloodFill(playerStart, color, TileOwner.Player);


            var tiles = AquisitionsByColor();

            Console.WriteLine("Computer options:");
            foreach (var tile in tiles)
            {
                Console.WriteLine("{0} - {1}", tile.Item1, tile.Item2);
            }

            if (tiles[0].Item2 == game.GameGrid[0, 0].TileColor)
            {
                Console.WriteLine("Selected second");
                cpuTiles += game.GameGrid.FloodFill(cpuStart, tiles[1].Item2, TileOwner.Computer);
            } else
            {
                Console.WriteLine("Selected first");
                cpuTiles += game.GameGrid.FloodFill(cpuStart, tiles[0].Item2, TileOwner.Computer);
            }
            

            game.Painter.Repaint();
            UpdateScoreboard();

            if (HasEnded())
            {

                if (playerTiles > cpuTiles)
                {
                    game.Screen.DisplayMessage("You have won!", View.GameScreen.MessageType.SUCCESS);
                } else if (playerTiles < cpuTiles)
                {
                    game.Screen.DisplayMessage("You have lost!", View.GameScreen.MessageType.ERROR);
                } else
                {
                    game.Screen.DisplayMessage("It's a tie!", View.GameScreen.MessageType.INFO);
                }
                
                running = false;

            }

        }

        public override bool HasEnded()
        {
            return playerTiles + cpuTiles == Math.Pow(game.GameGrid.GridSize, 2);
        }

        public override void UpdateScoreboard()
        {
            Scoreboard = "Player - " + playerTiles + " | " + cpuTiles + " - Computer";
        }

        private List<Tuple<int, Color>> AquisitionsByColor()
        {

            List<Tuple<int, Color>> ret = new List<Tuple<int, Color>>();

            foreach (var color in Tile.colors)
            {

                HashSet<int> uniqueTiles = new HashSet<int>();

                for (var i = 0; i < game.GameGrid.GridSize; i++)
                {
                    for (var j = 0; j < game.GameGrid.GridSize; j++)
                    {
                        Tile tile = game.GameGrid[i, j];
                        if (tile.TileColor == color && tile.Owner == TileOwner.None && HasCpuTileNeighbor(i, j))
                        {
                            uniqueTiles.Add(tile.Id);
                        }
                    }
                }

                ret.Add(new Tuple<int, Color>(uniqueTiles.Count, color));

            }

            return ret.OrderByDescending(o => o.Item1).ToList();

        }

        private bool HasCpuTileNeighbor(int i, int j)
        {

            if (game.GameGrid[i - 1, j] != null && game.GameGrid[i - 1, j].Owner == TileOwner.Computer)
            {
                return true;
            }

            if (game.GameGrid[i + 1, j] != null && game.GameGrid[i + 1, j].Owner == TileOwner.Computer)
            {
                return true;
            }

            if (game.GameGrid[i, j + 1] != null && game.GameGrid[i, j + 1].Owner == TileOwner.Computer)
            {
                return true;
            }

            if (game.GameGrid[i, j - 1] != null && game.GameGrid[i, j - 1].Owner == TileOwner.Computer)
            {
                return true;
            }

            return false;

        }

    }
}
