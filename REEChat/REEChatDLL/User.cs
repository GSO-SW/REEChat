using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
	public class User
	{
		public string Email { get; set; } = "";
		public string Nickname { get; set; } = "";
		public string PasswordHash { get; set; } = "";
		public DateTime Birthday { get; set; } = new DateTime();

		/// <summary>
		/// Creates a new instance of type user
		/// </summary>
		public User()
		{

		}

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

		public static bool TryParse(byte[] data, out User user)
		{
			user = null;

			if (data == null)
				return false;

			if (!Package.TrySplitByte(data, PackageControl.UnitSeperator, false, out byte[] email, out data))
				return false;
			if (!Package.TrySplitByte(data, PackageControl.UnitSeperator, false, out byte[] nickname, out data))
				return false;
			if (!Package.TrySplitByte(data, PackageControl.GroupSeperator, false, out byte[] birthday, out data))
				return false;
			if (data.Length != 0)
				return false;
			if (!DateTime.TryParse(Encoding.UTF8.GetString(birthday), out DateTime birthdayDate))
				return false;

			user = new User
			{
				Email = Encoding.UTF8.GetString(email),
				Nickname = Encoding.UTF8.GetString(nickname),
				Birthday = birthdayDate
			};

			return true;
		}

		/// <summary>
		/// Convert the object to a byte array
		/// </summary>
		/// <returns></returns>
		public byte[] ToByteArray()
		{
			List<byte> byteList = new List<byte>();

			byte[] emailByte = Encoding.UTF8.GetBytes(Email);
			byte[] nicknameByte = Encoding.UTF8.GetBytes(Nickname);
			byte[] birthdayByte = Encoding.UTF8.GetBytes(Birthday.ToShortDateString());

			byteList.AddRange(emailByte);
			byteList.Add(PackageControl.UnitSeperator);
			byteList.AddRange(nicknameByte);
			byteList.Add(PackageControl.UnitSeperator);
			byteList.AddRange(birthdayByte);
			byteList.Add(PackageControl.GroupSeperator);

			return byteList.ToArray();
		}

		public override string ToString()
		{
			return "[Email]: " + Email + " [Nickname]: " + Nickname + " [Birthday]: " + Birthday.ToShortDateString();
		}
	}
}
