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

		/// <summary>
		/// Converts a byte array to a LoginRequest Package and returns a value indicating whether the conversion was successful.
		/// </summary>
		/// <param name="data">Package byte array</param>
		/// <param name="loginRequest">Output LoginRequest package</param>
		/// <returns>Returns whether the conversion was successful.</returns>
		public static bool TryParse(byte[] data, out LoginRequest loginRequest)
		{
			loginRequest = null;

			byte[] emailByte, passwordByte;

			for (int i = 0; i < data.Length; i++)
			{
				if (data[i] == PackageControl.UnitSeperator)
				{
					emailByte = new byte[i];
					Array.Copy(data, 0, emailByte, 0, emailByte.Length);

					passwordByte = new byte[data.Length - i - 1];
					Array.Copy(data, i + 1, passwordByte, 0, passwordByte.Length);

					string email = Encoding.UTF8.GetString(emailByte);
					string password = Encoding.UTF8.GetString(passwordByte);

					loginRequest = new LoginRequest(email, password);
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Convert the contect to a byte array
		/// </summary>
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
