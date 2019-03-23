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
		public static CMainView MainView { get; set; } = null;
		public static CRegistrationForm RegistrationView { get; set; }
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
			MainView.Invoke(new CloseDelagate(MainView.Close));
		}

		/// <summary>
		/// Cancels RegistrationView
		/// </summary>
		public static void CancelRegistrationView()
		{
			RegistrationView.Invoke(new CloseDelagate(RegistrationView.Close));
		}
	}
}
