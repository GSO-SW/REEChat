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

		/// <summary>
		/// Creates a new instance of type user
		/// </summary>
		/// <param name="email">email of the user</param>
		/// <param name="nickname">nickname of the user</param>
		/// <param name="birthday">birthday of the user</param>
		public User(string email, string nickname, DateTime birthday)
		{
			Email = email;
			Nickname = nickname;
			Birthday = birthday;
		}

		/// <summary>
		/// Creates a new instance of type user
		/// </summary>
		/// <param name="email">email of the user</param>
		/// <param name="passwordHash">password hash of the user</param>
		public User(string email, string passwordHash)
		{
			Email = email;
			PasswordHash = passwordHash;
		}
	}
}
