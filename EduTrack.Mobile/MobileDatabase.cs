using System;
using System.Threading.Tasks;
using MySqlConnector;

namespace EduTrack.Mobile
{
    public static class MobileDatabase
    {

        public const string MYSQL_HOST = "10.0.2.2";
        public const uint DB_PORT = 3306;
        public const string DB_NAME = "DB_NAME";
        public const string DB_USER = "YOUR_USERNAME";
        public const string DB_PASSWORD = "YOUR_PASSWORD";
        public const uint CONNECTION_TIMEOUT_SECONDS = 4;


        public static string ActiveHost => MYSQL_HOST;
        public static bool IsConnected { get; private set; } = false;

        public static string GetConnectionString()
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = MYSQL_HOST,
                Port = DB_PORT,
                Database = DB_NAME,
                UserID = DB_USER,
                Password = DB_PASSWORD,
                ConnectionTimeout = CONNECTION_TIMEOUT_SECONDS,
                DefaultCommandTimeout = 8,
                SslMode = MySqlSslMode.Disabled,
                AllowPublicKeyRetrieval = true,
                AllowUserVariables = true
            };

            return builder.ConnectionString;
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(GetConnectionString());
        }

        public static async Task<bool> TestConnectionAsync()
        {
            try
            {
                await using (var connection = GetConnection())
                {
                    await connection.OpenAsync();
                    IsConnected = connection.State == System.Data.ConnectionState.Open;
                    return IsConnected;
                }
            }
            catch (MySqlException ex)
            {
                IsConnected = false;
                System.Diagnostics.Debug.WriteLine(
                    $"MySQL connection error: {ex.Message}"
                );
                return false;
            }
            catch (Exception ex)
            {
                IsConnected = false;
                System.Diagnostics.Debug.WriteLine(
                    $"Connection error: {ex.Message}"
                );
                return false;
            }
        }
    }
}
