//sing Microsoft.Data.Sqlite;

//public class DatabaseTaskRepository
//{
//    private string connectionString = "Data Source=tasks.db";

//    public void Initialize()
//    {
//        using var connection = new SqliteConnection(connectionString);
//        connection.Open();

//        var command = connection.CreateCommand();
//        command.CommandText =
//        @"
//        CREATE TABLE IF NOT EXISTS Tasks (
//            Id INTEGER PRIMARY KEY AUTOINCREMENT,
//            Title TEXT NOT NULL,
//            IsCompleted INTEGER NOT NULL
//        );
//        ";
//        command.ExecuteNonQuery();
//    }

//    public void Add(TaskItem task)
//    {
//        using var connection = new SqliteConnection(connectionString);
//        connection.Open();

//        var command = connection.CreateCommand();
//        command.CommandText =
//        @"
//        INSERT INTO Tasks (Title, IsCompleted)
//        VALUES ($title, $completed)
//        ";

//        command.Parameters.AddWithValue("$title", task.Title);
//        command.Parameters.AddWithValue("$completed", task.IsCompleted ? 1 : 0);

//        command.ExecuteNonQuery();
//    }

//    public List<TaskItem> GetAll()
//    {
//        var tasks = new List<TaskItem>();

//        using var connection = new SqliteConnection(connectionString);
//        connection.Open();

//        var command = connection.CreateCommand();
//        command.CommandText = "SELECT Id, Title, IsCompleted FROM Tasks";

//        using var reader = command.ExecuteReader();
//        while (reader.Read())
//        {
//            tasks.Add(new TaskItem
//            {
//                Id = reader.GetInt32(0),
//                Title = reader.GetString(1),
//                IsCompleted = reader.GetInt32(2) == 1
//            });
//        }

//        return tasks;
//    }
//}