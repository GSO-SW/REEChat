namespace Client
{
	partial class CMainView
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.listUser = new System.Windows.Forms.ListBox();
			this.listMessange = new System.Windows.Forms.ListBox();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.buttonSend = new System.Windows.Forms.Button();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// listUser
			// 
			this.listUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listUser.Dock = System.Windows.Forms.DockStyle.Left;
			this.listUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listUser.FormattingEnabled = true;
			this.listUser.ItemHeight = 20;
			this.listUser.Location = new System.Drawing.Point(0, 0);
			this.listUser.Name = "listUser";
			this.listUser.Size = new System.Drawing.Size(238, 531);
			this.listUser.TabIndex = 0;
			this.listUser.SelectedIndexChanged += new System.EventHandler(this.UserSelectedIndexChanged);
			// 
			// listMessange
			// 
			this.listMessange.Dock = System.Windows.Forms.DockStyle.Top;
			this.listMessange.Enabled = false;
			this.listMessange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listMessange.FormattingEnabled = true;
			this.listMessange.ItemHeight = 20;
			this.listMessange.Location = new System.Drawing.Point(238, 0);
			this.listMessange.Name = "listMessange";
			this.listMessange.Size = new System.Drawing.Size(694, 504);
			this.listMessange.TabIndex = 1;
			// 
			// txtMessage
			// 
			this.txtMessage.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMessage.Location = new System.Drawing.Point(238, 504);
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.Size = new System.Drawing.Size(599, 26);
			this.txtMessage.TabIndex = 2;
			// 
			// buttonSend
			// 
			this.buttonSend.Dock = System.Windows.Forms.DockStyle.Right;
			this.buttonSend.Location = new System.Drawing.Point(837, 504);
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.Size = new System.Drawing.Size(95, 27);
			this.buttonSend.TabIndex = 3;
			this.buttonSend.Text = "Send";
			this.buttonSend.UseVisualStyleBackColor = true;
			this.buttonSend.Click += new System.EventHandler(this.Send_Click);
			// 
			// timer
			// 
			this.timer.Interval = 1000;
			this.timer.Tick += new System.EventHandler(this.Tick);
			// 
			// CMainView
			// 
			this.AcceptButton = this.buttonSend;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(932, 531);
			this.Controls.Add(this.txtMessage);
			this.Controls.Add(this.buttonSend);
			this.Controls.Add(this.listMessange);
			this.Controls.Add(this.listUser);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CMainView";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "REEChat";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CMainView_FormClosed);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listUser;
		private System.Windows.Forms.ListBox listMessange;
		private System.Windows.Forms.Button buttonSend;
		private System.Windows.Forms.Timer timer;
		public System.Windows.Forms.TextBox txtMessage;
	}
}

