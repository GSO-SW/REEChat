namespace Client
{
	partial class CLoginView
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.txtPassword = new CustomControl.WatermarkTextBox();
			this.txtEmail = new CustomControl.WatermarkTextBox();
			this.txtAddress = new CustomControl.WatermarkTextBox();
			this.buttonConnect = new System.Windows.Forms.Button();
			this.buttonRegister = new System.Windows.Forms.Button();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// txtPassword
			// 
			this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
			this.txtPassword.Location = new System.Drawing.Point(12, 68);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Password = true;
			this.txtPassword.Size = new System.Drawing.Size(276, 22);
			this.txtPassword.TabIndex = 1;
			this.txtPassword.Watermark = "Passwort";
			// 
			// txtEmail
			// 
			this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
			this.txtEmail.Location = new System.Drawing.Point(12, 40);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Password = false;
			this.txtEmail.Size = new System.Drawing.Size(276, 22);
			this.txtEmail.TabIndex = 0;
			this.txtEmail.Watermark = "E-Mail";
			// 
			// txtAddress
			// 
			this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
			this.txtAddress.Location = new System.Drawing.Point(12, 12);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Password = true;
			this.txtAddress.Size = new System.Drawing.Size(276, 22);
			this.txtAddress.TabIndex = 3;
			this.txtAddress.Watermark = "Server IP Adresse";
			// 
			// buttonConnect
			// 
			this.buttonConnect.Location = new System.Drawing.Point(213, 104);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(75, 23);
			this.buttonConnect.TabIndex = 4;
			this.buttonConnect.Text = "Verbinden";
			this.buttonConnect.UseVisualStyleBackColor = true;
			this.buttonConnect.Click += new System.EventHandler(this.Connect_Click);
			// 
			// buttonRegister
			// 
			this.buttonRegister.Enabled = false;
			this.buttonRegister.Location = new System.Drawing.Point(132, 104);
			this.buttonRegister.Name = "buttonRegister";
			this.buttonRegister.Size = new System.Drawing.Size(75, 23);
			this.buttonRegister.TabIndex = 5;
			this.buttonRegister.Text = "Registrieren";
			this.buttonRegister.UseVisualStyleBackColor = true;
			// 
			// timer
			// 
			this.timer.Interval = 1000;
			this.timer.Tick += new System.EventHandler(this.Tick);
			// 
			// LoginView
			// 
			this.AcceptButton = this.buttonConnect;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(300, 139);
			this.Controls.Add(this.buttonRegister);
			this.Controls.Add(this.buttonConnect);
			this.Controls.Add(this.txtAddress);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtEmail);
			this.Name = "LoginView";
			this.Text = "LoginView";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private CustomControl.WatermarkTextBox txtEmail;
		private CustomControl.WatermarkTextBox txtPassword;
		private CustomControl.WatermarkTextBox txtAddress;
		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.Button buttonRegister;
		private System.Windows.Forms.Timer timer;
	}
}