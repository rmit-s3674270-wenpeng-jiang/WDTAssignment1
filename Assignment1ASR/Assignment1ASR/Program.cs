using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Assignment1ASR
{
    class Program
    {
        static string input;
        public static List<Room> roomList = new List<Room>();
        public static List<Slots> slotList = new List<Slots>();
        public static List<Staffs> staffList = new List<Staffs>();       
        public static List<Students> studentList = new List<Students>();

        static void Main(string[] args)
        {
            int mSelection; // Main Menu Selection

            Database.readRoom();
            Database.readSlot();
            Database.readStaff();
            Database.readStudent();

            Factory fa = new StaffFactory();
            Factory fb = new StudentFactory();
            Users staff = fa.CreateUser();
            Users student = fb.CreateUser();

            //Main Menu 
            do
            {
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine("Main menu: 1.List rooms 2.List slots 3.Staff menu 4.Student menu 5.Exit");
                do
                {
                    input = Console.ReadLine();
                }
                while (!UtilTool.checkInput(input));

                mSelection = int.Parse(input);
                if (mSelection == 1)
                {
                    Console.WriteLine("--- List rooms ---");
                    Console.WriteLine("Room");
                    UtilTool.listRoom();
                }
                else if (mSelection == 2)
                {
                    Console.WriteLine("--- list slots ---");
                    UtilTool.listSlot();
                }
                else if (mSelection == 3)//staff menu
                {
                    staff.Menu();
                }

                else if (mSelection == 4) // student menu
                {
                    student.Menu();
                }

                else if (mSelection == 5)
                {
                    Console.WriteLine("--- Exits --- ");
                }

            } while (mSelection != 5);

        }
    }
}
