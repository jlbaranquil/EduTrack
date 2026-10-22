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


        public DataTable GetAllStudents()
        {
            DataTable dt = new DataTable();

            try
            {
                using (MySqlConnection conn = StudentDatabase.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT student_id, name, gender, year_level, program, contact_number, photo_path FROM students ORDER BY student_id ASC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowDatabaseError(ex, "loading students");
            }

            return dt;
        }



        public DataTable FilterStudents(string keyword, string yearLevel, string program)
        {
            DataTable dt = new DataTable();

            try
            {
                using (MySqlConnection conn = StudentDatabase.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT student_id, name, gender, year_level, program, contact_number, photo_path 
                                     FROM students 
                                     WHERE (student_id LIKE @keyword OR name LIKE @keyword)";

                    if (!string.IsNullOrEmpty(yearLevel) && yearLevel != "All Years")
                    {
                        query += " AND year_level = @yearLevel";
                    }

                    if (!string.IsNullOrEmpty(program) && program != "All Programs")
                    {
                        query += " AND program = @program";
                    }

                    query += " ORDER BY student_id ASC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                        if (!string.IsNullOrEmpty(yearLevel) && yearLevel != "All Years")
                        {
                            cmd.Parameters.AddWithValue("@yearLevel", yearLevel);
                        }

                        if (!string.IsNullOrEmpty(program) && program != "All Programs")
                        {
                            cmd.Parameters.AddWithValue("@program", program);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowDatabaseError(ex, "filtering students");
            }

            return dt;
        }



        public bool UpdateStudent(Student student)
        {
            try
            {
                using (MySqlConnection conn = StudentDatabase.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE students 
                                     SET name = @name, 
                                         gender = @gender, 
                                         year_level = @year, 
                                         program = @program, 
                                         contact_number = @contact, 
                                         photo_path = @photo 
                                     WHERE student_id = @id";

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
                ErrorHandler.ShowDatabaseError(ex, "updating student");
                return false;
            }
        }



        public bool DeleteStudent(string studentID)
        {
            try
            {
                using (MySqlConnection conn = StudentDatabase.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM students WHERE student_id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", studentID);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowDatabaseError(ex, "deleting student");
                return false;
            }
        }



        public int GetTotalStudentCount()
        {
            try
            {
                using (MySqlConnection conn = StudentDatabase.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM students";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowDatabaseError(ex, "counting total students");
                return 0;
            }
        }



        public int GetStudentCountByProgram(string program)
        {
            try
            {
                using (MySqlConnection conn = StudentDatabase.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM students WHERE program = @program";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@program", program);
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowDatabaseError(ex, "counting students by program");
                return 0;
            }
        }
    }
}