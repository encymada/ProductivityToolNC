using System;
using System.Collections.Generic;
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

        public string AddTask(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Task name cannot be empty.";
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                return "Task description cannot be empty.";
            }

            ProdToolModels existingTask = dataService.GetTaskByName(name);
            if (existingTask != null)
            {
                return "Task already exists.";
            }

            ProdToolModels newTask = new ProdToolModels(name, description);
            dataService.AddTask(newTask);

            return "Task added successfully.";
        }

        public string UpdateTaskStatus(string name, string status)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Task name cannot be empty.";
            }

            if (string.IsNullOrWhiteSpace(status))
            {
                return "Status cannot be empty.";
            }

            ProdToolModels task = dataService.GetTaskByName(name);
            if (task == null)
            {
                return "Task not found.";
            }

            string cleanStatus = status.Trim().ToUpper();

            if (cleanStatus != "PENDING" &&
                cleanStatus != "IN PROGRESS" &&
                cleanStatus != "COMPLETED")
            {
                return "Invalid status. Use Pending, In Progress, or Completed.";
            }

            task.Status = cleanStatus;
            dataService.UpdateTask(task);

            return "Task status updated successfully.";
        }

        public string EditTask(string currentName, string newName, string newDescription)
        {
            if (string.IsNullOrWhiteSpace(currentName))
            {
                return "Current task name cannot be empty.";
            }

            if (string.IsNullOrWhiteSpace(newName))
            {
                return "New task name cannot be empty.";
            }

            if (string.IsNullOrWhiteSpace(newDescription))
            {
                return "New task description cannot be empty.";
            }

            ProdToolModels task = dataService.GetTaskByName(currentName);
            if (task == null)
            {
                return "Task not found.";
            }

            ProdToolModels duplicateTask = dataService.GetTaskByName(newName);
            if (duplicateTask != null && duplicateTask != task)
            {
                return "Another task with that name already exists.";
            }

            task.Name = newName.ToUpper();
            task.Description = newDescription.ToUpper();
            dataService.UpdateTask(task);

            return "Task updated successfully.";
        }

        public List<ProdToolModels> GetAllTasks()
        {
            return dataService.GetAllTasks();
        }

        public string GetTaskSummary(string name)
        {
            ProdToolModels task = dataService.GetTaskByName(name);

            if (task == null)
            {
                return "Task not found.";
            }

            return "Task Name: " + task.Name +
                   "\nDescription: " + task.Description +
                   "\nStatus: " + task.Status;
        }

        public string DeleteTask(string name)
        {
            bool deleted = dataService.DeleteTask(name);

            if (!deleted)
            {
                return "Task not found.";
            }

            return "Task deleted successfully.";
        }
    }
}
