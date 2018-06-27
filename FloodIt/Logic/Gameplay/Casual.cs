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

        public Casual() : base("Casual") { }

        public override void OnGameInit(Game game)
        {

            SetGameInstance(game);

            floodStart = new Tuple<int, int>(0, 0);
            steps = 0;

            Tile origin = game.GameGrid[0, 0];
            origin.Owner = TileOwner.Player1;

            tilesAcquired = 1 + game.GameGrid.FloodFill(floodStart, origin.TileColor, TileOwner.Player1);
            game.Painter.Repaint();

            OnScoreboardChanged();

        }

        public override void OnColorSelect(Color color)
        {

            if (game.GameGrid[0, 0].TileColor == color)
            {
                game.Screen.DisplayMessage("Your tiles are already flooded with that color", View.GameScreen.MessageType.HINT);
            }

            tilesAcquired += game.GameGrid.FloodFill(floodStart, color, TileOwner.Player1);
            game.Painter.Repaint();

            ++steps;
            OnScoreboardChanged();

            if (HasEnded())
            {
                OnGameEnded();
            }

        }

        public override void OnGameEnded()
        {
            game.Screen.DisplayMessage("You did it in " + steps + " steps!", View.GameScreen.MessageType.SUCCESS, true);
        }

        public override bool HasEnded()
        {
            return tilesAcquired == Math.Pow(game.GameGrid.GridDimension, 2);
        }

        public override void OnScoreboardChanged()
        {
            Scoreboard = "Steps: " + steps;
        }

    }

}
