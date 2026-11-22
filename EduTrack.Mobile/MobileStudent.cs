using System;
using System.IO;
using Microsoft.Maui.Graphics;

namespace EduTrack.Mobile
{
    public class MobileStudent
    {

        public static readonly Color BADGE_BSIT_BG = Color.FromArgb("#DCFCE7");
        public static readonly Color BADGE_BSIT_TEXT = Color.FromArgb("#15803D");

        public static readonly Color BADGE_BSCS_BG = Color.FromArgb("#FEF3C7");
        public static readonly Color BADGE_BSCS_TEXT = Color.FromArgb("#B45309");

        public static readonly Color BADGE_BSBA_BG = Color.FromArgb("#F3E8FF");
        public static readonly Color BADGE_BSBA_TEXT = Color.FromArgb("#7E22CE");

        public static readonly Color BADGE_BSIS_BG = Color.FromArgb("#E0E7FF");
        public static readonly Color BADGE_BSIS_TEXT = Color.FromArgb("#4338CA");

        public static readonly Color BADGE_BSHM_BG = Color.FromArgb("#FCE7F3");
        public static readonly Color BADGE_BSHM_TEXT = Color.FromArgb("#BE185D");

        public static readonly Color BADGE_BSED_BG = Color.FromArgb("#CFFAFE");
        public static readonly Color BADGE_BSED_TEXT = Color.FromArgb("#0E7490");

        public static readonly Color BADGE_DEFAULT_BG = Color.FromArgb("#F1F5F9");
        public static readonly Color BADGE_DEFAULT_TEXT = Color.FromArgb("#475569");


        private string _studentId = string.Empty;
        private byte[]? _photoData;
        private string? _resolvedPhotoPath;
        private bool _isPhotoPathResolved;

        public string StudentID
        {
            get => _studentId;
            set
            {
                string next = value ?? string.Empty;
                if (_studentId != next)
                {
                    _studentId = next;
                    _isPhotoPathResolved = false;
                }
            }
        }

        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string YearLevel { get; set; } = string.Empty;
        public string Program { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;

        public byte[]? PhotoData
        {
            get => _photoData;
            set
            {
                if (!ReferenceEquals(_photoData, value))
                {
                    _photoData = value;
                    _isPhotoPathResolved = false;
                }
            }
        }

        public string Initials => GetInitials(Name);

        public string? ResolvedPhotoPath
        {
            get
            {
                if (!_isPhotoPathResolved ||
                    (!string.IsNullOrWhiteSpace(_resolvedPhotoPath) && !File.Exists(_resolvedPhotoPath)))
                {
                    _resolvedPhotoPath = PhotoDownloader.DownloadStudentPhoto(StudentID, PhotoData);
                    _isPhotoPathResolved = true;
                }

                return _resolvedPhotoPath;
            }
        }

        public bool HasPhoto =>
            !string.IsNullOrWhiteSpace(ResolvedPhotoPath) && File.Exists(ResolvedPhotoPath);

        public bool HasNoPhoto => !HasPhoto;

        public Color ProgramBadgeBackground => (Program?.Trim().ToUpperInvariant() ?? string.Empty) switch
        {
            "BSIT" => BADGE_BSIT_BG,
            "BSCS" => BADGE_BSCS_BG,
            "BSBA" => BADGE_BSBA_BG,
            "BSIS" => BADGE_BSIS_BG,
            "BSHM" => BADGE_BSHM_BG,
            "BSED" => BADGE_BSED_BG,
            _ => BADGE_DEFAULT_BG
        };

        public Color ProgramBadgeTextColor => (Program?.Trim().ToUpperInvariant() ?? string.Empty) switch
        {
            "BSIT" => BADGE_BSIT_TEXT,
            "BSCS" => BADGE_BSCS_TEXT,
            "BSBA" => BADGE_BSBA_TEXT,
            "BSIS" => BADGE_BSIS_TEXT,
            "BSHM" => BADGE_BSHM_TEXT,
            "BSED" => BADGE_BSED_TEXT,
            _ => BADGE_DEFAULT_TEXT
        };

        public static string GetInitials(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return "ST";

            string[] words = fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 1)
                return words[0].Substring(0, 1).ToUpperInvariant();

            char firstInitial = words[0][0];
            char lastInitial = words[^1][0];

            return $"{firstInitial}{lastInitial}".ToUpperInvariant();
        }
    }
}
