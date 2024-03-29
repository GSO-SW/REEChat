﻿using REEChatDLL;
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
							SProgram.WritePackageReceiveInfo(package, clientAddress);
							SDataController.ProcessingReceivedPackage(package, clientAddress);
							break;
						case 1:
							SProgram.WritePackageSendFailInfo(buffer, clientAddress);
							ReceivedIncorrectPackage(clientAddress);
							break;
						case 2:
							SProgram.WritePackageSendFailInfo(buffer, clientAddress);
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
		internal static bool SendPackage(Package package, string clientAddress)
		{
			SProgram.WritePackageSendInfo(package, clientAddress);
			TcpClient client = null;
			byte[] buffer = package.ToByteArray();
		
			try
			{
				client = new TcpClient(clientAddress, ConnectionConfig.clientPort);
				NetworkStream stream = client.GetStream();
				stream.Write(buffer, 0, buffer.Length);
				stream.Close();
				client.Close();
			}
			catch (Exception)
			{
				return false;
			}
			return true;
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
