using REEChatDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
	internal static class CProgram
	{
		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			CConnectionController.Start();

			StartLoginView();
			CConnectionController.Stop();
		}

		/// <summary>
		/// Starts CLoginView
		/// </summary>
		public static void StartLoginView()
		{
			Application.Run(new CLoginView());
		}

		/// <summary>
		/// Starts CMainView
		/// </summary>
		public static void StartMainView()
		{
			Application.Run(new CMainView());
		}
	}
}
