using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
	public enum FeedbackCode
	{
		LoginDenied = 10,
		LoginAccepted = 11
	}

	public class Feedback : Package
	{
		public int FeedbackCode { get; set; }

		public Feedback(int feedbackCode)
		{
			FeedbackCode = feedbackCode;
		}

		public override byte[] UserData()
		{
			return Encoding.UTF8.GetBytes(((int)FeedbackCode).ToString());
		}
	}
}
