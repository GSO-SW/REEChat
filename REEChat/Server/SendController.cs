using REEChatDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	public static class SendController
	{
		/// <summary>
		/// Sender TcpClient
		/// </summary>
		private static TcpClient Client { get; set; }

		/// <summary>
		/// Sends a package
		/// </summary>
		/// <param name="package">package to send</param>
		/// <param name="clientAddress">destination address of the package</param>
		public static void SendPackage(Package package, string clientAddress)
		{
			byte[] buffer = package.ToByteArray();
			Client = new TcpClient(clientAddress, ConnectionConfig.clientPort);
			NetworkStream stream = Client.GetStream();
			stream.Write(buffer, 0, buffer.Length);
			stream.Close();
			Client.Close();
		}

		/// <summary>
		/// Sends a 'Incorrect Package' Feedback
		/// </summary>
		/// <param name="clientAddress">destination address of the package</param>
		public static void ReceivedIncorrectPackage(string clientAddress)
		{
			SendPackage(new Feedback(FeedbackCode.IncorrectPackage), clientAddress);
		}
	}
}
