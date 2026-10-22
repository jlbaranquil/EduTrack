using System;
using System.Windows.Forms;

namespace EduTrack
{
    public class ErrorHandler
    {
        public static bool ValidateLoginInputs(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            return true;
        }

        public static bool ValidateStudentInputs(Student s)
        {
            if (s == null ||
                string.IsNullOrWhiteSpace(s.StudentID) ||
                string.IsNullOrWhiteSpace(s.Name) ||
                string.IsNullOrWhiteSpace(s.Gender) ||
                string.IsNullOrWhiteSpace(s.YearLevel) || s.YearLevel == "-- Select Year --" ||
                string.IsNullOrWhiteSpace(s.Program) || s.Program == "-- Select Program --" ||
                string.IsNullOrWhiteSpace(s.ContactNumber))
            {
                MessageBox.Show(
                    "Please fill in all required student fields before saving.",
                    "Validation Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }

            if (s.ContactNumber.Length != 11 || !long.TryParse(s.ContactNumber, out _))
            {
                MessageBox.Show(
                    "Please enter a valid 11-digit contact number (e.g., 09171234567).",
                    "Invalid Contact Number",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }

            return true;
        }

        public static bool ValidateStudentID(string studentID)
        {
            if (string.IsNullOrWhiteSpace(studentID))
            {
                MessageBox.Show(
                    "Please select a student record from the table first.",
                    "No Student Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }
            return true;
        }

        public static bool ConfirmDelete(string studentID)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete student record (" + studentID + ")? This action cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            return result == DialogResult.Yes;
        }

        public static bool ConfirmSignOut()
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to sign out of EduTrack?",
                "Confirm Sign Out",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            return result == DialogResult.Yes;
        }

        public static void ShowDatabaseError(Exception ex, string action)
        {
            MessageBox.Show(
                "A database error occurred while " + action + ".\n\nDetails: " + ex.Message,
                "Database Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        public static void ShowSuccess(string message)
        {
            MessageBox.Show(
                message,
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}