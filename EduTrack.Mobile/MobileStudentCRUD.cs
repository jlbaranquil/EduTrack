using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using MySqlConnector;

namespace EduTrack.Mobile
{
    public class MobileStudentCRUD
    {
        public const string FILTER_ALL_YEARS = "All Years";
        public const string FILTER_ALL_PROGRAMS = "All Programs";
        public const string STUDENT_ID_PREFIX = "2026-";

        private static MobileStudent MapStudent(MySqlDataReader reader)
        {
            byte[]? photoBytes = null;
            object photoObj = reader["photo_path"];
            if (photoObj != DBNull.Value &&
                photoObj is byte[] rawBytes &&
                PhotoDownloader.IsValidImageBytes(rawBytes))
            {
                photoBytes = rawBytes;
            }

            return new MobileStudent
            {
                StudentID = reader["student_id"]?.ToString() ?? string.Empty,
                Name = reader["name"]?.ToString() ?? string.Empty,
                Gender = reader["gender"]?.ToString() ?? string.Empty,
                YearLevel = reader["year_level"]?.ToString() ?? string.Empty,
                Program = reader["program"]?.ToString() ?? string.Empty,
                ContactNumber = reader["contact_number"]?.ToString() ?? string.Empty,
                PhotoData = photoBytes
            };
        }

        public async Task<bool> AuthenticateUserAsync(
            string username, string password, Page? page = null)
        {
            try
            {
                await using var connection = MobileDatabase.GetConnection();
                await connection.OpenAsync();

                const string query = @"
                    SELECT COUNT(*)
                    FROM users
                    WHERE username = @u AND password = @p";

                await using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@u", (username ?? string.Empty).Trim());
                command.Parameters.AddWithValue("@p", (password ?? string.Empty).Trim());

                object? result = await command.ExecuteScalarAsync();

                return Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Authentication error: {ex.Message}");
                await MobileErrorHandler.ShowDatabaseErrorAsync(page, ex, "authenticating user");
                return false;
            }
        }

        public async Task<List<MobileStudent>> GetAllStudentsAsync(
            Page? page = null)
        {
            var students = new List<MobileStudent>();

            try
            {
                await using var connection = MobileDatabase.GetConnection();
                await connection.OpenAsync();

                const string query = @"
                    SELECT student_id, name, gender, year_level,
                           program, contact_number, photo_path
                    FROM students
                    ORDER BY student_id ASC";

                await using var command = new MySqlCommand(query, connection);
                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    students.Add(MapStudent(reader));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Get students error: {ex.Message}");
                await MobileErrorHandler.ShowDatabaseErrorAsync(page, ex, "loading student records");
            }

            return students;
        }

        public async Task<List<MobileStudent>> FilterStudentsAsync(
            string keyword,
            string yearLevel,
            string program,
            Page? page = null)
        {
            var students = new List<MobileStudent>();

            try
            {
                await using var connection = MobileDatabase.GetConnection();
                await connection.OpenAsync();

                string query = @"
                    SELECT student_id, name, gender, year_level,
                           program, contact_number, photo_path
                    FROM students
                    WHERE
                        (student_id LIKE @keyword
                         OR name LIKE @keyword)";

                if (!string.IsNullOrWhiteSpace(yearLevel) && yearLevel != FILTER_ALL_YEARS && yearLevel != "All")
                {
                    query += " AND year_level = @yearLevel";
                }

                if (!string.IsNullOrWhiteSpace(program) && program != FILTER_ALL_PROGRAMS && program != "All")
                {
                    query += " AND program = @program";
                }

                query += " ORDER BY student_id ASC";

                await using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@keyword",
                    $"%{keyword?.Trim() ?? string.Empty}%"
                );

                if (!string.IsNullOrWhiteSpace(yearLevel) && yearLevel != FILTER_ALL_YEARS && yearLevel != "All")
                {
                    command.Parameters.AddWithValue("@yearLevel", yearLevel);
                }

                if (!string.IsNullOrWhiteSpace(program) && program != FILTER_ALL_PROGRAMS && program != "All")
                {
                    command.Parameters.AddWithValue("@program", program);
                }

                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    students.Add(MapStudent(reader));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Filter students error: {ex.Message}");
                await MobileErrorHandler.ShowDatabaseErrorAsync(page, ex, "loading student records");
            }

            return students;
        }

