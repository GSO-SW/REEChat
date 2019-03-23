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
		TcpClient client = null;

		string serverAddress = ConfigurationManager.AppSettings["serverAddress"];

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
			LoginRequest loginRequest = new LoginRequest(textBox1.Text, textBox2.Text);
			byte[] buffer = loginRequest.ToByteArray();
			client = new TcpClient(serverAddress, ConnectionConfig.serverPort);
			NetworkStream stream = client.GetStream();
			stream.Write(buffer, 0, buffer.Length);
			stream.Close();
			client.Close();
		}
	}
}
