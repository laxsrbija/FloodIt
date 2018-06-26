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

        public CasualGameplay(Game game) : base("Casual", game) {
            
            floodStart = new Tuple<int, int>(0, 0);
            steps = 0;

            Tile origin = game.GameGrid[0, 0];
            origin.Owner = TileOwner.Player;

            game.GameGrid.FloodFill(floodStart, origin.TileColor, TileOwner.Player);
            game.Painter.Repaint();

            UpdateScoreboard();

        }

        public override void FloodToColor(Color color)
        {

            if (!running || game.GameGrid[0, 0].TileColor == color)
            {
                return;
            }

            game.GameGrid.FloodFill(floodStart, color, TileOwner.Player);
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
            
            for (var i = 0; i < game.GameGrid.GridSize; i++)
            {
                for (var j = 0; j < game.GameGrid.GridSize; j++)
                {
                    if (game.GameGrid[i, j].TileColor != game.GameGrid[0, 0].TileColor)
                    {
                        return false;
                    }
                }
            }

            return true;

        }

        public override void UpdateScoreboard()
        {
            Scoreboard = "Steps: " + steps;
        }

    }

}
