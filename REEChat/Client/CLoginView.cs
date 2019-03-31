using REEChatDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Client
{
	public delegate void CloseDelagate();

	public partial class CLoginView : Form
	{
		internal int Counter { get; set; }

		private bool waitForFeedback = false;

		/// <summary>
		/// Creates a new instance of type CLoginView
		/// </summary>
		public CLoginView()
		{
			InitializeComponent();

			CFormController.LoginView = this;

			ActiveControl = buttonConnect;

			txtAddress.Text = ConfigurationManager.AppSettings["serverAddress"];
		}
		
		/// <summary>
		/// State of wait for feedback
		/// </summary>
		public bool WaitForFeedback
		{
			get { return waitForFeedback; }
			set
			{
				waitForFeedback = value;
				buttonConnect.Enabled = !value;
				buttonRegister.Enabled = !value;
				txtAddress.Enabled = !value;
				txtEmail.Enabled = !value;
				txtPassword.Enabled = !value;
			}
		}

		private void Connect_Click(object sender, EventArgs e)
		{
			if (!WaitForFeedback)
			{
				string address = txtAddress.TextWithoutWatermark;
				string email = txtEmail.TextWithoutWatermark;
				string password = txtPassword.TextWithoutWatermark;

				if (!IPAddress.TryParse(address, out IPAddress ipAddress))
				{
					MessageBox.Show("Ungültige IP Addresse.");
					return;
				}

				if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
				{
					MessageBox.Show("Email und Passwort müssen ausgefühlt sein!");
					return;
				}

				CConnectionController.ServerAddress = txtAddress.TextWithoutWatermark;


				LoginRequest login = new LoginRequest(email, Encode.GetHash(password));

                CConnectionController.LoginUser = new User(login.Email, login.PasswordHash);

				if (!CConnectionController.TrySendPackage(login, CConnectionController.ServerAddress))
				{
					return;
				}

				WaitForFeedback = true;
				timer.Enabled = true;
			}
		}

		private void Tick(object sender, EventArgs e)
		{
			Counter++;

			if (!WaitForFeedback)
			{
				WaitForFeedback = false;
				timer.Enabled = false;
				Counter = 0;
			}

			if (Counter > 5)
			{
				WaitForFeedback = false;
				timer.Enabled = false;
				Counter = 0;
				MessageBox.Show("Keine Antwort vom Server erhalten!");
			}
		}

		private void Register_Click(object sender, EventArgs e)
		{
			new CRegistrationView(txtAddress.TextWithoutWatermark).ShowDialog();
		}
	}
}
