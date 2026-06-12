using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductivityToolModels;

namespace ProductivityToolDataServices
{
    public interface IPTDataService
    {
        // Tasks
        Task<IEnumerable<PTTask>> GetAllTasksAsync();
        Task<PTTask?> GetTaskByIdAsync(int id);
        Task<PTTask> AddTaskAsync(PTTask task);
        Task<PTTask?> UpdateTaskAsync(int id, PTTask task);
        Task<bool> DeleteTaskAsync(int id);

        // Categories (optional)
        Task<IEnumerable<PTCategory>> GetAllCategoriesAsync();
        Task<PTCategory> AddCategoryAsync(PTCategory category);
    }
}
