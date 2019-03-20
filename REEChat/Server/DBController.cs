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
	internal static class DBController
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
		/// Try to verify a user from the database by his email address and password.
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
	}
}
