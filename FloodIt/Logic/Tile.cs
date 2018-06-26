using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FloodIt.Logic
{

    public class Tile
    {

        public static IList<Color> colors = new ReadOnlyCollection<Color>(new List<Color>
        {
            Color.FromRgb(83, 82, 160),
            Color.FromRgb(92, 166, 199),
            Color.FromRgb(74, 190, 88),
            Color.FromRgb(245, 92, 83),
            Color.FromRgb(245, 178, 83),
            Color.FromRgb(250, 254, 94)
        });

        public int Id { private set; get; }
        public TileOwner Owner { get; set; }
        public Color TileColor { set; get; }

        public Tile(int id, Color color)
        {
            Id = id;
            TileColor = color;
            Owner = TileOwner.None;
        }

        public override string ToString()
        {
            return "[ID:" + Id + ", COLOR: " + TileColor.ToString() + ", OWNER: " + Owner + "]";
        }

    }

    public enum TileOwner { None, Player, Computer }

}
