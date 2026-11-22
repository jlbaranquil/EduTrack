
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
        private static string CURRENT_YEAR => DateTime.Now.Year.ToString(); // always get current year

        // Maps database records to MobileStudent objects
        private static MobileStudent MapStudent(MySqlDataReader reader)
        {
            return new MobileStudent
            {
                StudentID = reader["student_id"].ToString() ?? string.Empty,
                Name = reader["name"].ToString() ?? string.Empty,
                Gender = reader["gender"].ToString() ?? string.Empty,
                YearLevel = reader["year_level"].ToString() ?? string.Empty,
                Program = reader["program"].ToString() ?? string.Empty,
                ContactNumber = reader["contact_number"].ToString() ?? string.Empty,
                PhotoData = reader["photo_path"] == DBNull.Value
                    ? null
                    : (byte[])reader["photo_path"]
            };
        }

        // Authenticates a user asynchronously against the MySQL users table
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

                command.Parameters.AddWithValue("@u", username);
                command.Parameters.AddWithValue("@p", password);

                object? result = await command.ExecuteScalarAsync();

                return Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Authentication error: {ex.Message}");
                return false;
            }
        }

        // Retrieves all student records ordered by student_id ASC asynchronously
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
            }

            return students;
        }

        // Filters student records asynchronously by keyword (Student ID or Name), Year Level, and Program
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

                if (!string.IsNullOrWhiteSpace(yearLevel) &&
                    yearLevel != "All")
                {
                    query += " AND year_level = @yearLevel";
                }

                if (!string.IsNullOrWhiteSpace(program) &&
                    program != "All")
                {
                    query += " AND program = @program";
                }

                query += " ORDER BY student_id ASC";

                await using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@keyword",
                    $"%{keyword?.Trim() ?? string.Empty}%"
                );

                if (!string.IsNullOrWhiteSpace(yearLevel) &&
                    yearLevel != "All")
                {
                    command.Parameters.AddWithValue("@yearLevel", yearLevel);
                }

                if (!string.IsNullOrWhiteSpace(program) &&
                    program != "All")
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
            }

            return students;
        }

        // Returns the total count of student records in the database asynchronously
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

        // Generates the next sequential Student ID (e.g., "2026-0013") asynchronously
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
                    CURRENT_YEAR + "-%"
                );

                object? result = await command.ExecuteScalarAsync();

                int nextNumber = 1;

                if (result != null && result != DBNull.Value)
                {
                    string lastId = result.ToString() ?? "";
                    string[] parts = lastId.Split('-');

                    if (parts.Length == 2 &&
                        int.TryParse(parts[1], out int lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }

                return $"{CURRENT_YEAR}-{nextNumber:D4}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Generate Student ID error: {ex.Message}");
                return string.Empty;
            }
        }

        // Inserts a new student record into MySQL asynchronously
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

                photoParameter.Value = student.PhotoData == null
                    ? DBNull.Value
                    : student.PhotoData;

                int rowsAffected = await command.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Add student error: {ex.Message}");
                return false;
            }
        }

        // Updates an existing student record in MySQL asynchronously
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

                photoParameter.Value = student.PhotoData == null
                    ? DBNull.Value
                    : student.PhotoData;

                int rowsAffected = await command.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Update student error: {ex.Message}");
                return false;
            }
        }

        // Deletes a student record by Student ID from MySQL asynchronously
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
                return false;
            }
        }
    }
}