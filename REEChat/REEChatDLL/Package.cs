using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
	public abstract class Package
	{
		/// <summary>
		/// Classified package type
		/// </summary>
		public PackageType Type { get; set; }

		/// <summary>
		/// Returns a byte array containing all user data.
		/// </summary>
		/// <returns>Byte array with all user data.</returns>
		public abstract byte[] UserData();

		/// <summary>
		/// Returns a byte array containing all information.
		/// </summary>
		/// <returns>Byte array with all information.</returns>
		public byte[] ToByteArray()
		{
			//Package: StartOfHeader + [Type] + StartOfText + [UserData] + EndOfText

			int i = 0;
			int id = ((int)Type);

			byte[] header = Encoding.UTF8.GetBytes(id.ToString());
			byte[] userData = UserData();

			byte[] finalBytes = new byte[3 + header.Length + userData.Length];

			//Start of Header [SOH]
			finalBytes[i] = PackageControl.StartOfHeader;
			i += 1;

			//Type
			Array.Copy(header, 0, finalBytes, i, header.Length);
			i += header.Length;

			//Start Of Text [STX]
			finalBytes[i] = PackageControl.StartOfText;
			i += 1;

			//UserData
			Array.Copy(userData, 0, finalBytes, i, userData.Length);
			i += userData.Length;

			//End Of Text [ETX]
			finalBytes[i] = PackageControl.EndOfText;
			i += 1;

			//return byte array
			return finalBytes;
		}
	}
}
