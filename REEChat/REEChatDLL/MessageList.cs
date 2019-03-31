using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
	public class MessageList : Package
	{
		public List<MessagePackage> List { get; set; }

		/// <summary>
		/// Creates a new instance of type MessageList
		/// </summary>
		/// <param name="list"></param>
		public MessageList(List<MessagePackage> list)
		{
			Type = PackageType.MessageList;
			List = list;
		}

		/// <summary>
		/// Tries to parse a bytearray to a MessageList object
		/// </summary>
		/// <param name="data"></param>
		/// <param name="messageList"></param>
		/// <returns></returns>
		internal static bool TryParse(byte[] data, out MessageList messageList)
		{
			messageList = null;
			List<MessagePackage> messageListTemp = new List<MessagePackage>();

			while (TrySplitByte(data, PackageControl.GroupSeperator, true, out byte[] userByte, out data))
			{
				if (!MessagePackage.TryParse(userByte, out MessagePackage message))
					return false;
				messageListTemp.Add(message);

				if (data.Length == 0 && messageListTemp.Count > 0)
				{
					messageList = new MessageList(messageListTemp);
					return true;
				}
			}

			return false;
		}

		public override byte[] UserData()
		{
			List<byte> array = new List<byte>();

			foreach (MessagePackage mess in List)
			{
				array.AddRange(mess.ToByteArray());
			}

			return array.ToArray();
		}
	}
}
