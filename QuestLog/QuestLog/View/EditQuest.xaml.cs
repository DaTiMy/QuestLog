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
        private Quest LoadedQuest { get; set; }

        public EditQuest(Quest q)
        {
            LoadedQuest = q;
            
            InitializeComponent();

            FillDataInitial();
        }

        private void FillDataInitial()
        {
            txtCopper.Text = LoadedQuest.Copper.ToString();
            txtExp.Text = LoadedQuest.EXP.ToString();
            txtGold.Text = LoadedQuest.Gold.ToString();
            txtName.Text = LoadedQuest.Name.ToString();
            txtSilver.Text = LoadedQuest.Silver.ToString();
            listSubquests.DataContext = LoadedQuest.Subquests;
        }

        #region Toolbar functionality
        public void ExitApplication(object sender, RoutedEventArgs e)
        {
            VerifyChanges(sender, e);
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
            Close();
        }

        private void AddSubQuest(object sender, RoutedEventArgs e)
        {
            SubQuest sq = new SubQuest("new Description", false, "new Subquest", -1);

            sq = Connection.AddSubQuest(LoadedQuest.QID, sq);
            LoadedQuest.Subquests.Add(sq);
            Refresh();
        }

        private void RemoveSubQuest(object sender, RoutedEventArgs e)
        {
            int index = listSubquests.SelectedIndex;

            if (index == -1)
                return;

            Connection.RemoveSubQuest(LoadedQuest.Subquests[index].SQID);
            LoadedQuest.Subquests.RemoveAt(index);
            Refresh();
        }

        public void Refresh()
        {
            DataContext = LoadedQuest.Subquests;
            ICollectionView view = CollectionViewSource.GetDefaultView(LoadedQuest.Subquests);
            view.Refresh();
        }


    }
}