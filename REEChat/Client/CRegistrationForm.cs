using REEChatDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
	public partial class CRegistrationForm : Form
	{
		private int Counter { get; set; }

		private bool waitForFeedback;

		public bool WaitForFeedback
		{
			get { return waitForFeedback; }
			set
			{
				waitForFeedback = value;
				buttonRegister.Enabled = !value;
			}
		}


		public CRegistrationForm(string address)
		{
			InitializeComponent();

			ActiveControl = buttonRegister;

			if (!string.IsNullOrWhiteSpace(address))
				txtServerAddress.Text = address;
		}

		private void Register_Click(object sender, EventArgs e)
		{
			string address = txtServerAddress.TextWithoutWatermark;
			string email = txtEmail.TextWithoutWatermark;
			string nickname = txtNickname.TextWithoutWatermark;
			string birthday = txtBirthday.TextWithoutWatermark;
			string password = txtPassword.TextWithoutWatermark;
			string password2 = txtPassword2.TextWithoutWatermark;

			if (string.IsNullOrWhiteSpace(email))
			{
				MessageBox.Show("Sie müssen eine Email eingeben.");
				return;
			}
			if (string.IsNullOrWhiteSpace(nickname))
			{
				MessageBox.Show("Sie müssen einen Benutzername eingeben.");
				return;
			}
			if (string.IsNullOrWhiteSpace(birthday))
			{
				MessageBox.Show("Sie müssen ein Geburtsdatum eingeben.");
				return;
			}
			if (string.IsNullOrWhiteSpace(password))
			{
				MessageBox.Show("Sie müssen ein Passwort eingeben.");
				return;
			}
			if (string.IsNullOrWhiteSpace(password2))
			{
				MessageBox.Show("Sie müssen das Passwort bestätigen.");
				return;
			}

			if (password != password2)
			{
				MessageBox.Show("Die Passwörter stimmen nicht überein.");
				return;
			}
			if (!DateTime.TryParse(birthday, out DateTime date))
			{
				MessageBox.Show("Geben Sie ein gültiges Datum ein. \n   Tipp: dd.mm.yyyy");
				return;
			}
			if (!email.Contains('@') || !email.Contains('.'))
			{
				MessageBox.Show("Geben Sie eine gültige Email ein.");
				return;
			}

			if (nickname.Length > 16 || nickname.Length < 4)
			{
				MessageBox.Show("Der Benutzername muss mindestens 4 und maximal 16 Zeichen haben.");
				return;
			}
			if (password.Length < 8)
			{
				MessageBox.Show("Das Passwort muss mindestens 8 Zeichen lang sein.");
				return;
			}

			if (GetAgeFromDate(date) < 12)
			{
				MessageBox.Show("Sie müssen mindestens 12 Jahre alt sein!");
				return;
			}

			if (!IPAddress.TryParse(address, out IPAddress ipAddress))
			{
				MessageBox.Show("Ungültige IP Addresse.");
				return;
			}

			CConnectionController.ServerAddress = ipAddress.ToString();

			string hash = Encode.GetHash(password);

			RegistrationRequest registration = new RegistrationRequest(email, nickname, hash, date);

			if (!CConnectionController.TrySendPackage(registration, CConnectionController.ServerAddress))
			{
				WaitForFeedback = false;
				return;
			}

			timer.Enabled = true;
		}

		public static int GetAgeFromDate(DateTime birthday)
		{
			int years = DateTime.Now.Year - birthday.Year;
			birthday = birthday.AddYears(years);
			if (DateTime.Now.CompareTo(birthday) < 0) { years--; }
			return years;
		}

		private void Tick(object sender, EventArgs e)
		{
			Counter++;
			if (WaitForFeedback)
			{
				timer.Enabled = false;
				Counter = 0;
				WaitForFeedback = false;
			}
			if (Counter > 5)
			{
				timer.Enabled = false;
				Counter = 0;
				MessageBox.Show("Keine Antwort vom Server erhalten!");
				WaitForFeedback = false;
			}
		}
	}
}
