using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
	public class User
	{
		public string Email { get; set; }
		public string Nickname { get; set; }
		public string PasswordHash { get; set; }
		public DateTime Birthday { get; set; }

		public User(string email, string nickname, string passwordHash, DateTime birthday)
		{
			Email = email;
			Nickname = nickname;
			PasswordHash = passwordHash;
			Birthday = birthday;
		}

		public User(string email, string passwordHash)
		{
			Email = email;
			PasswordHash = passwordHash;
		}
	}
}
