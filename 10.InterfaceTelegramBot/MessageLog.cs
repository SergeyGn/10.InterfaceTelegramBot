﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10.InterfaceTelegramBot
{
   public class MessageLog
    {
        public string Time { get; set; }

        public long Id { get; set; }

        public string Msg { get; set; }

        public string FirstName { get; set; }

        public int CountNewMsg { get; set; }

        public MessageLog(string Time, string Msg, string FirstName, long Id, int CountNewMsg)
        {
            this.Time = Time;
            this.Msg = Msg;
            this.FirstName = FirstName;
            this.Id = Id;
            this.CountNewMsg = CountNewMsg;
        }
    }
}