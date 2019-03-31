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
		public static List<Message> Messages { get; set; } = new List<Message>();

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
			if (!CFormController.LoggedIn)
			{
				if (CFormController.RegistrationView != null)
				{
					if (package is Feedback feedback)
					{
						switch (feedback.FeedbackCode)
						{
							case FeedbackCode.InternalServerError:
								CFormController.RegistrationView.WaitForFeedback = false;
								MessageBox.Show("Der Server hat interne Probleme!");
								break;
							case FeedbackCode.RegistrationDenied:
								CFormController.RegistrationView.WaitForFeedback = false;
								MessageBox.Show("Der Registrierungsanfrage wurde abgelehnt!");
								break;
							case FeedbackCode.RegistrationDeniedEmailAlreadyUsed:
								CFormController.RegistrationView.WaitForFeedback = false;
								MessageBox.Show("Email wird bereits verwendet!");
								break;
							case FeedbackCode.RegistrationAccepted:
								CFormController.RegistrationView.WaitForFeedback = false;
								MessageBox.Show("Registrierung erfolgreich!");
								CFormController.RegistrationClose();
								break;
						}
					}
				}
				else
				{
					if (package is UserList userList)
					{
						Users = userList.List;
						MessageBox.Show("Login erfolgreich!");
						CFormController.LoginClose();
					}
					else if (package is Feedback feedback)
					{
						switch (feedback.FeedbackCode)
						{
							case FeedbackCode.InternalServerError:
								CFormController.LoginView.WaitForFeedback = false;
								MessageBox.Show("Der Server hat interne Probleme!");
								break;
							case FeedbackCode.LoginDenied:
								CFormController.LoginView.WaitForFeedback = false;
								MessageBox.Show("Login verweigert! Falsche Anmeldedaten!");
								break;
						}
					}
				}
			}
			else
			{
				if (package is UserList userList)
				{

				}
				else if (package is MessageList messageList)
				{
					foreach (MessagePackage k in messageList.List)
					{
						Messages.Add(new Message(k));
					}
					CFormController.MainView.UpdateMessage();
				}
				else if (package is ReceiveTextMessage receiveTextMessage)
				{
					CFormController.MainView.WaitForFeedback = false;
					Messages.Add(new Message(receiveTextMessage.EMail, CConnectionController.LoginUser.Email, DateTime.Now, receiveTextMessage.Text));
					CFormController.MainView.UpdateMessage();
				}
				else if (package is Feedback feedback)
				{
					switch (feedback.FeedbackCode)
					{
						case FeedbackCode.InternalServerError:
							CFormController.MainView.WaitForFeedback = false;
							MessageBox.Show("Der Server hat interne Probleme!");
							break;
						case FeedbackCode.InvalidSession:

							break;
						case FeedbackCode.MessageSendSuccess:
							CFormController.MainView.WaitForFeedback = false;
							CFormController.MainView.SetText("");
							break;
						case FeedbackCode.MessageSendFailed:
							CFormController.MainView.WaitForFeedback = false;
							MessageBox.Show("Die Nachricht konnte nicht gesendet werden!");
							break;
					}
				}
			}
		}
	}
}
