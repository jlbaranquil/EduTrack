using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace EduTrack
{
    public partial class LoginForm : Form
    {
        private StudentCRUD crud = new StudentCRUD();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;

            // UI Styling Helpers (Pre-configured)
            ApplyRoundedCorners(pnlLogoBox, 14, Color.FromArgb(59, 130, 246));
            ApplyRoundedCorners(pnlStatusBox, 10, Color.FromArgb(30, 64, 175));
            ApplyRoundedCorners(btnLogin, 8, Color.Empty);
            ApplyRoundedCorners(btnClose, 8, Color.FromArgb(203, 213, 225));

            AddRoundedInputBox(pnlRight, txtUsername, 8, Color.FromArgb(203, 213, 225));
            AddRoundedInputBox(pnlRight, txtPassword, 8, Color.FromArgb(203, 213, 225));

            this.Resize += (s, ev) =>
            {
                pnlLeft.Invalidate();
                pnlRight.Invalidate();
            };

            // TODO: 1. Test the database connection using StudentDatabase.TestConnection().
            // TODO: 2. If connected, set lblStatus.Text to "🟢  MySQL Status: Connected" and ForeColor to RGB(74, 222, 128).
            // TODO: 3. If offline, set lblStatus.Text to "🔴  MySQL Status: Offline" and ForeColor to RGB(248, 113, 113).
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            // TODO: Toggle txtPassword.UseSystemPasswordChar based on chkShowPassword.Checked.
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // TODO: 1. Retrieve and trim the text from txtUsername and txtPassword.
            
            // TODO: 2. Validate inputs using ErrorHandler.ValidateLoginInputs(username, password).
            //          If invalid, display "Please enter both username and password." in lblError and return.

            // TODO: 3. Authenticate the user via crud.AuthenticateUser(username, password).
            //          - If valid: hide lblError, instantiate and show MainForm, and hide this LoginForm.
            //          - If invalid: display "Invalid username or password." in lblError.
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // TODO: Exit the application.
        }

        #region UI Drawing Helpers (Do Not Modify)
        private void pnlLeft_Paint(object sender, PaintEventArgs e)
        {
            if (pnlLeft.ClientRectangle.Width <= 0 || pnlLeft.ClientRectangle.Height <= 0)
                return;

            using (LinearGradientBrush brush = new LinearGradientBrush(
                pnlLeft.ClientRectangle,
                Color.FromArgb(15, 29, 64),
                Color.FromArgb(29, 78, 216),
                135F))
            {
                e.Graphics.FillRectangle(brush, pnlLeft.ClientRectangle);
            }
        }

        private GraphicsPath CreateRoundRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void ApplyRoundedCorners(Control control, int radius, Color borderColor)
        {
            control.Paint += (s, e) =>
            {
                if (control.Width <= 1 || control.Height <= 1) return;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, control.Width - 1, control.Height - 1);
                using (GraphicsPath path = CreateRoundRectPath(rect, radius))
                {
                    control.Region = new Region(path);
                    if (borderColor != Color.Empty)
                    {
                        using (Pen pen = new Pen(borderColor, 1))
                        {
                            e.Graphics.DrawPath(pen, path);
                        }
                    }
                }
            };
            control.Resize += (s, e) => control.Invalidate();
        }

        private void AddRoundedInputBox(Panel parent, Control input, int radius, Color borderColor)
        {
            parent.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle boxRect = new Rectangle(input.Left - 8, input.Top - 7, input.Width + 16, input.Height + 14);
                using (GraphicsPath path = CreateRoundRectPath(boxRect, radius))
                using (SolidBrush fillBrush = new SolidBrush(input.BackColor))
                using (Pen borderPen = new Pen(borderColor, 1))
                {
                    e.Graphics.FillPath(fillBrush, path);
                    e.Graphics.DrawPath(borderPen, path);
                }
            };
        }
        #endregion
    }
}