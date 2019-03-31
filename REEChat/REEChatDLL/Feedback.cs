using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
	/// <summary>
	/// Feedback list
	/// </summary>
	public enum FeedbackCode
	{
		Default = 0,
		InternalServerError = 1,
		IncorrectPackage = 2,
		LoginDenied = 10,
		LoginAccepted = 11,
		RegistrationDenied = 20,
		RegistrationDeniedEmailAlreadyUsed = 21,
		RegistrationAccepted = 22,
		InvalidSession = 30,
		MessageSendSuccess = 40,
		MessageSendFailed = 41,
	}

	public class Feedback : Package
	{
		/// <summary>
		/// Content of the feedback
		/// </summary>
		public FeedbackCode FeedbackCode { get; set; }

		/// <summary>
		/// Creates a new instance of the type Feedback
		/// </summary>
		/// <param name="feedbackCode">Content of the feedback</param>
		public Feedback(FeedbackCode feedbackCode)
		{
			Type = PackageType.Feedback;
			FeedbackCode = feedbackCode;
		}

		/// <summary>
		/// Converts a byte array to a Feedback Package and returns a value indicating whether the conversion was successful.
		/// </summary>
		/// <param name="data">Package byte array</param>
		/// <param name="feedback">Output Feedback package</param>
		/// <returns>Returns whether the conversion was successful.</returns>
		internal static bool TryParse(byte[] data, out Feedback feedback)
		{
			feedback = null;

			if (!int.TryParse(Encoding.UTF8.GetString(data), out int id))
				return false;
			if (!Enum.IsDefined(typeof(FeedbackCode), id))
				return false;

			feedback = new Feedback((FeedbackCode)id);
			return true;
		}

		/// <summary>
		/// Convert the contect to a byte array
		/// </summary>
		public override byte[] UserData()
		{
			return Encoding.UTF8.GetBytes(((int)FeedbackCode).ToString());
		}

		public override string ToString()
		{
			return FeedbackCode.ToString();
		}
	}
}
