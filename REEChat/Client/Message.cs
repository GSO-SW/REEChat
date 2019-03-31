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
		public string Sender { get; set; }
		public string Receiver { get; set; }
		public DateTime? TimeReceived { get; set; }
		public string Text { get; set; }

		/// <summary>
		/// Display member for list box
		/// </summary>
		public string DisplayMember
		{
			get
			{
				// [12:12:34] Peter: Hallo
				// [12:12:45] Hans: Was geht?	
				string time = TimeReceived.Value.ToString("HH':'mm':'ss");
				return "[" + time + "] " + Sender + ": " + Text;
			}
		}

		/// <summary>
		/// Creates a new instance of type Message
		/// </summary>
		/// <param name="sender">sender of the message</param>
		/// <param name="receiver">receiver of the message</param>
		/// <param name="timeReceived">time</param>
		/// <param name="text">text</param>
		public Message(string sender, string receiver, DateTime? timeReceived, string text)
		{
			Sender = sender;
			Receiver = receiver;
			TimeReceived = timeReceived;
			Text = text;
		}

		/// <summary>
		/// Creates a new instance of type Message
		/// </summary>
		/// <param name="messagePackage"></param>
		public Message(MessagePackage messagePackage)
		{
			Sender = messagePackage.Sender;
			Receiver = messagePackage.Receiver;
			TimeReceived = messagePackage.TimeReceived;
			Text = messagePackage.Text;
		}
	}
}
