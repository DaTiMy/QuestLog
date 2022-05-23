using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestLog
{
    internal class DBSubQuest
    {
        public string Description { get; set; }

        public string Name { get; set; }

        public DBSubQuest(string description, string name)
        {
            Description = description;
            Name = name;
        }
    }
}
