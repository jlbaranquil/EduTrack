using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace EduTrack.Mobile
{
    public static class MobileErrorHandler
    {

        public const string SELECT_YEAR_PLACEHOLDER = "-- Select Year --";
        public const string SELECT_PROGRAM_PLACEHOLDER = "-- Select Program --";
        public const int REQUIRED_CONTACT_DIGITS = 11;


        public static async Task<bool> ValidateLoginInputsAsync(
            Page page, string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                await page.DisplayAlertAsync(
                    "Validation Warning",
                    "Please enter your username.",
                    "OK"
                );

                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                await page.DisplayAlertAsync(
                    "Validation Warning",
                    "Please enter your password.",
                    "OK"
                );

                return false;
            }

            return true;
        }

        public static async Task<bool> ValidateStudentInputsAsync(
            Page page, MobileStudent? student)
        {
            if (student == null)
            {
                await page.DisplayAlertAsync(
                    "Validation Warning",
                    "Student information is missing.",
                    "OK"
                );

                return false;
            }

            if (string.IsNullOrWhiteSpace(student.StudentID))
            {
                await page.DisplayAlertAsync(
                    "Validation Warning",
                    "Student ID is required.",
                    "OK"
                );

                return false;
            }

            if (string.IsNullOrWhiteSpace(student.Name))
            {
                await page.DisplayAlertAsync(
                    "Validation Warning",
                    "Please enter the student's full name.",
                    "OK"
                );

                return false;
            }

            if (string.IsNullOrWhiteSpace(student.Gender))
            {
                await page.DisplayAlertAsync(
                    "Validation Warning",
                    "Please select a gender.",
                    "OK"
                );

                return false;
            }

            string yearLevel = student.YearLevel?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(yearLevel) ||
                yearLevel == SELECT_YEAR_PLACEHOLDER ||
                yearLevel == "Select Year Level")
            {
                await page.DisplayAlertAsync(
                    "Validation Warning",
                    "Please select a year level.",
                    "OK"
                );

                return false;
            }

            string program = student.Program?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(program) ||
                program == SELECT_PROGRAM_PLACEHOLDER ||
                program == "Select Program")
            {
                await page.DisplayAlertAsync(
                    "Validation Warning",
                    "Please select a program.",
                    "OK"
                );

                return false;
            }

            if (string.IsNullOrWhiteSpace(student.ContactNumber))
            {
                await page.DisplayAlertAsync(
                    "Validation Warning",
                    "Please enter a contact number.",
                    "OK"
                );

                return false;
            }

            string contact = student.ContactNumber.Trim();

            if (contact.Length != REQUIRED_CONTACT_DIGITS ||
                !contact.All(char.IsDigit))
            {
                await page.DisplayAlertAsync(
                    "Invalid Contact Number",
                    "Please enter a valid 11-digit contact number (e.g., 09171234567).",
                    "OK"
                );

                return false;
            }

            return true;
        }

        public static async Task<bool> ConfirmDeleteAsync(
            Page page, string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId))
            {
                await page.DisplayAlertAsync(
                    "No Student Selected",
                    "Please enter or select a valid Student ID first.",
                    "OK"
                );
                return false;
            }

            return await page.DisplayAlertAsync(
                "Confirm Delete",
                $"Are you sure you want to delete student record ({studentId})? This action cannot be undone.",
                "Delete",
                "Cancel"
            );
        }

        public static async Task<bool> ConfirmSignOutAsync(Page page)
        {
            return await page.DisplayAlertAsync(
                "Confirm Sign Out",
                "Are you sure you want to sign out of EduTrack?",
                "Sign Out",
                "Cancel"
            );
        }

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
                $"A database error occurred while {action}.\n\n" +
                $"Active Host: {MobileDatabase.ActiveHost}\n" +
                $"Details: {ex.Message}\n\n" +
                "Tip: Ensure MySQL Server is running and reachable via 10.0.2.2 (Emulator), localhost, or your LAN IP.";

            await page.DisplayAlertAsync(
                "Database Error",
                message,
                "OK"
            );
        }

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
