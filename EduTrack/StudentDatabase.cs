using System;
using MySql.Data.MySqlClient;

namespace EduTrack
{
    public class StudentDatabase
    {
        private static string connectionString = "server=localhost;port=3306;database=edutrack_db;uid=username;pwd=password;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    conn.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}