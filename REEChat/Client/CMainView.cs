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
	public partial class CMainView : Form
	{
		/// <summary>
		/// Creates a new instance of type CMainView
		/// </summary>
		public CMainView()
		{
			InitializeComponent();

			CFormController.Main = this;
		}

		private void button1_Click(object sender, EventArgs e)
		{

		}
	}
}
