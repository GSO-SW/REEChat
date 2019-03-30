using REEChatDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Message
    {
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public DateTime TimeReceived { get; set; }
        public string Text { get; set; }

        public string DisplayMember
        {
            get
            {
                // [12:12:34] Ich: Hallo
                // [12:12:45] Hans: Was geht?

                string name = "";
                string time = TimeReceived.ToString("HH':'mm':'ss");

                if (CConnectionController.LoginUser.Email == Sender.Email)
                    name = "Ich";
                else
                    name = Sender.Nickname;

                return "[" + time + "] " + name + ": " + Text;
            }
        }

        public Message(User sender, User receiver, DateTime timeReceived, string text)
        {
            Sender = sender;
            Receiver = receiver;
            TimeReceived = timeReceived;
            Text = text;
        }
    }
}
