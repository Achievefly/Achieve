using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AchieveEntity
{
    public class CSBaseModel
    {
        public string id { get; set; }
        public string name { get; set; }
      //  public string time { get; set; }
        public string face { get; set; }
    }

    public class CSFriend : CSBaseModel { }
    public class CSGroup : CSBaseModel { }
    public class CSGroups : CSBaseModel { }

    public class CSChatLog : CSBaseModel
    {
        public string time { get; set; }
    }
}