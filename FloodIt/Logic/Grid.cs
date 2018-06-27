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

        public int GridSize { get; private set; }

        private List<List<Tile>> grid;

        public Grid(int gridSize)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            GridSize = gridSize;

            grid = new List<List<Tile>>();
            for (var i = 0; i < GridSize; i++)
            {
                grid.Add(new List<Tile>());

                for (var j = 0; j < GridSize; j++)
                {
                    int id = (i * GridSize) + j;
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

            Console.WriteLine("############################");

            Stack<Tile> tiles = new Stack<Tile>();
            HashSet<int> visitedTiles = new HashSet<int>();
            int tilesAcquired = 0;

            Tile startingTile = this[startingPoint.Item1, startingPoint.Item2];

            tiles.Push(startingTile);

            while (tiles.Count > 0)
            {

                Tile tile = tiles.Pop();
                visitedTiles.Add(tile.Id);

                Console.WriteLine("----");
                Console.WriteLine("Ispitujem plocicu: {0}", tile.Id);

                if (tile.Owner == owner || (tile.Owner == TileOwner.None && tile.TileColor == newColor))
                {

                    Console.WriteLine("Plocica OK: {0}", tile.Id);

                    if (tile.Owner == TileOwner.None)
                    {
                        ++tilesAcquired;
                    }

                    tile.Owner = owner;
                    tile.TileColor = newColor;

                    int i = tile.Id / GridSize;
                    int j = tile.Id % GridSize;

                    TestAndPush(tiles, i + 1, j, visitedTiles);
                    TestAndPush(tiles, i - 1, j, visitedTiles);
                    TestAndPush(tiles, i, j + 1, visitedTiles);
                    TestAndPush(tiles, i, j - 1, visitedTiles);

                } else
                {
                    Console.WriteLine("Plocica PALA: {0}", tile.Id);
                }

            }

            Console.WriteLine("Tiles Acquired: {0}", tilesAcquired);

            return tilesAcquired;

        }

        private void TestAndPush(Stack<Tile> queue, int i, int j, HashSet<int> visited)
        {
            Tile tile = this[i, j];
            if (tile != null && !visited.Contains(tile.Id))
            {
                Console.WriteLine("Plocica dodata u red: {0}", tile.Id);
                queue.Push(tile);
            } else
            {
                if (tile != null)
                {
                    Console.WriteLine("Plocica NIJE dodata u red: {0}", tile.Id);
                }
            }
        }

    }
}
