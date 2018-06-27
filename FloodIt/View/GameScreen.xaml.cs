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
using static FloodIt.Logic.Grid;

namespace FloodIt.View
{
    /// <summary>
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : Page
    {

        private MainWindow mainWindow;

        public Game GameInstance { get; private set; }

        public GameScreen(MainWindow mainWindow, IGameplay gameplay)
        {

            InitializeComponent();
            DataContext = this;

            this.mainWindow = mainWindow;

            GameInstance = new Game(this, gameplay, mainWindow.Size);

        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GameInstance.HandleClick(e.GetPosition(GameCanvas));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Window = new MainMenu(mainWindow);
        }

        public void DisplayMessage(string message, MessageType type, bool hasEnded = false)
        {
            new MessageDialog(message, type).ShowDialog();
            if (hasEnded)
            {
                mainWindow.Window = new MainMenu(mainWindow);
            }
        }

        public enum MessageType { HINT, SUCCESS, INFO }

    }

}
