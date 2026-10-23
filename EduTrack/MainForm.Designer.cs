namespace EduTrack
{
    partial class MainForm
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
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle defaultCellStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle altRowStyle = new DataGridViewCellStyle();

            this.pnlHeader = new Panel();
            this.pnlHeaderLogo = new Panel();
            this.picHeaderLogo = new PictureBox();
            this.lblHeaderIcon = new Label();
            this.lblAppTitle = new Label();
            this.lblAppSubtitle = new Label();
            this.lblAdminBadge = new Label();
            this.pnlDbStatusBadge = new Panel();
            this.lblDbStatus = new Label();
            this.btnSignOut = new Button();

            this.flpStats = new FlowLayoutPanel();
            this.pnlCardTotal = new Panel();
            this.pnlAccentTotal = new Panel();
            this.lblTitleTotal = new Label();
            this.lblTotalCount = new Label();
            this.lblIconTotal = new Label();

            this.pnlCardBsit = new Panel();
            this.pnlAccentBsit = new Panel();
            this.lblTitleBsit = new Label();
            this.lblBsitCount = new Label();
            this.lblIconBsit = new Label();

            this.pnlCardBscs = new Panel();
            this.pnlAccentBscs = new Panel();
            this.lblTitleBscs = new Label();
            this.lblBscsCount = new Label();
            this.lblIconBscs = new Label();

            this.pnlCardOther = new Panel();
            this.pnlAccentOther = new Panel();
            this.lblTitleOther = new Label();
            this.lblOtherCount = new Label();
            this.lblIconOther = new Label();

            this.pnlStudentDetails = new Panel();
            this.pnlDetailsHeader = new Panel();
            this.lblDetailsHeaderTitle = new Label();
            this.picStudent = new PictureBox();
            this.btnBrowsePhoto = new Button();
            this.lblPhotoHint = new Label();
            this.lblStudentID = new Label();
            this.txtStudentID = new TextBox();
            this.lblFullName = new Label();
            this.txtFullName = new TextBox();
            this.lblGender = new Label();
            this.rbMale = new RadioButton();
            this.rbFemale = new RadioButton();
            this.rbOther = new RadioButton();
            this.lblYearLevel = new Label();
            this.cmbYearLevel = new ComboBox();
            this.lblProgram = new Label();
            this.cmbProgram = new ComboBox();
            this.lblContact = new Label();
            this.txtContact = new TextBox();
            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnClear = new Button();

            this.pnlFilterBar = new Panel();
            this.lblFilterLabel = new Label();
            this.txtSearch = new TextBox();
            this.cmbFilterYear = new ComboBox();
            this.cmbFilterProgram = new ComboBox();

            this.pnlGridContainer = new Panel();
            this.dgvStudents = new DataGridView();
            this.lblRecordCount = new Label();
            this.btnExportCsv = new Button();

            this.pnlHeader.SuspendLayout();
            this.pnlHeaderLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeaderLogo)).BeginInit();
            this.pnlDbStatusBadge.SuspendLayout();
            this.flpStats.SuspendLayout();
            this.pnlCardTotal.SuspendLayout();
            this.pnlCardBsit.SuspendLayout();
            this.pnlCardBscs.SuspendLayout();
            this.pnlCardOther.SuspendLayout();
            this.pnlStudentDetails.SuspendLayout();
            this.pnlDetailsHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStudent)).BeginInit();
            this.pnlFilterBar.SuspendLayout();
            this.pnlGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = Color.FromArgb(15, 23, 42);
            this.pnlHeader.Controls.Add(this.pnlHeaderLogo);
            this.pnlHeader.Controls.Add(this.lblAppTitle);
            this.pnlHeader.Controls.Add(this.lblAppSubtitle);
            this.pnlHeader.Controls.Add(this.lblAdminBadge);
            this.pnlHeader.Controls.Add(this.pnlDbStatusBadge);
            this.pnlHeader.Controls.Add(this.btnSignOut);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Location = new Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new Size(1220, 62);
            this.pnlHeader.TabIndex = 0;

            // pnlHeaderLogo
            this.pnlHeaderLogo.BackColor = Color.Transparent;
            this.pnlHeaderLogo.Controls.Add(this.picHeaderLogo);
            this.pnlHeaderLogo.Controls.Add(this.lblHeaderIcon);
            this.pnlHeaderLogo.Location = new Point(18, 7);
            this.pnlHeaderLogo.Name = "pnlHeaderLogo";
            this.pnlHeaderLogo.Size = new Size(48, 48);
            this.pnlHeaderLogo.TabIndex = 0;

            // picHeaderLogo
            this.picHeaderLogo.Dock = DockStyle.Fill;
            this.picHeaderLogo.Location = new Point(0, 0);
            this.picHeaderLogo.Name = "picHeaderLogo";
            this.picHeaderLogo.Size = new Size(48, 48);
            this.picHeaderLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.picHeaderLogo.TabIndex = 1;
            this.picHeaderLogo.TabStop = false;
            this.picHeaderLogo.Visible = false;

            // lblHeaderIcon
            this.lblHeaderIcon.Dock = DockStyle.Fill;
            this.lblHeaderIcon.Font = new Font("Segoe UI Emoji", 14F, FontStyle.Bold);
            this.lblHeaderIcon.ForeColor = Color.White;
            this.lblHeaderIcon.Location = new Point(0, 0);
            this.lblHeaderIcon.Name = "lblHeaderIcon";
            this.lblHeaderIcon.Size = new Size(38, 38);
            this.lblHeaderIcon.TabIndex = 0;
            this.lblHeaderIcon.Text = "🎓";
            this.lblHeaderIcon.TextAlign = ContentAlignment.MiddleCenter;

            // lblAppTitle
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new Font("Plus Jakarta Sans", 13F, FontStyle.Bold);
            this.lblAppTitle.ForeColor = Color.White;
            this.lblAppTitle.Location = new Point(66, 10);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new Size(102, 25);
            this.lblAppTitle.TabIndex = 1;
            this.lblAppTitle.Text = "EDUTRACK";

            // lblAppSubtitle
            this.lblAppSubtitle.AutoSize = true;
            this.lblAppSubtitle.Font = new Font("Inter", 8.5F, FontStyle.Regular);
            this.lblAppSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            this.lblAppSubtitle.Location = new Point(67, 34);
            this.lblAppSubtitle.Name = "lblAppSubtitle";
            this.lblAppSubtitle.Size = new Size(218, 15);
            this.lblAppSubtitle.TabIndex = 2;
            this.lblAppSubtitle.Text = "Student Information & Directory System";
            this.lblAppSubtitle.UseMnemonic = false;

            // lblAdminBadge
            this.lblAdminBadge.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.lblAdminBadge.BackColor = Color.FromArgb(30, 41, 59);
            this.lblAdminBadge.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold);
            this.lblAdminBadge.ForeColor = Color.White;
            this.lblAdminBadge.Location = new Point(865, 16);
            this.lblAdminBadge.Name = "lblAdminBadge";
            this.lblAdminBadge.Size = new Size(85, 30);
            this.lblAdminBadge.TabIndex = 3;
            this.lblAdminBadge.Text = "👤 Admin";
            this.lblAdminBadge.TextAlign = ContentAlignment.MiddleCenter;

            // pnlDbStatusBadge
            this.pnlDbStatusBadge.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.pnlDbStatusBadge.BackColor = Color.FromArgb(30, 41, 59);
            this.pnlDbStatusBadge.Controls.Add(this.lblDbStatus);
            this.pnlDbStatusBadge.Location = new Point(960, 16);
            this.pnlDbStatusBadge.Name = "pnlDbStatusBadge";
            this.pnlDbStatusBadge.Size = new Size(135, 30);
            this.pnlDbStatusBadge.TabIndex = 4;

            // lblDbStatus
            this.lblDbStatus.Dock = DockStyle.Fill;
            this.lblDbStatus.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold);
            this.lblDbStatus.ForeColor = Color.FromArgb(16, 185, 129);
            this.lblDbStatus.Location = new Point(0, 0);
            this.lblDbStatus.Name = "lblDbStatus";
            this.lblDbStatus.Size = new Size(135, 30);
            this.lblDbStatus.TabIndex = 0;
            this.lblDbStatus.Text = "🟢  DB Connected";
            this.lblDbStatus.TextAlign = ContentAlignment.MiddleCenter;

            // btnSignOut
            this.btnSignOut.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.btnSignOut.BackColor = Color.FromArgb(244, 63, 94);
            this.btnSignOut.Cursor = Cursors.Hand;
            this.btnSignOut.FlatAppearance.BorderSize = 0;
            this.btnSignOut.FlatStyle = FlatStyle.Flat;
            this.btnSignOut.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold);
            this.btnSignOut.ForeColor = Color.White;
            this.btnSignOut.Location = new Point(1105, 16);
            this.btnSignOut.Name = "btnSignOut";
            this.btnSignOut.Size = new Size(95, 30);
            this.btnSignOut.TabIndex = 5;
            this.btnSignOut.Text = "➔ Sign Out";
            this.btnSignOut.UseVisualStyleBackColor = false;
            this.btnSignOut.Click += new System.EventHandler(this.btnSignOut_Click);

            // flpStats
            this.flpStats.BackColor = Color.Transparent;
            this.flpStats.Controls.Add(this.pnlCardTotal);
            this.flpStats.Controls.Add(this.pnlCardBsit);
            this.flpStats.Controls.Add(this.pnlCardBscs);
            this.flpStats.Controls.Add(this.pnlCardOther);
            this.flpStats.Location = new Point(350, 76);
            this.flpStats.Name = "flpStats";
            this.flpStats.Size = new Size(855, 86);
            this.flpStats.TabIndex = 1;

            // pnlCardTotal
            this.pnlCardTotal.BackColor = Color.White;
            this.pnlCardTotal.Controls.Add(this.lblIconTotal);
            this.pnlCardTotal.Controls.Add(this.pnlAccentTotal);
            this.pnlCardTotal.Controls.Add(this.lblTitleTotal);
            this.pnlCardTotal.Controls.Add(this.lblTotalCount);
            this.pnlCardTotal.Location = new Point(0, 0);
            this.pnlCardTotal.Margin = new Padding(0, 0, 14, 0);
            this.pnlCardTotal.Name = "pnlCardTotal";
            this.pnlCardTotal.Size = new Size(202, 80);
            this.pnlCardTotal.TabIndex = 0;

            // lblIconTotal
            this.lblIconTotal.AutoSize = true;
            this.lblIconTotal.Font = new Font("Segoe UI Emoji", 26F, FontStyle.Regular);
            this.lblIconTotal.ForeColor = Color.FromArgb(79, 70, 229);
            this.lblIconTotal.Location = new Point(145, 18);
            this.lblIconTotal.Name = "lblIconTotal";
            this.lblIconTotal.Size = new Size(40, 47);
            this.lblIconTotal.TabIndex = 3;
            this.lblIconTotal.Text = "👥";

            this.pnlAccentTotal.BackColor = Color.FromArgb(79, 70, 229);
            this.pnlAccentTotal.Dock = DockStyle.Left;
            this.pnlAccentTotal.Size = new Size(5, 80);

            this.lblTitleTotal.AutoSize = true;
            this.lblTitleTotal.Font = new Font("Inter", 8.5F, FontStyle.Bold);
            this.lblTitleTotal.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblTitleTotal.Location = new Point(16, 12);
            this.lblTitleTotal.Text = "TOTAL STUDENTS";

            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Font = new Font("Plus Jakarta Sans", 20F, FontStyle.Bold);
            this.lblTotalCount.ForeColor = Color.FromArgb(79, 70, 229);
            this.lblTotalCount.Location = new Point(14, 32);
            this.lblTotalCount.Text = "0";

            // pnlCardBsit
            this.pnlCardBsit.BackColor = Color.White;
            this.pnlCardBsit.Controls.Add(this.lblIconBsit);
            this.pnlCardBsit.Controls.Add(this.pnlAccentBsit);
            this.pnlCardBsit.Controls.Add(this.lblTitleBsit);
            this.pnlCardBsit.Controls.Add(this.lblBsitCount);
            this.pnlCardBsit.Location = new Point(216, 0);
            this.pnlCardBsit.Margin = new Padding(0, 0, 14, 0);
            this.pnlCardBsit.Name = "pnlCardBsit";
            this.pnlCardBsit.Size = new Size(202, 80);
            this.pnlCardBsit.TabIndex = 1;

            // lblIconBsit
            this.lblIconBsit.AutoSize = true;
            this.lblIconBsit.Font = new Font("Segoe UI Emoji", 26F, FontStyle.Regular);
            this.lblIconBsit.ForeColor = Color.FromArgb(16, 185, 129);
            this.lblIconBsit.Location = new Point(145, 18);
            this.lblIconBsit.Name = "lblIconBsit";
            this.lblIconBsit.Size = new Size(40, 47);
            this.lblIconBsit.TabIndex = 3;
            this.lblIconBsit.Text = "💻";

            this.pnlAccentBsit.BackColor = Color.FromArgb(16, 185, 129);
            this.pnlAccentBsit.Dock = DockStyle.Left;
            this.pnlAccentBsit.Size = new Size(5, 80);

            this.lblTitleBsit.AutoSize = true;
            this.lblTitleBsit.Font = new Font("Inter", 8.5F, FontStyle.Bold);
            this.lblTitleBsit.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblTitleBsit.Location = new Point(16, 12);
            this.lblTitleBsit.Text = "BSIT ENROLLED";

            this.lblBsitCount.AutoSize = true;
            this.lblBsitCount.Font = new Font("Plus Jakarta Sans", 20F, FontStyle.Bold);
            this.lblBsitCount.ForeColor = Color.FromArgb(16, 185, 129);
            this.lblBsitCount.Location = new Point(14, 32);
            this.lblBsitCount.Text = "0";

            // pnlCardBscs
            this.pnlCardBscs.BackColor = Color.White;
            this.pnlCardBscs.Controls.Add(this.lblIconBscs);
            this.pnlCardBscs.Controls.Add(this.pnlAccentBscs);
            this.pnlCardBscs.Controls.Add(this.lblTitleBscs);
            this.pnlCardBscs.Controls.Add(this.lblBscsCount);
            this.pnlCardBscs.Location = new Point(432, 0);
            this.pnlCardBscs.Margin = new Padding(0, 0, 14, 0);
            this.pnlCardBscs.Name = "pnlCardBscs";
            this.pnlCardBscs.Size = new Size(202, 80);
            this.pnlCardBscs.TabIndex = 2;

            // lblIconBscs
            this.lblIconBscs.AutoSize = true;
            this.lblIconBscs.Font = new Font("Segoe UI Emoji", 26F, FontStyle.Regular);
            this.lblIconBscs.ForeColor = Color.FromArgb(245, 158, 11);
            this.lblIconBscs.Location = new Point(145, 18);
            this.lblIconBscs.Name = "lblIconBscs";
            this.lblIconBscs.Size = new Size(40, 47);
            this.lblIconBscs.TabIndex = 3;
            this.lblIconBscs.Text = "⌨️";

            this.pnlAccentBscs.BackColor = Color.FromArgb(245, 158, 11);
            this.pnlAccentBscs.Dock = DockStyle.Left;
            this.pnlAccentBscs.Size = new Size(5, 80);

            this.lblTitleBscs.AutoSize = true;
            this.lblTitleBscs.Font = new Font("Inter", 8.5F, FontStyle.Bold);
            this.lblTitleBscs.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblTitleBscs.Location = new Point(16, 12);
            this.lblTitleBscs.Text = "BSCS ENROLLED";

            this.lblBscsCount.AutoSize = true;
            this.lblBscsCount.Font = new Font("Plus Jakarta Sans", 20F, FontStyle.Bold);
            this.lblBscsCount.ForeColor = Color.FromArgb(245, 158, 11);
            this.lblBscsCount.Location = new Point(14, 32);
            this.lblBscsCount.Text = "0";

            // pnlCardOther
            this.pnlCardOther.BackColor = Color.White;
            this.pnlCardOther.Controls.Add(this.lblIconOther);
            this.pnlCardOther.Controls.Add(this.pnlAccentOther);
            this.pnlCardOther.Controls.Add(this.lblTitleOther);
            this.pnlCardOther.Controls.Add(this.lblOtherCount);
            this.pnlCardOther.Location = new Point(648, 0);
            this.pnlCardOther.Margin = new Padding(0);
            this.pnlCardOther.Name = "pnlCardOther";
            this.pnlCardOther.Size = new Size(202, 80);
            this.pnlCardOther.TabIndex = 3;

            // lblIconOther
            this.lblIconOther.AutoSize = true;
            this.lblIconOther.Font = new Font("Segoe UI Emoji", 26F, FontStyle.Regular);
            this.lblIconOther.ForeColor = Color.FromArgb(139, 92, 246);
            this.lblIconOther.Location = new Point(145, 18);
            this.lblIconOther.Name = "lblIconOther";
            this.lblIconOther.Size = new Size(40, 47);
            this.lblIconOther.TabIndex = 3;
            this.lblIconOther.Text = "📁";

            this.pnlAccentOther.BackColor = Color.FromArgb(139, 92, 246);
            this.pnlAccentOther.Dock = DockStyle.Left;
            this.pnlAccentOther.Size = new Size(5, 80);

            this.lblTitleOther.AutoSize = true;
            this.lblTitleOther.Font = new Font("Inter", 8.5F, FontStyle.Bold);
            this.lblTitleOther.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblTitleOther.Location = new Point(16, 12);
            this.lblTitleOther.Text = "OTHER PROGRAMS";

            this.lblOtherCount.AutoSize = true;
            this.lblOtherCount.Font = new Font("Plus Jakarta Sans", 20F, FontStyle.Bold);
            this.lblOtherCount.ForeColor = Color.FromArgb(139, 92, 246);
            this.lblOtherCount.Location = new Point(14, 32);
            this.lblOtherCount.Text = "0";

            // pnlStudentDetails
            this.pnlStudentDetails.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left)));
            this.pnlStudentDetails.BackColor = Color.White;
            this.pnlStudentDetails.Controls.Add(this.pnlDetailsHeader);
            this.pnlStudentDetails.Controls.Add(this.picStudent);
            this.pnlStudentDetails.Controls.Add(this.btnBrowsePhoto);
            this.pnlStudentDetails.Controls.Add(this.lblPhotoHint);
            this.pnlStudentDetails.Controls.Add(this.lblStudentID);
            this.pnlStudentDetails.Controls.Add(this.txtStudentID);
            this.pnlStudentDetails.Controls.Add(this.lblFullName);
            this.pnlStudentDetails.Controls.Add(this.txtFullName);
            this.pnlStudentDetails.Controls.Add(this.lblGender);
            this.pnlStudentDetails.Controls.Add(this.rbMale);
            this.pnlStudentDetails.Controls.Add(this.rbFemale);
            this.pnlStudentDetails.Controls.Add(this.rbOther);
            this.pnlStudentDetails.Controls.Add(this.lblYearLevel);
            this.pnlStudentDetails.Controls.Add(this.cmbYearLevel);
            this.pnlStudentDetails.Controls.Add(this.lblProgram);
            this.pnlStudentDetails.Controls.Add(this.cmbProgram);
            this.pnlStudentDetails.Controls.Add(this.lblContact);
            this.pnlStudentDetails.Controls.Add(this.txtContact);
            this.pnlStudentDetails.Controls.Add(this.btnAdd);
            this.pnlStudentDetails.Controls.Add(this.btnUpdate);
            this.pnlStudentDetails.Controls.Add(this.btnDelete);
            this.pnlStudentDetails.Controls.Add(this.btnClear);
            this.pnlStudentDetails.Location = new Point(18, 76);
            this.pnlStudentDetails.Name = "pnlStudentDetails";
            this.pnlStudentDetails.Size = new Size(316, 610);
            this.pnlStudentDetails.TabIndex = 2;

            // pnlDetailsHeader
            this.pnlDetailsHeader.BackColor = Color.FromArgb(15, 23, 42);
            this.pnlDetailsHeader.Controls.Add(this.lblDetailsHeaderTitle);
            this.pnlDetailsHeader.Dock = DockStyle.Top;
            this.pnlDetailsHeader.Location = new Point(0, 0);
            this.pnlDetailsHeader.Name = "pnlDetailsHeader";
            this.pnlDetailsHeader.Size = new Size(316, 42);
            this.pnlDetailsHeader.TabIndex = 0;

            // lblDetailsHeaderTitle
            this.lblDetailsHeaderTitle.AutoSize = true;
            this.lblDetailsHeaderTitle.Font = new Font("Plus Jakarta Sans", 10F, FontStyle.Bold);
            this.lblDetailsHeaderTitle.ForeColor = Color.White;
            this.lblDetailsHeaderTitle.Location = new Point(16, 11);
            this.lblDetailsHeaderTitle.Name = "lblDetailsHeaderTitle";
            this.lblDetailsHeaderTitle.Size = new Size(160, 19);
            this.lblDetailsHeaderTitle.TabIndex = 0;
            this.lblDetailsHeaderTitle.Text = "📋 STUDENT DETAILS";

            // picStudent
            this.picStudent.BackColor = Color.FromArgb(224, 242, 254);
            this.picStudent.BorderStyle = BorderStyle.None;
            this.picStudent.Location = new Point(18, 56);
            this.picStudent.Name = "picStudent";
            this.picStudent.Size = new Size(76, 76);
            this.picStudent.SizeMode = PictureBoxSizeMode.Zoom;
            this.picStudent.TabIndex = 1;
            this.picStudent.TabStop = false;

            // btnBrowsePhoto
            this.btnBrowsePhoto.BackColor = Color.FromArgb(241, 245, 249);
            this.btnBrowsePhoto.Cursor = Cursors.Hand;
            this.btnBrowsePhoto.FlatAppearance.BorderSize = 0;
            this.btnBrowsePhoto.FlatStyle = FlatStyle.Flat;
            this.btnBrowsePhoto.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold);
            this.btnBrowsePhoto.ForeColor = Color.FromArgb(79, 70, 229);
            this.btnBrowsePhoto.Location = new Point(108, 66);
            this.btnBrowsePhoto.Name = "btnBrowsePhoto";
            this.btnBrowsePhoto.Size = new Size(190, 32);
            this.btnBrowsePhoto.TabIndex = 2;
            this.btnBrowsePhoto.Text = "📷 Browse Photo...";
            this.btnBrowsePhoto.UseVisualStyleBackColor = false;
            this.btnBrowsePhoto.Click += new System.EventHandler(this.btnBrowsePhoto_Click);

            // lblPhotoHint
            this.lblPhotoHint.AutoSize = true;
            this.lblPhotoHint.Font = new Font("Inter", 8F, FontStyle.Regular);
            this.lblPhotoHint.ForeColor = Color.FromArgb(148, 163, 184);
            this.lblPhotoHint.Location = new Point(108, 103);
            this.lblPhotoHint.Name = "lblPhotoHint";
            this.lblPhotoHint.Size = new Size(99, 13);
            this.lblPhotoHint.TabIndex = 3;
            this.lblPhotoHint.Text = "Allowed: JPG, PNG";

            // lblStudentID
            this.lblStudentID.AutoSize = true;
            this.lblStudentID.Font = new Font("Inter", 9F, FontStyle.Bold);
            this.lblStudentID.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblStudentID.Location = new Point(18, 146);
            this.lblStudentID.Name = "lblStudentID";
            this.lblStudentID.Size = new Size(68, 15);
            this.lblStudentID.TabIndex = 4;
            this.lblStudentID.Text = "Student ID:";

            // txtStudentID
            this.txtStudentID.BackColor = Color.FromArgb(248, 250, 252);
            this.txtStudentID.BorderStyle = BorderStyle.None;
            this.txtStudentID.Font = new Font("Inter", 10F);
            this.txtStudentID.Location = new Point(26, 170);
            this.txtStudentID.Name = "txtStudentID";
            this.txtStudentID.Size = new Size(264, 18);
            this.txtStudentID.TabIndex = 5;

            // lblFullName
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new Font("Inter", 9F, FontStyle.Bold);
            this.lblFullName.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblFullName.Location = new Point(18, 202);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new Size(65, 15);
            this.lblFullName.TabIndex = 6;
            this.lblFullName.Text = "Full Name:";

            // txtFullName
            this.txtFullName.BackColor = Color.FromArgb(248, 250, 252);
            this.txtFullName.BorderStyle = BorderStyle.None;
            this.txtFullName.Font = new Font("Inter", 10F);
            this.txtFullName.Location = new Point(26, 226);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new Size(264, 18);
            this.txtFullName.TabIndex = 7;

            // lblGender
            this.lblGender.AutoSize = true;
            this.lblGender.Font = new Font("Inter", 9F, FontStyle.Bold);
            this.lblGender.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblGender.Location = new Point(18, 258);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new Size(50, 15);
            this.lblGender.TabIndex = 8;
            this.lblGender.Text = "Gender:";

            // rbMale
            this.rbMale.AutoSize = true;
            this.rbMale.Checked = true;
            this.rbMale.Font = new Font("Inter", 9.5F);
            this.rbMale.ForeColor = Color.FromArgb(51, 65, 85);
            this.rbMale.Location = new Point(20, 279);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new Size(53, 21);
            this.rbMale.TabIndex = 9;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "Male";
            this.rbMale.UseVisualStyleBackColor = true;

            // rbFemale
            this.rbFemale.AutoSize = true;
            this.rbFemale.Font = new Font("Inter", 9.5F);
            this.rbFemale.ForeColor = Color.FromArgb(51, 65, 85);
            this.rbFemale.Location = new Point(92, 279);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new Size(65, 21);
            this.rbFemale.TabIndex = 10;
            this.rbFemale.Text = "Female";
            this.rbFemale.UseVisualStyleBackColor = true;

            // rbOther
            this.rbOther.AutoSize = true;
            this.rbOther.Font = new Font("Inter", 9.5F);
            this.rbOther.ForeColor = Color.FromArgb(51, 65, 85);
            this.rbOther.Location = new Point(176, 279);
            this.rbOther.Name = "rbOther";
            this.rbOther.Size = new Size(58, 21);
            this.rbOther.TabIndex = 11;
            this.rbOther.Text = "Other";
            this.rbOther.UseVisualStyleBackColor = true;

            // lblYearLevel
            this.lblYearLevel.AutoSize = true;
            this.lblYearLevel.Font = new Font("Inter", 9F, FontStyle.Bold);
            this.lblYearLevel.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblYearLevel.Location = new Point(18, 312);
            this.lblYearLevel.Name = "lblYearLevel";
            this.lblYearLevel.Size = new Size(67, 15);
            this.lblYearLevel.TabIndex = 12;
            this.lblYearLevel.Text = "Year Level:";

            // cmbYearLevel
            this.cmbYearLevel.BackColor = Color.FromArgb(248, 250, 252);
            this.cmbYearLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbYearLevel.FlatStyle = FlatStyle.Flat;
            this.cmbYearLevel.Font = new Font("Inter", 10F);
            this.cmbYearLevel.Items.AddRange(new object[] {
                "-- Select Year --",
                "1st Year",
                "2nd Year",
                "3rd Year",
                "4th Year"
            });
            this.cmbYearLevel.Location = new Point(18, 331);
            this.cmbYearLevel.Name = "cmbYearLevel";
            this.cmbYearLevel.Size = new Size(280, 25);
            this.cmbYearLevel.TabIndex = 13;

            // lblProgram
            this.lblProgram.AutoSize = true;
            this.lblProgram.Font = new Font("Inter", 9F, FontStyle.Bold);
            this.lblProgram.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblProgram.Location = new Point(18, 368);
            this.lblProgram.Name = "lblProgram";
            this.lblProgram.Size = new Size(58, 15);
            this.lblProgram.TabIndex = 14;
            this.lblProgram.Text = "Program:";

            // cmbProgram
            this.cmbProgram.BackColor = Color.FromArgb(248, 250, 252);
            this.cmbProgram.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbProgram.FlatStyle = FlatStyle.Flat;
            this.cmbProgram.Font = new Font("Inter", 10F);
            this.cmbProgram.Items.AddRange(new object[] {
                "-- Select Program --",
                "BSIT",
                "BSCS",
                "BSIS",
                "BSHM",
                "BSED",
                "BSBA"
            });
            this.cmbProgram.Location = new Point(18, 387);
            this.cmbProgram.Name = "cmbProgram";
            this.cmbProgram.Size = new Size(280, 25);
            this.cmbProgram.TabIndex = 15;

            // lblContact
            this.lblContact.AutoSize = true;
            this.lblContact.Font = new Font("Inter", 9F, FontStyle.Bold);
            this.lblContact.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblContact.Location = new Point(18, 424);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new Size(73, 15);
            this.lblContact.TabIndex = 16;
            this.lblContact.Text = "Contact No.:";

            // txtContact
            this.txtContact.BackColor = Color.FromArgb(248, 250, 252);
            this.txtContact.BorderStyle = BorderStyle.None;
            this.txtContact.Font = new Font("Inter", 10F);
            this.txtContact.Location = new Point(26, 448);
            this.txtContact.MaxLength = 11;
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new Size(264, 18);
            this.txtContact.TabIndex = 17;

            // btnAdd
            this.btnAdd.BackColor = Color.FromArgb(79, 70, 229);
            this.btnAdd.Cursor = Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.Font = new Font("Segoe UI Emoji", 9.5F, FontStyle.Bold);
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Location = new Point(18, 532);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(134, 38);
            this.btnAdd.TabIndex = 18;
            this.btnAdd.Text = "➕ Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnUpdate
            this.btnUpdate.BackColor = Color.FromArgb(16, 185, 129); 
            this.btnUpdate.Cursor = Cursors.Hand;
            this.btnUpdate.Enabled = false;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = FlatStyle.Flat;
            this.btnUpdate.Font = new Font("Segoe UI Emoji", 9.5F, FontStyle.Bold);
            this.btnUpdate.ForeColor = Color.White;
            this.btnUpdate.Location = new Point(164, 532);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new Size(134, 38);
            this.btnUpdate.TabIndex = 19;
            this.btnUpdate.Text = "✏️ Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            // btnDelete
            this.btnDelete.BackColor = Color.FromArgb(244, 63, 94); 
            this.btnDelete.Cursor = Cursors.Hand;
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.Font = new Font("Segoe UI Emoji", 9.5F, FontStyle.Bold);
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.Location = new Point(18, 582);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(134, 38);
            this.btnDelete.TabIndex = 20;
            this.btnDelete.Text = "🗑️ Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnClear
            this.btnClear.BackColor = Color.FromArgb(226, 232, 240);
            this.btnClear.Cursor = Cursors.Hand;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.Font = new Font("Segoe UI Emoji", 9.5F, FontStyle.Bold);
            this.btnClear.ForeColor = Color.FromArgb(51, 65, 85);
            this.btnClear.Location = new Point(164, 582);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(134, 38);
            this.btnClear.TabIndex = 21;
            this.btnClear.Text = "🔄 Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // pnlFilterBar
            this.pnlFilterBar.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            this.pnlFilterBar.BackColor = Color.White;
            this.pnlFilterBar.Controls.Add(this.lblFilterLabel);
            this.pnlFilterBar.Controls.Add(this.txtSearch);
            this.pnlFilterBar.Controls.Add(this.cmbFilterYear);
            this.pnlFilterBar.Controls.Add(this.cmbFilterProgram);
            this.pnlFilterBar.Location = new Point(350, 170);
            this.pnlFilterBar.Name = "pnlFilterBar";
            this.pnlFilterBar.Size = new Size(850, 52);
            this.pnlFilterBar.TabIndex = 3;

            // lblFilterLabel
            this.lblFilterLabel.AutoSize = true;
            this.lblFilterLabel.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold);
            this.lblFilterLabel.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblFilterLabel.Location = new Point(14, 17);
            this.lblFilterLabel.Name = "lblFilterLabel";
            this.lblFilterLabel.Size = new Size(63, 15);
            this.lblFilterLabel.TabIndex = 0;
            this.lblFilterLabel.Text = "🔍 FILTER:";

            // txtSearch
            this.txtSearch.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            this.txtSearch.BackColor = Color.FromArgb(248, 250, 252);
            this.txtSearch.BorderStyle = BorderStyle.None;
            this.txtSearch.Font = new Font("Inter", 10F);
            this.txtSearch.Location = new Point(96, 17);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new Size(399, 18);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // cmbFilterYear
            this.cmbFilterYear.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.cmbFilterYear.BackColor = Color.FromArgb(248, 250, 252);
            this.cmbFilterYear.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFilterYear.FlatStyle = FlatStyle.Flat;
            this.cmbFilterYear.Font = new Font("Inter", 10F);
            this.cmbFilterYear.Items.AddRange(new object[] {
                "All Years",
                "1st Year",
                "2nd Year",
                "3rd Year",
                "4th Year"
            });
            this.cmbFilterYear.Location = new Point(518, 13);
            this.cmbFilterYear.Name = "cmbFilterYear";
            this.cmbFilterYear.Size = new Size(155, 25);
            this.cmbFilterYear.TabIndex = 2;
            this.cmbFilterYear.SelectedIndexChanged += new System.EventHandler(this.cmbFilterYear_SelectedIndexChanged);

            // cmbFilterProgram
            this.cmbFilterProgram.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.cmbFilterProgram.BackColor = Color.FromArgb(248, 250, 252);
            this.cmbFilterProgram.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFilterProgram.FlatStyle = FlatStyle.Flat;
            this.cmbFilterProgram.Font = new Font("Inter", 10F);
            this.cmbFilterProgram.Items.AddRange(new object[] {
                "All Programs",
                "BSIT",
                "BSCS",
                "BSIS",
                "BSHM",
                "BSED",
                "BSBA"
            });
            this.cmbFilterProgram.Location = new Point(685, 13);
            this.cmbFilterProgram.Name = "cmbFilterProgram";
            this.cmbFilterProgram.Size = new Size(150, 25);
            this.cmbFilterProgram.TabIndex = 3;
            this.cmbFilterProgram.SelectedIndexChanged += new System.EventHandler(this.cmbFilterProgram_SelectedIndexChanged);

            // pnlGridContainer
            this.pnlGridContainer.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.pnlGridContainer.BackColor = Color.White;
            this.pnlGridContainer.Controls.Add(this.dgvStudents);
            this.pnlGridContainer.Controls.Add(this.lblRecordCount);
            this.pnlGridContainer.Controls.Add(this.btnExportCsv);
            this.pnlGridContainer.Location = new Point(350, 234);
            this.pnlGridContainer.Name = "pnlGridContainer";
            this.pnlGridContainer.Size = new Size(850, 452);
            this.pnlGridContainer.TabIndex = 4;

            // dgvStudents
            this.dgvStudents.AllowUserToAddRows = false;
            this.dgvStudents.AllowUserToDeleteRows = false;
            this.dgvStudents.AllowUserToResizeRows = false;
            altRowStyle.BackColor = Color.FromArgb(248, 250, 252);
            altRowStyle.Padding = new Padding(14, 6, 14, 6);
            this.dgvStudents.AlternatingRowsDefaultCellStyle = altRowStyle;
            this.dgvStudents.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStudents.BackgroundColor = Color.White;
            this.dgvStudents.BorderStyle = BorderStyle.None;
            this.dgvStudents.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvStudents.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = Color.FromArgb(15, 23, 42);
            headerStyle.Font = new Font("Inter", 9F, FontStyle.Bold);
            headerStyle.ForeColor = Color.White;
            headerStyle.Padding = new Padding(14, 0, 14, 0);
            headerStyle.SelectionBackColor = Color.FromArgb(15, 23, 42);
            headerStyle.SelectionForeColor = Color.White;
            this.dgvStudents.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvStudents.ColumnHeadersHeight = 44;
            this.dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            defaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            defaultCellStyle.BackColor = Color.White;
            defaultCellStyle.Font = new Font("Inter", 9.5F);
            defaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            defaultCellStyle.Padding = new Padding(14, 6, 14, 6);
            defaultCellStyle.SelectionBackColor = Color.FromArgb(238, 242, 255);
            defaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            this.dgvStudents.DefaultCellStyle = defaultCellStyle;
            this.dgvStudents.EnableHeadersVisualStyles = false;
            this.dgvStudents.GridColor = Color.FromArgb(226, 232, 240);
            this.dgvStudents.Location = new Point(1, 1);
            this.dgvStudents.MultiSelect = false;
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.ReadOnly = true;
            this.dgvStudents.RowHeadersVisible = false;
            this.dgvStudents.RowTemplate.Height = 42;
            this.dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudents.Size = new Size(848, 392);
            this.dgvStudents.TabIndex = 0;
            this.dgvStudents.CellClick += new DataGridViewCellEventHandler(this.dgvStudents_CellClick);

            // lblRecordCount
            this.lblRecordCount.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.lblRecordCount.AutoSize = true;
            this.lblRecordCount.Font = new Font("Inter", 9F, FontStyle.Regular);
            this.lblRecordCount.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblRecordCount.Location = new Point(16, 415);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new Size(135, 15);
            this.lblRecordCount.TabIndex = 1;
            this.lblRecordCount.Text = "Showing 0 of 0 records";

            // btnExportCsv
            this.btnExportCsv.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btnExportCsv.BackColor = Color.FromArgb(16, 185, 129);
            this.btnExportCsv.Cursor = Cursors.Hand;
            this.btnExportCsv.FlatAppearance.BorderSize = 0;
            this.btnExportCsv.FlatStyle = FlatStyle.Flat;
            this.btnExportCsv.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold);
            this.btnExportCsv.ForeColor = Color.White;
            this.btnExportCsv.Location = new Point(706, 408);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new Size(130, 32);
            this.btnExportCsv.TabIndex = 2;
            this.btnExportCsv.Text = "📥 Export to CSV";
            this.btnExportCsv.UseVisualStyleBackColor = false;
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);

            // MainForm
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.ClientSize = new Size(1220, 704);
            this.MinimumSize = new Size(1220, 704);
            this.Controls.Add(this.pnlGridContainer);
            this.Controls.Add(this.pnlFilterBar);
            this.Controls.Add(this.pnlStudentDetails);
            this.Controls.Add(this.flpStats);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "EduTrack - Student Information & Directory System";
            this.Load += new System.EventHandler(this.MainForm_Load);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlHeaderLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHeaderLogo)).EndInit();
            this.pnlDbStatusBadge.ResumeLayout(false);
            this.flpStats.ResumeLayout(false);
            this.pnlCardTotal.ResumeLayout(false);
            this.pnlCardTotal.PerformLayout();
            this.pnlCardBsit.ResumeLayout(false);
            this.pnlCardBsit.PerformLayout();
            this.pnlCardBscs.ResumeLayout(false);
            this.pnlCardBscs.PerformLayout();
            this.pnlCardOther.ResumeLayout(false);
            this.pnlCardOther.PerformLayout();
            this.pnlStudentDetails.ResumeLayout(false);
            this.pnlStudentDetails.PerformLayout();
            this.pnlDetailsHeader.ResumeLayout(false);
            this.pnlDetailsHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStudent)).EndInit();
            this.pnlFilterBar.ResumeLayout(false);
            this.pnlFilterBar.PerformLayout();
            this.pnlGridContainer.ResumeLayout(false);
            this.pnlGridContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
            this.ResumeLayout(false);
        }
		
		#endregion
		
        private Panel pnlHeader;
        private Panel pnlHeaderLogo;
        private PictureBox picHeaderLogo;
        private Label lblHeaderIcon;
        private Label lblAppTitle;
        private Label lblAppSubtitle;
        private Label lblAdminBadge;
        private Panel pnlDbStatusBadge;
        private Label lblDbStatus;
        private Button btnSignOut;

        private FlowLayoutPanel flpStats;
        private Panel pnlCardTotal;
        private Panel pnlAccentTotal;
        private Label lblTitleTotal;
        private Label lblTotalCount;
        private Label lblIconTotal;

        private Panel pnlCardBsit;
        private Panel pnlAccentBsit;
        private Label lblTitleBsit;
        private Label lblBsitCount;
        private Label lblIconBsit;

        private Panel pnlCardBscs;
        private Panel pnlAccentBscs;
        private Label lblTitleBscs;
        private Label lblBscsCount;
        private Label lblIconBscs;

        private Panel pnlCardOther;
        private Panel pnlAccentOther;
        private Label lblTitleOther;
        private Label lblOtherCount;
        private Label lblIconOther;

        private Panel pnlStudentDetails;
        private Panel pnlDetailsHeader;
        private Label lblDetailsHeaderTitle;
        private PictureBox picStudent;
        private Button btnBrowsePhoto;
        private Label lblPhotoHint;
        private Label lblStudentID;
        private TextBox txtStudentID;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblGender;
        private RadioButton rbMale;
        private RadioButton rbFemale;
        private RadioButton rbOther;
        private Label lblYearLevel;
        private ComboBox cmbYearLevel;
        private Label lblProgram;
        private ComboBox cmbProgram;
        private Label lblContact;
        private TextBox txtContact;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;

        private Panel pnlFilterBar;
        private Label lblFilterLabel;
        private TextBox txtSearch;
        private ComboBox cmbFilterYear;
        private ComboBox cmbFilterProgram;

        private Panel pnlGridContainer;
        private DataGridView dgvStudents;
        private Label lblRecordCount;
        private Button btnExportCsv;
			
    }
}