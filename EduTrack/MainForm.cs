using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EduTrack
{
    public partial class MainForm : Form
    {
        private StudentCRUD crud = new StudentCRUD();
        private bool isInitializing = false;

        // Pre-input placeholder constants
        private const string PH_STUDENT_ID = "e.g. 2026-0001";
        private const string PH_FULL_NAME = "First & Last Name";
        private const string PH_CONTACT = "09XX-XXX-XXXX";
        private const string PH_SEARCH = "Search by Student ID or Full Name...";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            isInitializing = true;

            // Default dropdown selections
            cmbYearLevel.SelectedIndex = 0;
            cmbProgram.SelectedIndex = 0;
            cmbFilterYear.SelectedIndex = 0;
            cmbFilterProgram.SelectedIndex = 0;

            // Set up pre-input placeholders
            SetupPlaceholder(txtStudentID, PH_STUDENT_ID);
            SetupPlaceholder(txtFullName, PH_FULL_NAME);
            SetupPlaceholder(txtContact, PH_CONTACT);
            SetupPlaceholder(txtSearch, PH_SEARCH);

            // UI Styling Helpers (Pre-configured)
            Color thinBorder = Color.FromArgb(226, 232, 240);
            Color inputBorder = Color.FromArgb(203, 213, 225);

            ApplyRoundedCorners(pnlHeaderLogo, 8, Color.Empty);
            ApplyRoundedCorners(lblAdminBadge, 6, Color.FromArgb(51, 65, 85));
            ApplyRoundedCorners(pnlDbStatusBadge, 6, Color.FromArgb(34, 197, 94));
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

            if (StudentDatabase.TestConnection())
            {
                lblDbStatus.Text = "Connected";
            }
            else
            {
                lblDbStatus.Text = "Disconnected";
            }

            LoadStudentsToGrid();
            UpdateSummaryCards();
            ClearInputs();
        }

        public void LoadStudentsToGrid()
        {
            System.Data.DataTable dt = crud.GetAllStudents();

            dgvStudents.DataSource = dt;
            FormatStudentGrid();

            int currentCount = dt != null ? dt.Rows.Count : 0;
            int totalCount = crud.GetTotalStudentCount();
            lblRecordCount.Text = $"Showing {currentCount} of {totalCount} records";
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

            if (dgvStudents.Columns.Contains("photo_path"))
            {
                dgvStudents.Columns["photo_path"].Visible = false;
            }
        }

        public void UpdateSummaryCards()
        {
            int totalStudents = crud.GetTotalStudentCount();

            int bsitCount = crud.GetStudentCountByProgram("BSIT");
            int bscsCount = crud.GetStudentCountByProgram("BSCS");

            int otherCount = Math.Max(0, totalStudents - bsitCount - bscsCount);

            lblTotalCount.Text = totalStudents.ToString();
            lblBsitCount.Text = bsitCount.ToString();
            lblBscsCount.Text = bscsCount.ToString();
            lblOtherCount.Text = otherCount.ToString();
        }
        public void ApplyFilters()
        {
            if (isInitializing) return;

            string keyword = GetCleanInput(txtSearch, PH_SEARCH);

            string yearFilter = cmbFilterYear.SelectedItem?.ToString() ?? "";
            string programFilter = cmbFilterProgram.SelectedItem?.ToString() ?? "";

            System.Data.DataTable dt = crud.FilterStudents(keyword, yearFilter, programFilter);

            dgvStudents.DataSource = dt;
            FormatStudentGrid();

            int currentCount = dt != null ? dt.Rows.Count : 0;
            int totalCount = crud.GetTotalStudentCount();
            lblRecordCount.Text = $"Showing {currentCount} of {totalCount} records";
        }

        public Student GetStudentFromInputs()
        {
            Student s = new Student();

            string gender = "Other";
            if (rbMale.Checked) gender = "Male";
            else if (rbFemale.Checked) gender = "Female";

            s.StudentID = GetCleanInput(txtStudentID, PH_STUDENT_ID);
            s.Name = GetCleanInput(txtFullName, PH_FULL_NAME);
            s.Gender = gender;
            s.YearLevel = cmbYearLevel.SelectedItem?.ToString() ?? "";
            s.Program = cmbProgram.SelectedItem?.ToString() ?? "";
            s.ContactNumber = GetCleanInput(txtContact, PH_CONTACT);
            s.PhotoPath = picStudent.Tag?.ToString() ?? "";

            return s;
        }

        public void ClearInputs()
        {
            SetPlaceholderState(txtStudentID, PH_STUDENT_ID);
            SetPlaceholderState(txtFullName, PH_FULL_NAME);
            SetPlaceholderState(txtContact, PH_CONTACT);

            rbMale.Checked = true;
            if (cmbYearLevel.Items.Count > 0) cmbYearLevel.SelectedIndex = 0;
            if (cmbProgram.Items.Count > 0) cmbProgram.SelectedIndex = 0;

            if (picStudent.Image != null)
            {
                picStudent.Image.Dispose(); // Free resources up safely
                picStudent.Image = null;
            }
            picStudent.Tag = null;
            picStudent.Invalidate(); // Forces a redraw to clean up custom paint effects

            txtStudentID.ReadOnly = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            dgvStudents.ClearSelection();
        }

        private void btnBrowsePhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                ofd.Title = "Select Student Photo";

                // If DialogResult.OK, load the image into picStudent.Image, store FileName in picStudent.Tag, and Invalidate().
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (picStudent.Image != null) picStudent.Image.Dispose(); // Free memory safely

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
                MessageBox.Show("Student successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadStudentsToGrid();
                UpdateSummaryCards();
                ClearInputs();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string studentId = GetCleanInput(txtStudentID, PH_STUDENT_ID);

            if (!ErrorHandler.ValidateStudentID(studentId)) return;

            Student s = GetStudentFromInputs();
            if (!ErrorHandler.ValidateStudentInputs(s)) return;

            // TODO: 3. Call crud.UpdateStudent(s). If successful, show success message, reload grid, update cards, and clear inputs.
            if (crud.UpdateStudent(s))
            {
                ErrorHandler.ShowSuccess("Student profile successfully updated!");
                LoadStudentsToGrid();
                UpdateSummaryCards();
                ClearInputs();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string studentId = GetCleanInput(txtStudentID, PH_STUDENT_ID);

            if (!ErrorHandler.ValidateStudentID(studentId)) return;

            if (ErrorHandler.ConfirmDelete(studentId))
            {
                if (crud.DeleteStudent(studentId))
                {
                    MessageBox.Show("Student record successfully deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            txtStudentID.Text = row.Cells["student_id"].Value?.ToString() ?? "";
            txtStudentID.ForeColor = Color.Black; // Remove placeholder style color if text changes

            txtFullName.Text = row.Cells["name"].Value?.ToString() ?? "";
            txtFullName.ForeColor = Color.Black;

            string gender = row.Cells["gender"].Value?.ToString() ?? "Other";
            if (gender == "Male") rbMale.Checked = true;
            else if (gender == "Female") rbFemale.Checked = true;
            else rbOther.Checked = true;

            cmbYearLevel.SelectedItem = row.Cells["year_level"].Value?.ToString();
            cmbProgram.SelectedItem = row.Cells["program"].Value?.ToString();

            string contact = row.Cells["contact_number"].Value?.ToString() ?? "";
            txtContact.Text = contact.Replace("-", ""); // Strip dashes
            txtContact.ForeColor = Color.Black;

            if (picStudent.Image != null) picStudent.Image.Dispose(); // Clear old cached allocations safely

            string photoPath = row.Cells["photo_path"].Value?.ToString() ?? "";
            if (!string.IsNullOrWhiteSpace(photoPath) && System.IO.File.Exists(photoPath))
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
            // TODO: 1. Check if dgvStudents has rows; if empty, show a Warning MessageBox and return.
            if (dgvStudents.Rows.Count == 0)
            {
                MessageBox.Show("There are no records to export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TODO: 2. Open a SaveFileDialog for "*.csv" (default name "EduTrack_Students.csv").
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.FileName = "EduTrack_Students.csv";
                sfd.Title = "Export Data to CSV";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        // Build CSV Headers dynamically based on what's visible/hidden
                        List<string> headers = new List<string>();
                        foreach (DataGridViewColumn col in dgvStudents.Columns)
                        {
                            if (col.Visible) headers.Add($"\"{col.HeaderText}\"");
                        }
                        sb.AppendLine(string.Join(",", headers));

                        foreach (DataGridViewRow row in dgvStudents.Rows)
                        {
                            if (row.IsNewRow) continue;

                            List<string> cells = new List<string>();
                            foreach (DataGridViewColumn col in dgvStudents.Columns)
                            {
                                if (col.Visible)
                                {
                                    string cellValue = row.Cells[col.Index].Value?.ToString() ?? "";
                                    // Escape quotation marks inside strings if necessary to maintain proper CSV structure
                                    cellValue = cellValue.Replace("\"", "\"\"");
                                    cells.Add($"\"{cellValue}\"");
                                }
                            }
                            sb.AppendLine(string.Join(",", cells));
                        }

                        System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), System.Text.Encoding.UTF8);
                        MessageBox.Show("Data successfully exported to CSV!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // Show error if this is ever possible
                        MessageBox.Show($"An error occurred while exporting: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void btnSignOut_Click(object sender, EventArgs e)
        {
            if (ErrorHandler.ConfirmSignOut())
            {
                LoginForm loginForm = new LoginForm();
                loginForm.Show(); // Display the login window

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

        #region UI & Placeholder Helpers (Do Not Modify)
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
            return val == placeholderText ? "" : val;
        }

        private void picStudent_Paint(object sender, PaintEventArgs e)
        {
            if (picStudent.Image == null)
            {
                using (Font iconFont = new Font("Segoe UI Emoji", 24F, FontStyle.Regular))
                using (SolidBrush iconBrush = new SolidBrush(Color.FromArgb(59, 130, 246)))
                {
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
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
        #endregion
    }
}