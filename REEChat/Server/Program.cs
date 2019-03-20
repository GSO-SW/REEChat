using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;
using REEChatDLL;

namespace Server
{
	class Program
	{
		static void Main(string[] args)
		{
			WriteLine("Willkommen bei REEChat Server!");
			WriteLine("Starte den Server...");
			WriteLine("Überprüfung der Datenbankverbindung...");

			while (!DBController.ConnectionAvailable())
			{
				WriteLine("Datenbankverbindung fehlgeschlagen.\nErneuter Versuch in 10 Sekunden");
				Thread.Sleep(10 * 1000);
			}
			WriteLine("Datenbankverbindung erfolgreich!");
			Console.ReadKey();
		}

		static void WriteLine(string text)
		{
			string time = "[" + DateTime.Now.ToString("HH':'mm':'ss") + "] ";
			Console.Write(time + text + "\n");
		}
	}
}
