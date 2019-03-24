using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
	public class RegistrationRequest : Package
	{
		public string Email { get; set; }
		public string Nickname { get; set; }
		public string PasswordHash { get; set; }
		public DateTime Birthday { get; set; }

		public RegistrationRequest(string email, string nickname, string passwordHash, DateTime birthday)
		{
			Type = PackageType.RegistrationRequest;
			Email = email;
			Nickname = nickname;
			PasswordHash = passwordHash;
			Birthday = birthday;
		}

		internal static bool TryParse(byte[] data, out RegistrationRequest request)
		{
			request = null;
			byte[] emailByte = null;
			byte[] nicknameByte = null;
			byte[] passwordByte = null;
			byte[] birthdayByte = null;

			for (int i = 0; i < data.Length; i++)
			{
				if (data[i] == PackageControl.UnitSeperator)
				{
					if (emailByte == null)
					{
						emailByte = new byte[i];
						Array.Copy(data, 0, emailByte, 0, emailByte.Length);
					}
					else if (nicknameByte == null)
					{
						nicknameByte = new byte[i - 1 - emailByte.Length];
						Array.Copy(data, i - nicknameByte.Length, nicknameByte, 0, nicknameByte.Length);
					}
					else if (passwordByte == null)
					{
						passwordByte = new byte[i - 2 - emailByte.Length - nicknameByte.Length];
						Array.Copy(data, i - passwordByte.Length, passwordByte, 0, passwordByte.Length);

						birthdayByte = new byte[data.Length - i - 1];
						Array.Copy(data, i + 1, birthdayByte, 0, birthdayByte.Length);
					}
					else
					{
						return false;
					}
				}
			}

			string email = Encoding.UTF8.GetString(emailByte);
			string nickname = Encoding.UTF8.GetString(nicknameByte);
			string password = Encoding.UTF8.GetString(passwordByte);
			string birthday = Encoding.UTF8.GetString(birthdayByte);

			if (!DateTime.TryParse(birthday, out DateTime date))
				return false;

			request = new RegistrationRequest(email, nickname, password, date);
			return true;
		}

		public override byte[] UserData()
		{
			int i = 0;
			//[Email] + UnitSeperator + [Nickname] + UnitSeperator + [PasswordHash] + UnitSeperator + [Birthday]

			byte[] email = Encoding.UTF8.GetBytes(Email);
			byte[] nickname = Encoding.UTF8.GetBytes(Nickname);
			byte[] password = Encoding.UTF8.GetBytes(PasswordHash);
			byte[] birthday = Encoding.UTF8.GetBytes(Birthday.ToShortDateString());

			byte[] userData = new byte[3 + email.Length + nickname.Length + password.Length + birthday.Length];

			//Email
			Array.Copy(email, 0, userData, i, email.Length);
			i += email.Length;

			//Unit Seperator
			userData[i] = PackageControl.UnitSeperator;
			i += 1;

			//Nickname
			Array.Copy(nickname, 0, userData, i, nickname.Length);
			i += nickname.Length;

			//Unit Seperator
			userData[i] = PackageControl.UnitSeperator;
			i += 1;

			//PasswordHash
			Array.Copy(password, 0, userData, i, password.Length);
			i += password.Length;

			//Unit Seperator
			userData[i] = PackageControl.UnitSeperator;
			i += 1;

			//Birthday
			Array.Copy(birthday, 0, userData, i, birthday.Length);


			return userData;
		}
	}
}
