using REEChatDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace Client
{
	internal partial class CMainView : Form
	{
		private delegate void SimpleDelegate();
		private delegate void SafeCallDelegate(bool value);
		private delegate void SafeCallDelegate2(string value);

		internal int Counter { get; set; }

		private bool waitForFeedback;

		public bool WaitForFeedback
		{
			get { return waitForFeedback; }
			set
			{
				waitForFeedback = value;
				SetEnable(!value);
			}
			
		}

		/// <summary>
		/// Creates a new instance of type CMainView
		/// </summary>
		public CMainView()
		{
			CFormController.MainView = this;

			InitializeComponent();			
			
			listMessange.DisplayMember = "DisplayMember";
			UpdateUser();
		}

		public void SetEnable(bool value)
		{
			if (buttonSend.InvokeRequired)
			{
				SafeCallDelegate safeCall = new SafeCallDelegate(SetEnable);
				Invoke(safeCall, new object[] { value });
			}
			else
			{
				buttonSend.Enabled = value;
				txtMessage.Enabled = value;
			}
		}

		public void SetText(string value)
		{
			if (txtMessage.InvokeRequired)
			{
				var d = new SafeCallDelegate2(SetText);
				Invoke(d, new object[] { value });
			}
			else
			{
				txtMessage.Text = value;
			}
		}

		public void UpdateUser()
		{
			if (InvokeRequired)
			{
				SimpleDelegate simpleDelegate = new SimpleDelegate(UpdateUser);
				Invoke(simpleDelegate);
			}
			else
			{
				listUser.DisplayMember = "Nickname";
				listUser.Items.Clear();
				listUser.Items.AddRange(CDataController.Users.ToArray());
			}
		}

		public void UpdateMessage()
		{
			if (InvokeRequired)
			{
				SimpleDelegate simpleDelegate = new SimpleDelegate(UpdateMessage);
				Invoke(simpleDelegate);
			}
			else
			{
				listMessange.Items.Clear();

				if (listUser.SelectedItem != null)
					if (listUser.SelectedItem is User user)
						foreach (Message message in CDataController.Messages)
						{
							if (message.Receiver == user.Email && message.Sender == CConnectionController.LoginUser.Email || message.Receiver == CConnectionController.LoginUser.Email && message.Sender == user.Email)
							{
								listMessange.Items.Add(message);
							}
						}
			}
		}

		private void UserSelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateMessage();
		}

		private void Send_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtMessage.Text))
				return;

			if (listUser.SelectedItem != null)
				if (listUser.SelectedItem is User receiver)
				{
					SendTextMessage sendTextMessage = new SendTextMessage(receiver.Email, txtMessage.Text);
					if (CConnectionController.TrySendPackage(sendTextMessage, CConnectionController.ServerAddress))
					{
						CDataController.Messages.Add(new Message(CConnectionController.LoginUser.Email, receiver.Email, DateTime.Now, txtMessage.Text));
						UpdateMessage();
						WaitForFeedback = true;
						timer.Enabled = true;
					}
					else
					{
						MessageBox.Show("Fehler beim Senden!");
					}
				}
		}

		private void Tick(object sender, EventArgs e)
		{
			Counter++;
			if (!WaitForFeedback)
			{
				timer.Enabled = false;
				Counter = 0;
			}
			if (Counter > 5)
			{
				timer.Enabled = false;
				Counter = 0;
				MessageBox.Show("Keine Antwort vom Server erhalten!");
				WaitForFeedback = false;
			}
		}

		private void CMainView_FormClosed(object sender, FormClosedEventArgs e)
		{
			CConnectionController.Stop();
		}
	}
}