        public async Task<int> GetTotalStudentCountAsync()
        {
            try
            {
                await using var connection = MobileDatabase.GetConnection();
                await connection.OpenAsync();

                const string query = "SELECT COUNT(*) FROM students";

                await using var command = new MySqlCommand(query, connection);

                object? result = await command.ExecuteScalarAsync();

                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Student count error: {ex.Message}");
                return 0;
            }
        }

        public async Task<string> GenerateNextStudentIdAsync()
        {
            try
            {
                await using var connection = MobileDatabase.GetConnection();
                await connection.OpenAsync();

                string query = @"
                    SELECT student_id
                    FROM students
                    WHERE student_id LIKE @prefix
                    ORDER BY student_id DESC
                    LIMIT 1";

                await using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@prefix",
                    STUDENT_ID_PREFIX + "%"
                );

                object? result = await command.ExecuteScalarAsync();

                if (result != null && result != DBNull.Value)
                {
                    string lastId = result.ToString() ?? string.Empty;
                    string[] parts = lastId.Split('-');

                    if (parts.Length == 2 &&
                        int.TryParse(parts[1], out int lastNumber))
                    {
                        return $"{STUDENT_ID_PREFIX}{(lastNumber + 1):D4}";
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Generate Student ID error: {ex.Message}");
            }

            return $"{STUDENT_ID_PREFIX}0013";
        }

        public async Task<bool> AddStudentAsync(
            MobileStudent student, Page? page = null)
        {
            try
            {
                await using var connection = MobileDatabase.GetConnection();
                await connection.OpenAsync();

                const string query = @"
                    INSERT INTO students
                        (student_id, name, gender, year_level,
                         program, contact_number, photo_path)
                    VALUES
                        (@id, @name, @gender, @yearLevel,
                         @program, @contact, @photo)";

                await using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", student.StudentID);
                command.Parameters.AddWithValue("@name", student.Name);
                command.Parameters.AddWithValue("@gender", student.Gender);
                command.Parameters.AddWithValue("@yearLevel", student.YearLevel);
                command.Parameters.AddWithValue("@program", student.Program);
                command.Parameters.AddWithValue("@contact", student.ContactNumber);

                var photoParameter = command.Parameters.Add(
                    "@photo",
                    MySqlDbType.LongBlob
                );

                photoParameter.Value = PhotoDownloader.IsValidImageBytes(student.PhotoData)
                    ? student.PhotoData!
                    : DBNull.Value;

                int rowsAffected = await command.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Add student error: {ex.Message}");
                await MobileErrorHandler.ShowDatabaseErrorAsync(page, ex, "adding student");
                return false;
            }
        }

        public async Task<bool> UpdateStudentAsync(
            MobileStudent student, Page? page = null)
        {
            try
            {
                await using var connection = MobileDatabase.GetConnection();
                await connection.OpenAsync();

                const string query = @"
                    UPDATE students
                    SET name = @name,
                        gender = @gender,
                        year_level = @yearLevel,
                        program = @program,
                        contact_number = @contact,
                        photo_path = @photo
                    WHERE student_id = @id";

                await using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", student.StudentID);
                command.Parameters.AddWithValue("@name", student.Name);
                command.Parameters.AddWithValue("@gender", student.Gender);
                command.Parameters.AddWithValue("@yearLevel", student.YearLevel);
                command.Parameters.AddWithValue("@program", student.Program);
                command.Parameters.AddWithValue("@contact", student.ContactNumber);

                var photoParameter = command.Parameters.Add(
                    "@photo",
                    MySqlDbType.LongBlob
                );

                photoParameter.Value = PhotoDownloader.IsValidImageBytes(student.PhotoData)
                    ? student.PhotoData!
                    : DBNull.Value;

                int rowsAffected = await command.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Update student error: {ex.Message}");
                await MobileErrorHandler.ShowDatabaseErrorAsync(page, ex, "updating student");
                return false;
            }
        }

        public async Task<bool> DeleteStudentAsync(
            string studentId, Page? page = null)
        {
            try
            {
                await using var connection = MobileDatabase.GetConnection();
                await connection.OpenAsync();

                const string query = @"
                    DELETE FROM students
                    WHERE student_id = @id";

                await using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", studentId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Delete student error: {ex.Message}");
                await MobileErrorHandler.ShowDatabaseErrorAsync(page, ex, "deleting student");
                return false;
            }
        }
    }
}
