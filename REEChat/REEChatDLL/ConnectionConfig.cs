using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
	public static class ConnectionConfig
	{
		public static readonly IPAddress localAddress = IPAddress.Parse("127.0.0.1");
		public static readonly int serverPort = 27720;
		public static readonly int clientPort = 22721;
	}
}
