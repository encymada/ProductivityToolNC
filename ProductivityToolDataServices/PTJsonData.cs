using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using ProductivityToolModels;

namespace ProductivityToolDataServices
{
    public class PTJsonData : IPTDataService
    {
        private readonly string _tasksFile = "tasks.json";
        private readonly string _categoriesFile = "categories.json";

        private List<PTTask> LoadTasks() =>
            File.Exists(_tasksFile)
                ? JsonSerializer.Deserialize<List<PTTask>>(File.ReadAllText(_tasksFile)) ?? new()
                : new();

        private void SaveTasks(List<PTTask> tasks) =>
            File.WriteAllText(_tasksFile, JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true }));

        private List<PTCategory> LoadCategories() =>
            File.Exists(_categoriesFile)
                ? JsonSerializer.Deserialize<List<PTCategory>>(File.ReadAllText(_categoriesFile)) ?? new()
                : new();

        private void SaveCategories(List<PTCategory> cats) =>
            File.WriteAllText(_categoriesFile, JsonSerializer.Serialize(cats, new JsonSerializerOptions { WriteIndented = true }));

        public Task<IEnumerable<PTTask>> GetAllTasksAsync()
            => Task.FromResult(LoadTasks().AsEnumerable());

        public Task<PTTask?> GetTaskByIdAsync(int id)
            => Task.FromResult(LoadTasks().FirstOrDefault(t => t.Id == id));

        public Task<PTTask> AddTaskAsync(PTTask task)
        {
            var tasks = LoadTasks();
            task.Id = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
            tasks.Add(task);
            SaveTasks(tasks);
            return Task.FromResult(task);
        }

        public Task<PTTask?> UpdateTaskAsync(int id, PTTask task)
        {
            var tasks = LoadTasks();
            var existing = tasks.FirstOrDefault(t => t.Id == id);
            if (existing == null) return Task.FromResult<PTTask?>(null);
            existing.Title = task.Title;
            existing.IsCompleted = task.IsCompleted;
            existing.DueDate = task.DueDate;
            SaveTasks(tasks);
            return Task.FromResult<PTTask?>(existing);
        }

        public Task<bool> DeleteTaskAsync(int id)
        {
            var tasks = LoadTasks();
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return Task.FromResult(false);
            tasks.Remove(task);
            SaveTasks(tasks);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<PTCategory>> GetAllCategoriesAsync()
            => Task.FromResult(LoadCategories().AsEnumerable());

        public Task<PTCategory> AddCategoryAsync(PTCategory category)
        {
            var cats = LoadCategories();
            category.Id = cats.Any() ? cats.Max(c => c.Id) + 1 : 1;
            cats.Add(category);
            SaveCategories(cats);
            return Task.FromResult(category);
        }
    }
}
