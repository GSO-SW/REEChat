using REEChatDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
	internal static class SConnectionController
	{
		internal static bool Running { get; set; }
		internal static string ServerAddress { get; set; }
		internal static Thread ThreadListener { get; set; }
		internal static TcpListener Listener { get; set; }

		/// <summary>
		/// Initialize the listener
		/// </summary>
		internal static void Init()
		{
			Running = true;
			ThreadListener = new Thread(Listen);
			Listener = new TcpListener(IPAddress.Any, ConnectionConfig.serverPort);
		}

		/// <summary>
		/// Stops the listener
		/// </summary>
		internal static void Stop()
		{
			Running = false;
			if (Listener != null)
				Listener.Stop();
		}

		/// <summary>
		/// Listen while listener is running
		/// </summary>
		private static void Listen()
		{
			while (Running)
			{
				try
				{
					TcpClient client = Listener.AcceptTcpClient();
					string clientAddress = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

					NetworkStream stream = client.GetStream();

					while (client.Available == 0) { }

					byte[] buffer = new byte[client.Available];
					stream.Read(buffer, 0, buffer.Length);

					stream.Close();

					switch (Package.TryParse(buffer, out Package package))
					{
						case 0:
							SProgram.WriteLine("Erfolgreich Paket von [" + clientAddress + "] empfangen!");
							SDataController.ProcessingReceivedPackage(package, clientAddress);
							break;
						case 1:
							SProgram.WriteLine("Fehlerhaftes Paket von [" + clientAddress + "] empfangen!");
							SProgram.WriteLine("Grund: Strukturfehler!");

							for (int i = 0; i < buffer.Length; i++)
							{
								SProgram.WriteLine(i.ToString() + ": " + buffer[i].ToString());
							}

							ReceivedIncorrectPackage(clientAddress);
							break;
						case 2:
							SProgram.WriteLine("Fehlerhaftes Paket von [" + clientAddress + "] empfangen!");
							SProgram.WriteLine("Grund: Umwandlungsfehler!");

							for (int i = 0; i < buffer.Length; i++)
							{
								SProgram.WriteLine(i.ToString() + ": " + buffer[i].ToString());
							}

							ReceivedIncorrectPackage(clientAddress);
							break;
					}
				}
				catch (SocketException)
				{

				}
			}
		}


		/// <summary>
		/// Sends a package
		/// </summary>
		/// <param name="package">package to send</param>
		/// <param name="clientAddress">destination address of the package</param>
		internal static void SendPackage(Package package, string clientAddress)
		{
			TcpClient client = null;
			byte[] buffer = package.ToByteArray();
			client = new TcpClient(clientAddress, ConnectionConfig.clientPort);
			NetworkStream stream = client.GetStream();
			stream.Write(buffer, 0, buffer.Length);
			stream.Close();
			client.Close();
		}

		/// <summary>
		/// Sends a 'Incorrect Package' Feedback
		/// </summary>
		/// <param name="clientAddress">destination address of the package</param>
		internal static void ReceivedIncorrectPackage(string clientAddress)
		{
			SendPackage(new Feedback(FeedbackCode.IncorrectPackage), clientAddress);
		}
	}
}
