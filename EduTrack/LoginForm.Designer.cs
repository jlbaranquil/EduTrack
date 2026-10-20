using System.Drawing;
using System.Windows.Forms;

namespace EduTrack
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        // Control declarations required by LoginForm.cs
        private Panel pnlLeft;
        private Panel pnlLogoBox;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {


        }
    }
}