# EduTrack - Student Information System

**Group Name:** JARVIS

## Team Members & Roles
| Member Name | Assigned Role |
| :--- | :--- |
| **Sean Ronald Vibar** | UI Layout & DataGridView Display |
| **Angelica Abalita** | MySQL Setup & Database Connection |
| **Jhon Lloyd Baranquil** | CRUD Logic (Add, Edit, Delete) |
| **John Philip Lipio** | Error Handling & Technical Documentation |

## Project Overview
EduTrack is a desktop-based Student Information System built for our CS 106 Midterm Project. It acts as a digital student directory allowing users to securely manage student records. 
    
### Core Features:
* **Create:** Add new student records with input validation.
* **Read:** View and search records dynamically via a DataGridView.
* **Update:** Edit existing student details safely.
* **Delete:** Remove records with mandatory confirmation prompts.
* **Safety:** Full exception handling to prevent application crashes during database operations.

## Technology Stack
* **Frontend/Backend:** C# (.NET Windows Forms)
* **Database:** MySQL
* **Dependencies:** `MySql.Data` (NuGet Package)
* **Documentation:** CherryTree (`.ctb`)

## How to Run the Project
1. **Set up the Database:** 
   * Open MySQL Workbench.
   * Run the `edutrack_db.sql` script located in the `Database/` folder to generate the schema and tables.
2. **Open the Application:**
   * Open `EduTrack.slnx` in Visual Studio 2026.
   * Ensure the `MySql.Data` package is installed.
   * Update the connection string in `StudentDatabase.cs` with your local MySQL password if necessary.
   * Press **F5** to build and run.