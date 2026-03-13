using System.Collections.Generic;
using System.Linq;
using ProductivityToolModels;


namespace ProductivityToolDataServices
{
    public class ProdToolDataServices
    {
        private List<ProdToolModels> tasks = new List<ProdToolModels>();

        public void AddTask(ProdToolModels task)
        {
            tasks.Add(task);
        }

        public List<ProdToolModels> GetAllTasks()
        {
            return tasks;
        }

        public ProdToolModels GetTaskByName(string name)
        {
            return tasks.FirstOrDefault(t => t.Name == name.ToUpper());
        }

        public bool UpdateTask(ProdToolModels updatedTask)
        {
            ProdToolModels existingTask = GetTaskByName(updatedTask.Name);

            if (existingTask == null)
            {
                return false;
            }

            existingTask.Description = updatedTask.Description;
            existingTask.Status = updatedTask.Status;
            return true;
        }

        public bool DeleteTask(string name)
        {
            ProdToolModels task = GetTaskByName(name);

            if (task == null)
            {
                return false;
            }

            tasks.Remove(task);
            return true;
        }
    }
}