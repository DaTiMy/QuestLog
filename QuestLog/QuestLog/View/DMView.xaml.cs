using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace QuestLog
{
    /// <summary>
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class DMView : UserControl
    {
        public DMView()
        {
            InitializeComponent();
        }

        public void CheckedQuest(object sender, RoutedEventArgs e)
        {
            vm.CheckedQuest(sender, e);
        }

        public void CheckedSubQuest(object sender, RoutedEventArgs e)
        {
            vm.CheckedSubQuest(sender, e);
        }

        public void AddQuest(object sender, RoutedEventArgs e)
        {
            vm.AddQuest(sender, e);
        }

        public void EditQuest(object sender, RoutedEventArgs e)
        {
            vm.EditQuest(sender, e);

        }

        public void RemoveQuest(object sender, RoutedEventArgs e)
        {
            vm.RemoveQuest(sender, e);
        }
    }
}
