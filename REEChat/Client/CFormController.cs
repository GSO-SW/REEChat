using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
	internal static class CFormController
	{
		public static CLoginView LoginView { get; set; } = null;
		public static CMainView Main { get; set; } = null;
		public static string RegisteredEmail { get; private set; } = "";

		/// <summary>
		/// Switchs to MainView
		/// </summary>
		public static void Login()
		{
			CancelLoginView();
			CProgram.StartMainView();
		}

		/// <summary>
		/// Switchs back to LoginView
		/// </summary>
		public static void Logoff()
		{
			CancelMainView();
			CProgram.StartLoginView();
		}
		
		/// <summary>
		/// Cancels LoginView
		/// </summary>
		public static void CancelLoginView()
		{
			LoginView.Invoke(new CloseDelagate(LoginView.Close));
		}

		/// <summary>
		/// Cancels MainView
		/// </summary>
		private static void CancelMainView()
		{
			Main.Invoke(new CloseDelagate(Main.Close));
		}
	}
}
