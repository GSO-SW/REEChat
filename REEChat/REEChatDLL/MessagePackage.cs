using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
	public class MessagePackage
	{
		public string Sender { get; set; }
		public string Receiver { get; set; }
		public DateTime? TimeReceived { get; set; }
		public string Text { get; set; }

		/// <summary>
		/// Creates a new instance of type MessagePackage
		/// </summary>
		/// <param name="sender">sender of the message</param>
		/// <param name="receiver">receiver of the message</param>
		/// <param name="timeReceived">time</param>
		/// <param name="text">text</param>
		public MessagePackage(string sender, string receiver, DateTime? timeReceived, string text)
		{
			Sender = sender;
			Receiver = receiver;
			TimeReceived = timeReceived;
			Text = text;
		}

		public byte[] ToByteArray()
		{
			List<byte> byteList = new List<byte>();

			byteList.AddRange(Encoding.UTF8.GetBytes(Sender));
			byteList.Add(PackageControl.UnitSeperator);
			byteList.AddRange(Encoding.UTF8.GetBytes(Receiver));
			byteList.Add(PackageControl.UnitSeperator);
			byteList.AddRange(Encoding.UTF8.GetBytes(TimeReceived.ToString()));
			byteList.Add(PackageControl.UnitSeperator);
			byteList.AddRange(Encoding.UTF8.GetBytes(Text));
			byteList.Add(PackageControl.GroupSeperator);

			return byteList.ToArray();
		}

		/// <summary>
		/// Tries to parse a byte array to a MessagePackage obejct
		/// </summary>
		/// <param name="data">user data</param>
		/// <param name="messagePackage">output object</param>
		/// <returns></returns>
		public static bool TryParse(byte[] data, out MessagePackage messagePackage)
		{
			DateTime? date;
			messagePackage = null;

			if (data == null)
				return false;

			if (!Package.TrySplitByte(data, PackageControl.UnitSeperator, false, out byte[] sender, out data))
				return false;
			if (!Package.TrySplitByte(data, PackageControl.UnitSeperator, false, out byte[] receiver, out data))
				return false;
			if (!Package.TrySplitByte(data, PackageControl.UnitSeperator, false, out byte[] timeReceived, out data))
				return false;
			if (!Package.TrySplitByte(data, PackageControl.GroupSeperator, false, out byte[] text, out data))
				return false;
			if (data.Length != 0)
				return false;

			if (!DateTime.TryParse(Encoding.UTF8.GetString(timeReceived), out DateTime timeRe))
				date = DateTime.Now;
			else
				date = timeRe;


			messagePackage = new MessagePackage(Encoding.UTF8.GetString(sender), Encoding.UTF8.GetString(receiver), date, Encoding.UTF8.GetString(text));

			return true;
		}
	}
}
