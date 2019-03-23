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

		private bool waitForFeedback;

		/// <summary>
		/// Creates a new instance of type CLoginView
		/// </summary>
		public CLoginView()
		{
			InitializeComponent();

			WaitForFeedback = false;
			CFormController.LoginView = this;

			ActiveControl = buttonConnect;
			txtAddress.Text = ConfigurationManager.AppSettings["serverAddress"];
		}
		
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
				WaitForFeedback = true;

				string address = txtAddress.TextWithoutWatermark;
				string email = txtEmail.TextWithoutWatermark;
				string password = txtPassword.TextWithoutWatermark;

				if (!IPAddress.TryParse(address, out IPAddress ipAddress))
				{
					MessageBox.Show("Ungültige IP Addresse.");
					WaitForFeedback = false;
					return;
				}

				if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
				{
					MessageBox.Show("Email und Passwort müssen ausgefühlt sein!");
					WaitForFeedback = false;
					return;
				}

				CConnectionController.ServerAddress = txtAddress.TextWithoutWatermark;


				LoginRequest login = new LoginRequest(email, Encode.GetHash(password));

				if (!CConnectionController.TrySendPackage(login, CConnectionController.ServerAddress))
				{
					WaitForFeedback = false;
					return;
				}

				timer.Enabled = true;
			}
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
			if(Counter > 5)
			{
				timer.Enabled = false;
				Counter = 0;
				MessageBox.Show("Keine Antwort vom Server erhalten!");
				WaitForFeedback = false;
			}
		}

		private void Register_Click(object sender, EventArgs e)
		{
			new CRegistrationForm(txtAddress.TextWithoutWatermark).ShowDialog();
		}
	}
}
