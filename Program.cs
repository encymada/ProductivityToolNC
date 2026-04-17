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
            ProdToolAppService appService = new ProdToolAppService(dataService);

            //TaskService service = new TaskService();

            //// JSON
            //var tasks = service.LoadFromJson();

            //// DATABASE
            //service.InitializeDatabase();
            //service.AddToDatabase(new TaskItem { Title = "Test Task", IsCompleted = false });

            //var dbTasks = service.LoadFromDatabase();

            //bool running = true;

            while (running)
            {
                
                Console.WriteLine("------------- PRODUCTIVITY TOOL -------------");
                Console.WriteLine("[1] Add New Task");
                Console.WriteLine("[2] Check/ Update Task Status");
                Console.WriteLine("[3] Edit Existing Tasks");
                Console.WriteLine("[4] View All Tasks");
                Console.WriteLine("[5] Exit");
                Console.Write("Enter Choice: ");

                if (!byte.TryParse(Console.ReadLine(), out byte choice))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddTask(appService);
                        break;

                    case 2:
                        UpdateTaskStatus(appService);
                        break;

                    case 3:
                        EditTask(appService);
                        break;

                    case 4:
                        ViewAllTasks(appService);
                        break;

                    case 5:
                        running = false;
                        Console.WriteLine("Exiting program...");
                        break;

                    default:
                        Console.WriteLine("Incorrect input.");
                        break;
                }
            }
        }

        static void AddTask(ProdToolAppService appService)
        {
            
            Console.WriteLine("- ADD NEW TASK -");

            Console.Write("Enter Task Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Task Description: ");
            string description = Console.ReadLine();

            string result = appService.AddTask(name, description);
            Console.WriteLine(result);
        }

        static void UpdateTaskStatus(ProdToolAppService appService)
        {
            
            Console.WriteLine("- CHECK / UPDATE TASK STATUS -");

            Console.Write("Enter Task Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter New Status (Pending / In Progress / Completed): ");
            string status = Console.ReadLine();

            string result = appService.UpdateTaskStatus(name, status);
            Console.WriteLine(result);
        }

        static void EditTask(ProdToolAppService appService)
        {
          
            Console.WriteLine("- EDIT EXISTING TASK -");

            Console.Write("Enter Current Task Name: ");
            string currentName = Console.ReadLine();

            Console.Write("Enter New Task Name: ");
            string newName = Console.ReadLine();

            Console.Write("Enter New Task Description: ");
            string newDescription = Console.ReadLine();

            string result = appService.EditTask(currentName, newName, newDescription);
            Console.WriteLine(result);
        }

        static void ViewAllTasks(ProdToolAppService appService)
        {
       
            Console.WriteLine("- VIEW ALL TASKS -");

            List<ProdToolModels> tasks = appService.GetAllTasks();

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine("Task #" + (i + 1));
                Console.WriteLine("Name: " + tasks[i].Name);
                Console.WriteLine("Description: " + tasks[i].Description);
                Console.WriteLine("Status: " + tasks[i].Status);
                Console.WriteLine("-----------------------------------");
            }
        }
    }
}