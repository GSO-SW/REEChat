using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using REEChatDLL;

namespace Server
{
	internal static class SDBController
	{
		private static readonly string connectionString = @"host=localhost;user=root;database=reechat";

		/// <summary>
		/// Test, if the connection to the database is available.
		/// </summary>
		/// <returns>Return true, if the connection is available, otherwise false.</returns>
		internal static bool ConnectionAvailable()
		{
			bool result;
			using (MySqlConnection con = new MySqlConnection(connectionString))
			{
				try
				{
					con.Open();
					result = true;
				}
				catch (Exception)
				{
					result = false;
				}
				finally
				{
					con.Close();
				}
			}
			return result;
		}

		/// <summary>
		/// Tries to verify a user from the database by his email address and password.
		/// </summary>
		/// <param name="email">The email from the user who is requested</param>
		/// <param name="passwordHash">The password from the user who is requested</param>
		/// <param name="result">True if email and password matches, otherwise false.</param>
		/// <returns>Returns false if there was a problem with the database connection, otherwise true.</returns>
		internal static bool TryVerifyUser(string email, string passwordHash, out bool result)
		{
			result = false;
			DataTable t = new DataTable();

			using (MySqlConnection con = new MySqlConnection(connectionString))
			{
				try
				{
					con.Open();
					//SELECT * FROM `user` WHERE Email = 'replayme87@gmail.com' AND Password = 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f'
					using (MySqlDataAdapter a = new MySqlDataAdapter("SELECT * FROM `user` WHERE `Email` = @email AND `Password` = @password", con))
					{
						a.SelectCommand.Parameters.AddWithValue("@email", email);
						a.SelectCommand.Parameters.AddWithValue("@password", passwordHash);
						a.Fill(t);
					}
				}
				catch (Exception)
				{
					return false;
				}
				finally
				{
					con.Close();
				}
			}

			if (t.Rows.Count == 1)
				result = true;

			return true;
		}

		/// <summary>
		/// Tries to update the last ip address of a user.
		/// </summary>
		/// <param name="email">The email of the user</param>
		/// <param name="address">The ip address of the user</param>
		/// <returns>Returns false if there was a problem with the database connection, otherwise true.</returns>
		internal static bool TryUpdateClientLastIPAddress(string email, string address)
		{
			using (MySqlConnection con = new MySqlConnection(connectionString))
			{
				try
				{
					con.Open();
					using (MySqlCommand command = new MySqlCommand("UPDATE `user` SET `LastIPAddress`= null WHERE `LastIPAddress`=@LastIPAddress", con))
					{
						command.Parameters.AddWithValue("LastIPAddress", address);

						command.ExecuteNonQuery();
					}
					using (MySqlCommand command = new MySqlCommand("UPDATE `user` SET `LastIPAddress`=@LastIPAddress WHERE `Email`=@Email", con))
					{
						command.Parameters.AddWithValue("LastIPAddress", address);
						command.Parameters.AddWithValue("Email", email);

						command.ExecuteNonQuery();
					}
				}
				catch (Exception)
				{
					return false;
				}
				finally
				{
					con.Close();
				}
				return true;
			}
		}

		/// <summary>
		/// Tries to add a user to the DB
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Returns false if there was a problem with the database connection, otherwise true.</returns>
		internal static int TryAddClient(RegistrationRequest request)
		{
			using (MySqlConnection con = new MySqlConnection(connectionString))
			{
				try
				{
					con.Open();
					using (MySqlCommand command = new MySqlCommand("INSERT INTO `user` (`Nickname`, `Email`, `Password`, `Birthday`) VALUES (@Nickname, @Email, @Password, @Birthday);", con))
					{
						command.Parameters.AddWithValue("Nickname", request.Nickname);
						command.Parameters.AddWithValue("Email", request.Email);
						command.Parameters.AddWithValue("Password", request.PasswordHash);
						command.Parameters.AddWithValue("Birthday", request.Birthday.ToString("yyyy-MM-dd"));

						command.ExecuteNonQuery();
					}
				}
				catch (Exception e)
				{
					if (((MySqlException)e).Number == 1062)
						return 2;
					return 1;
				}
				finally
				{
					con.Close();
				}
				return 0;
			}
		}

		/// <summary>
		/// Tries to get a list of all users
		/// </summary>
		/// <param name="userList">output list</param>
		/// <returns>Returns false if there was a problem with the database connection, otherwise true.</returns>
		internal static bool TryGetClientList(out List<User> userList)
		{
			userList = null;
			DataTable table = new DataTable();

			using (MySqlConnection con = new MySqlConnection(connectionString))
			{
				try
				{
					con.Open();

					using (MySqlDataAdapter a = new MySqlDataAdapter("SELECT Email, Nickname, Birthday FROM `user`", con))
					{
						a.Fill(table);
					}
				}
				catch (Exception)
				{
					return false;
				}
				finally
				{
					con.Close();
				}
			}

			List<User> userListTemp = new List<User>();

			foreach (DataRow row in table.Rows)
			{
				if (!TryGetUserFromDataRow(row, out User user))
					return false;
				userListTemp.Add(user);
			}
			userList = userListTemp;
			return true;
		}

