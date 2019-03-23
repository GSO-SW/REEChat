using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
	/// <summary>
	/// All package types
	/// </summary>
	public enum PackageType
	{
		RegistrationRequest = 10,
		LoginRequest = 11,
		Online = 12,
		Offline = 13,
		UserList = 14,
		UserAdd = 15,
		UserRemove = 16,
		TextMessageSend = 17,
		TextMessageReceive = 18,
		Ping = 19,
		Feedback = 20
	}

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


		/// <summary>
		/// Converts a byte array to a Package and returns a code.
		/// 0 - Successful
		/// 1 - Wrong package structur
		/// 2 - Wrong package type
		/// </summary>
		/// <param name="input"></param>
		/// <param name="package"></param>
		/// <returns>0 - Successful, 1 - Wrong package structur, 2 - Wrong package type</returns>
		public static int TryParse(byte[] input, out Package package)
		{
			package = null;

			if (!TryConvertInputByte(input, out PackageType packageType, out byte[] userData))
				return 1;
			switch (packageType)
			{
				case PackageType.RegistrationRequest:
					if (RegistrationRequest.TryParse(userData, out RegistrationRequest request))
						package = request;
					break;
				case PackageType.LoginRequest:
					if(LoginRequest.TryParse(userData, out LoginRequest login))
						package = login;
					break;
				case PackageType.Online:
					break;
				case PackageType.Offline:
					break;
				case PackageType.UserList:
					break;
				case PackageType.UserAdd:
					break;
				case PackageType.UserRemove:
					break;
				case PackageType.TextMessageSend:
					break;
				case PackageType.TextMessageReceive:
					break;
				case PackageType.Ping:
					break;
				case PackageType.Feedback:
					if (Feedback.TryParse(userData, out Feedback feedback))
						package = feedback;
					break;
				default:
					break;
			}
			if (package == null)
				return 2;


			return 0;
		}

		private static bool TryConvertInputByte(byte[] input, out PackageType packageType, out byte[] userData)
		{
			packageType = 0;
			userData = null;

			if (input.Length <= 3)
				return false;
			if (input[0] != PackageControl.StartOfHeader && input[input.Length - 1] != PackageControl.EndOfText)
				return false;

			byte[] typeByte;

			for (int i = 1; i < input.Length; i++)
			{
				if (input[i] == PackageControl.StartOfText)
				{
					typeByte = new byte[i - 1];
					Array.Copy(input, 1, typeByte, 0, typeByte.Length);

					userData = new byte[input.Length - i - 2];
					Array.Copy(input, i + 1, userData, 0, userData.Length);

					if (!int.TryParse(Encoding.UTF8.GetString(typeByte), out int id))
						return false;
					packageType = (PackageType)id;
					return true;
				}
			}
			return false;
		}
	}
}
