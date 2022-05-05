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
using System.Windows.Shapes;

namespace QuestLog
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class DMView : UserControl
    {

        object Quests = Enumerable.Range(1, 50)
            .Select(x => new Quest()
            {
                Name = "Quest " + x,
                OrderNumber = x,
                Subquests = Enumerable.Range(1, 8)
                            .Select(y => new SubQuest()
                            {
                                Name = "SubQuest " + y
                            }).ToList()

            });

        public DMView()
        {
            InitializeComponent();

            object Q = Connection.GetQuestList(2);
            DataContext = Connection.GetQuestList(2);
        }

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
            MainWindow.Instance.DragMove();
        }
        #endregion
    }
}
