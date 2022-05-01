using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestLog
{
    internal class Quest
    {
        public string Name { get; set; }

        public int Nr { get; set; }

        public List<SubQuest> Subquests { get; set; }

    }
}
