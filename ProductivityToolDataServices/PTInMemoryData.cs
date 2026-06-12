using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductivityToolModels;

namespace ProductivityToolDataServices
{
    public class PTInMemoryData : IPTDataService
    {
        private readonly List<PTTask> _tasks = new();
        private readonly List<PTCategory> _categories = new();
        private int _nextTaskId = 1;
        private int _nextCatId = 1;

        public Task<IEnumerable<PTTask>> GetAllTasksAsync()
            => Task.FromResult(_tasks.AsEnumerable());

        public Task<PTTask?> GetTaskByIdAsync(int id)
            => Task.FromResult(_tasks.FirstOrDefault(t => t.Id == id));

        public Task<PTTask> AddTaskAsync(PTTask task)
        {
            task.Id = _nextTaskId++;
            _tasks.Add(task);
            return Task.FromResult(task);
        }

        public Task<PTTask?> UpdateTaskAsync(int id, PTTask task)
        {
            var existing = _tasks.FirstOrDefault(t => t.Id == id);
            if (existing == null) return Task.FromResult<PTTask?>(null);
            existing.Title = task.Title;
            existing.IsCompleted = task.IsCompleted;
            existing.DueDate = task.DueDate;
            return Task.FromResult<PTTask?>(existing);
        }

        public Task<bool> DeleteTaskAsync(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return Task.FromResult(false);
            _tasks.Remove(task);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<PTCategory>> GetAllCategoriesAsync()
            => Task.FromResult(_categories.AsEnumerable());

        public Task<PTCategory> AddCategoryAsync(PTCategory category)
        {
            category.Id = _nextCatId++;
            _categories.Add(category);
            return Task.FromResult(category);
        }
    }
}
