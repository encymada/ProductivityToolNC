using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using ProductivityToolModels;

namespace ProductivityToolDataServices
{
    public class PTDBData
    {
        private const string ConnectionString =
            "Server=localhost\\SQLEXPRESS;Database=ProductivityToolDB;Trusted_Connection=True;TrustServerCertificate=True;";

        // Call once at startup to make sure the table exists
        public PTDBData()
        {
            EnsureTableExists();
        }

        private void EnsureTableExists()
        {
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var cmd = new SqlCommand(@"
                IF NOT EXISTS (
                    SELECT * FROM sysobjects WHERE name='Tasks' AND xtype='U'
                )
                CREATE TABLE Tasks (
                    Id          INT IDENTITY(1,1) PRIMARY KEY,
                    Name        NVARCHAR(200) NOT NULL UNIQUE,
                    Description NVARCHAR(1000),
                    Status      NVARCHAR(50) DEFAULT 'PENDING'
                );", conn);
            cmd.ExecuteNonQuery();
        }

        public List<ProdToolModels> GetAllTasks()
        {
            var tasks = new List<ProdToolModels>();
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var cmd = new SqlCommand("SELECT Name, Description, Status FROM Tasks", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tasks.Add(new ProdToolModels(
                    reader.GetString(0),
                    reader.GetString(1))
                {
                    Status = reader.GetString(2)
                });
            }
            return tasks;
        }

        public ProdToolModels? GetTaskByName(string name)
        {
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var cmd = new SqlCommand(
                "SELECT Name, Description, Status FROM Tasks WHERE UPPER(Name) = UPPER(@name)", conn);
            cmd.Parameters.AddWithValue("@name", name);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new ProdToolModels(reader.GetString(0), reader.GetString(1))
                {
                    Status = reader.GetString(2)
                };
            }
            return null;
        }

        public void AddTask(ProdToolModels task)
        {
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var cmd = new SqlCommand(
                "INSERT INTO Tasks (Name, Description, Status) VALUES (@name, @desc, @status)", conn);
            cmd.Parameters.AddWithValue("@name", task.Name);
            cmd.Parameters.AddWithValue("@desc", task.Description);
            cmd.Parameters.AddWithValue("@status", task.Status ?? "PENDING");
            cmd.ExecuteNonQuery();
        }

        public void UpdateTask(string oldName, ProdToolModels task)
        {
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var cmd = new SqlCommand(@"
                UPDATE Tasks
                SET Name = @newName, Description = @desc, Status = @status
                WHERE UPPER(Name) = UPPER(@oldName)", conn);
            cmd.Parameters.AddWithValue("@newName", task.Name);
            cmd.Parameters.AddWithValue("@desc", task.Description);
            cmd.Parameters.AddWithValue("@status", task.Status);
            cmd.Parameters.AddWithValue("@oldName", oldName);
            cmd.ExecuteNonQuery();
        }

        public bool DeleteTask(string name)
        {
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var cmd = new SqlCommand(
                "DELETE FROM Tasks WHERE UPPER(Name) = UPPER(@name)", conn);
            cmd.Parameters.AddWithValue("@name", name);
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}