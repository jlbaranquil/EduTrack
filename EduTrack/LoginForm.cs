using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace EduTrack
{
    public partial class LoginForm : Form
    {
        // ==========================================
        // UI CONFIGURATION & THEMING
        // ==========================================
        public static string APP_TITLE = "EDUTRACK";
        public static string APP_SUBTITLE = "Student Information &\nDirectory Management System";

        // If missing, defaults to Emoji.
        public static string LOGO_IMAGE_PATH = @"Assets\edutrack-logo.png";
        public static string LOGO_EMOJI = "🎓";

        // Fonts
        public static string FONT_HEADING_PATH = @"Assets\Poppins.ttf";
        public static string FONT_BODY_PATH = @"Assets\Inter.ttf";
        // ==========================================

        private StudentCRUD crud = new StudentCRUD();
        private PrivateFontCollection pfc = new PrivateFontCollection();

        public LoginForm()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            ApplyTheme();

            pnlLeft.Resize += (s, e) =>
            {
                CenterLeftContent();
                DrawLeftBackground();
            };
            pnlRight.Resize += (s, e) => CenterRightContent();
        }

        private FontFamily LoadCustomFont(string path)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
                {
                    pfc.AddFontFile(path);
                    return pfc.Families[pfc.Families.Length - 1];
                }
            }
            catch { }
            return null;
        }

        private void ApplyTheme()
        {
            lblBrandTitle.Text = APP_TITLE;
            lblBrandSubtitle.Text = APP_SUBTITLE;

            if (File.Exists(LOGO_IMAGE_PATH))
            {
                picLogo.Image = Image.FromFile(LOGO_IMAGE_PATH);
                picLogo.Visible = true;
                lblLogoIcon.Visible = false;
            }
            else
            {
                lblLogoIcon.Text = LOGO_EMOJI;
                picLogo.Visible = false;
                lblLogoIcon.Visible = true;
            }

            FontFamily headingFamily = LoadCustomFont(FONT_HEADING_PATH);
            if (headingFamily != null)
            {
                lblBrandTitle.Font = new Font(headingFamily, lblBrandTitle.Font.Size, lblBrandTitle.Font.Style);
                lblWelcomeTitle.Font = new Font(headingFamily, lblWelcomeTitle.Font.Size, lblWelcomeTitle.Font.Style);
            }

            FontFamily bodyFamily = LoadCustomFont(FONT_BODY_PATH);
            if (bodyFamily != null)
            {
                lblBrandSubtitle.Font = new Font(bodyFamily, lblBrandSubtitle.Font.Size, lblBrandSubtitle.Font.Style);
                lblWelcomeSubtitle.Font = new Font(bodyFamily, lblWelcomeSubtitle.Font.Size, lblWelcomeSubtitle.Font.Style);
                lblUsernameTitle.Font = new Font(bodyFamily, lblUsernameTitle.Font.Size, lblUsernameTitle.Font.Style);
                lblPasswordTitle.Font = new Font(bodyFamily, lblPasswordTitle.Font.Size, lblPasswordTitle.Font.Style);
            }
        }

        private void CenterLeftContent()
        {
            if (pnlLeftContent != null && pnlLeft != null)
            {
                pnlLeftContent.Left = (pnlLeft.Width - pnlLeftContent.Width) / 2;
                pnlLeftContent.Top = (pnlLeft.Height - pnlLeftContent.Height) / 2;
            }
        }

        private void CenterRightContent()
        {
            if (pnlRightContent != null && pnlRight != null)
            {
                pnlRightContent.Left = (pnlRight.Width - pnlRightContent.Width) / 2;
                pnlRightContent.Top = (pnlRight.Height - pnlRightContent.Height) / 2;
            }
        }

        private void DrawLeftBackground()
        {
            if (pnlLeft.Width <= 0 || pnlLeft.Height <= 0) return;

            Bitmap bmp = new Bitmap(pnlLeft.Width, pnlLeft.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    new Rectangle(0, 0, pnlLeft.Width, pnlLeft.Height),
                    Color.FromArgb(15, 23, 42),
                    Color.FromArgb(79, 70, 229),
                    135F))
                {
                    g.FillRectangle(brush, new Rectangle(0, 0, pnlLeft.Width, pnlLeft.Height));
                }
            }

            Image oldImage = pnlLeft.BackgroundImage;
            pnlLeft.BackgroundImage = bmp;
            pnlLeft.BackgroundImageLayout = ImageLayout.None;

            if (oldImage != null)
            {
                oldImage.Dispose();
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            CenterLeftContent();
            CenterRightContent();
            DrawLeftBackground();

            ApplyRoundedCorners(pnlStatusBox, 10, Color.FromArgb(30, 41, 59));
            ApplyRoundedCorners(btnLogin, 8, Color.Empty);
            ApplyRoundedCorners(btnClose, 8, Color.FromArgb(203, 213, 225));

            AddRoundedInputBox(pnlRightContent, txtUsername, 8, Color.FromArgb(203, 213, 225));
            AddRoundedInputBox(pnlRightContent, txtPassword, 8, Color.FromArgb(203, 213, 225));

            bool isConnected = StudentDatabase.TestConnection();
            if (isConnected)
            {
                lblStatus.Text = "🟢  MySQL Status: Connected";
                lblStatus.ForeColor = Color.FromArgb(16, 185, 129);
            }
            else
            {
                lblStatus.Text = "🔴  MySQL Status: Offline";
                lblStatus.ForeColor = Color.FromArgb(244, 63, 94);
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

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (!ErrorHandler.ValidateLoginInputs(username, password))
            {
                lblError.Text = "Please enter both username and password.";
                lblError.Visible = true;
                return;
            }

            if (crud.AuthenticateUser(username, password))
            {
                lblError.Visible = false;
                MainForm mainForm = new MainForm();

                mainForm.StartPosition = FormStartPosition.Manual;
                mainForm.Location = this.Location;
                mainForm.Size = this.Size;
                mainForm.WindowState = this.WindowState;

                mainForm.Show();
                this.Hide();
            }
            else
            {
                lblError.Text = "Invalid username or password.";
                lblError.Visible = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}