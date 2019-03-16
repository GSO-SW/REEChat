using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
	/// <summary>
	/// All package types
	/// </summary>
	public enum PackageType
	{
		RegistrationRequest = 10,
		LoginRequest = 11,
		Online = 12,
		Offline = 13,
		UserList = 14,
		UserAdd = 15,
		UserRemove = 16,
		TextMessageSend = 17,
		TextMessageReceive = 18,
		Ping = 19,
		Feedback = 20
	}

	/// <summary>
	/// All package controls
	/// </summary>
	public static class PackageControl
	{
		public const byte StartOfHeader = 001;
		public const byte StartOfText = 002;
		public const byte EndOfText = 003;
		public const byte UnitSeperator = 031;
	}
}
