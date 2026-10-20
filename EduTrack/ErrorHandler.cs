using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EduTrack
{
    public static class ErrorHandler
    {
        /// Handles all database exceptions caught in StudentCRUD.cs.
        /// Matches the exact signature: ErrorHandler.ShowDatabaseError(e.g., "action")
        public static void ShowDatabaseError(Exception ex, string action = "performing a database operation")
        {
            if (ex is MySqlException mysqlEx)
            {
                string userMessage = mysqlEx.Number switch
                {
                    1062 => $"Duplicate Entry: A record with that ID already exists while {action}.",
                    0 or 1042 => $"Connection Failed: Unable to reach the MySQL server while {action}. Please ensure MySQL is running.",
                    1045 => $"Access Denied: Invalid MySQL username or password while {action}.",
                    _ => $"A database error occurred while {action}.\n\nMySQL Error ({mysqlEx.Number}): {mysqlEx.Message}"
                };

                MessageBox.Show(
                    userMessage,
                    "EduTrack - Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            else
            {
                MessageBox.Show(
                    $"An unexpected error occurred while {action}.\n\nDetails: {ex.Message}",
                    "EduTrack - System Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// Displays a warning popup for blank or invalid form inputs.
        public static void ShowValidationError(string message)
        {
            MessageBox.Show(
                message,
                "EduTrack - Invalid Input",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        /// Validates the LoginForm text boxes before authenticating.
        public static bool ValidateLoginInput(string? username, string? password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                ShowValidationError("Please enter your username.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowValidationError("Please enter your password.");
                return false;
            }

            return true;
        }

        /// Validates a Student object in MainForm before adding or updating a record.
        /// PhotoPath is intentionally optional based on StudentCRUD.cs logic.
        public static bool ValidateStudentInput(Student? student)
        {
            if (student == null)
            {
                ShowValidationError("Student data is missing.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(student.StudentID))
            {
                ShowValidationError("Student ID cannot be empty.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(student.Name))
            {
                ShowValidationError("Student Name cannot be empty.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(student.Gender))
            {
                ShowValidationError("Please select a Gender.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(student.YearLevel))
            {
                ShowValidationError("Please select a Year Level.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(student.Program))
            {
                ShowValidationError("Please select or enter a Program.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(student.ContactNumber))
            {
                ShowValidationError("Contact Number cannot be empty.");
                return false;
            }

            return true;
        }

        /// Displays the mandatory "Are you sure?" confirmation prompt before deleting a student.
        public static bool ConfirmDelete(string? studentID = null, string? studentName = null)
        {
            string target = !string.IsNullOrWhiteSpace(studentName)
                ? $"{studentName} ({studentID})"
                : !string.IsNullOrWhiteSpace(studentID)
                    ? $"student ID {studentID}"
                    : "this student record";

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete {target}? This action cannot be undone.",
                "EduTrack - Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            return result == DialogResult.Yes;
        }

        /// Handles general UI/file errors (e.g., loading a profile photo in MainForm).
        public static void ShowUIError(Exception ex, string action = "processing your request")
        {
            MessageBox.Show(
                $"Something went wrong while {action}.\n\nDetails: {ex.Message}",
                "EduTrack - Application Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}