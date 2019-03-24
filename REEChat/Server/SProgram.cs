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

			WriteLine("Server erfolgreich gestartet!");
			
			while (true)
			{

			}
		}

		/// <summary>
		/// Writes a line into the console
		/// </summary>
		/// <param name="text">text to write</param>
		internal static void WriteLine(string text)
		{
			string time = "[" + DateTime.Now.ToString("HH':'mm':'ss") + "] ";
			Console.Write(time + text + "\n");
		}
	}
}
