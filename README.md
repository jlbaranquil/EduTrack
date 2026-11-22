<div align="center">
  <img src="EduTrack/Assets/edutrack-logo.svg" alt="EduTrack Logo" width="150"/>

  # EduTrack - Integrated Student Information & Mobile Analytics Ecosystem
  **CS 106 Final Project** • Group 8
</div>


**Group Name:** JARVIS

**GitHub repository link:** https://github.com/jlbaranquil/EduTrack.git

## Team Members & Task Designation
| Member | Name | Assigned Role & Key Deliverables |
| :--- | :--- | :--- |
| **Member 1** | **Angelica Abalita** | Desktop ScottPlot Analytics & Dashboard (`StudentAnalytics.cs`, `ReportExporter.cs`, `MainForm.cs`) |
| **Member 2** | **Jhon Lloyd Baranquil** | .NET MAUI Mobile UI, Theme & Navigation (`App.xaml`, `ThemeManager.cs`, `SplashPage`, `LoginPage`, `MainPage`, `StudentDetailsPage`) |
| **Member 3** | **Sean Ronald Vibar** | Async MySQL Data Sync & Host Routing (`MobileStudent.cs`, `MobileDatabase.cs`, `MobileStudentCRUD.cs`, `MobileErrorHandler.cs`, `PhotoDownloader.cs`) |
| **Member 4** | **John Philip Lipio** | Installer Packaging & Documentation (`Installers/EduTrack-Setup.msi`, `Installers/EduTrack-Mobile-Signed.apk`, `Docs/EduTrack-Docs.ctb`, `README.md`) |

### Core Capabilities
- **Desktop Administrative Dashboard (`EduTrack`):** Full student CRUD with `LONGBLOB` photo storage, real-time filtering, CSV export, interactive **ScottPlot 5** enrollment charts (Program Bar Chart & Year-Level Pie Chart), live refresh sync, and **QuestPDF** summary report generation.
- **Mobile Student Directory (`EduTrack.Mobile`):** Asynchronous MySQL queries via `MySqlConnector`, local `LONGBLOB` avatar caching (`PhotoDownloader`), dynamic search and year/program filtering, Light/Dark mode switching (`ThemeManager`), and mobile CRUD management.
    
## Technology Stack
- **Desktop Client:** C# `.NET 10` Windows Forms (`MySql.Data 26.7.0`, `ScottPlot.WinForms 5.0.54`, `QuestPDF 2026.9.1`)
- **Mobile Client:** C# `.NET 10` MAUI Android (`com.jarvis.edutrack.mobile`, `MySqlConnector 2.4.0`)
- **Database:** MySQL Server 8.x (`Database/edutrack_db.sql`)
- **Documentation:** CherryTree (`Docs/EduTrack-Docs.ctb`)

## Setup & Installation
1. **Database Setup:**
   - Open MySQL Workbench and execute `Database/edutrack_db.sql` to create `edutrack_db`, the `users` and `students` tables, the default admin account (`admin` / `admin123`), and 12 sample records.
2. **Desktop Application (`.msi`):**
   - Run `Installers/EduTrack-Setup.msi` on Windows 10/11 and launch **EduTrack** from the Start Menu or Desktop shortcut.
   - If running from source in Visual Studio 2026, update the connection string in `EduTrack/App.config` with your local MySQL credentials before pressing **F5**.
3. **Mobile Application (`.apk`):**
   - Install `Installers/EduTrack-Mobile-Signed.apk` onto an Android 10.0+ device or Android Emulator (configured for `10.0.2.2:3306` host loopback in `MobileDatabase.cs`).
4. **System Documentation:**
   - Open `Docs/EduTrack-Docs.ctb` in CherryTree for full technical specifications, deployment guides, user manuals, and QA test matrices.
