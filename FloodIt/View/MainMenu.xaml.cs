using FloodIt.Logic.Gameplay;
using System;
using System.Collections.Generic;
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
using static FloodIt.Logic.Grid;

namespace FloodIt.View
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {

        private MainWindow mainWindow;

        public MainMenu(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            SetAndMarkGridSize(mainWindow.Size);
        }

        private void Casual_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Window = new GameScreen(mainWindow, new Casual());
        }

        private void Classic_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Window = new GameScreen(mainWindow, new Classic());
        }

        private void CpuFloodRace_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Window = new GameScreen(mainWindow, new ComputerFloodRace());
        }

        private void TwoPlayerFloodRace_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Window = new GameScreen(mainWindow, new TwoPlayerFloodRace());
        }

        private void Author_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/laxsrbija");
        }

        private void SetAndMarkGridSize(GridSize size)
        {
            mainWindow.Size = size;

            var notSelected = new SolidColorBrush(Color.FromRgb(230, 57, 70));
            var selected = new SolidColorBrush(Color.FromRgb(241, 250, 238));

            switch (mainWindow.Size)
            {
                case GridSize.SMALL:
                    SizeSmall.Foreground = selected;
                    SizeMedium.Foreground = notSelected;
                    SizeLarge.Foreground = notSelected;
                    break;
                case GridSize.MEDIUM:
                    SizeSmall.Foreground = notSelected;
                    SizeMedium.Foreground = selected;
                    SizeLarge.Foreground = notSelected;
                    break;
                case GridSize.LARGE:
                    SizeSmall.Foreground = notSelected;
                    SizeMedium.Foreground = notSelected;
                    SizeLarge.Foreground = selected;
                    break;
            }

        }

        private void SizeLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {

            var label = sender as Label;
            
            if (label.Name == "SizeSmall")
            {
                SetAndMarkGridSize(GridSize.SMALL);
            } else if (label.Name == "SizeMedium")
            {
                SetAndMarkGridSize(GridSize.MEDIUM);
            } else if (label.Name == "SizeLarge")
            {
                SetAndMarkGridSize(GridSize.LARGE);
            }

        }

    }
}
