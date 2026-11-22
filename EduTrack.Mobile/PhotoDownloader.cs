using System;
using System.IO;
using Microsoft.Maui.Storage;

namespace EduTrack.Mobile
{
    public static class PhotoDownloader
    {
        public static bool IsValidImageBytes(byte[]? bytes)
        {
            if (bytes == null || bytes.Length < 4)
                return false;

            if (bytes.Length >= 3 &&
                bytes[0] == 0xFF &&
                bytes[1] == 0xD8 &&
                bytes[2] == 0xFF)
                return true;

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

            if (bytes.Length >= 6 &&
                bytes[0] == 0x47 &&
                bytes[1] == 0x49 &&
                bytes[2] == 0x46 &&
                bytes[3] == 0x38 &&
                (bytes[4] == 0x37 || bytes[4] == 0x39) &&
                bytes[5] == 0x61)
                return true;

            if (bytes[0] == 0x42 && bytes[1] == 0x4D)
                return true;

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

        private static string GetExtensionFromBytes(byte[] bytes)
        {
            if (!IsValidImageBytes(bytes))
                throw new ArgumentException("Invalid image data.");

            if (bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF)
                return ".jpg";

            if (bytes[0] == 0x89 && bytes[1] == 0x50 &&
                bytes[2] == 0x4E && bytes[3] == 0x47)
                return ".png";

            if (bytes[0] == 0x47 && bytes[1] == 0x49 &&
                bytes[2] == 0x46)
                return ".gif";

            if (bytes[0] == 0x42 && bytes[1] == 0x4D)
                return ".bmp";

            if (bytes[0] == 0x52 && bytes[1] == 0x49 &&
                bytes[2] == 0x46 && bytes[3] == 0x46)
                return ".webp";

            throw new ArgumentException("Unsupported image format.");
        }

        private static string ComputeContentHash(byte[] bytes)
        {
            unchecked
            {
                uint hash = 2166136261;
                int step = Math.Max(1, bytes.Length / 64);

                for (int i = 0; i < bytes.Length; i += step)
                {
                    hash = (hash ^ bytes[i]) * 16777619;
                }

                hash ^= (uint)bytes.Length;
                return hash.ToString("X8");
            }
        }

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

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string safeStudentId = string.Concat(
                    studentId.Trim().Split(Path.GetInvalidFileNameChars())
                );

                if (string.IsNullOrWhiteSpace(safeStudentId))
                    return null;

                string fileName = $"{safeStudentId}_{hash}{extension}";
                string filePath = Path.Combine(folderPath, fileName);

                if (!File.Exists(filePath))
                {
                    foreach (string oldFile in Directory.GetFiles(
                        folderPath,
                        $"{safeStudentId}_*.*"))
                    {
                        if (!string.Equals(
                            oldFile,
                            filePath,
                            StringComparison.OrdinalIgnoreCase))
                        {
                            try { File.Delete(oldFile); } catch { }
                        }
                    }

                    File.WriteAllBytes(filePath, photoData!);
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
