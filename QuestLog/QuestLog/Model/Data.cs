using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QuestLog
{
    public class Data
    {
        public static ObservableCollection<Quest> Quests { get; set; }

        public static int QuestSelectedIndex { get; set; }

        public static int SubQuestSelectedIndex { get; set; }

        public static void QuestRefresh()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(Data.Quests);
            view.Refresh();
        }
    }
}