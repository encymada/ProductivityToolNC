//using System.Text.Json;

//public class JsonTaskRepository
//{
//    private readonly string filePath = "tasks.json";

//    public List<TaskItem> Load()
//    {
//        if (!File.Exists(filePath))
//            return new List<TaskItem>();

//        string json = File.ReadAllText(filePath);
//        return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
//    }

//    public void Save(List<TaskItem> tasks)
//    {
//        var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions
//        {
//            WriteIndented = true
//        });

//        File.WriteAllText(filePath, json);
//    }
//}