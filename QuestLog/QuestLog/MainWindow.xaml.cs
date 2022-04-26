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

namespace QuestLog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ActWindow currWindow;

        public ActWindow CurrWindow
        {
            get { return currWindow; }
            set
            {
                switch (value)
                {
                    case ActWindow.Login:
                        ////Setze Background auf Menü-Background
                        //BackgroundGame.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Images/Millenium_Menu.png"));
                        ////Lade die Menu-UserControl
                        //MainContent.Content = new Menu(this);
                        break;
                }
                currWindow = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            CurrWindow = ActWindow.Login;
        }
        #region Enum ActWindow
        //Register, Login, MainMenu, DMView, PlayerView
        public enum ActWindow
        {
            Login
        }
        #endregion
    }
}
