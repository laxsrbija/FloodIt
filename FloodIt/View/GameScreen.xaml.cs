using FloodIt.Logic;
using FloodIt.Logic.Gameplay;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static FloodIt.Logic.Gameplay.IGameplay;

namespace FloodIt.View
{
    /// <summary>
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : Page
    {

        private MainWindow mainWindow;

        public Game GameInstance { get; private set; }

        public GameScreen(MainWindow mainWindow, int id/* TODO !! */)
        {

            InitializeComponent();

            DataContext = this;

            this.mainWindow = mainWindow;

            if (id == 1) // TODO!
                GameInstance = new Game(this, GameType.Singleplayer);
            else if (id == 2)
                GameInstance = new Game(this, GameType.FloodRace);
            else if (id == 3)
                GameInstance = new Game(this, GameType.FloodRaceCPU);
            else if (id == 4)
                GameInstance = new Game(this, GameType.FloodRace2P);
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GameInstance.HandleClick(e.GetPosition(GameCanvas));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Window = new MainMenu(mainWindow);
        }

        public bool DisplayMessage(string message, MessageType type)
        {
            // TODO odgovor prozora yes ili no
            MessageBox.Show(type + ": " + message);
            return true;
        }

        public enum MessageType { ERROR, SUCCESS, INFO }

    }

}
