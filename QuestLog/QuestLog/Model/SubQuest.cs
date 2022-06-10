using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestLog
{
    [Serializable]

    public class SubQuest
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("finish")]
        public bool Finish { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ordernumber")]
        public int OrderNumber { get; set; }

        [JsonProperty("sgid")]
        public int SQID { get; set; }

        public SubQuest(string description, bool finish, string name, int sqid)
        {
            Description = description;
            Finish = finish;
            Name = name;
            SQID = sqid;
        }
    }
}
