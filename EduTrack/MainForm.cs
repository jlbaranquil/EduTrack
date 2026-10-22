using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EduTrack
{
    public partial class MainForm : Form
    {
        // ==========================================
        // UI CONFIGURATION & THEMING
        // ==========================================
        public static string APP_TITLE = "EDUTRACK";
        public static string APP_SUBTITLE = "Student Information & Directory System";

        // If missing, defaults to Emoji.
        public static string LOGO_IMAGE_PATH = @"Assets\edutrack-logo.png";
        public static string LOGO_EMOJI = "🎓";

        // Dashboard Tile Icons
        public static string ICON_TOTAL = "👥";
        public static string ICON_BSIT = "💻";
        public static string ICON_BSCS = "⌨️";
        public static string ICON_OTHER = "📁";

        // Fonts
        public static string FONT_HEADING_PATH = @"Assets\Poppins.ttf";
        public static string FONT_BODY_PATH = @"Assets\Inter.ttf";
        // ==========================================

        private StudentCRUD crud = new StudentCRUD();
        private bool isInitializing = false;
        private PrivateFontCollection pfc = new PrivateFontCollection();

        // Pre-input placeholder texts
        private const string PH_STUDENT_ID = "e.g. 2026-0001";
        private const string PH_FULL_NAME = "First & Last Name";
        private const string PH_CONTACT = "XXXX-XXX-XXXX";
        private const string PH_SEARCH = "Search by Student ID or Full Name...";

        public MainForm()
        {
            InitializeComponent();
            ApplyTheme();
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
            lblAppTitle.Text = APP_TITLE;
            lblAppSubtitle.Text = APP_SUBTITLE;

            if (File.Exists(LOGO_IMAGE_PATH))
            {
                picHeaderLogo.Image = Image.FromFile(LOGO_IMAGE_PATH);
                picHeaderLogo.Visible = true;
                lblHeaderIcon.Visible = false;
            }
            else
            {
                lblHeaderIcon.Text = LOGO_EMOJI;
                picHeaderLogo.Visible = false;
                lblHeaderIcon.Visible = true;
            }

            lblIconTotal.Text = ICON_TOTAL;
            lblIconBsit.Text = ICON_BSIT;
            lblIconBscs.Text = ICON_BSCS;
            lblIconOther.Text = ICON_OTHER;

            FontFamily headingFamily = LoadCustomFont(FONT_HEADING_PATH);
            if (headingFamily != null)
            {
                lblAppTitle.Font = new Font(headingFamily, lblAppTitle.Font.Size, lblAppTitle.Font.Style);
                lblDetailsHeaderTitle.Font = new Font(headingFamily, lblDetailsHeaderTitle.Font.Size, lblDetailsHeaderTitle.Font.Style);
            }

            FontFamily bodyFamily = LoadCustomFont(FONT_BODY_PATH);
            if (bodyFamily != null)
            {
                lblAppSubtitle.Font = new Font(bodyFamily, lblAppSubtitle.Font.Size, lblAppSubtitle.Font.Style);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            isInitializing = true;

            cmbYearLevel.SelectedIndex = 0;
            cmbProgram.SelectedIndex = 0;
            cmbFilterYear.SelectedIndex = 0;
            cmbFilterProgram.SelectedIndex = 0;

            SetupPlaceholder(txtStudentID, PH_STUDENT_ID);
            SetupPlaceholder(txtFullName, PH_FULL_NAME);
            SetupPlaceholder(txtContact, PH_CONTACT);
            SetupPlaceholder(txtSearch, PH_SEARCH);

            Color thinBorder = Color.FromArgb(226, 232, 240);
            Color inputBorder = Color.FromArgb(203, 213, 225);

            ApplyRoundedCorners(lblAdminBadge, 6, Color.FromArgb(30, 41, 59));
            ApplyRoundedCorners(pnlDbStatusBadge, 6, Color.FromArgb(16, 185, 129));
            ApplyRoundedCorners(btnSignOut, 6, Color.Empty);

            ApplyRoundedCorners(pnlCardTotal, 10, thinBorder);
            ApplyRoundedCorners(pnlCardBsit, 10, thinBorder);
            ApplyRoundedCorners(pnlCardBscs, 10, thinBorder);
            ApplyRoundedCorners(pnlCardOther, 10, thinBorder);

            ApplyRoundedCorners(pnlStudentDetails, 10, thinBorder);
            ApplyRoundedCorners(picStudent, 10, Color.FromArgb(147, 197, 253));
            ApplyRoundedCorners(btnBrowsePhoto, 6, inputBorder);
            ApplyRoundedCorners(btnAdd, 8, Color.Empty);
            ApplyRoundedCorners(btnUpdate, 8, Color.Empty);
            ApplyRoundedCorners(btnDelete, 8, Color.Empty);
            ApplyRoundedCorners(btnClear, 8, inputBorder);

            ApplyRoundedCorners(pnlFilterBar, 10, thinBorder);
            ApplyRoundedCorners(pnlGridContainer, 10, thinBorder);
            ApplyRoundedCorners(btnExportCsv, 6, Color.Empty);

            AddRoundedInputBox(pnlStudentDetails, txtStudentID, 6, inputBorder);
            AddRoundedInputBox(pnlStudentDetails, txtFullName, 6, inputBorder);
            AddRoundedInputBox(pnlStudentDetails, txtContact, 6, inputBorder);
            AddRoundedInputBox(pnlFilterBar, txtSearch, 6, inputBorder);

            picStudent.Paint += picStudent_Paint;

            isInitializing = false;

            bool isConnected = StudentDatabase.TestConnection();
            if (isConnected)
            {
                lblDbStatus.Text = "🟢  DB Connected";
                lblDbStatus.ForeColor = Color.FromArgb(16, 185, 129);
            }
            else
            {
                lblDbStatus.Text = "🔴  DB Offline";
                lblDbStatus.ForeColor = Color.FromArgb(244, 63, 94);
            }

            LoadStudentsToGrid();
            UpdateSummaryCards();
            ClearInputs();
        }

        private void SetupPlaceholder(TextBox textBox, string placeholderText)
        {
            SetPlaceholderState(textBox, placeholderText);

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholderText)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.FromArgb(15, 23, 42);
                }
                textBox.SelectionStart = 0;
                textBox.SelectionLength = 0;
            };

            textBox.MouseUp += (s, e) =>
            {
                if (textBox.Text == placeholderText || string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.SelectionStart = 0;
                    textBox.SelectionLength = 0;
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    SetPlaceholderState(textBox, placeholderText);
                }
            };
        }

        private void SetPlaceholderState(TextBox textBox, string placeholderText)
        {
            textBox.Text = placeholderText;
            textBox.ForeColor = Color.FromArgb(148, 163, 184);
        }

        private string GetCleanInput(TextBox textBox, string placeholderText)
        {
            string val = textBox.Text.Trim();
            if (val == placeholderText)
            {
                return "";
            }
            return val;
        }

        private void picStudent_Paint(object sender, PaintEventArgs e)
        {
            if (picStudent.Image == null)
            {
                using (Font iconFont = new Font("Segoe UI Emoji", 24F, FontStyle.Regular))
                using (SolidBrush iconBrush = new SolidBrush(Color.FromArgb(59, 130, 246)))
                {
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    e.Graphics.DrawString("👤", iconFont, iconBrush, picStudent.ClientRectangle, sf);
                }
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
                        using (Pen pen = new Pen(borderColor, 1)) e.Graphics.DrawPath(pen, path);
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
                Rectangle boxRect = new Rectangle(input.Left - 8, input.Top - 5, input.Width + 16, input.Height + 10);
                using (GraphicsPath path = CreateRoundRectPath(boxRect, radius))
                using (SolidBrush fillBrush = new SolidBrush(input.BackColor))
                using (Pen borderPen = new Pen(borderColor, 1))
                {
                    e.Graphics.FillPath(fillBrush, path);
                    e.Graphics.DrawPath(borderPen, path);
                }
            };
            parent.Resize += (s, e) => parent.Invalidate();
        }

        public void LoadStudentsToGrid()
        {
            DataTable dt = crud.GetAllStudents();
            dgvStudents.DataSource = dt;
            FormatStudentGrid();

            int totalCount = crud.GetTotalStudentCount();
            lblRecordCount.Text = "Showing " + dt.Rows.Count + " of " + totalCount + " records";
        }

        private void FormatStudentGrid()
        {
            if (dgvStudents.Columns.Count == 0) return;
            if (dgvStudents.Columns.Contains("student_id")) dgvStudents.Columns["student_id"].HeaderText = "STUDENT ID";
            if (dgvStudents.Columns.Contains("name")) dgvStudents.Columns["name"].HeaderText = "FULL NAME";
            if (dgvStudents.Columns.Contains("gender")) dgvStudents.Columns["gender"].HeaderText = "GENDER";
            if (dgvStudents.Columns.Contains("year_level")) dgvStudents.Columns["year_level"].HeaderText = "YEAR LEVEL";
            if (dgvStudents.Columns.Contains("program")) dgvStudents.Columns["program"].HeaderText = "PROGRAM";
            if (dgvStudents.Columns.Contains("contact_number")) dgvStudents.Columns["contact_number"].HeaderText = "CONTACT NUMBER";
            if (dgvStudents.Columns.Contains("photo_path")) dgvStudents.Columns["photo_path"].Visible = false;
        }

        public void UpdateSummaryCards()
        {
            int total = crud.GetTotalStudentCount();
            int bsit = crud.GetStudentCountByProgram("BSIT");
            int bscs = crud.GetStudentCountByProgram("BSCS");
            int others = total - bsit - bscs;
            if (others < 0) others = 0;

            lblTotalCount.Text = total.ToString();
            lblBsitCount.Text = bsit.ToString();
            lblBscsCount.Text = bscs.ToString();
            lblOtherCount.Text = others.ToString();
        }

        public void ApplyFilters()
        {
            if (isInitializing) return;

            string keyword = GetCleanInput(txtSearch, PH_SEARCH);
            string yearFilter = cmbFilterYear.Text;
            string programFilter = cmbFilterProgram.Text;

            DataTable dt = crud.FilterStudents(keyword, yearFilter, programFilter);
            dgvStudents.DataSource = dt;
            FormatStudentGrid();

            int totalCount = crud.GetTotalStudentCount();
            lblRecordCount.Text = "Showing " + dt.Rows.Count + " of " + totalCount + " records";
        }

        public Student GetStudentFromInputs()
        {
            string selectedGender = "Male";
            if (rbFemale.Checked) selectedGender = "Female";
            else if (rbOther.Checked) selectedGender = "Other";

            Student s = new Student();
            s.StudentID = GetCleanInput(txtStudentID, PH_STUDENT_ID);
            s.Name = GetCleanInput(txtFullName, PH_FULL_NAME);
            s.Gender = selectedGender;
            s.YearLevel = cmbYearLevel.Text;
            s.Program = cmbProgram.Text;
            s.ContactNumber = GetCleanInput(txtContact, PH_CONTACT);
            s.PhotoPath = picStudent.Tag != null ? picStudent.Tag.ToString() : null;

            return s;
        }

        public void ClearInputs()
        {
            SetPlaceholderState(txtStudentID, PH_STUDENT_ID);
            SetPlaceholderState(txtFullName, PH_FULL_NAME);
            rbMale.Checked = true;
            cmbYearLevel.SelectedIndex = 0;
            cmbProgram.SelectedIndex = 0;
            SetPlaceholderState(txtContact, PH_CONTACT);

            picStudent.Image = null;
            picStudent.Tag = null;
            picStudent.Invalidate();

            txtStudentID.ReadOnly = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            dgvStudents.ClearSelection();
        }

        private void btnBrowsePhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select Student Photo";
                ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picStudent.Image = Image.FromFile(ofd.FileName);
                    picStudent.Tag = ofd.FileName;
                    picStudent.Invalidate();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Student s = GetStudentFromInputs();
            if (!ErrorHandler.ValidateStudentInputs(s)) return;

            if (crud.AddStudent(s))
            {
                ErrorHandler.ShowSuccess("Student record added successfully!");
                LoadStudentsToGrid();
                UpdateSummaryCards();
                ClearInputs();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = GetCleanInput(txtStudentID, PH_STUDENT_ID);
            if (!ErrorHandler.ValidateStudentID(id)) return;

            Student s = GetStudentFromInputs();
            if (!ErrorHandler.ValidateStudentInputs(s)) return;

            if (crud.UpdateStudent(s))
            {
                ErrorHandler.ShowSuccess("Student record updated successfully!");
                LoadStudentsToGrid();
                UpdateSummaryCards();
                ClearInputs();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = GetCleanInput(txtStudentID, PH_STUDENT_ID);
            if (!ErrorHandler.ValidateStudentID(id)) return;

            if (ErrorHandler.ConfirmDelete(id))
            {
                if (crud.DeleteStudent(id))
                {
                    ErrorHandler.ShowSuccess("Student record deleted successfully!");
                    LoadStudentsToGrid();
                    UpdateSummaryCards();
                    ClearInputs();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvStudents.Rows[e.RowIndex];

            txtStudentID.Text = row.Cells["student_id"].Value.ToString();
            txtStudentID.ForeColor = Color.FromArgb(15, 23, 42);

            txtFullName.Text = row.Cells["name"].Value.ToString();
            txtFullName.ForeColor = Color.FromArgb(15, 23, 42);

            string gender = row.Cells["gender"].Value.ToString();
            if (gender == "Female") rbFemale.Checked = true;
            else if (gender == "Other") rbOther.Checked = true;
            else rbMale.Checked = true;

            cmbYearLevel.Text = row.Cells["year_level"].Value.ToString();
            cmbProgram.Text = row.Cells["program"].Value.ToString();

            string rawContact = row.Cells["contact_number"].Value.ToString().Replace("-", "");
            txtContact.Text = rawContact;
            txtContact.ForeColor = Color.FromArgb(15, 23, 42);

            string photoPath = row.Cells["photo_path"].Value != DBNull.Value ? row.Cells["photo_path"].Value.ToString() : null;

            if (!string.IsNullOrEmpty(photoPath) && File.Exists(photoPath))
            {
                picStudent.Image = Image.FromFile(photoPath);
                picStudent.Tag = photoPath;
            }
            else
            {
                picStudent.Image = null;
                picStudent.Tag = null;
            }
            picStudent.Invalidate();

            txtStudentID.ReadOnly = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            if (dgvStudents.Rows.Count == 0)
            {
                MessageBox.Show("No student records available to export.", "Export to CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Export Student Records to CSV";
                sfd.Filter = "CSV File (*.csv)|*.csv";
                sfd.FileName = "EduTrack_Students.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Student ID,Full Name,Gender,Year Level,Program,Contact Number");

                        foreach (DataGridViewRow row in dgvStudents.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                string id = row.Cells["student_id"].Value?.ToString() ?? "";
                                string name = "\"" + (row.Cells["name"].Value?.ToString() ?? "") + "\"";
                                string gender = row.Cells["gender"].Value?.ToString() ?? "";
                                string year = row.Cells["year_level"].Value?.ToString() ?? "";
                                string prog = row.Cells["program"].Value?.ToString() ?? "";
                                string contact = row.Cells["contact_number"].Value?.ToString() ?? "";

                                sb.AppendLine(id + "," + name + "," + gender + "," + year + "," + prog + "," + contact);
                            }
                        }

                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                        ErrorHandler.ShowSuccess("Student records exported to CSV successfully!");
                    }
                    catch (Exception ex)
                    {
                        ErrorHandler.ShowDatabaseError(ex, "exporting CSV file");
                    }
                }
            }
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            if (ErrorHandler.ConfirmSignOut())
            {
                LoginForm loginForm = new LoginForm();

                loginForm.StartPosition = FormStartPosition.Manual;
                loginForm.Location = this.Location;
                loginForm.Size = this.Size;
                loginForm.WindowState = this.WindowState;

                loginForm.Show();
                this.Close();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cmbFilterYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cmbFilterProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }
    }
}