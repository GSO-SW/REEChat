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
		LoginError = 12
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
			FeedbackCode = feedbackCode;
		}

		/// <summary>
		/// Convert the contect to a byte array
		/// </summary>
		public override byte[] UserData()
		{
			return Encoding.UTF8.GetBytes(((int)FeedbackCode).ToString());
		}
	}
}
