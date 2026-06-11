using Microsoft.Data.Sqlite;
using ProductivityToolModels;

namespace ProductivityToolDataServices
{
    public class ProdToolDataServices
    {
        private const string ConnectionString = "Data Source=productivity.db";

        public ProdToolDataServices()
        {
            InitializeDatabase();
        }

        // ── Schema ────────────────────────────────────────────────────────────
        private void InitializeDatabase()
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText =
            @"CREATE TABLE IF NOT EXISTS Tasks (
                Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                Name        TEXT    NOT NULL UNIQUE,
                Description TEXT    NOT NULL,
                Status      TEXT    NOT NULL DEFAULT 'PENDING'
            );";
            cmd.ExecuteNonQuery();
        }

        // ── Create ────────────────────────────────────────────────────────────
        public void AddTask(ProdToolModels task)
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText =
            @"INSERT INTO Tasks (Name, Description, Status)
              VALUES ($name, $description, $status)";
            cmd.Parameters.AddWithValue("$name",        task.Name);
            cmd.Parameters.AddWithValue("$description", task.Description);
            cmd.Parameters.AddWithValue("$status",      task.Status);
            cmd.ExecuteNonQuery();
        }

        // ── Read All ──────────────────────────────────────────────────────────
        public List<ProdToolModels> GetAllTasks()
        {
            var tasks = new List<ProdToolModels>();
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Name, Description, Status FROM Tasks ORDER BY Id";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tasks.Add(new ProdToolModels(reader.GetString(0), reader.GetString(1))
                {
                    Status = reader.GetString(2)
                });
            }
            return tasks;
        }

        // ── Read One ──────────────────────────────────────────────────────────
        public ProdToolModels? GetTaskByName(string name)
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText =
            @"SELECT Name, Description, Status FROM Tasks
              WHERE Name = $name";
            cmd.Parameters.AddWithValue("$name", name.ToUpper());
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

        // ── Update ────────────────────────────────────────────────────────────
        // oldName is required to correctly handle renames
        public bool UpdateTask(string oldName, ProdToolModels updatedTask)
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText =
            @"UPDATE Tasks
              SET Name        = $newName,
                  Description = $description,
                  Status      = $status
              WHERE Name = $oldName";
            cmd.Parameters.AddWithValue("$oldName",     oldName.ToUpper());
            cmd.Parameters.AddWithValue("$newName",     updatedTask.Name);
            cmd.Parameters.AddWithValue("$description", updatedTask.Description);
            cmd.Parameters.AddWithValue("$status",      updatedTask.Status);
            return cmd.ExecuteNonQuery() > 0;
        }

        // ── Delete ────────────────────────────────────────────────────────────
        public bool DeleteTask(string name)
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Tasks WHERE Name = $name";
            cmd.Parameters.AddWithValue("$name", name.ToUpper());
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
