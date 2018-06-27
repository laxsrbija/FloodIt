using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FloodIt.Logic.Gameplay
{
    public abstract class IGameplay : INotifyPropertyChanged
    {
        
        protected Game game;
        protected bool running;

        public event PropertyChangedEventHandler PropertyChanged;

        public IGameplay(string gameType, Game game) 
        {
            this.running = true;
            this.game = game;
            Gametype = gameType;
        }

        private string scoreboard;
        public string Scoreboard {
            get => scoreboard;
            protected set {
                scoreboard = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Scoreboard"));
            }
        }

        private string gametype;
        public string Gametype {
            get => gametype;
            protected set {
                gametype = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Gametype"));
            }
        }

        abstract public void OnColorSelect(Color color);

        abstract public void UpdateScoreboard();

        abstract public bool HasEnded();

        public enum GameType { Singleplayer, FloodRace, FloodRaceCPU, FloodRace2P} // TODO eliminisati ovaj deo

    }
}