		private static bool TryGetUserFromDataRow(DataRow row, out User user)
		{
			user = null;
			try
			{
				user = new User(row.Field<string>("Email"), row.Field<string>("Nickname"), row.Field<DateTime>("Birthday"));
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Tries to get a user by his ipaddress
		/// </summary>
		/// <param name="ipaddress">ipaddress of the user</param>
		/// <param name="user">out user</param>
		/// <returns>Returns false if there was a problem with the database connection, otherwise true.</returns>
		internal static bool TryGetUser(string ipaddress, out User user)
		{
			user = null;
			DataTable table = new DataTable();

			using (MySqlConnection con = new MySqlConnection(connectionString))
			{
				try
				{
					con.Open();

					using (MySqlDataAdapter a = new MySqlDataAdapter("SELECT Email, Nickname, Birthday FROM `user` WHERE `LastIPAddress` = @LastIPAddress", con))
					{
						a.SelectCommand.Parameters.AddWithValue("@LastIPAddress", ipaddress);

						a.Fill(table);
					}
				}
				catch (Exception)
				{
					return false;
				}
				finally
				{
					con.Close();
				}
			}

			if (!TryGetUserFromDataRow(table.Rows[0], out User tUser))
				return false;

			user = tUser;
			return true;
		}

		/// <summary>
		/// Tries to add a message to DB
		/// </summary>
		/// <param name="senderEmail">email of the sender</param>
		/// <param name="receiverEmail">email of the receiver</param>
		/// <param name="text">text of the message</param>
		/// <param name="sendTime">send time of the message</param>
		/// <returns>Returns false if there was a problem with the database connection, otherwise true.</returns>
		internal static bool TryAddMessage(string senderEmail, string receiverEmail, string text, DateTime? sendTime)
		{
			if (!TryGetUserID(senderEmail, out int senderID))
				return false;
			if (!TryGetUserID(receiverEmail, out int receiverID))
				return false;

			using (MySqlConnection con = new MySqlConnection(connectionString))
			{
				try
				{
					con.Open();
					using (MySqlCommand command = new MySqlCommand("INSERT INTO `message` (`Sender_ID`, `Receiver_ID`, `Text`, `Send_Time`) VALUES (@Sender, @Receiver, @Text, @Send_Time);", con))
					{
						command.Parameters.AddWithValue("Sender", senderID);
						command.Parameters.AddWithValue("Receiver", receiverEmail);
						command.Parameters.AddWithValue("Text", text);

						if (sendTime == null)
							command.Parameters.AddWithValue("Send_Time", null);
						else
							command.Parameters.AddWithValue("Send_Time", sendTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));

						command.ExecuteNonQuery();
					}
				}
				catch (Exception)
				{
					return false;
				}
				finally
				{
					con.Close();
				}
				return true;
			}
		}

		/// <summary>
		/// Tries to get the id of a user
		/// </summary>
		/// <param name="email">email of the user</param>
		/// <param name="id">out id</param>
		/// <returns>Returns false if there was a problem with the database connection, otherwise true.</returns>
		internal static bool TryGetUserID(string email, out int id)
		{
			id = 0;
			DataTable table = new DataTable();

			using (MySqlConnection con = new MySqlConnection(connectionString))
			{
				try
				{
					con.Open();

					using (MySqlDataAdapter a = new MySqlDataAdapter("SELECT U_ID FROM `user` WHERE `Email` = @email", con))
					{
						a.SelectCommand.Parameters.AddWithValue("@email", email);

						a.Fill(table);
					}
				}
				catch (Exception)
				{
					return false;
				}
				finally
				{
					con.Close();
				}
			}

			if (table.Rows != null)
				id = table.Rows[0].Field<int>("U_ID");
			return true;
		}

		internal static bool TryGetIPAddressByEmail(string email, out string ipAddress)
		{
			ipAddress = null;
			DataTable table = new DataTable();

			using (MySqlConnection con = new MySqlConnection(connectionString))
			{
				try
				{
					con.Open();

					using (MySqlDataAdapter a = new MySqlDataAdapter("SELECT LastIPAddress FROM `user` WHERE `Email` = @email", con))
					{
						a.SelectCommand.Parameters.AddWithValue("@email", email);

						a.Fill(table);
					}
				}
				catch (Exception)
				{
					return false;
				}
				finally
				{
					con.Close();
				}
			}

			if (table.Rows != null)
				ipAddress = table.Rows[0].Field<string>("LastIPAddress");
			return true;
		}
	}
}
