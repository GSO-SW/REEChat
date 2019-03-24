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
		/// <summary>
		/// Processes a received package
		/// </summary>
		/// <param name="package">received package</param>
		/// <param name="clientAddress">address of the sender of the package</param>
		internal static void ProcessingReceivedPackage(Package package, string clientAddress)
		{
			Feedback transmittedFeedback;
			switch (package.Type)
			{
				case PackageType.RegistrationRequest:
					RegistrationRequest request = (RegistrationRequest)package;
					transmittedFeedback = RegistrationRequest(request, clientAddress);
					break;
				case PackageType.LoginRequest:
					LoginRequest login = (LoginRequest)package;
					transmittedFeedback = LoginRequest(login, clientAddress);
					break;
				case PackageType.Online:
					break;
				case PackageType.Offline:
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
			{
				if (result)
				{
					if (SDBController.TryUpdateClientLastIPAddress(loginRequest.Email, clientAddress))
						feedback = new Feedback(FeedbackCode.LoginAccepted);
					else
						feedback = new Feedback(FeedbackCode.InternalServerError);
				}
				else
					feedback = new Feedback(FeedbackCode.LoginDenied);
			}


			if (feedback.FeedbackCode == FeedbackCode.LoginAccepted)
			{
				if (!SDBController.TryGetClientList(out List<User> userList))
					feedback = new Feedback(FeedbackCode.InternalServerError);
				else
				{
					UserList userListPackage = new UserList(userList);
					SConnectionController.SendPackage(userListPackage, clientAddress);
					return feedback;
				}
			}

			SConnectionController.SendPackage(feedback, clientAddress);
			return feedback;
		}

		/// <summary>
		/// Handle a RegistrationRequest
		/// </summary>
		/// <param name="request">RegistrationRequest to handle</param>
		/// <param name="clientAddress">address of the sender of the package</param>
		/// <returns>the feedback transmitted</returns>
		private static Feedback RegistrationRequest(RegistrationRequest request, string clientAddress)
		{
			Feedback feedback = null;

			if (!CheckRegistrationRequest(request))
				feedback = new Feedback(FeedbackCode.RegistrationDenied);
			else
			{
				switch (SDBController.TryAddClient(request))
				{
					case 0:
						feedback = new Feedback(FeedbackCode.RegistrationAccepted);
						break;
					case 1:
						feedback = new Feedback(FeedbackCode.InternalServerError);
						break;
					case 2:
						feedback = new Feedback(FeedbackCode.RegistrationDeniedEmailAlreadyUsed);
						break;
				}
			}

			SConnectionController.SendPackage(feedback, clientAddress);

			return feedback;
		}

		/// <summary>
		/// Checks a RegistrationRequest
		/// </summary>
		/// <param name="request">RegistrationRequest to check</param>
		/// <returns>Indicates if it was successful</returns>
		private static bool CheckRegistrationRequest(RegistrationRequest request)
		{
			string email = request.Email;
			string nickname = request.Nickname;
			string password = request.PasswordHash;
			DateTime date = request.Birthday;

			if (string.IsNullOrWhiteSpace(email))
				return false;
			if (string.IsNullOrWhiteSpace(nickname))
				return false;
			if (string.IsNullOrWhiteSpace(password))
				return false;
			if (!email.Contains('@') || !email.Contains('.'))
				return false;
			if (nickname.Length > 16 || nickname.Length < 4)
				return false;
			if (password.Length != 64)
				return false;


			if (DateConverter.GetAgeFromDate(date) < 12)
				return false;

			return true;
		}
	}
}
