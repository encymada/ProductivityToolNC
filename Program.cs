using System;
using System.Collections.Generic;

namespace ProductivityToolCLAVERIA
{
    internal class Program
    {
            static List <TaskItem>tasks = new List<TaskItem>();      
            static void Main(string[]args){

            byte choice;
            char tryAgain = 'Y';

            Console.WriteLine("------------- PRODUCTIVITY TOOL -------------");

            Console.WriteLine("[1] Add New Task");
            Console.WriteLine("[2] Check/ Update Task Status");
            Console.WriteLine("[3] Edit Existing Tasks");
            Console.WriteLine("[4] View All Tasks");
            Console.Write("Enter Choice: ");
            choice = Convert.ToByte(Console.ReadLine());

            switch (choice)
            {
                case (1):
                    AddTask();
                    break;

                case (2):
                    UpdateTaskStatus();
                    break;
                    /*Console.WriteLine("CHECK TASK STATUS: ");
                    break;*/
                
                case (3):
                    EditTask();
                    break;
                    /*Console.WriteLine("EDIT EXISTING TASKS: ");
                    Console.WriteLine("ENTER TASK NAME: ");
                    Console.WriteLine("ENTER TASK DESCRIPTION: ");
                    break;*/

                case (4):
                    ViewAllTask();
                    break;
                    /*Console.WriteLine("VIEW ALL EXISTING TASKS: ");
                    Console.WriteLine("ENTER TASK NAME: ");
                    break;*/
                    /*Console.WriteLine("INCORRECT INPUT!!");
                    Environment.Exit(0);
                    break;*/
            }

            static void AddTask()
                {
                    Console.WriteLine("- ADD NEW TASK: ");
                    Console.WriteLine("ENTER TASK NAME: ");
                    string name = Console.ReadLine().ToUpper();
                    Console.WriteLine("ENTER TASK DESCRIPTION: ");
                    string description = Console.ReadLine().ToUpper();

                        tasks.Add(new TaskItem(name,description));
                        System.Console.WriteLine("TASK ADDED SUCCESSFULLY!");
                    


                }

            static void UpdateTaskStatus()
                {
                    

                    
                }

            static void EditTask()
                {
                    


                }

            static void ViewAllTask()
                {
                    


                }

            




        }
    }
}


class TaskItem
{
    public string Name { get; set; }
    public string Description { get; set; }

    public TaskItem(string name, string description)
    {
        Name = name;
        Description = description;
    }
}

