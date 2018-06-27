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

        public event PropertyChangedEventHandler PropertyChanged;

        public IGameplay(string gameType) 
        {
            Gametype = gameType;
        }

        public void SetGameInstance(Game game)
        {
            this.game = game; 
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

        abstract public void OnGameInit(Game game);

        abstract public void OnColorSelect(Color color);

        abstract public void OnGameEnded();

        abstract public void OnScoreboardChanged();

        abstract public bool HasEnded();

    }
}
