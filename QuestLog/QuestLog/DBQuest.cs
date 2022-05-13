using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestLog
{
    internal class DBQuest
    {
        public int Copper { get; set; }

        public int EXP { get; set; }

        public int Gold { get; set; }

        public string Name { get; set; }

        public int Silver { get; set; }

        public DBQuest(int copper, int exp, int gold, string name, int silver)
        {
            Copper = copper;
            EXP = exp;
            Gold = gold;
            Name = name;
            Silver = silver;
        }
    }
}
