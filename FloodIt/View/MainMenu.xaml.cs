﻿using System;
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

namespace FloodIt.View
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {

        private MainWindow mainWindow; // TODO Preimenovati dogadjaje

        public MainMenu(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Window = new GameScreen(mainWindow, 1);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainWindow.Window = new GameScreen(mainWindow, 2);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mainWindow.Window = new GameScreen(mainWindow, 3);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            mainWindow.Window = new GameScreen(mainWindow, 4);
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/laxsrbija");
        }
    }
}
