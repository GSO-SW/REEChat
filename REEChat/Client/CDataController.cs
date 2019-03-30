using REEChatDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
	internal static class CDataController
	{
		public static List<User> Users { get; set; } = null;

		/// <summary>
		/// Processes a received package
		/// </summary>
		/// <param name="package">received package</param>
		/// <param name="address">address of the sender of the package</param>
		internal static void ProcessingReceivedPackage(Package package, string address)
		{
			//sender != server
			if (CConnectionController.ServerAddress != address)
				return;
			//Not logged in
			if (string.IsNullOrWhiteSpace(CFormController.RegisteredEmail))
				switch (package.Type)
				{
					case PackageType.Online:
						break;
					case PackageType.Offline:
						break;
					case PackageType.UserList:
						Users = ((UserList)package).List;
						MessageBox.Show("Login erfolgreich!");
						CFormController.Login();
						break;
					case PackageType.TextMessageReceive:
						break;
					case PackageType.Feedback:
						Feedback feedback = (Feedback)package;
						bool check = false;
						if (CFormController.RegistrationView != null)
							check = CFormController.RegistrationView.WaitForFeedback;
						if (CFormController.LoginView.WaitForFeedback || check)
						{
							switch (feedback.FeedbackCode)
							{
								case FeedbackCode.InternalServerError:
									MessageBox.Show("Der Server hat interne Probleme!");
									break;
								case FeedbackCode.LoginDenied:
									MessageBox.Show("Login verweigert! Falsche Anmeldedaten!");
									break;
								case FeedbackCode.RegistrationDenied:
									MessageBox.Show("Der Registrierungsanfrage wurde abgelehnt! \nWahrscheinlich wurden die Daten verändert!");
									break;
								case FeedbackCode.RegistrationAccepted:
									MessageBox.Show("Registrierung erfolgreich!");
									CFormController.CancelRegistrationView();
									break;
								case FeedbackCode.RegistrationDeniedEmailAlreadyUsed:
									MessageBox.Show("Email wird bereits verwendet!");
									break;
							}
						}
						break;
				}
		}
	}
}
