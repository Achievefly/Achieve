using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AchieveEntity
{
    public enum CSMessageType
    {
        System = 1,//系统消息，出错，参数错误等消息
        Custom = 2 //普通消息，对话，或者群组消息
    }
}