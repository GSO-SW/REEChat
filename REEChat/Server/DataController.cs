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
	internal static class DataController
	{
		/// <summary>
		/// Handle a LoginRequest package 
		/// </summary>
		/// <param name="loginRequest">LoginRequest to handle</param>
		/// <param name="clientAddress">address of the sender of the package</param>
		/// <returns>the feedback transmitted</returns>
		public static Feedback LoginRequest(LoginRequest loginRequest, string clientAddress)
		{
			Feedback feedback;

			if (!DBController.TryVerifyUser(loginRequest.Email, loginRequest.PasswordHash, out bool result))
			{
				feedback = new Feedback(FeedbackCode.InternalServerError);
			}
			else
			{
				if (result)
					feedback = new Feedback(FeedbackCode.LoginAccepted);
				else
					feedback = new Feedback(FeedbackCode.LoginDenied);
			}

			SendController.SendPackage((Package)feedback, clientAddress);

			return feedback;
		}
	}
}
