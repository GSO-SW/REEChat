using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
	public static class CFormController
	{
		internal static CLoginView LoginView { get; set; }
		internal static CMainView MainView { get; set; }
		internal static CRegistrationView RegistrationView { get; set; }

		internal static bool LoggedIn
		{
			get
			{
				if (CDataController.Users == null)
					return false;
				return true;
			}
		}

		/// <summary>
		/// Closes the login view
		/// </summary>
		public static void LoginClose()
		{
			LoginView.Invoke(new CloseDelagate(LoginView.Close));
		}

		/// <summary>
		/// Closes the registration view
		/// </summary>
		public static void RegistrationClose()
		{
			RegistrationView.Invoke(new CloseDelagate(RegistrationView.Close));
		}

		/// <summary>
		/// Closes the main view
		/// </summary>
		public static void MainClose()
		{
			MainView.Invoke(new CloseDelagate(MainView.Close));
		}
	}
}
