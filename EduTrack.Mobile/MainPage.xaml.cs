using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace EduTrack.Mobile
{
    public partial class MainPage : ContentPage
    {
        public const string APP_TITLE = "EDUTRACK";
        public const string LOGO_IMAGE_SOURCE = "edutrack_logo.png";
        public const string PLACEHOLDER_SEARCH = "Search by Student ID or Full Name...";
        public const string TEXT_ADD_STUDENT_BTN = "+ Add Student";
        public const string TEXT_SIGN_OUT_BTN = "➔ Sign Out";

        public static readonly string[] YEAR_FILTER_OPTIONS =
        {
            "All Years",
            "1st Year",
            "2nd Year",
            "3rd Year",
            "4th Year"
        };

        public static readonly string[] PROGRAM_FILTER_OPTIONS =
        {
            "All Programs",
            "BSIT",
            "BSCS",
            "BSIS",
            "BSHM",
            "BSED",
            "BSBA"
        };

        public static readonly Color HOST_CONNECTED_COLOR = Color.FromArgb("#10B981");
        public static readonly Color HOST_OFFLINE_COLOR = Color.FromArgb("#F43F5E");

        private readonly MobileStudentCRUD crud = new MobileStudentCRUD();
        private bool isInitializing = false;

        public MainPage()
        {
            InitializeComponent();
            ApplyConfiguration();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshStudentDirectoryAsync();
        }

        private void ApplyConfiguration()
        {
            isInitializing = true;

            lblAppTitle.Text = APP_TITLE;
            imgHeaderLogo.Source = LOGO_IMAGE_SOURCE;
            txtSearch.Placeholder = PLACEHOLDER_SEARCH;
            btnAddStudent.Text = TEXT_ADD_STUDENT_BTN;
            btnSignOut.Text = TEXT_SIGN_OUT_BTN;
            btnThemeToggle.Text = ThemeManager.CurrentThemeButtonIcon;

            cmbFilterYear.ItemsSource = YEAR_FILTER_OPTIONS;
            cmbFilterYear.SelectedIndex = 0;

            cmbFilterProgram.ItemsSource = PROGRAM_FILTER_OPTIONS;
            cmbFilterProgram.SelectedIndex = 0;

            isInitializing = false;
        }

        public async Task RefreshStudentDirectoryAsync()
        {
            bool connected = await MobileDatabase.TestConnectionAsync();
            lblHostAddress.Text = MobileDatabase.MYSQL_HOST;

            if (connected)
            {
                lblHostDot.TextColor = HOST_CONNECTED_COLOR;
                lblHostAddress.TextColor = HOST_CONNECTED_COLOR;
            }
            else
            {
                lblHostDot.TextColor = HOST_OFFLINE_COLOR;
                lblHostAddress.TextColor = HOST_OFFLINE_COLOR;
            }

            await ApplySearchAndFiltersAsync();
        }

        private async Task ApplySearchAndFiltersAsync()
        {
            if (isInitializing) return;

            string keyword = txtSearch.Text?.Trim() ?? string.Empty;
            string selectedYear = cmbFilterYear.SelectedItem?.ToString() ?? "All Years";
            string selectedProgram = cmbFilterProgram.SelectedItem?.ToString() ?? "All Programs";

            List<MobileStudent> filteredList = await crud.FilterStudentsAsync(
                keyword,
                selectedYear,
                selectedProgram,
                this);

            int totalCount = await crud.GetTotalStudentCountAsync();
            if (totalCount < filteredList.Count)
            {
                totalCount = filteredList.Count;
            }

            cvStudents.ItemsSource = null;
            cvStudents.ItemsSource = filteredList;
            lblRecordCount.Text = $"Showing {filteredList.Count} of {totalCount} records";
        }

        private async void refreshView_Refreshing(object? sender, EventArgs e)
        {
            try
            {
                await RefreshStudentDirectoryAsync();
            }
            finally
            {
                refreshView.IsRefreshing = false;
            }
        }

        private async void txtSearch_TextChanged(object? sender, TextChangedEventArgs e)
        {
            await ApplySearchAndFiltersAsync();
        }

        private async void FilterPicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            await ApplySearchAndFiltersAsync();
        }

        private async void btnAddStudent_Clicked(object? sender, EventArgs e)
        {
            var addPage = new StudentDetailsPage(null);
            await Navigation.PushModalAsync(addPage);
        }

        private async void StudentCard_Tapped(object? sender, TappedEventArgs e)
        {
            MobileStudent? tappedStudent = e.Parameter as MobileStudent;

            if (tappedStudent == null && sender is BindableObject bindable)
            {
                tappedStudent = bindable.BindingContext as MobileStudent;
            }

            if (tappedStudent != null)
            {
                var detailsPage = new StudentDetailsPage(tappedStudent);
                await Navigation.PushModalAsync(detailsPage);
            }
        }

        private void btnThemeToggle_Clicked(object? sender, EventArgs e)
        {
            ThemeManager.ToggleTheme();
            btnThemeToggle.Text = ThemeManager.CurrentThemeButtonIcon;
        }

        private async void btnSignOut_Clicked(object? sender, EventArgs e)
        {
            if (await MobileErrorHandler.ConfirmSignOutAsync(this))
            {
                if (Application.Current != null && Application.Current.Windows.Count > 0)
                {
                    Application.Current.Windows[0].Page = new LoginPage();
                }
            }
        }
    }
}
