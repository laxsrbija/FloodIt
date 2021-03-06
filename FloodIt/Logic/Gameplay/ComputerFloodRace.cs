﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FloodIt.Logic.Gameplay
{
    public class ComputerFloodRace : IGameplay
    {

        private Tuple<int, int> playerStart;
        private Tuple<int, int> cpuStart;

        private int playerTiles;
        private int cpuTiles;

        private int gridSize;

        public ComputerFloodRace() : base("Computer Flood Race") { }

        public override void OnGameInit(Game game)
        {

            SetGameInstance(game);

            gridSize = game.GameGrid.GridDimension;

            playerStart = new Tuple<int, int>(0, 0);
            cpuStart = new Tuple<int, int>(gridSize - 1, gridSize - 1);

            Tile playerOrigin = game.GameGrid[0, 0];
            playerOrigin.Owner = TileOwner.Player1;

            playerTiles = 1 + game.GameGrid.FloodFill(playerStart, playerOrigin.TileColor, TileOwner.Player1);

            Tile cpuOrigin = game.GameGrid[gridSize - 1, gridSize - 1];
            cpuOrigin.Owner = TileOwner.Computer;

            cpuTiles = 1 + game.GameGrid.FloodFill(cpuStart, cpuOrigin.TileColor, TileOwner.Computer);

            OnScoreboardChanged();
            game.Painter.Repaint();

        }

        public override void OnColorSelect(Color color)
        {

            if (color == game.GameGrid[gridSize - 1, gridSize - 1].TileColor)
            {
                game.Screen.DisplayMessage("Cannot select the same color as the Computer!", View.GameScreen.MessageType.HINT);
                return;
            }

            playerTiles += game.GameGrid.FloodFill(playerStart, color, TileOwner.Player1);

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
            OnScoreboardChanged();

            if (HasEnded())
            {
                OnScoreboardChanged();
                OnGameEnded();
            }

        }

        public override void OnGameEnded()
        {

            if (playerTiles > cpuTiles)
            {
                game.Screen.DisplayMessage("You have won!", View.GameScreen.MessageType.SUCCESS, true);
            }
            else if (playerTiles < cpuTiles)
            {
                game.Screen.DisplayMessage("You have lost!", View.GameScreen.MessageType.INFO, true);
            }
            else
            {
                game.Screen.DisplayMessage("The game is a tie!", View.GameScreen.MessageType.INFO, true);
            }

        }

        public override bool HasEnded()
        {
            if (playerTiles + cpuTiles == Math.Pow(game.GameGrid.GridDimension, 2))
            {
                return true;
            }

            if (PlayerStuck(TileOwner.Player1))
            {
                cpuTiles += (int)Math.Pow(game.GameGrid.GridDimension, 2) - (cpuTiles + playerTiles);
                return true;
            }

            if (PlayerStuck(TileOwner.Computer))
            {
                playerTiles += (int)Math.Pow(game.GameGrid.GridDimension, 2) - (cpuTiles + playerTiles);
                return true;
            }

            return false;
        }

        public override void OnScoreboardChanged()
        {
            Scoreboard = "Player - " + playerTiles + " | " + cpuTiles + " - Computer";
        }

        private List<Tuple<int, Color>> AquisitionsByColor()
        {

            List<Tuple<int, Color>> ret = new List<Tuple<int, Color>>();
            Dictionary<Color, int> dict = new Dictionary<Color, int>();
            HashSet<int> uniqueTiles = new HashSet<int>();

            for (var i = 0; i < game.GameGrid.GridDimension; i++)
            {
                for (var j = 0; j < game.GameGrid.GridDimension; j++)
                {
                    Tile tile = game.GameGrid[i, j];
                    if (!uniqueTiles.Contains(tile.Id) 
                        && tile.Owner == TileOwner.None 
                        && HasSameOwnerTileNeighbor(TileOwner.Computer, i, j))
                    {

                        uniqueTiles.Add(tile.Id);

                        if (!dict.ContainsKey(tile.TileColor))
                        {
                            dict[tile.TileColor] = 0;
                        }

                        ++dict[tile.TileColor];

                    }
                }
            }

            foreach (var color in dict.Keys)
            {
                ret.Add(new Tuple<int, Color>(dict[color], color));
            }

            return ret.OrderByDescending(o => o.Item1).ToList();

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

        private bool PlayerStuck(TileOwner owner)
        {

            var startColor = game.GameGrid[playerStart.Item1, playerStart.Item2].TileColor;
            var oponentColor = game.GameGrid[cpuStart.Item1, cpuStart.Item2].TileColor;
            if (owner == TileOwner.Computer)
            {
                oponentColor = game.GameGrid[playerStart.Item1, playerStart.Item2].TileColor;
                startColor = game.GameGrid[cpuStart.Item1, cpuStart.Item2].TileColor;
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

    }
}
