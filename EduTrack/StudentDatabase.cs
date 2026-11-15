using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace EduTrack
{
    public class StudentDatabase
    {
        private static readonly string connectionString = GetConfiguredConnectionString();

        private static string GetConfiguredConnectionString()
        {
            if (ConfigurationManager.ConnectionStrings["EduTrackMySql"] != null)
            {
                return ConfigurationManager.ConnectionStrings["EduTrackMySql"].ConnectionString;
            }

            return "server=localhost;port=3306;database=edutrack_db;uid=root;pwd=;";
        }

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
