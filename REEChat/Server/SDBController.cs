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
		internal static bool TryClientUpdateLastIPAddress(string email, string address)
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
		internal static int TryClientAdd(RegistrationRequest request)
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
	}
}
