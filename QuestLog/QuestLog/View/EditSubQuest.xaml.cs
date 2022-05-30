using Newtonsoft.Json.Linq;
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
    public partial class EditSubQuest : Window
    {
        private SubQuest LoadedSubQuest { get; set; }

        public EditSubQuest(SubQuest sq)
        {
            LoadedSubQuest = sq;

            InitializeComponent();
            
            FillDataInitial();
        }

        private void FillDataInitial()
        {
            txtName.Text = LoadedSubQuest.Name.ToString();
            txtDesc.Text = LoadedSubQuest.Description.ToString();
        }

        #region Toolbar functionality
        public void ExitApplication(object sender, RoutedEventArgs e)
        {
            Close();
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

        private void VerifyChanges(object sender, RoutedEventArgs e)
        {
            JObject update =
                new JObject(
                    new JProperty("Name", txtName.Text.ToString()),
                    new JProperty("Description", txtDesc.Text.ToString()));

            var json = update.ToString();
            Connection.EditSubQuest(LoadedSubQuest.SQID, json);
            Close();
        }
    }
}
