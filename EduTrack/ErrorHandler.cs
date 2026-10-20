using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace EduTrack
{
    public static class ErrorHandler
    {
        // 1. Validates login inputs
        public static bool ValidateLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // 2. Validates student fields and enforces the 0900-000-0000 contact mask
        public static bool ValidateStudent(Student student)
        {
            if (student == null || 
                string.IsNullOrWhiteSpace(student.StudentID) || 
                string.IsNullOrWhiteSpace(student.Name) || 
                string.IsNullOrWhiteSpace(student.Gender) || 
                string.IsNullOrWhiteSpace(student.YearLevel) || 
                string.IsNullOrWhiteSpace(student.Program))
            {
                MessageBox.Show("Please fill in all required student fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Checks if the contact number matches the exact mask required by the UI
            if (string.IsNullOrWhiteSpace(student.ContactNumber) || !Regex.IsMatch(student.ContactNumber, @"^09\d{2}-\d{3}-\d{4}$"))
            {
                MessageBox.Show("Please enter a valid contact number in the format: 0900-000-0000.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // 3. Ensures a row is selected before Edit/Delete
        public static bool ValidateSelection(string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId))
            {
                MessageBox.Show("Please select a student record from the table first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // 4. Deletion confirmation using the student's name
        public static bool ConfirmDelete(string studentName)
        {
            DialogResult result = MessageBox.Show($"Are you sure you want to delete {studentName}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }

        // 5. Sign Out confirmation
        public static bool ConfirmSignOut()
        {
            DialogResult result = MessageBox.Show("Are you sure you want to sign out?", "Confirm Sign Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }

        // 6. Database & runtime error handling
        public static void ShowDatabaseError(Exception ex, string action)
        {
            if (ex is MySqlException mysqlEx)
            {
                string userMessage = mysqlEx.Number switch
                {
                    1062 => $"A record with that ID already exists while {action}.",
                    0 or 1042 => $"Unable to reach the database while {action}. Please ensure MySQL is running.",
                    _ => $"A database error occurred while {action}.\n\nMySQL Error ({mysqlEx.Number}): {mysqlEx.Message}"
                };

                MessageBox.Show(userMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"An unexpected error occurred while {action}.\n\nDetails: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 7. Post-action success popup
        public static void ShowSuccess(string action)
        {
            MessageBox.Show($"Successfully completed: {action}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}