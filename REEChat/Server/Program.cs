using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;
using REEChatDLL;

namespace Server
{
	class Program
	{
		static TcpListener server = null;

		static void Main(string[] args)
		{
			WriteLine("Willkommen bei REEChat Server!");
			WriteLine("Überprüfung der Datenbankverbindung...");

			while (!DBController.ConnectionAvailable())
			{
				WriteLine("Datenbankverbindung fehlgeschlagen.\nErneuter Versuch in 10 Sekunden");
				Thread.Sleep(10 * 1000);
			}
			WriteLine("Datenbankverbindung erfolgreich!");

			WriteLine("Server wird initialisiert...");
			server = new TcpListener(IPAddress.Any, ConnectionConfig.serverPort);

			WriteLine("Server wird gestartet...");
			server.Start(10);

			WriteLine("Server erfolgreich gestartet!");

			while (true)
			{
				TcpClient client = server.AcceptTcpClient();
				string clientAddress = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

				NetworkStream stream = client.GetStream();

				byte[] buffer = new byte[client.Available];
				stream.Read(buffer, 0, buffer.Length);

				stream.Close();

				if (!TryConvertInputByte(buffer, out PackageType packageType, out byte[] userData))
				{
					WriteLine("Fehlerhaftes Paket von [" + clientAddress + "] empfangen!");
					SendController.ReceivedIncorrectPackage(clientAddress);
				}
				else
					DataHandling(packageType, userData, clientAddress);
			}
		}

		static void WriteLine(string text)
		{
			string time = "[" + DateTime.Now.ToString("HH':'mm':'ss") + "] ";
			Console.Write(time + text + "\n");
		}

		static bool TryConvertInputByte(byte[] input, out PackageType packageType, out byte[] userData)
		{
			packageType = 0;
			userData = null;

			if (input.Length <= 3)
				return false;
			if (input[0] != PackageControl.StartOfHeader && input[input.Length - 1] != PackageControl.EndOfText)
				return false;

			byte[] typeByte;

			for (int i = 1; i < input.Length; i++)
			{
				if (input[i] == PackageControl.StartOfText)
				{
					typeByte = new byte[i - 1];
					Array.Copy(input, 1, typeByte, 0, typeByte.Length);

					userData = new byte[input.Length - i - 2];
					Array.Copy(input, i + 1, userData, 0, userData.Length);

					if (!int.TryParse(Encoding.UTF8.GetString(typeByte), out int id))
						return false;
					packageType = (PackageType)id;
					return true;
				}
			}
			return false;
		}

		static bool TryGetPackageType(byte[] typeByte, out PackageType packageType)
		{
			packageType = 0;
			if (!int.TryParse(Encoding.UTF8.GetString(typeByte), out int id))
				return false;
			packageType = (PackageType)id;
			return true;
		}

		static void DataHandling(PackageType packageType, byte[] userData, string clientAddress)
		{
			switch (packageType)
			{
				case PackageType.RegistrationRequest:
					break;
				case PackageType.LoginRequest:
					if (LoginRequest.TryParse(userData, out LoginRequest login))
					{
						WriteLine("Erfolgreich Paket von [" + clientAddress + "] empfangen!");
						Feedback transmittedFeedback = DataController.LoginRequest(login, clientAddress);
						WriteLine("Ergebnis: " + transmittedFeedback.ToString());
					}
					else
					{
						WriteLine("Fehlerhaftes Paket von [" + clientAddress + "] empfangen!");
						SendController.ReceivedIncorrectPackage(clientAddress);
					}
					break;
				case PackageType.Online:
					break;
				case PackageType.Offline:
					break;
				case PackageType.UserList:
					break;
				case PackageType.UserAdd:
					break;
				case PackageType.UserRemove:
					break;
				case PackageType.TextMessageSend:
					break;
				case PackageType.TextMessageReceive:
					break;
				case PackageType.Ping:
					break;
				case PackageType.Feedback:
					break;
			}
		}
	}
}
