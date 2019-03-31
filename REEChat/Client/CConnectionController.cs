using REEChatDLL;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
	internal static class CConnectionController
	{
		internal static bool Running { get; set; }
		internal static string ServerAddress { get; set; }
		internal static Thread ThreadListener { get; set; }
		internal static TcpListener Listener { get; set; }
		internal static User LoginUser { get; set; }

		/// <summary>
		/// Starts the listener
		/// </summary>
		internal static void Start()
		{
			Running = true;
			ThreadListener = new Thread(Listen);
			ThreadListener.Start();
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
			Listener = new TcpListener(IPAddress.Any, ConnectionConfig.clientPort);

			try
			{
				Listener.Start(3);
			}

			catch (SocketException)
			{
				MessageBox.Show("Server kann nicht gestartet werden. Der Port [" + ConnectionConfig.clientPort + "] wird bereits verwendet.\n" +
								"Mögliche Ursache dafür ist, dass bereits ein REEChat Client auf diesem Gerät läuft!");
				CFormController.LoginClose();
			}

			while (Running)
			{
				try
				{
					TcpClient server = Listener.AcceptTcpClient();
					string serverAddress = ((IPEndPoint)server.Client.RemoteEndPoint).Address.ToString();

					NetworkStream stream = server.GetStream();

					while (server.Available == 0) { }

					byte[] buffer = new byte[server.Available];
					stream.Read(buffer, 0, buffer.Length);

					stream.Close();

					int i = Package.TryParse(buffer, out Package package);
					if (i == 0)
					{
						CDataController.ProcessingReceivedPackage(package, serverAddress);
					}						
				}
				catch (SocketException)
				{

				}
			}
		}

		/// <summary>
		/// Attempts to send a package and indicates if this was successful
		/// </summary>
		/// <param name="package">package to send</param>
		/// <param name="serverAddress">receiver of the package</param>
		/// <returns>Indicates if it was successful</returns>
		internal static bool TrySendPackage(Package package, string serverAddress)
		{
			TcpClient sender = null;
			byte[] buffer = package.ToByteArray();

			try
			{
				sender = new TcpClient(serverAddress, ConnectionConfig.serverPort);
			}
			catch (Exception)
			{
				MessageBox.Show("Es konnte keine Verbindung zum Server hergestellt werden.");
				return false;
			}

			if (sender == null)
				return false;

			NetworkStream stream = sender.GetStream();
			stream.Write(buffer, 0, buffer.Length);
			stream.Close();
			sender.Close();
			return true;
		}
	}
}
