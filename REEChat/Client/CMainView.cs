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
		/// <summary>
		/// Creates a new instance of type CMainView
		/// </summary>
		public CMainView()
		{
			InitializeComponent();

			CFormController.MainView = this;

            listMessange.DisplayMember = "DisplayMember";
			UpdateListBox();
		}

		public void UpdateListBox()
		{
			userListbox.DisplayMember = "Nickname";
			userListbox.Items.Clear();
			userListbox.Items.AddRange(CDataController.Users.ToArray());
		}

		private void UserSelectedIndexChanged(object sender, EventArgs e)
		{

		}

        private void Send_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessage.Text))
                return;

            if(userListbox.SelectedItem != null)
                if (userListbox.SelectedItem is User receiver)
                {
                    SendTextMessage sendTextMessage = new SendTextMessage(receiver.Email, txtMessage.Text);
                    if (CConnectionController.TrySendPackage(sendTextMessage, CConnectionController.ServerAddress))
                    {
                        Message message = new Message(CConnectionController.LoginUser, receiver, DateTime.Now, txtMessage.Text);
                        txtMessage.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Fehler beim Senden!");
                    }
                }
        }
    }
}
