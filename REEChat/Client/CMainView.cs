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
	}
}
