using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FloodIt.Logic.Gameplay
{
    public class CasualGameplay : IGameplay
    {
        
        private Tuple<int, int> floodStart;
        private int steps;
        private int tilesAquired;

        public CasualGameplay(Game game) : base("Casual", game) {
            
            floodStart = new Tuple<int, int>(0, 0);
            steps = 0;

            Tile origin = game.GameGrid[0, 0];
            origin.Owner = TileOwner.Player;

            tilesAquired = 1 + game.GameGrid.FloodFill(floodStart, origin.TileColor, TileOwner.Player);
            game.Painter.Repaint();

            UpdateScoreboard();

        }

        public override void FloodToColor(Color color)
        {

            if (!running || game.GameGrid[0, 0].TileColor == color)
            {
                return;
            }

            tilesAquired += game.GameGrid.FloodFill(floodStart, color, TileOwner.Player);
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
            return tilesAquired == Math.Pow(game.GameGrid.GridSize, 2);
        }

        public override void UpdateScoreboard()
        {
            Scoreboard = "Steps: " + steps;
        }

    }

}
