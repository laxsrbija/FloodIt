using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FloodIt.Logic.Gameplay
{
    public class Casual : IGameplay
    {
        
        private Tuple<int, int> floodStart;
        private int steps;
        private int tilesAcquired;

        public Casual(Game game) : base("Casual", game) {
            
            floodStart = new Tuple<int, int>(0, 0);
            steps = 0;

            Tile origin = game.GameGrid[0, 0];
            origin.Owner = TileOwner.Player1;

            tilesAcquired = 1 + game.GameGrid.FloodFill(floodStart, origin.TileColor, TileOwner.Player1);
            game.Painter.Repaint();

            UpdateScoreboard();

        }

        public override void FloodToColor(Color color)
        {

            if (!running || game.GameGrid[0, 0].TileColor == color)
            {
                return;
            }

            tilesAcquired += game.GameGrid.FloodFill(floodStart, color, TileOwner.Player1);
            game.Painter.Repaint();

            ++steps;
            UpdateScoreboard();

            if (HasEnded())
            {
                game.Screen.DisplayMessage("You did it in " + steps + " steps!", View.GameScreen.MessageType.SUCCESS);
                running = false;
            }

        }

        public override bool HasEnded()
        {
            return tilesAcquired == Math.Pow(game.GameGrid.GridSize, 2);
        }

        public override void UpdateScoreboard()
        {
            Scoreboard = "Steps: " + steps;
        }

    }

}
