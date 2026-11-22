
using System;
using System.Threading.Tasks;
using MySqlConnector;

namespace EduTrack.Mobile
{
    public static class MobileDatabase
    {
        // Single configurable MySQL host constant ("10.0.2.2", "localhost", or LAN IP e.g. "192.168.0.100")
        public const string MYSQL_HOST = "10.0.2.2";

        // Builds the MySQL connection string using MYSQL_HOST
        public static string GetConnectionString()
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = MYSQL_HOST,
                Port = 3306,
                Database = "student_db",
                UserID = "root",
                Password = "your_password",
                ConnectionTimeout = 30,
                AllowUserVariables = true
            };

            return builder.ConnectionString;
        }

        // Creates and returns a new MySqlConnection instance for MYSQL_HOST
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(GetConnectionString());
        }

        // Tests MySQL connectivity asynchronously against MYSQL_HOST
        public static async Task<bool> TestConnectionAsync()
        {
            try
            {
                await using (var connection = GetConnection())
                {
                    await connection.OpenAsync();
                    return connection.State == System.Data.ConnectionState.Open;
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"MySQL connection error: {ex.Message}"
                );
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"Connection error: {ex.Message}"
                );
                return false;
            }
        }
    }
}