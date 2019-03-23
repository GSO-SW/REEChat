﻿using REEChatDLL;
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
		/// <summary>
		/// Processes a received package
		/// </summary>
		/// <param name="package">received package</param>
		/// <param name="address">address of the sender of the package</param>
		internal static void ProcessingReceivedPackage(Package package, string address)
		{
			if (string.IsNullOrWhiteSpace(CFormController.RegisteredEmail))
				switch (package.Type)
				{
					case PackageType.Online:
						break;
					case PackageType.Offline:
						break;
					case PackageType.UserList:
						break;
					case PackageType.TextMessageReceive:
						break;
					case PackageType.Feedback:
						Feedback feedback = (Feedback)package;
						if (CFormController.LoginView.WaitForFeedback)
							switch (feedback.FeedbackCode)
							{
								case FeedbackCode.InternalServerError:
									break;
								case FeedbackCode.LoginDenied:
									MessageBox.Show("Login verweigert! Falsche Anmeldedaten!");
									break;
								case FeedbackCode.LoginAccepted:
									MessageBox.Show("Login erfolgreich!");
									CFormController.Login();
									break;
								case FeedbackCode.LoginError:
									break;
								default:
									break;
							}
						break;
				}

		}
	}
}