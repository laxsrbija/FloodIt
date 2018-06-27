using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FloodIt.Logic
{
    public class Grid
    {

        public int GridDimension { get; private set; }

        private List<List<Tile>> grid;

        public Grid(GridSize gridType)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            DetermineGridSize(gridType);

            grid = new List<List<Tile>>();
            for (var i = 0; i < GridDimension; i++)
            {
                grid.Add(new List<Tile>());

                for (var j = 0; j < GridDimension; j++)
                {
                    int id = (i * GridDimension) + j;
                    Color color = Tile.colors[random.Next(Tile.colors.Count)];
                    grid[i].Add(new Tile(id, color));
                }
            }
        }

        public Tile this[int i, int j] {
            get {

                if (i >= 0 && i < grid.Count && j >= 0 && j < grid.Count)
                {
                    return grid[i][j];
                }

                return null;

            }
        }

        public int FloodFill(Tuple<int, int> startingPoint, Color newColor, TileOwner owner)
        {

            Stack<Tile> tiles = new Stack<Tile>();
            HashSet<int> visitedTiles = new HashSet<int>();
            int tilesAcquired = 0;

            Tile startingTile = this[startingPoint.Item1, startingPoint.Item2];

            tiles.Push(startingTile);

            while (tiles.Count > 0)
            {

                Tile tile = tiles.Pop();
                visitedTiles.Add(tile.Id);

                if (tile.Owner == owner || (tile.Owner == TileOwner.None && tile.TileColor == newColor))
                {

                    if (tile.Owner == TileOwner.None)
                    {
                        ++tilesAcquired;
                    }

                    tile.Owner = owner;
                    tile.TileColor = newColor;

                    int i = tile.Id / GridDimension;
                    int j = tile.Id % GridDimension;

                    TestAndPush(tiles, i + 1, j, visitedTiles);
                    TestAndPush(tiles, i - 1, j, visitedTiles);
                    TestAndPush(tiles, i, j + 1, visitedTiles);
                    TestAndPush(tiles, i, j - 1, visitedTiles);

                }

            }

            return tilesAcquired;

        }

        private void TestAndPush(Stack<Tile> queue, int i, int j, HashSet<int> visited)
        {
            Tile tile = this[i, j];
            if (tile != null && !visited.Contains(tile.Id))
            {
                queue.Push(tile);
            }
        }

        public enum GridSize { SMALL, MEDIUM, LARGE }

        private void DetermineGridSize(GridSize gridType)
        {
            switch (gridType)
            {
                case GridSize.SMALL:
                    GridDimension = 12;
                    break;
                case GridSize.MEDIUM:
                    GridDimension = 18;
                    break;
                case GridSize.LARGE:
                    GridDimension = 24;
                    break;
            }
        }

    }
}
