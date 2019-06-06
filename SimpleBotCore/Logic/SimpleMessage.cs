using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBotCore.Logic
{
    public class SimpleMessage
    {
        public string Id { get; set; }
        public string User { get; set; }
        public string Text { get; set; }

        public SimpleMessage(string id, string username, string text)
        {
            this.Id = id;
            this.User = username;
            this.Text = text;
        }
    }
}