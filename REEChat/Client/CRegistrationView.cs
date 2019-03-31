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
	public partial class CRegistrationView : Form
	{
		private delegate void SafeCallDelegate(bool value);
		private int Counter { get; set; }

		private bool waitForFeedback;
		
		/// <summary>
		/// Creates a new instance of type CRegistrationForm
		/// </summary>
		/// <param name="address"></param>
		internal CRegistrationView(string address)
		{
			InitializeComponent();

			CFormController.RegistrationView = this;

			ActiveControl = buttonRegister;

			if (!string.IsNullOrWhiteSpace(address))
				txtServerAddress.Text = address;
		}

		/// <summary>
		/// State of wait for feedback
		/// </summary>
		internal bool WaitForFeedback
		{
			get { return waitForFeedback; }
			set
			{
				waitForFeedback = value;
				SetEnable(!value);
			}
		}

		public void SetEnable(bool value)
		{
			if (buttonRegister.InvokeRequired)
			{
				var d = new SafeCallDelegate(SetEnable);
				Invoke(d, new object[] { value });
			}
			else
			{
				buttonRegister.Enabled = value;
				txtBirthday.Enabled = value;
				txtEmail.Enabled = value;
				txtNickname.Enabled = value;
				txtPassword.Enabled = value;
				txtPassword2.Enabled = value;
				txtServerAddress.Enabled = value;
			}
		}

		private void Register_Click(object sender, EventArgs e)
		{
			WaitForFeedback = true;

			string address = txtServerAddress.TextWithoutWatermark;
			string email = txtEmail.TextWithoutWatermark;
			string nickname = txtNickname.TextWithoutWatermark;
			string birthday = txtBirthday.TextWithoutWatermark;
			string password = txtPassword.TextWithoutWatermark;
			string password2 = txtPassword2.TextWithoutWatermark;

			if (string.IsNullOrWhiteSpace(email))
			{
				MessageBox.Show("Sie müssen eine Email eingeben.");
				WaitForFeedback = false;
				return;
			}
			if (string.IsNullOrWhiteSpace(nickname))
			{
				MessageBox.Show("Sie müssen einen Benutzername eingeben.");
				WaitForFeedback = false;
				return;
			}
			if (string.IsNullOrWhiteSpace(birthday))
			{
				MessageBox.Show("Sie müssen ein Geburtsdatum eingeben.");
				WaitForFeedback = false;
				return;
			}
			if (string.IsNullOrWhiteSpace(password))
			{
				MessageBox.Show("Sie müssen ein Passwort eingeben.");
				WaitForFeedback = false;
				return;
			}
			if (string.IsNullOrWhiteSpace(password2))
			{
				MessageBox.Show("Sie müssen das Passwort bestätigen.");
				WaitForFeedback = false;
				return;
			}

			if (password != password2)
			{
				MessageBox.Show("Die Passwörter stimmen nicht überein.");
				WaitForFeedback = false;
				return;
			}
			if (!DateTime.TryParse(birthday, out DateTime date))
			{
				MessageBox.Show("Geben Sie ein gültiges Datum ein. \n   Tipp: dd.mm.yyyy");
				WaitForFeedback = false;
				return;
			}
			if (!email.Contains('@') || !email.Contains('.'))
			{
				MessageBox.Show("Geben Sie eine gültige Email ein.");
				WaitForFeedback = false;
				return;
			}

			if (nickname.Length > 16 || nickname.Length < 4)
			{
				MessageBox.Show("Der Benutzername muss mindestens 4 und maximal 16 Zeichen haben.");
				WaitForFeedback = false;
				return;
			}
			if (password.Length < 8)
			{
				MessageBox.Show("Das Passwort muss mindestens 8 Zeichen lang sein.");
				WaitForFeedback = false;
				return;
			}

			if (DateConverter.GetAgeFromDate(date) < 12)
			{
				MessageBox.Show("Sie müssen mindestens 12 Jahre alt sein!");
				WaitForFeedback = false;
				return;
			}

			if (!IPAddress.TryParse(address, out IPAddress ipAddress))
			{
				MessageBox.Show("Ungültige IP Addresse.");
				WaitForFeedback = false;
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

		private void Tick(object sender, EventArgs e)
		{
			Counter++;
			if (!WaitForFeedback)
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

		private void CRegistrationForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			CFormController.RegistrationView = null;
		}
	}
}
