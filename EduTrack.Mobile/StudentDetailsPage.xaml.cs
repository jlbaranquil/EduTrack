using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace EduTrack.Mobile
{
    public partial class StudentDetailsPage : ContentPage
    {
		
        public const string HEADER_ADD_MODE = "📋 ADD NEW STUDENT";
        public const string HEADER_DETAILS_MODE = "📋 STUDENT DETAILS";

        public const string PLACEHOLDER_STUDENT_ID = "2026-0013";
        public const string PLACEHOLDER_FULL_NAME = "First & Last Name";
        public const string PLACEHOLDER_CONTACT = "XXXX-XXXX-XXX";

        public static readonly string[] YEAR_LEVEL_OPTIONS =
        {
            "-- Select Year --",
            "1st Year",
            "2nd Year",
            "3rd Year",
            "4th Year"
        };

        public static readonly string[] PROGRAM_OPTIONS =
        {
            "-- Select Program --",
            "BSIT",
            "BSCS",
            "BSIS",
            "BSHM",
            "BSED",
            "BSBA"
        };

        private readonly MobileStudentCRUD crud = new MobileStudentCRUD();
        private readonly MobileStudent? initialStudent;
        private byte[]? existingPhotoData = null;

        public StudentDetailsPage(MobileStudent? existingStudent = null)
        {
            InitializeComponent();
            initialStudent = existingStudent;
            ApplyConfiguration();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await InitializeFormModeAsync();
        }

        private void ApplyConfiguration()
        {
            txtStudentID.Placeholder = PLACEHOLDER_STUDENT_ID;
            txtFullName.Placeholder = PLACEHOLDER_FULL_NAME;
            txtContact.Placeholder = PLACEHOLDER_CONTACT;

            pkrYearLevel.ItemsSource = YEAR_LEVEL_OPTIONS;
            pkrYearLevel.SelectedIndex = 0;

            pkrProgram.ItemsSource = PROGRAM_OPTIONS;
            pkrProgram.SelectedIndex = 0;
        }

        private async Task InitializeFormModeAsync()
        {
            if (initialStudent != null)
            {
                lblPageHeaderTitle.Text = HEADER_DETAILS_MODE;

                bdrProfilePicture.IsVisible = true;
                btnAdd.IsVisible = false;
                btnClear.IsVisible = false;
                btnUpdate.IsVisible = true;
                btnDelete.IsVisible = true;

                PopulateFormFields(initialStudent);
            }
            else
            {
                lblPageHeaderTitle.Text = HEADER_ADD_MODE;

                bdrProfilePicture.IsVisible = false;
                btnAdd.IsVisible = true;
                btnClear.IsVisible = true;
                btnUpdate.IsVisible = false;
                btnDelete.IsVisible = false;

                txtStudentID.IsReadOnly = false;
                txtStudentID.Text = await crud.GenerateNextStudentIdAsync();
            }
        }

        private void PopulateFormFields(MobileStudent student)
        {
            txtStudentID.Text = student.StudentID;
            txtStudentID.IsReadOnly = true;

            txtFullName.Text = student.Name;

            if (student.Gender == "Female") rbFemale.IsChecked = true;
            else if (student.Gender == "Other") rbOther.IsChecked = true;
            else rbMale.IsChecked = true;

            int yearIndex = Array.IndexOf(YEAR_LEVEL_OPTIONS, student.YearLevel);
            pkrYearLevel.SelectedIndex = yearIndex >= 0 ? yearIndex : 0;

            int progIndex = Array.IndexOf(PROGRAM_OPTIONS, student.Program);
            pkrProgram.SelectedIndex = progIndex >= 0 ? progIndex : 0;

            txtContact.Text = student.ContactNumber.Replace("-", "");
            existingPhotoData = student.PhotoData;

            UpdatePhotoPreview(student.StudentID, existingPhotoData);
        }

        private void UpdatePhotoPreview(string? studentId, byte[]? photoData)
        {
            string? downloadedPath = PhotoDownloader.DownloadStudentPhoto(studentId, photoData);

            if (!string.IsNullOrWhiteSpace(downloadedPath) && File.Exists(downloadedPath))
            {
                imgStudentPhoto.Source = ImageSource.FromFile(downloadedPath);
                imgStudentPhoto.IsVisible = true;
                lblPhotoInitials.IsVisible = false;
            }
            else
            {
                imgStudentPhoto.Source = null;
                imgStudentPhoto.IsVisible = false;
                lblPhotoInitials.IsVisible = true;

                string currentName = txtFullName.Text?.Trim() ?? string.Empty;
                lblPhotoInitials.Text = string.IsNullOrWhiteSpace(currentName)
                    ? "👤"
                    : MobileStudent.GetInitials(currentName);
            }
        }

        private MobileStudent GetStudentFromInputs()
        {
            string selectedGender = "Male";
            if (rbFemale.IsChecked) selectedGender = "Female";
            else if (rbOther.IsChecked) selectedGender = "Other";

            return new MobileStudent
            {
                StudentID = txtStudentID.Text?.Trim() ?? string.Empty,
                Name = txtFullName.Text?.Trim() ?? string.Empty,
                Gender = selectedGender,
                YearLevel = pkrYearLevel.SelectedItem?.ToString() ?? "-- Select Year --",
                Program = pkrProgram.SelectedItem?.ToString() ?? "-- Select Program --",
                ContactNumber = txtContact.Text?.Trim() ?? string.Empty,
                PhotoData = existingPhotoData
            };
        }

        private async Task ClearInputsAsync()
        {
            lblPageHeaderTitle.Text = HEADER_ADD_MODE;
            txtStudentID.IsReadOnly = false;
            txtStudentID.Text = await crud.GenerateNextStudentIdAsync();
            txtFullName.Text = string.Empty;
            rbMale.IsChecked = true;
            pkrYearLevel.SelectedIndex = 0;
            pkrProgram.SelectedIndex = 0;
            txtContact.Text = string.Empty;
            existingPhotoData = null;
        }

        private async void btnAdd_Clicked(object? sender, EventArgs e)
        {
            MobileStudent student = GetStudentFromInputs();
            if (!await MobileErrorHandler.ValidateStudentInputsAsync(this, student))
            {
                return;
            }

            if (await crud.AddStudentAsync(student, this))
            {
                await MobileErrorHandler.ShowSuccessAsync(this, "Student record added successfully!");
                await Navigation.PopModalAsync();
            }
        }

        private async void btnUpdate_Clicked(object? sender, EventArgs e)
        {
            MobileStudent student = GetStudentFromInputs();
            if (!await MobileErrorHandler.ValidateStudentInputsAsync(this, student))
            {
                return;
            }

            if (await crud.UpdateStudentAsync(student, this))
            {
                await MobileErrorHandler.ShowSuccessAsync(this, "Student record updated successfully!");
                await Navigation.PopModalAsync();
            }
        }

        private async void btnDelete_Clicked(object? sender, EventArgs e)
        {
            string id = txtStudentID.Text?.Trim() ?? string.Empty;
            if (await MobileErrorHandler.ConfirmDeleteAsync(this, id))
            {
                if (await crud.DeleteStudentAsync(id, this))
                {
                    await MobileErrorHandler.ShowSuccessAsync(this, "Student record deleted successfully!");
                    await Navigation.PopModalAsync();
                }
            }
        }

        private async void btnClear_Clicked(object? sender, EventArgs e)
        {
            await ClearInputsAsync();
        }

        private async void btnClose_Clicked(object? sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
