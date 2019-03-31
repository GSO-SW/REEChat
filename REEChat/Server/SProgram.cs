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
	class SProgram
	{
		static void Main(string[] args)
		{
			WriteLine("Willkommen bei REEChat Server!");
			WriteLine("Überprüfung der Datenbankverbindung...");

			//checks whether a database connection can be created
			while (!SDBController.ConnectionAvailable())
			{
				WriteLine("Datenbankverbindung fehlgeschlagen.\nErneuter Versuch in 10 Sekunden");
				Thread.Sleep(10 * 1000);
			}
			WriteLine("Datenbankverbindung erfolgreich!");

			WriteLine("Server wird initialisiert...");
			SConnectionController.Init();

			WriteLine("Server wird gestartet...");

			//Trying to start the listener
			try
			{
				SConnectionController.Listener.Start(10);
			}
			catch (SocketException)
			{

				WriteLine("Server kann nicht gestartet werden. Der Port [" + ConnectionConfig.serverPort + "] wird bereits verwendet.");
				WriteLine("Mögliche Ursache dafür ist, dass bereits ein REEChat Server auf diesem Gerät läuft!");
				WriteLine("Beliebige Taste drücken zum Schließen...");
				Console.ReadKey();
				return;
			}

			SConnectionController.ThreadListener.Start();

			WriteLine("Server erfolgreich gestartet!\n");

			while (true)
			{

			}
		}

		internal static void WriteConsoleTime()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write("[" + DateTime.Now.ToString("HH':'mm':'ss") + "] ");
			Console.ForegroundColor = ConsoleColor.White;
		}

		/// <summary>
		/// Writes a line into the console
		/// </summary>
		/// <param name="text">text to write</param>
		internal static void WriteLine(string text)
		{
			WriteConsoleTime();
			Console.ForegroundColor = ConsoleColor.White; Console.Write(text);
			Console.ForegroundColor = ConsoleColor.White; Console.WriteLine();
		}

		/// <summary>
		/// Writes a line into the console with some information about a received package
		/// </summary>
		/// <param name="package">received package</param>
		/// <param name="sender">sender ip</param>
		internal static void WritePackageReceiveInfo(Package package, string sender)
		{
			WriteConsoleTime();
			Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + package.Type + "] ");
			Console.ForegroundColor = ConsoleColor.Green; Console.Write("empfangen von ");
			Console.ForegroundColor = ConsoleColor.DarkCyan; Console.Write(sender);
			Console.WriteLine();
			switch (package.Type)
			{
				case PackageType.RegistrationRequest:
					WriteDetail(((RegistrationRequest)package).ToString());
					break;
				case PackageType.LoginRequest:
					WriteDetail(((LoginRequest)package).ToString());
					break;
				case PackageType.TextMessageSend:
					WriteDetail(((SendTextMessage)package).ToString());
					break;
				case PackageType.Ping:
					//Console.Write(((RegistrationRequest)package).ToString());
					break;
				case PackageType.Feedback:
					WriteDetail(((Feedback)package).ToString());
					break;
				default:
					break;
			}
			Console.ForegroundColor = ConsoleColor.White; Console.Write("\n");
		}

		/// <summary>
		/// Writes a line into the console with some information about a sended package
		/// </summary>
		/// <param name="package">sended package</param>
		/// <param name="receiver">receiver ip</param>
		internal static void WritePackageSendInfo(Package package, string receiver)
		{
			WriteConsoleTime();
			Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("[" + package.Type + "] ");
			Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("gesendet an ");
			Console.ForegroundColor = ConsoleColor.DarkCyan; Console.Write(receiver);
			Console.WriteLine();

			switch (package.Type)
			{
				case PackageType.UserList:
					foreach (User user in ((UserList)package).List)
					{
						WriteDetail(user.ToString());
					}
					break;
				case PackageType.TextMessageReceive:
					WriteDetail(((ReceiveTextMessage)package).ToString());
					break;
				case PackageType.Ping:
					break;
				case PackageType.Feedback:
					WriteDetail(((Feedback)package).ToString());
					break;
				default:
					break;
			}
			Console.ForegroundColor = ConsoleColor.White; Console.Write("\n");
		}

		/// <summary>
		/// Writes a line into the console with some information about some details of a package
		/// </summary>
		/// <param name="detail">detail of the package</param>
		internal static void WriteDetail(string detail)
		{
			WriteConsoleTime();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("[Content] " + detail);
			Console.ForegroundColor = ConsoleColor.White; Console.Write("\n");
		}

		internal static void WritePackageSendFailInfo(byte[] buffer, string receiver)
		{
			WriteConsoleTime();
			Console.ForegroundColor = ConsoleColor.DarkRed; Console.Write("[Fehlerhaftes Packet]");
			Console.ForegroundColor = ConsoleColor.DarkRed; Console.Write("empfangen von ");
			Console.ForegroundColor = ConsoleColor.DarkCyan; Console.Write(receiver);
			Console.WriteLine();
			foreach (byte b in buffer)
			{
				Console.Write(b.ToString() + " ");
			}
			Console.ForegroundColor = ConsoleColor.White; Console.Write("\n");
		}


	}
}
