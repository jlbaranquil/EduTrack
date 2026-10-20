using System.Drawing;
using System.Windows.Forms;

namespace EduTrack
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // Control declarations required by MainForm.cs
        private Panel pnlHeader;
        private Panel pnlHeaderLogo;
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

        private Panel pnlCardBsit;
        private Panel pnlAccentBsit;
        private Label lblTitleBsit;
        private Label lblBsitCount;

        private Panel pnlCardBscs;
        private Panel pnlAccentBscs;
        private Label lblTitleBscs;
        private Label lblBscsCount;

        private Panel pnlCardOther;
        private Panel pnlAccentOther;
        private Label lblTitleOther;
        private Label lblOtherCount;

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