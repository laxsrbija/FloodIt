using FloodIt.View;
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

namespace FloodIt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private Page window;
        public Page Window {
            get => window;
            set {
                window = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Window"));
            }
        }

        public MainWindow()
        {

            InitializeComponent();
            DataContext = this;

            Window = new MainMenu(this);

        }
    }
}
