using Newtonsoft.Json.Linq;
using QuestLog.Model;
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

            if (LoadedQuest.Subquests == null)
                LoadedQuest.Subquests = new List<SubQuest>();
            
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
            JObject update =
                new JObject(
                    new JProperty("Copper", int.Parse(txtCopper.Text)),
                    new JProperty("EXP", int.Parse(txtExp.Text)),
                    new JProperty("Gold", int.Parse(txtGold.Text)),
                    new JProperty("Name", txtName.Text.ToString()),
                    new JProperty("Silver", int.Parse(txtSilver.Text))
                    );

            var json = update.ToString();
            Connection.EditQuest(LoadedQuest.QID, json);
            Close();
        }

        private void AddSubQuest(object sender, RoutedEventArgs e)
        {
            SubQuest sq = new SubQuest("new Description", false, "new Subquest", -1);

            sq = Connection.AddSubQuest(LoadedQuest.QID, sq);
            LoadedQuest.Subquests.Add(sq);
            Refresh();
        }

        private void EditSubQuest(object sender, RoutedEventArgs e)
        {
            int index = listSubquests.SelectedIndex;

            if (index == -1)
                return;

            EditSubQuest editsqwindow = new EditSubQuest(LoadedQuest.Subquests[index]);
            editsqwindow.ShowDialog();
            Refresh();
        }

        private void RemoveSubQuest(object sender, RoutedEventArgs e)
        {
            int index = listSubquests.SelectedIndex;

            if (index == -1)
                return;

            Connection.RemoveSubQuest(LoadedQuest.Subquests[index].SQID);
            LoadedQuest.Subquests.RemoveAt(index);

            if (LoadedQuest.Subquests.Count == 0)
                LoadedQuest.Subquests = new List<SubQuest>();
            Refresh();
        }

        public void Refresh()
        {
            DataContext = LoadedQuest.Subquests;
            ICollectionView view = CollectionViewSource.GetDefaultView(listSubquests.Items);
            view.Refresh();
        }
    }
}