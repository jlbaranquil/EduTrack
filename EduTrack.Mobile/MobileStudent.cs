using System;
using Microsoft.Maui.Graphics;

namespace EduTrack.Mobile
{
    public class MobileStudent
    {
        public string StudentID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string YearLevel { get; set; } = string.Empty;
        public string Program { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public byte[]? PhotoData { get; set; }

        // Generates 1- or 2-letter uppercase initials from the student's full name (e.g., "Emilio Aguinaldo" -> "EA")

        public static string GetInitials(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return string.Empty;

            string[] words = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 1)
                return words[0][0].ToString().ToUpper();

            char firstInitial = words[0][0];
            char lastInitial = words[^1][0];

            return $"{firstInitial}{lastInitial}".ToUpper();
        }
    }
}