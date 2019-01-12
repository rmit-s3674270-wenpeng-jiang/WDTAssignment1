using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Assignment1ASR
{
    class Program
    {
        static string input;
        static int number;
        public static List<Room> roomList = new List<Room>();
        public static List<Slots> slotList = new List<Slots>();
        public static List<Bookings> bookingList = new List<Bookings>();

        static void Main(string[] args)
        {
            roomList.Add(new Room("A"));
            
            
            int mSelection; // Main Menu Selection
            int sfSelection; // Staff Menu Selection
            int stSelection; // Student Menu Selection

            //Main Menu 
            do
            {
                Console.WriteLine("Main menu: 1.List rooms 2.List slots 3.Staff menu 4.Student menu 5.Exit");

                do
                {
                    input = Console.ReadLine();
                }
                while (!Program.checkInput(input));

                mSelection = int.Parse(input);
                if (mSelection == 1)
                {
                    Console.WriteLine("--- List rooms ---");
                    Console.WriteLine("Room");
                    foreach (Room room in roomList)
                    {
                        Console.WriteLine(room.name);
                    }
                }
                else if (mSelection == 2)
                {
                    Console.WriteLine("--- List slots ---");
                    foreach (Slots slots in slotList)
                    {
                        Console.WriteLine(slots.room + " " +slots.date + " " + slots.start + " " + slots.staffId + " " + slots.booking);
                    }
                }
                else if (mSelection == 3)//staff menu
                {
                    Console.WriteLine("--- Staff menu ---");
                    Staffs staffs = new Staffs();

                    do
                    {
                        Console.WriteLine("1. List staff 2.Room availability 3.Create slot 4.Remove slot 5.Exit");
                        //check input
                        do
                        {
                            input = Console.ReadLine();
                        }
                        while (!Program.checkInput(input));

                        sfSelection = int.Parse(input);
                        if (sfSelection == 1)
                        {
                            Console.WriteLine("--- List staff ---");
                            //把数据库中的staff打印出来
                        }
                        else if (sfSelection == 2)
                        {
                            Console.WriteLine("--- Room availability ---");
                        }
                        else if (sfSelection == 3)
                        {
                            Console.WriteLine("--- Create slots --- ");// Create a slot
                            staffs.createSlot();
                        }
                        else if (sfSelection == 4)
                        {
                            Console.WriteLine("--- Remove slots --- ");
                            //在slot有booking的情况下无法删除
                        }
                        else if (sfSelection == 5)
                        {
                            Console.WriteLine("--- Back to main menu --- ");
                            break;
                        }
                    } while (sfSelection != 5);
                }

                else if (mSelection == 4) // student menu
                {
                    Console.WriteLine("--- Student menu --- ");

                    do
                    {
                        Console.WriteLine("1. List students 2.Staff availability 3.Make booking 4.Cancel booking 5.Exit");
                        //check input
                        do
                        {
                            input = Console.ReadLine();
                        }
                        while (!Program.checkInput(input));

                        stSelection = int.Parse(input);

                        if (stSelection == 1)
                        {
                            Console.WriteLine("--- List students ---");
                            //把数据库中的student打印出来
                        }
                        else if (stSelection == 2)
                        {
                            Console.WriteLine("--- Staff availability---");
                        }
                        else if (stSelection == 3)
                        {
                            Console.WriteLine("--- Make booking ---");
                            Students.makeBooking();

                        }
                        else if (stSelection == 4)
                        {
                            Console.WriteLine("--- Cancel booking ---");
                        }
                        else if (stSelection == 5)
                        {
                            Console.WriteLine("--- Back to main menu --- ");
                            break;
                        }
                    } while (stSelection != 5);
                }

                else if (mSelection == 5)
                {
                    Console.WriteLine("--- Exits --- ");
                }

            } while (mSelection != 5);
           
        }

       

        public static bool checkInput(string input) // Check if input is valid
        {
            try
            {
                if (!int.TryParse(input, out number))
                {
                    throw new InvalidInputException();
                }

                int selection = int.Parse(input);

                if (selection != 1 && selection != 2 && selection != 3 && selection != 4 && selection != 5)
                {
                    throw new InvalidInputException();
                }
                else
                {
                    return true;
                }
            }
            catch (InvalidInputException)
            {
                Console.WriteLine("Input a valid number");
                return false;
            }
        }

        public static bool checkRoom(string roomName)
        {
            foreach (Room room in roomList)
            {
                if (roomName == room.name)
                {
                    return true;
                }
            }
            Console.WriteLine("This room does not exist");
            return false;
        }

        public static bool checkDate(string date)
        {
            string pattern = @"^(0[1-9]|1[0-9]|2[0-9]|3[0-1])[-.](0[1-9]|1[0-2])[-.]\d{4}$"; //check format dd-mm-yyyy

            Match match = Regex.Match(date, pattern);
            if (match.Success)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Format is not correct");
                return false;
            }
        }

        public static bool checkTime(string time)
        {
            string pattern = @"^(09|1[0-3])[:.]00$"; //check format hh-mm
            Match match = Regex.Match(time, pattern);
            if (match.Success)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Enter time between 09:00 and 13:00");
                return false;
            }
        }


        // Check room info in slot list
        public static bool checkSlotRoom(string roomName)
        {
            foreach (Slots slots in slotList)
            {
                if (roomName == slots.room)
                {
                    return true;
                }
            }
            Console.WriteLine("Room is not available");
            return false;
        }

        public static bool checkSlotDate(string date)
        {
            foreach (Slots slots in slotList)
            {
                if (date == slots.date)
                {
                    return true;
                }
            }
            Console.WriteLine("Date is not available");
            return false;
        }

        public static bool checkSlotTime(string time)
        {
            foreach (Slots slots in slotList)
            {
                if (time == slots.start)
                {
                    return true;
                }
            }
            Console.WriteLine("Time is not available");
            return false;
        }
        //check room info in slot list


        
    }
}
