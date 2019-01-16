using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Assignment1ASR
{
    class Students : Users
    {
        public Students(string _id, string _name, string _email) : base(_id, _name, _email)
        {
        }

        public Students() { }

        private int maxBookingPerDayStudent = 1;
        private string input;
        int stSelection; // Student Menu Selection
        private SqlCommand cmd;


        // Student submenu
        public override void Menu()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("--- Student menu --- ");            
            do
            {
                Console.WriteLine("--------------------------------------------------------------------");
                Console.WriteLine("1. List students 2.Staff availability 3.Make booking 4.Cancel booking 5.Exit");
                //check input
                do
                {
                    input = Console.ReadLine();
                }
                while (!UtilTool.checkInput(input));

                stSelection = int.Parse(input);

                if (stSelection == 1)
                {
                    Console.WriteLine("--- List students ---");
                    foreach (Students s in Program.studentList)
                    {
                        Console.WriteLine(s.id + " " + s.name + " " + s.email);
                    }
                    Console.WriteLine("--------------------");
                }
                else if (stSelection == 2)
                {
                    Console.WriteLine("--- Staff availability---");
                    string date;
                    string staffId;
                    do
                    {
                        Console.WriteLine("Enter date for staff availability (dd-mm-yyyy): ");
                        date = Console.ReadLine();
                    } while (!UtilTool.checkSlotDate(date));

                    do
                    {
                        Console.WriteLine("Enter staff ID: ");
                        staffId = Console.ReadLine();
                    } while (!UtilTool.checkStaffId(staffId));
                    
                    UtilTool.printAvailableStaff(date, staffId);
                    Console.WriteLine("--------------------");
                }
                else if (stSelection == 3)
                {
                    Console.WriteLine("--- Make booking ---");
                    makeBooking();
                    Console.WriteLine("----------------------");

                }
                else if (stSelection == 4)
                {
                    Console.WriteLine("--- Cancel booking ---");
                    cancelBooking();
                    Console.WriteLine("--------------------");
                }
                else if (stSelection == 5)
                {
                    Console.WriteLine("--- Back to main menu --- ");
                    Console.WriteLine("--------------------");
                    break;
                }
            } while (stSelection != 5);
        }

        public void makeBooking()
        {
            string roomName;
            string date;
            string time;
            string studentId;

            // Check room availability, if room is in slot list
            do
            {
                Console.WriteLine("Enter room name: ");
                roomName = Console.ReadLine(); 
            } while (!UtilTool.checkSlotRoom(roomName));

            do
            {
                Console.WriteLine("Enter date for slot (dd-mm-yyyy)");
                date = Console.ReadLine();
            } while (!UtilTool.checkSlotDate(date));

            do
            {
                Console.WriteLine("Enter time for slot (hh:mm): ");
                time = Console.ReadLine();
            } while (!UtilTool.checkSlotTime(time));

            //check student ID in database
            Console.WriteLine("Enter student ID: ");
            studentId = Console.ReadLine();
            if (!UtilTool.checkStudentID(studentId))
            {
                return;
            }
           
            if (!checkMaxBooking(studentId, date))
            {
                return;
            }

            foreach (Slots slots in Program.slotList){
                // A slot can have a maximum of 1 student booked into it
                if (roomName == slots.room && date == slots.date && time == slots.start && (slots.booking == null || slots.booking == ""))
                {       
                    slots.booking = studentId;
                    updateBookingDatabase(roomName, date, time, studentId);
                    Console.WriteLine("You add a new booking");
                    return;
                }  
            }
            Console.WriteLine("This room has been booked");           
        }

        public void cancelBooking()
        {
            string roomName;
            string date;
            string time;

            Console.WriteLine("Enter room name: ");
            roomName = Console.ReadLine();
            if (!UtilTool.checkRoom(roomName))
            {
                return;
            }

            Console.WriteLine("Enter date for slot (dd-mm-yyyy): ");
            date = Console.ReadLine();
            if (!UtilTool.checkDate(date))
            {
                return;
            }

            Console.WriteLine("Enter time for slot (hh:mm)");
            time = Console.ReadLine();
            if (!UtilTool.checkTime(time))
            {
                return;
            }

            foreach (Slots slot in Program.slotList)
            {
                if (slot.room == roomName && slot.date == date && slot.start == time && (slot.booking != null && slot.booking != ""))
                {
                    slot.booking = null;
                    Console.WriteLine("Successfully cancelled");
                    cancelBookingUpdate(roomName, date, time);
                    return;
                }
                if (slot.room == roomName && slot.date == date && slot.start == time && (slot.booking == null || slot.booking == ""))
                {
                    Console.WriteLine("Unable to remove this slot because it has not been booked yet");
                    return;
                }
            }
            Console.WriteLine("Slot does not exist");
        }


        public bool checkMaxBooking(string studentId, string date) // Each student can only add one booking per day
        {
            int count = 0;

            foreach(Slots s in Program.slotList)
            {
                if (studentId == s.booking && date == s.date)
                {
                    count++;
                }
            }

            if (count < maxBookingPerDayStudent)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Unable to add booking because " + studentId + " has added 1 booking on " + date);
                return false;
            }
        }

       

        public void updateBookingDatabase(string room, string date, string time, string studentId)
        {
            cmd = Database.Dblink().CreateCommand();
            cmd.CommandText = string.Format("Update [Slot] Set BookedInStudentId = '{0}' " +
                                    "Where roomId = '{1}' AND date = '{2}' AND startTime = '{3}'", studentId, room, date, time);

            Database.executeSql(cmd);
        }

        public void cancelBookingUpdate(string room, string date, string time)
        {
            cmd = Database.Dblink().CreateCommand();
            cmd.CommandText = string.Format("Update [Slot] Set BookedInStudentId = null " +
                                    "Where roomId = '{0}' AND date = '{1}' AND startTime = '{2}'", room, date, time);
            Database.executeSql(cmd);
        }

    
  
    }
}
