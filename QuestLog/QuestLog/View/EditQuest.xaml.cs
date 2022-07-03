using Newtonsoft.Json.Linq;
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
using System.Windows.Shapes;

namespace QuestLog
{
    public partial class EditQuest : Window
    {
        public EditQuest()
        {
            InitializeComponent();
        }

        #region Toolbar functionality
        public void ExitApplication(object sender, RoutedEventArgs e)
        {
            Confirm(sender, e);
        }
        public void Drag(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Instance.Drag(sender, e);
        }
        #endregion

        private void Confirm(object sender, RoutedEventArgs e)
        {
            vm.VerifyChanges(sender, e);
            Close();
        }

        private void AddSubQuest(object sender, RoutedEventArgs e)
        {
            vm.AddSubQuest(sender, e);
        }

        private void EditSubQuest(object sender, RoutedEventArgs e)
        {
            vm.EditSubQuest(sender, e);
        }

        private void RemoveSubQuest(object sender, RoutedEventArgs e)
        {
            vm.RemoveSubQuest(sender, e);
        }
    }
}