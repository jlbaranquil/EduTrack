
using System;
using System.IO;
using Microsoft.Maui.Storage;

namespace EduTrack.Mobile
{
    public static class PhotoDownloader
    {
        // Validates whether a byte array contains a valid image header (JPEG, PNG, GIF, BMP, WEBP)
        public static bool IsValidImageBytes(byte[]? bytes)
        {
            if (bytes == null || bytes.Length < 4)
                return false;

            // JPEG
            if (bytes.Length >= 3 &&
                bytes[0] == 0xFF &&
                bytes[1] == 0xD8 &&
                bytes[2] == 0xFF)
                return true;

            // PNG
            if (bytes.Length >= 8 &&
                bytes[0] == 0x89 &&
                bytes[1] == 0x50 &&
                bytes[2] == 0x4E &&
                bytes[3] == 0x47 &&
                bytes[4] == 0x0D &&
                bytes[5] == 0x0A &&
                bytes[6] == 0x1A &&
                bytes[7] == 0x0A)
                return true;

            // GIF
            if (bytes.Length >= 6 &&
                bytes[0] == 0x47 &&
                bytes[1] == 0x49 &&
                bytes[2] == 0x46 &&
                bytes[3] == 0x38 &&
                (bytes[4] == 0x37 || bytes[4] == 0x39) &&
                bytes[5] == 0x61)
                return true;

            // BMP
            if (bytes[0] == 0x42 && bytes[1] == 0x4D)
                return true;

            // WEBP
            if (bytes.Length >= 12 &&
                bytes[0] == 0x52 &&
                bytes[1] == 0x49 &&
                bytes[2] == 0x46 &&
                bytes[3] == 0x46 &&
                bytes[8] == 0x57 &&
                bytes[9] == 0x45 &&
                bytes[10] == 0x42 &&
                bytes[11] == 0x50)
                return true;

            return false;
        }

        // Detects the appropriate file extension (.jpg, .png, .gif, .bmp, .webp) from image magic bytes
        private static string GetExtensionFromBytes(byte[] bytes)
        {
            if (!IsValidImageBytes(bytes))
                throw new ArgumentException("Invalid image data.");

            // JPEG
            if (bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF)
                return ".jpg";

            // PNG
            if (bytes[0] == 0x89 && bytes[1] == 0x50 &&
                bytes[2] == 0x4E && bytes[3] == 0x47)
                return ".png";

            // GIF
            if (bytes[0] == 0x47 && bytes[1] == 0x49 &&
                bytes[2] == 0x46)
                return ".gif";

            // BMP
            if (bytes[0] == 0x42 && bytes[1] == 0x4D)
                return ".bmp";

            // WEBP
            if (bytes[0] == 0x52 && bytes[1] == 0x49 &&
                bytes[2] == 0x46 && bytes[3] == 0x46)
                return ".webp";

            throw new ArgumentException("Unsupported image format.");
        }

        // Computes a fast FNV-1a 32-bit hex hash of the image bytes for local cache filename busting
        private static string ComputeContentHash(byte[] bytes)
        {
            uint hash = 2166136261;

            foreach (byte b in bytes)
            {
                hash ^= b;
                hash *= 16777619;
            }

            return hash.ToString("x8");
        }

        // Writes student photo bytes (from MySQL LONGBLOB) to local AppDataDirectory cache and returns the file path
        public static string? DownloadStudentPhoto(string? studentId, byte[]? photoData)
        {
            if (string.IsNullOrWhiteSpace(studentId) ||
                !IsValidImageBytes(photoData))
                return null;

            try
            {
                string extension = GetExtensionFromBytes(photoData!);
                string hash = ComputeContentHash(photoData!);

                string folderPath = Path.Combine(
                    FileSystem.AppDataDirectory,
                    "DownloadedPhotos"
                );

                Directory.CreateDirectory(folderPath);

                // Sanitize student ID for safe filenames
                string safeStudentId = string.Concat(
                    studentId.Trim().Split(Path.GetInvalidFileNameChars())
                );

                if (string.IsNullOrWhiteSpace(safeStudentId))
                    return null;

                string fileName = $"{safeStudentId}_{hash}{extension}";
                string filePath = Path.Combine(folderPath, fileName);

                // Write the image only if it doesn't already exist
                if (!File.Exists(filePath))
                {
                    File.WriteAllBytes(filePath, photoData!);
                }

                // Remove older cached photos belonging to this student
                foreach (string oldFile in Directory.GetFiles(
                    folderPath,
                    $"{safeStudentId}_*"))
                {
                    if (!string.Equals(
                        oldFile,
                        filePath,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        File.Delete(oldFile);
                    }
                }

                return filePath;
            }
            catch (IOException)
            {
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }
        }
    }
}