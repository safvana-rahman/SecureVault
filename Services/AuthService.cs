using SecureVault.Helpers;
using SecureVault.Models;
using System.Data.SqlClient;

namespace SecureVault.Services
{
    public class AuthService
    {
        private readonly string _connectionString;

        public AuthService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public bool RegisterUser(RegisterModel model)
        {
            string hashedPassword = PasswordHelper.HashPassword(model.Password);

            string query = "INSERT INTO Users (Username, Email, PasswordHash) VALUES (@Username, @Email, @PasswordHash)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", model.Username);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
