using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
	public class UserList : Package
	{
		public List<User> List { get; set; }

		public UserList(List<User> list)
		{
			Type = PackageType.UserList;
			List = list;
		}

		public UserList()
		{
			Type = PackageType.UserList;
			List = new List<User>();
		}

		internal static bool TryParse(byte[] data, out UserList userList)
		{
			userList = null;
			List<User> clientListTemp = new List<User>();

			while (TrySplitByte(data, PackageControl.GroupSeperator, true, out byte[] userByte, out data))
			{
				if (!User.TryParse(userByte, out User user))
					return false;
				clientListTemp.Add(user);

				if (data.Length == 0 && clientListTemp.Count > 0)
				{
					userList = new UserList(clientListTemp);
					return true;
				}
			}

			return false;
		}

		public override byte[] UserData()
		{
			List<byte> array = new List<byte>();

			foreach (User user in List)
			{
				array.AddRange(user.ToByteArray());
			}

			return array.ToArray();
		}
	}
}
