namespace Client
{
	partial class CRegistrationView
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
			this.txtEmail = new CustomControl.WatermarkTextBox();
			this.txtNickname = new CustomControl.WatermarkTextBox();
			this.txtBirthday = new CustomControl.WatermarkTextBox();
			this.txtPassword = new CustomControl.WatermarkTextBox();
			this.txtPassword2 = new CustomControl.WatermarkTextBox();
			this.buttonRegister = new System.Windows.Forms.Button();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.txtServerAddress = new CustomControl.WatermarkTextBox();
			this.SuspendLayout();
			// 
			// txtEmail
			// 
			this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
			this.txtEmail.Location = new System.Drawing.Point(12, 44);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Password = false;
			this.txtEmail.Size = new System.Drawing.Size(276, 22);
			this.txtEmail.TabIndex = 4;
			this.txtEmail.Watermark = "Email";
			// 
			// txtNickname
			// 
			this.txtNickname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtNickname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
			this.txtNickname.Location = new System.Drawing.Point(12, 72);
			this.txtNickname.Name = "txtNickname";
			this.txtNickname.Password = false;
			this.txtNickname.Size = new System.Drawing.Size(276, 22);
			this.txtNickname.TabIndex = 5;
			this.txtNickname.Watermark = "Benutzername";
			// 
			// txtBirthday
			// 
			this.txtBirthday.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtBirthday.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
			this.txtBirthday.Location = new System.Drawing.Point(12, 156);
			this.txtBirthday.Name = "txtBirthday";
			this.txtBirthday.Password = false;
			this.txtBirthday.Size = new System.Drawing.Size(276, 22);
			this.txtBirthday.TabIndex = 8;
			this.txtBirthday.Watermark = "Gebutstag (dd.mm.yyyy)";
			// 
			// txtPassword
			// 
			this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
			this.txtPassword.Location = new System.Drawing.Point(12, 100);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Password = true;
			this.txtPassword.Size = new System.Drawing.Size(276, 22);
			this.txtPassword.TabIndex = 6;
			this.txtPassword.Watermark = "Passwort";
			// 
			// txtPassword2
			// 
			this.txtPassword2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPassword2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
			this.txtPassword2.Location = new System.Drawing.Point(12, 128);
			this.txtPassword2.Name = "txtPassword2";
			this.txtPassword2.Password = true;
			this.txtPassword2.Size = new System.Drawing.Size(276, 22);
			this.txtPassword2.TabIndex = 7;
			this.txtPassword2.Watermark = "Passwort bestätigen";
			// 
			// buttonRegister
			// 
			this.buttonRegister.Location = new System.Drawing.Point(180, 190);
			this.buttonRegister.Name = "buttonRegister";
			this.buttonRegister.Size = new System.Drawing.Size(108, 23);
			this.buttonRegister.TabIndex = 10;
			this.buttonRegister.Text = "Registrieren";
			this.buttonRegister.UseVisualStyleBackColor = true;
			this.buttonRegister.Click += new System.EventHandler(this.Register_Click);
			// 
			// timer
			// 
			this.timer.Interval = 1000;
			this.timer.Tick += new System.EventHandler(this.Tick);
			// 
			// txtServerAddress
			// 
			this.txtServerAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtServerAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
			this.txtServerAddress.Location = new System.Drawing.Point(12, 12);
			this.txtServerAddress.Name = "txtServerAddress";
			this.txtServerAddress.Password = false;
			this.txtServerAddress.Size = new System.Drawing.Size(276, 22);
			this.txtServerAddress.TabIndex = 3;
			this.txtServerAddress.Watermark = "Server Adresse";
			// 
			// CRegistrationForm
			// 
			this.AcceptButton = this.buttonRegister;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(307, 234);
			this.Controls.Add(this.txtServerAddress);
			this.Controls.Add(this.buttonRegister);
			this.Controls.Add(this.txtPassword2);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtBirthday);
			this.Controls.Add(this.txtNickname);
			this.Controls.Add(this.txtEmail);
			this.Name = "CRegistrationForm";
			this.Text = "CRegistrationForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CRegistrationForm_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private CustomControl.WatermarkTextBox txtEmail;
		private CustomControl.WatermarkTextBox txtNickname;
		private CustomControl.WatermarkTextBox txtBirthday;
		private CustomControl.WatermarkTextBox txtPassword;
		private CustomControl.WatermarkTextBox txtPassword2;
		private System.Windows.Forms.Button buttonRegister;
		private System.Windows.Forms.Timer timer;
		private CustomControl.WatermarkTextBox txtServerAddress;
	}
}