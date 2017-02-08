using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AchieveEntity
{
    public class CSGroupResult
    {
        public int id { get; set; }
        public string name { get; set; }
        public int nums { get { return item == null ? 0 : item.Count; } }
        public List<CSBaseModel> item { get; set; }
    }
}