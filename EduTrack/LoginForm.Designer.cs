namespace EduTrack
{
    partial class LoginForm
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
            this.pnlLeft = new Panel();
            this.pnlLeftContent = new Panel();
            this.pnlLogoBox = new Panel();
            this.picLogo = new PictureBox();
            this.lblLogoIcon = new Label();
            this.lblBrandTitle = new Label();
            this.lblBrandSubtitle = new Label();
            this.pnlDivider = new Panel();
            this.lblFeature1 = new Label();
            this.lblFeature2 = new Label();
            this.lblFeature3 = new Label();
            this.pnlStatusBox = new Panel();
            this.lblStatus = new Label();

            this.pnlRight = new Panel();
            this.pnlRightContent = new Panel();
            this.lblAuthHeader = new Label();
            this.lblWelcomeTitle = new Label();
            this.lblWelcomeSubtitle = new Label();
            this.lblError = new Label();
            this.lblUsernameTitle = new Label();
            this.txtUsername = new TextBox();
            this.lblPasswordTitle = new Label();
            this.txtPassword = new TextBox();
            this.chkShowPassword = new CheckBox();
            this.btnLogin = new Button();
            this.btnClose = new Button();

            this.pnlLeft.SuspendLayout();
            this.pnlLeftContent.SuspendLayout();
            this.pnlLogoBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlStatusBox.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlRightContent.SuspendLayout();
            this.SuspendLayout();

            // pnlLeft
            this.pnlLeft.BackColor = Color.FromArgb(15, 23, 42);
            this.pnlLeft.Controls.Add(this.pnlLeftContent);
            this.pnlLeft.Dock = DockStyle.Left;
            this.pnlLeft.Location = new Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new Size(520, 704);
            this.pnlLeft.TabIndex = 0;

            // pnlLeftContent
            this.pnlLeftContent.BackColor = Color.Transparent;
            this.pnlLeftContent.Controls.Add(this.pnlLogoBox);
            this.pnlLeftContent.Controls.Add(this.lblBrandTitle);
            this.pnlLeftContent.Controls.Add(this.lblBrandSubtitle);
            this.pnlLeftContent.Controls.Add(this.pnlDivider);
            this.pnlLeftContent.Controls.Add(this.lblFeature1);
            this.pnlLeftContent.Controls.Add(this.lblFeature2);
            this.pnlLeftContent.Controls.Add(this.lblFeature3);
            this.pnlLeftContent.Controls.Add(this.pnlStatusBox);
            this.pnlLeftContent.Location = new Point(115, 145);
            this.pnlLeftContent.Name = "pnlLeftContent";
            this.pnlLeftContent.Size = new Size(290, 410);
            this.pnlLeftContent.TabIndex = 8;

            // pnlLogoBox
            this.pnlLogoBox.BackColor = Color.Transparent;
            this.pnlLogoBox.Controls.Add(this.picLogo);
            this.pnlLogoBox.Controls.Add(this.lblLogoIcon);
            this.pnlLogoBox.Location = new Point(102, 0);
            this.pnlLogoBox.Name = "pnlLogoBox";
            this.pnlLogoBox.Size = new Size(85, 85);
            this.pnlLogoBox.TabIndex = 0;

            // picLogo
            this.picLogo.Dock = DockStyle.Fill;
            this.picLogo.Location = new Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new Size(85, 85);
            this.picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 1;
            this.picLogo.TabStop = false;
            this.picLogo.Visible = false;

            // lblLogoIcon
            this.lblLogoIcon.Dock = DockStyle.Fill;
            this.lblLogoIcon.Font = new Font("Segoe UI Emoji", 24F, FontStyle.Bold);
            this.lblLogoIcon.ForeColor = Color.White;
            this.lblLogoIcon.Location = new Point(0, 0);
            this.lblLogoIcon.Name = "lblLogoIcon";
            this.lblLogoIcon.Size = new Size(85, 85);
            this.lblLogoIcon.TabIndex = 0;
            this.lblLogoIcon.Text = "🎓";
            this.lblLogoIcon.TextAlign = ContentAlignment.MiddleCenter;

            // lblBrandTitle
            this.lblBrandTitle.AutoSize = false;
            this.lblBrandTitle.Font = new Font("Plus Jakarta Sans", 22F, FontStyle.Bold);
            this.lblBrandTitle.ForeColor = Color.White;
			this.lblBrandTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblBrandTitle.Location = new Point(0, 80);
            this.lblBrandTitle.Name = "lblBrandTitle";
            this.lblBrandTitle.Size = new Size(290, 41);
            this.lblBrandTitle.TabIndex = 1;
            this.lblBrandTitle.Text = "EDUTRACK";

            // lblBrandSubtitle
            this.lblBrandSubtitle.AutoSize = false;
            this.lblBrandSubtitle.Font = new Font("Inter", 10.5F, FontStyle.Regular);
            this.lblBrandSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
			this.lblBrandSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblBrandSubtitle.Location = new Point(0, 127);
            this.lblBrandSubtitle.Name = "lblBrandSubtitle";
            this.lblBrandSubtitle.Size = new Size(290, 40);
            this.lblBrandSubtitle.TabIndex = 2;
            this.lblBrandSubtitle.Text = "Student Information &\r\nDirectory Management System";
            this.lblBrandSubtitle.UseMnemonic = false;

            // pnlDivider
            this.pnlDivider.BackColor = Color.FromArgb(99, 102, 241);
            this.pnlDivider.Location = new Point(0, 187);
            this.pnlDivider.Name = "pnlDivider";
            this.pnlDivider.Size = new Size(290, 1);
            this.pnlDivider.TabIndex = 3;

            // lblFeature1
            this.lblFeature1.AutoSize = true;
            this.lblFeature1.Font = new Font("Segoe UI Emoji", 9.5F, FontStyle.Regular);
            this.lblFeature1.ForeColor = Color.FromArgb(203, 213, 225);
            this.lblFeature1.Location = new Point(0, 210);
            this.lblFeature1.Name = "lblFeature1";
            this.lblFeature1.Size = new Size(190, 17);
            this.lblFeature1.TabIndex = 4;
            this.lblFeature1.Text = "🎓   Centralized Student Records";

            // lblFeature2
            this.lblFeature2.AutoSize = true;
            this.lblFeature2.Font = new Font("Segoe UI Emoji", 9.5F, FontStyle.Regular);
            this.lblFeature2.ForeColor = Color.FromArgb(203, 213, 225);
            this.lblFeature2.Location = new Point(0, 244);
            this.lblFeature2.Name = "lblFeature2";
            this.lblFeature2.Size = new Size(219, 17);
            this.lblFeature2.TabIndex = 5;
            this.lblFeature2.Text = "⚡   Real-Time MySQL Synchronization";

            // lblFeature3
            this.lblFeature3.AutoSize = true;
            this.lblFeature3.Font = new Font("Segoe UI Emoji", 9.5F, FontStyle.Regular);
            this.lblFeature3.ForeColor = Color.FromArgb(203, 213, 225);
            this.lblFeature3.Location = new Point(0, 278);
            this.lblFeature3.Name = "lblFeature3";
            this.lblFeature3.Size = new Size(190, 17);
            this.lblFeature3.TabIndex = 6;
            this.lblFeature3.Text = "🛡️   Secure Access";

            // pnlStatusBox
            this.pnlStatusBox.BackColor = Color.FromArgb(30, 41, 59);
            this.pnlStatusBox.Controls.Add(this.lblStatus);
            this.pnlStatusBox.Location = new Point(0, 370);
            this.pnlStatusBox.Name = "pnlStatusBox";
            this.pnlStatusBox.Size = new Size(290, 38);
            this.pnlStatusBox.TabIndex = 7;

			// lblStatus
			this.lblStatus.AutoSize = false;
			this.lblStatus.Dock = DockStyle.Fill;
			this.lblStatus.TextAlign = ContentAlignment.MiddleCenter;
			this.lblStatus.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold);
			this.lblStatus.ForeColor = Color.FromArgb(16, 185, 129);
			this.lblStatus.Location = new Point(0, 0);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new Size(290, 38);
			this.lblStatus.TabIndex = 0;
			this.lblStatus.Text = "🟢  MySQL Status: Connected";

            // pnlRight
            this.pnlRight.BackColor = Color.White;
            this.pnlRight.Controls.Add(this.pnlRightContent);
            this.pnlRight.Dock = DockStyle.Fill;
            this.pnlRight.Location = new Point(520, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new Size(700, 704);
            this.pnlRight.TabIndex = 1;

            // pnlRightContent
            this.pnlRightContent.BackColor = Color.Transparent;
            this.pnlRightContent.Controls.Add(this.lblAuthHeader);
            this.pnlRightContent.Controls.Add(this.lblWelcomeTitle);
            this.pnlRightContent.Controls.Add(this.lblWelcomeSubtitle);
            this.pnlRightContent.Controls.Add(this.lblError);
            this.pnlRightContent.Controls.Add(this.lblUsernameTitle);
            this.pnlRightContent.Controls.Add(this.txtUsername);
            this.pnlRightContent.Controls.Add(this.lblPasswordTitle);
            this.pnlRightContent.Controls.Add(this.txtPassword);
            this.pnlRightContent.Controls.Add(this.chkShowPassword);
            this.pnlRightContent.Controls.Add(this.btnLogin);
            this.pnlRightContent.Controls.Add(this.btnClose);
            this.pnlRightContent.Location = new Point(168, 145);
            this.pnlRightContent.Name = "pnlRightContent";
            this.pnlRightContent.Size = new Size(365, 400);
            this.pnlRightContent.TabIndex = 11;

            // lblAuthHeader
            this.lblAuthHeader.AutoSize = true;
            this.lblAuthHeader.Font = new Font("Inter", 9F, FontStyle.Bold);
            this.lblAuthHeader.ForeColor = Color.FromArgb(79, 70, 229);
            this.lblAuthHeader.Location = new Point(4, 0);
            this.lblAuthHeader.Name = "lblAuthHeader";
            this.lblAuthHeader.Size = new Size(130, 15);
            this.lblAuthHeader.TabIndex = 0;
            this.lblAuthHeader.Text = "🔒 AUTHENTICATION";

            // lblWelcomeTitle
            this.lblWelcomeTitle.AutoSize = true;
            this.lblWelcomeTitle.Font = new Font("Plus Jakarta Sans", 20F, FontStyle.Bold);
            this.lblWelcomeTitle.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblWelcomeTitle.Location = new Point(0, 20);
            this.lblWelcomeTitle.Name = "lblWelcomeTitle";
            this.lblWelcomeTitle.Size = new Size(225, 37);
            this.lblWelcomeTitle.TabIndex = 1;
            this.lblWelcomeTitle.Text = "WELCOME BACK";

            // lblWelcomeSubtitle
            this.lblWelcomeSubtitle.AutoSize = true;
            this.lblWelcomeSubtitle.Font = new Font("Inter", 9.5F, FontStyle.Regular);
            this.lblWelcomeSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblWelcomeSubtitle.Location = new Point(4, 61);
            this.lblWelcomeSubtitle.Name = "lblWelcomeSubtitle";
            this.lblWelcomeSubtitle.Size = new Size(261, 17);
            this.lblWelcomeSubtitle.TabIndex = 2;
            this.lblWelcomeSubtitle.Text = "Sign in to manage student directory records";

            // lblError
            this.lblError.AutoSize = true;
            this.lblError.Font = new Font("Inter", 9F, FontStyle.Bold);
            this.lblError.ForeColor = Color.FromArgb(244, 63, 94);
            this.lblError.Location = new Point(4, 90);
            this.lblError.Name = "lblError";
            this.lblError.Size = new Size(206, 15);
            this.lblError.TabIndex = 3;
            this.lblError.Text = "Invalid username or password.";
            this.lblError.Visible = false;

            // lblUsernameTitle
            this.lblUsernameTitle.AutoSize = true;
            this.lblUsernameTitle.Font = new Font("Inter", 8.5F, FontStyle.Bold);
            this.lblUsernameTitle.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblUsernameTitle.Location = new Point(4, 120);
            this.lblUsernameTitle.Name = "lblUsernameTitle";
            this.lblUsernameTitle.Size = new Size(95, 15);
            this.lblUsernameTitle.TabIndex = 4;
            this.lblUsernameTitle.Text = "👤 USERNAME";

            // txtUsername
            this.txtUsername.BackColor = Color.FromArgb(248, 250, 252);
            this.txtUsername.BorderStyle = BorderStyle.None;
            this.txtUsername.Font = new Font("Inter", 11F, FontStyle.Regular);
            this.txtUsername.ForeColor = Color.FromArgb(15, 23, 42);
            this.txtUsername.Location = new Point(12, 147);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new Size(339, 20);
            this.txtUsername.TabIndex = 5;
            this.txtUsername.Text = "admin";

            // lblPasswordTitle
            this.lblPasswordTitle.AutoSize = true;
            this.lblPasswordTitle.Font = new Font("Inter", 8.5F, FontStyle.Bold);
            this.lblPasswordTitle.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblPasswordTitle.Location = new Point(4, 190);
            this.lblPasswordTitle.Name = "lblPasswordTitle";
            this.lblPasswordTitle.Size = new Size(96, 15);
            this.lblPasswordTitle.TabIndex = 6;
            this.lblPasswordTitle.Text = "🔑 PASSWORD";

            // txtPassword
            this.txtPassword.BackColor = Color.FromArgb(248, 250, 252);
            this.txtPassword.BorderStyle = BorderStyle.None;
            this.txtPassword.Font = new Font("Inter", 11F, FontStyle.Regular);
            this.txtPassword.ForeColor = Color.FromArgb(15, 23, 42);
            this.txtPassword.Location = new Point(12, 217);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new Size(339, 20);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.UseSystemPasswordChar = true;

            // chkShowPassword
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.Font = new Font("Inter", 9F, FontStyle.Regular);
            this.chkShowPassword.ForeColor = Color.FromArgb(71, 85, 105);
            this.chkShowPassword.Location = new Point(4, 253);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new Size(109, 19);
            this.chkShowPassword.TabIndex = 8;
            this.chkShowPassword.Text = "Show Password";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);

            // btnLogin
            this.btnLogin.BackColor = Color.FromArgb(79, 70, 229);
            this.btnLogin.Cursor = Cursors.Hand;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.Font = new Font("Segoe UI Emoji", 10.5F, FontStyle.Bold);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Location = new Point(4, 295);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(355, 44);
            this.btnLogin.TabIndex = 9;
            this.btnLogin.Text = "➔  SIGN IN";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // btnClose
            this.btnClose.BackColor = Color.FromArgb(248, 250, 252);
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold);
            this.btnClose.ForeColor = Color.FromArgb(100, 116, 139);
            this.btnClose.Location = new Point(4, 355);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(355, 38);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "❌ CLOSE APPLICATION";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // LoginForm
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1220, 704);
            this.MinimumSize = new Size(1000, 620);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Name = "LoginForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "EduTrack - Sign In";
            this.Load += new System.EventHandler(this.LoginForm_Load);

            this.pnlLeft.ResumeLayout(false);
            this.pnlLeftContent.ResumeLayout(false);
            this.pnlLeftContent.PerformLayout();
            this.pnlLogoBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlStatusBox.ResumeLayout(false);
            this.pnlStatusBox.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlRightContent.ResumeLayout(false);
            this.pnlRightContent.PerformLayout();
            this.ResumeLayout(false);
        }
		
		#endregion
		
        private Panel pnlLeft;
        private Panel pnlLeftContent;
        private Panel pnlLogoBox;
        private PictureBox picLogo;
        private Label lblLogoIcon;
        private Label lblBrandTitle;
        private Label lblBrandSubtitle;
        private Panel pnlDivider;
        private Label lblFeature1;
        private Label lblFeature2;
        private Label lblFeature3;
        private Panel pnlStatusBox;
        private Label lblStatus;

        private Panel pnlRight;
        private Panel pnlRightContent;
        private Label lblAuthHeader;
        private Label lblWelcomeTitle;
        private Label lblWelcomeSubtitle;
        private Label lblError;
        private Label lblUsernameTitle;
        private TextBox txtUsername;
        private Label lblPasswordTitle;
        private TextBox txtPassword;
        private CheckBox chkShowPassword;
        private Button btnLogin;
        private Button btnClose;
    }
}