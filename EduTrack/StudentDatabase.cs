using MySql.Data.MySqlClient;

namespace EduTrack
{
    public static class StudentDatabase
    {
        private static string connectionString =
            "Server=localhost;Port=3306;Database=edutrack_db;Uid=root;Pwd=@BicolU130589li~07;";

        public static MySqlConnection GetConnection() //Creates and returns a new MySqlConnection object using the connection string.
        {
            return new MySqlConnection(connectionString);
        }

        public static bool TestConnection() //Attempts to open and close a connection in a try-catch block.
        {
            try
            {
                using (MySqlConnection connection = GetConnection())
                {
                    connection.Open();
                    connection.Close();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}