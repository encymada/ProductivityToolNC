using ProductivityToolModels;

namespace ProductivityToolDataServices
{
    public class ProdToolDataServices
    {
        private readonly PTDBData _db;

        public ProdToolDataServices()
        {
            _db = new PTDBData();
        }

        public ProdToolModels? GetTaskByName(string name)
            => _db.GetTaskByName(name);

        public void AddTask(ProdToolModels task)
            => _db.AddTask(task);

        public void UpdateTask(string oldName, ProdToolModels task)
            => _db.UpdateTask(oldName, task);

        public bool DeleteTask(string name)
            => _db.DeleteTask(name);

        public List<ProdToolModels> GetAllTasks()
            => _db.GetAllTasks();
    }
}