
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace EduTrack.Mobile
{
    public static class MobileErrorHandler
    {
        // Validates login inputs
        public static async Task<bool> ValidateLoginInputsAsync(
            Page page, string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                await page.DisplayAlertAsync(
                    "Validation Error",
                    "Please enter your username.",
                    "OK"
                );

                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                await page.DisplayAlertAsync(
                    "Validation Error",
                    "Please enter your password.",
                    "OK"
                );

                return false;
            }

            return true;
        }

        // Validates student inputs
        public static async Task<bool> ValidateStudentInputsAsync(
            Page page, MobileStudent? student)
        {
            if (student == null)
            {
                await page.DisplayAlertAsync(
                    "Validation Error",
                    "Student information is missing.",
                    "OK"
                );

                return false;
            }

            // Required fields
            if (string.IsNullOrWhiteSpace(student.StudentID))
            {
                await page.DisplayAlertAsync(
                    "Validation Error",
                    "Student ID is required.",
                    "OK"
                );

                return false;
            }

            if (string.IsNullOrWhiteSpace(student.Name))
            {
                await page.DisplayAlertAsync(
                    "Validation Error",
                    "Please enter the student's full name.",
                    "OK"
                );

                return false;
            }

            if (string.IsNullOrWhiteSpace(student.Gender))
            {
                await page.DisplayAlertAsync(
                    "Validation Error",
                    "Please select a gender.",
                    "OK"
                );

                return false;
            }

            if (string.IsNullOrWhiteSpace(student.YearLevel) ||
                student.YearLevel == "Select Year Level")
            {
                await page.DisplayAlertAsync(
                    "Validation Error",
                    "Please select a year level.",
                    "OK"
                );

                return false;
            }

            if (string.IsNullOrWhiteSpace(student.Program) ||
                student.Program == "Select Program")
            {
                await page.DisplayAlertAsync(
                    "Validation Error",
                    "Please select a program.",
                    "OK"
                );

                return false;
            }

            if (string.IsNullOrWhiteSpace(student.ContactNumber))
            {
                await page.DisplayAlertAsync(
                    "Validation Error",
                    "Please enter a contact number.",
                    "OK"
                );

                return false;
            }

            // Validate 11-digit numeric contact number
            string contact = student.ContactNumber.Trim();

            if (contact.Length != 11 ||
                !contact.All(char.IsDigit))
            {
                await page.DisplayAlertAsync(
                    "Invalid Contact Number",
                    "Contact number must contain exactly 11 digits.",
                    "OK"
                );

                return false;
            }

            return true;
        }

        // Confirms student deletion
        public static async Task<bool> ConfirmDeleteAsync(
            Page page, string studentId)
        {
            return await page.DisplayAlertAsync(
                "Confirm Delete",
                $"Are you sure you want to delete student {studentId}?",
                "Delete",
                "Cancel"
            );
        }

        // Confirms sign out
        public static async Task<bool> ConfirmSignOutAsync(Page page)
        {
            return await page.DisplayAlertAsync(
                "Confirm Sign Out",
                "Are you sure you want to sign out of EduTrack?",
                "Sign Out",
                "Cancel"
            );
        }

        // Displays database errors
        public static async Task ShowDatabaseErrorAsync(
            Page? page, Exception ex, string action)
        {
            if (page == null)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"{action} failed: {ex.Message}"
                );

                return;
            }

            string message =
                $"Unable to {action}.\n\n" +
                $"Error: {ex.Message}\n\n" +
                $"Troubleshooting:\n" +
                $"- Check your internet connection.\n" +
                $"- Ensure MySQL Server is running.\n" +
                $"- Verify the database connection settings.\n" +
                $"- Check MYSQL_HOST ({MobileDatabase.MYSQL_HOST}).\n" +
                $"- Ensure your firewall allows connections on port 3306.";

            await page.DisplayAlertAsync(
                "Database Error",
                message,
                "OK"
            );
        }

        // Displays success messages
        public static async Task ShowSuccessAsync(
            Page page, string message)
        {
            await page.DisplayAlertAsync(
                "Success",
                message,
                "OK"
            );
        }
    }
}