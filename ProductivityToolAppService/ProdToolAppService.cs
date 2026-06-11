using ProductivityToolModels;
using ProductivityToolDataServices;

namespace ProductivityToolAppService
{
    public class ProdToolAppService
    {
        private readonly ProdToolDataServices dataService;

        public ProdToolAppService(ProdToolDataServices dataService)
        {
            this.dataService = dataService;
        }

        // ── Add ───────────────────────────────────────────────────────────────
        public string AddTask(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "Task name cannot be empty.";

            if (string.IsNullOrWhiteSpace(description))
                return "Task description cannot be empty.";

            if (dataService.GetTaskByName(name) != null)
                return "Task already exists.";

            dataService.AddTask(new ProdToolModels(name, description));
            return "Task added successfully.";
        }

        // ── Update Status ─────────────────────────────────────────────────────
        public string UpdateTaskStatus(string name, string status)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "Task name cannot be empty.";

            if (string.IsNullOrWhiteSpace(status))
                return "Status cannot be empty.";

            ProdToolModels? task = dataService.GetTaskByName(name);
            if (task == null)
                return "Task not found.";

            string cleanStatus = status.Trim().ToUpper();
            if (cleanStatus != "PENDING" && cleanStatus != "IN PROGRESS" && cleanStatus != "COMPLETED")
                return "Invalid status. Use Pending, In Progress, or Completed.";

            task.Status = cleanStatus;
            dataService.UpdateTask(task.Name, task);
            return "Task status updated successfully.";
        }

        // ── Edit (rename + re-describe) ───────────────────────────────────────
        public string EditTask(string currentName, string newName, string newDescription)
        {
            if (string.IsNullOrWhiteSpace(currentName))
                return "Current task name cannot be empty.";

            if (string.IsNullOrWhiteSpace(newName))
                return "New task name cannot be empty.";

            if (string.IsNullOrWhiteSpace(newDescription))
                return "New task description cannot be empty.";

            ProdToolModels? task = dataService.GetTaskByName(currentName);
            if (task == null)
                return "Task not found.";

            // Block duplicate only if it's a different task
            ProdToolModels? duplicate = dataService.GetTaskByName(newName);
            if (duplicate != null && newName.ToUpper() != currentName.ToUpper())
                return "Another task with that name already exists.";

            string oldName    = task.Name;
            task.Name         = newName.ToUpper();
            task.Description  = newDescription.ToUpper();
            dataService.UpdateTask(oldName, task);  // pass oldName so WHERE clause works
            return "Task updated successfully.";
        }

        // ── Delete ────────────────────────────────────────────────────────────
        public string DeleteTask(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "Task name cannot be empty.";

            return dataService.DeleteTask(name)
                ? "Task deleted successfully."
                : "Task not found.";
        }

        // ── Read ──────────────────────────────────────────────────────────────
        public List<ProdToolModels> GetAllTasks()
            => dataService.GetAllTasks();

        public string GetTaskSummary(string name)
        {
            ProdToolModels? task = dataService.GetTaskByName(name);
            if (task == null) return "Task not found.";
            return $"Task Name  : {task.Name}\n" +
                   $"Description: {task.Description}\n" +
                   $"Status     : {task.Status}";
        }
    }
}
