using System;


namespace ProductivityToolCLAVERIA
{
    internal class Program
    {
        static void Main(string[] args)

        {

            byte choice;

            Console.WriteLine("------------- PRODUCTIVITY TOOL -------------");

            Console.WriteLine("[1] Add New Task");
            Console.WriteLine("[2] Check Task Status");
            Console.WriteLine("[3] Edit Existing Tasks");
            Console.WriteLine("[4] View all Tasks");
            Console.Write("Enter Choice: ");
            choice = Convert.ToByte(Console.ReadLine());

            switch (choice)
            {
                case (1):
                    Console.WriteLine("ADD NEW TASK: ");
                    Console.WriteLine("ENTER TASK NAME: ");
                    string taskName1 = Console.ReadLine();
                    Console.WriteLine("ENTER TASK DESCRIPTION: ");
                    string taskDesc1 = Console.ReadLine();
                    break;

                case (2):
                    Console.WriteLine("CHECK TASK STATUS: ");
                    break;
                
                case (3):
                    Console.WriteLine("EDIT EXISTING TASKS: ");
                    Console.WriteLine("ENTER TASK NAME: ");
                    Console.WriteLine("ENTER TASK DESCRIPTION: ");
                    break;

                case (4):
                    Console.WriteLine("VIEW ALL EXISTING TASKS: ");
                    Console.WriteLine("ENTER TASK NAME: ");
                    break;

                default:
                    Console.WriteLine("INCORRECT INPUT!!");
                    Environment.Exit(0);
                    break;


            }




        }
    }
}
