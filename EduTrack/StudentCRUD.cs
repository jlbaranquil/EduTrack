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
    }
}