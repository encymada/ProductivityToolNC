using System;
using System.Collections.Generic;
using ProductivityToolModels;
using ProductivityToolDataServices;
using ProductivityToolAppService;

namespace ProductivityToolCLAVERIA
{
    internal class Program
    {
        static void Main(string[] args)
        {


            ProdToolDataServices dataService = new ProdToolDataServices();
            ProdToolAppService   appService  = new ProdToolAppService(dataService);

            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("============= PRODUCTIVITY TOOL =============");
                Console.WriteLine("[1] Add New Task");
                Console.WriteLine("[2] Check / Update Task Status");
                Console.WriteLine("[3] Edit Existing Task");
                Console.WriteLine("[4] View All Tasks");
                Console.WriteLine("[5] Delete a Task");
                Console.WriteLine("[6] Exit");
                Console.Write("Enter Choice: ");

                if (!byte.TryParse(Console.ReadLine(), out byte choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1: AddTask(appService);          break;
                    case 2: UpdateTaskStatus(appService); break;
                    case 3: EditTask(appService);         break;
                    case 4: ViewAllTasks(appService);     break;
                    case 5: DeleteTask(appService);       break;
                    case 6:
                        running = false;
                        Console.WriteLine("Exiting program. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Incorrect input. Please choose 1-6.");
                        break;
                }
            }
        }

        // ── Add Task ──────────────────────────────────────────────────────────
        static void AddTask(ProdToolAppService appService)
        {
            Console.WriteLine();
            Console.WriteLine("--- ADD NEW TASK ---");

            Console.Write("Enter Task Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter Task Description: ");
            string description = Console.ReadLine() ?? "";

            Console.WriteLine(appService.AddTask(name, description));
        }

        // ── Update Status ─────────────────────────────────────────────────────
        static void UpdateTaskStatus(ProdToolAppService appService)
        {
            Console.WriteLine();
            Console.WriteLine("--- CHECK / UPDATE TASK STATUS ---");

            Console.Write("Enter Task Name: ");
            string name = Console.ReadLine() ?? "";

            Console.WriteLine("Select New Status:");
            Console.WriteLine("  [1] Pending");
            Console.WriteLine("  [2] In Progress");
            Console.WriteLine("  [3] Completed");
            Console.Write("Enter Choice: ");

            string status = Console.ReadLine() switch
            {
                "1" => "PENDING",
                "2" => "IN PROGRESS",
                "3" => "COMPLETED",
                _   => ""
            };

            if (string.IsNullOrEmpty(status))
            {
                Console.WriteLine("Invalid status choice.");
                return;
            }

            Console.WriteLine(appService.UpdateTaskStatus(name, status));
        }

        // ── Edit Task ─────────────────────────────────────────────────────────
        static void EditTask(ProdToolAppService appService)
        {
            Console.WriteLine();
            Console.WriteLine("--- EDIT EXISTING TASK ---");

            Console.Write("Enter Current Task Name: ");
            string currentName = Console.ReadLine() ?? "";

            Console.Write("Enter New Task Name: ");
            string newName = Console.ReadLine() ?? "";

            Console.Write("Enter New Task Description: ");
            string newDescription = Console.ReadLine() ?? "";

            Console.WriteLine(appService.EditTask(currentName, newName, newDescription));
        }

        // ── View All ──────────────────────────────────────────────────────────
        static void ViewAllTasks(ProdToolAppService appService)
        {
            Console.WriteLine();
            Console.WriteLine("--- VIEW ALL TASKS ---");

            List<ProdToolModels> tasks = appService.GetAllTasks();

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"Task #{i + 1}");
                Console.WriteLine($"  Name       : {tasks[i].Name}");
                Console.WriteLine($"  Description: {tasks[i].Description}");
                Console.WriteLine($"  Status     : {tasks[i].Status}");
                Console.WriteLine("  -----------------------------------");
            }
        }

        // ── Delete Task ───────────────────────────────────────────────────────
        static void DeleteTask(ProdToolAppService appService)
        {
            Console.WriteLine();
            Console.WriteLine("--- DELETE TASK ---");

            Console.Write("Enter Task Name to Delete: ");
            string name = Console.ReadLine() ?? "";

            Console.Write($"Are you sure you want to delete '{name.ToUpper()}'? (y/n): ");
            string confirm = Console.ReadLine() ?? "";

            if (confirm.Trim().ToLower() != "y")
            {
                Console.WriteLine("Delete cancelled.");
                return;
            }

            Console.WriteLine(appService.DeleteTask(name));
        }
    }
}
