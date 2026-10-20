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

            // TODO: 1. Check database connectivity via StudentDatabase.TestConnection() and update lblDbStatus.
            // TODO: 2. Call LoadStudentsToGrid(), UpdateSummaryCards(), and ClearInputs().
        }

        public void LoadStudentsToGrid()
        {
            // TODO: 1. Fetch all students into a DataTable using crud.GetAllStudents().
            // TODO: 2. Bind the DataTable to dgvStudents.DataSource and call FormatStudentGrid().
            // TODO: 3. Update lblRecordCount.Text ("Showing X of Y records") using crud.GetTotalStudentCount().
        }

        private void FormatStudentGrid()
        {
            if (dgvStudents.Columns.Count == 0) return;

            // TODO: Rename column headers for:
            //       "student_id" -> "STUDENT ID"
            //       "name" -> "FULL NAME"
            //       "gender" -> "GENDER"
            //       "year_level" -> "YEAR LEVEL"
            //       "program" -> "PROGRAM"
            //       "contact_number" -> "CONTACT NUMBER"
            // TODO: Hide the "photo_path" column (Visible = false).
        }

        public void UpdateSummaryCards()
        {
            // TODO: 1. Query total student count via crud.GetTotalStudentCount().
            // TODO: 2. Query BSIT and BSCS counts via crud.GetStudentCountByProgram().
            // TODO: 3. Calculate "Other Programs" count (total - bsit - bscs, minimum 0).
            // TODO: 4. Assign the counts to lblTotalCount, lblBsitCount, lblBscsCount, and lblOtherCount.
        }

        public void ApplyFilters()
        {
            if (isInitializing) return;

            // TODO: 1. Get clean search keyword via GetCleanInput(txtSearch, PH_SEARCH).
            // TODO: 2. Read selected filter texts from cmbFilterYear and cmbFilterProgram.
            // TODO: 3. Fetch filtered DataTable via crud.FilterStudents(keyword, yearFilter, programFilter).
            // TODO: 4. Bind to dgvStudents, format grid, and update lblRecordCount.Text.
        }

        public Student GetStudentFromInputs()
        {
            Student s = new Student();

            // TODO: 1. Determine selected gender from rbMale, rbFemale, and rbOther.
            // TODO: 2. Populate Student properties (StudentID, Name, Gender, YearLevel, Program, ContactNumber, PhotoPath)
            //          using GetCleanInput() for textboxes and picStudent.Tag for the photo path.

            return s;
        }

        public void ClearInputs()
        {
            // TODO: 1. Reset txtStudentID, txtFullName, and txtContact to their placeholder states via SetPlaceholderState().
            // TODO: 2. Reset rbMale.Checked = true, and reset cmbYearLevel and cmbProgram to index 0.
            // TODO: 3. Clear picStudent.Image and picStudent.Tag, then call picStudent.Invalidate().
            // TODO: 4. Set txtStudentID.ReadOnly = false, disable btnUpdate and btnDelete, and clear dgvStudents selection.
        }

        private void btnBrowsePhoto_Click(object sender, EventArgs e)
        {
            // TODO: Open an OpenFileDialog filtered for JPG/JPEG/PNG images.
            //       If DialogResult.OK, load the image into picStudent.Image, store FileName in picStudent.Tag, and Invalidate().
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // TODO: 1. Get Student object from GetStudentFromInputs().
            // TODO: 2. Validate inputs with ErrorHandler.ValidateStudentInputs(s).
            // TODO: 3. Call crud.AddStudent(s). If successful, show success message, reload grid, update cards, and clear inputs.
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // TODO: 1. Validate Student ID with ErrorHandler.ValidateStudentID().
            // TODO: 2. Get Student object and validate inputs with ErrorHandler.ValidateStudentInputs().
            // TODO: 3. Call crud.UpdateStudent(s). If successful, show success message, reload grid, update cards, and clear inputs.
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // TODO: 1. Validate Student ID with ErrorHandler.ValidateStudentID().
            // TODO: 2. Ask for confirmation using ErrorHandler.ConfirmDelete(id).
            // TODO: 3. Call crud.DeleteStudent(id). If successful, show success message, reload grid, update cards, and clear inputs.
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // TODO: Call ClearInputs().
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // TODO: 1. Retrieve the clicked DataGridViewRow.
            // TODO: 2. Populate txtStudentID, txtFullName, gender radio buttons, cmbYearLevel, cmbProgram, and txtContact (strip dashes).
            // TODO: 3. Load the student's photo into picStudent if "photo_path" exists on disk; otherwise clear picStudent.
            // TODO: 4. Set txtStudentID.ReadOnly = true, and enable btnUpdate and btnDelete.
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            // TODO: 1. Check if dgvStudents has rows; if empty, show a Warning MessageBox and return.
            // TODO: 2. Open a SaveFileDialog for "*.csv" (default name "EduTrack_Students.csv").
            // TODO: 3. Loop through dgvStudents.Rows, format each row as comma-separated values, and write to file using File.WriteAllText().
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            // TODO: Confirm sign out via ErrorHandler.ConfirmSignOut(). If confirmed, show LoginForm and close MainForm.
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // TODO: Trigger ApplyFilters().
        }

        private void cmbFilterYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Trigger ApplyFilters().
        }

        private void cmbFilterProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Trigger ApplyFilters().
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