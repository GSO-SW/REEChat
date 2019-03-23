using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
	public static class Encode
	{
		/// <summary>
		/// Hashs a string (used for passwords)
		/// </summary>
		/// <param name="password">string to hash</param>
		/// <returns>hashed string</returns>
		public static string GetHash(string password)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(password);
			var hashstring = new SHA256Managed();
			byte[] hash = hashstring.ComputeHash(bytes);
			string hashString = string.Empty;
			foreach (byte x in hash)
			{
				hashString += string.Format("{0:x2}", x);
			}
			return hashString;
		}
	}
}
