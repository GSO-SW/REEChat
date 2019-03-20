using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
	public class LoginRequest : Package
	{
		/// <summary>
		/// Login email
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// Login password hash
		/// </summary>
		public string PasswordHash { get; set; }

		/// <summary>
		/// Creates a instance of type LoginRequest
		/// </summary>
		/// <param name="email">Login email</param>
		/// <param name="passwordHash">Login password hash</param>
		public LoginRequest(string email, string passwordHash)
		{
			Type = PackageType.LoginRequest;
			Email = email;
			PasswordHash = passwordHash;
		}

		public override byte[] UserData()
		{
			int i = 0;
			//[Email] + UnitSeperator + [PasswordHash]
			byte[] email = Encoding.UTF8.GetBytes(Email);
			byte[] password = Encoding.UTF8.GetBytes(PasswordHash);
			byte[] userData = new byte[1 + email.Length + password.Length];

			//Email
			Array.Copy(email, 0, userData, i, email.Length);
			i += email.Length;

			//Unit Seperator
			userData[i] = PackageControl.UnitSeperator;
			i += 1;

			//PasswordHash
			Array.Copy(password, 0, userData, i, password.Length);


			return userData;
		}
	}
}
