using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace EduTrack
{
    public class StudentCRUD
    {
        public bool AuthenticateUser(string username, string password)
        {
            try
            {
                using (MySqlConnection conn = StudentDatabase.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowDatabaseError(ex, "authenticating user");
                return false;
            }
        }
		
		
        public bool AddStudent(Student student)
        {
            try
            {
                using (MySqlConnection conn = StudentDatabase.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO students 
                                    (student_id, name, gender, year_level, program, contact_number, photo_path) 
                                    VALUES 
                                    (@id, @name, @gender, @year, @program, @contact, @photo)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", student.StudentID);
                        cmd.Parameters.AddWithValue("@name", student.Name);
                        cmd.Parameters.AddWithValue("@gender", student.Gender);
                        cmd.Parameters.AddWithValue("@year", student.YearLevel);
                        cmd.Parameters.AddWithValue("@program", student.Program);
                        cmd.Parameters.AddWithValue("@contact", student.ContactNumber);
                        cmd.Parameters.AddWithValue("@photo", string.IsNullOrEmpty(student.PhotoPath) ? (object)DBNull.Value : student.PhotoPath);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowDatabaseError(ex, "adding student");
                return false;
            }
        }
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
    }
}