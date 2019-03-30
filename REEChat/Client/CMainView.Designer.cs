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
            this.userListbox = new System.Windows.Forms.ListBox();
            this.listMessange = new System.Windows.Forms.ListBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userListbox
            // 
            this.userListbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userListbox.Dock = System.Windows.Forms.DockStyle.Left;
            this.userListbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userListbox.FormattingEnabled = true;
            this.userListbox.ItemHeight = 25;
            this.userListbox.Location = new System.Drawing.Point(0, 0);
            this.userListbox.Margin = new System.Windows.Forms.Padding(4);
            this.userListbox.Name = "userListbox";
            this.userListbox.Size = new System.Drawing.Size(317, 677);
            this.userListbox.TabIndex = 0;
            this.userListbox.SelectedIndexChanged += new System.EventHandler(this.UserSelectedIndexChanged);
            // 
            // listMessange
            // 
            this.listMessange.Dock = System.Windows.Forms.DockStyle.Top;
            this.listMessange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listMessange.FormattingEnabled = true;
            this.listMessange.ItemHeight = 25;
            this.listMessange.Location = new System.Drawing.Point(317, 0);
            this.listMessange.Margin = new System.Windows.Forms.Padding(4);
            this.listMessange.Name = "listMessange";
            this.listMessange.Size = new System.Drawing.Size(926, 629);
            this.listMessange.TabIndex = 1;
            // 
            // txtMessage
            // 
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.Location = new System.Drawing.Point(317, 629);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(4);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(799, 30);
            this.txtMessage.TabIndex = 2;
            // 
            // buttonSend
            // 
            this.buttonSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonSend.Location = new System.Drawing.Point(1116, 629);
            this.buttonSend.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(127, 48);
            this.buttonSend.TabIndex = 3;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.Send_Click);
            // 
            // CMainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1243, 677);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.listMessange);
            this.Controls.Add(this.userListbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CMainView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "REEChat";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox userListbox;
		private System.Windows.Forms.ListBox listMessange;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.Button buttonSend;
	}
}

