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
        private static readonly MainWindow instance = new MainWindow();

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
                    case ActWindow.DMView:
                        MainContent.Content = new DMView();
                        break;
                }
                currWindow = value;
            }
        }

        public static MainWindow Instance
        {
            get
            {
                return instance;
            }
        }

        static MainWindow()
        {
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
            Register, Login, MainMenu, DMView, PlayerView
        }
        #endregion

        #region Toolbar functionality
        public void ExitApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }
        public void MaximizeApplication(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Normal)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }
        public void MinimizeApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        public void Drag(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CurrWindow = ActWindow.DMView;
        }
    }
}
