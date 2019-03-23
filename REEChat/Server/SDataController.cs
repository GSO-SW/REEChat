using REEChatDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	internal static class SDataController
	{
		internal static void ProcessingReceivedPackage(Package package, string clientAddress)
		{
			switch (package.Type)
			{
				case PackageType.RegistrationRequest:
					break;
				case PackageType.LoginRequest:
					LoginRequest login = (LoginRequest)package;
					Feedback transmittedFeedback = LoginRequest(login, clientAddress);
					SProgram.WriteLine("Ergebnis: " + transmittedFeedback.FeedbackCode.ToString());
					break;
				case PackageType.Online:
					break;
				case PackageType.Offline:
					break;
				case PackageType.UserList:
					break;
				case PackageType.UserAdd:
					break;
				case PackageType.UserRemove:
					break;
				case PackageType.TextMessageSend:
					break;
				case PackageType.TextMessageReceive:
					break;
				case PackageType.Ping:
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Handle a LoginRequest package 
		/// </summary>
		/// <param name="loginRequest">LoginRequest to handle</param>
		/// <param name="clientAddress">address of the sender of the package</param>
		/// <returns>the feedback transmitted</returns>
		internal static Feedback LoginRequest(LoginRequest loginRequest, string clientAddress)
		{
			Feedback feedback = null;

			if (!SDBController.TryVerifyUser(loginRequest.Email, loginRequest.PasswordHash, out bool result))
				feedback = new Feedback(FeedbackCode.InternalServerError);
			else
				if (result)
				if (SDBController.TryClientUpdateLastIPAddress(loginRequest.Email, clientAddress))
					feedback = new Feedback(FeedbackCode.LoginAccepted);
				else
					feedback = new Feedback(FeedbackCode.InternalServerError);
			else
				feedback = new Feedback(FeedbackCode.LoginDenied);


			SConnectionController.SendPackage(feedback, clientAddress);

			return feedback;
		}
	}
}
