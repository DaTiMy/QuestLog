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
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class PlayerView : UserControl
    {
        public List<Quest> Quests { get; set; }


        public PlayerView()
        {
            InitializeComponent();

            Quests = Connection.GetQuestList(1);

            DataContext = Quests;
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

        public void CheckedQuest(object sender, RoutedEventArgs e)
        {
            int qid = ((sender as CheckBox).DataContext as Quest).QID;
            object header = (((sender as CheckBox).Parent as StackPanel).Children[0] as TreeViewItem).Header;

            TextBlock text = new TextBlock();

            if ((sender as CheckBox).IsChecked == true)
            {
                if(header is TextBlock)
                {
                    text.Text = (header as TextBlock).Text;
                }
                else if(header is string)
                {
                    string questtext = header.ToString();
                    text.Text = questtext;
                }
                text.TextDecorations = TextDecorations.Strikethrough;
            }
            else
            {
                if (header is TextBlock)
                {
                    text.Text = (header as TextBlock).Text;
                }
                else if (header is string)
                {
                    string questtext = header.ToString();
                    text.Text = questtext;
                }
                text.TextDecorations = null;
            }

            (((sender as CheckBox).Parent as StackPanel).Children[0] as TreeViewItem).Header = text;

            Connection.FinishQuest(qid);
        }

        public void CheckedSubQuest(object sender, RoutedEventArgs e)
        {
            int sqid = ((sender as CheckBox).DataContext as SubQuest).SQID;

            //TextBlock subquest = ((sender as CheckBox).Parent as StackPanel).Children[1] as TextBlock;

            //TextBlock text = new TextBlock();

            //if ((sender as CheckBox).IsChecked == true)
            //{
            //    subquest.TextDecorations = TextDecorations.Strikethrough;
            //}
            //else
            //{
            //    subquest.TextDecorations = null;
            //}

            Connection.FinishSubQuest(sqid);
        }
    }
}
