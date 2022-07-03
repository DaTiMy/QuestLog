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
        public EditSubQuest()
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
            vm.VerifyChanges();
            Close();
        }
    }
}
