using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
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
