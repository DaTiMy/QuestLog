﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestLog
{
    public class Data
    {
        public static ObservableCollection<Quest> Quests { get; set; }

        public static int QuestSelectedIndex { get; set; }

        public static int SubQuestSelectedIndex { get; set; }
    }
}