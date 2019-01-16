using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Assignment1ASR
{
    class Staffs : Users
    {
        public Staffs(string _id, string _name, string _email) : base(_id, _name, _email)
        {
        }

        public Staffs() { }

        private int sfSelection; // Staff Menu Selection
        private string input;

        private int maxSlotPerDayStaff = 4;
        private int maxBookingPerDaySlot = 2;

        private SqlCommand cmd;

        // Staff submenu
        public override void Menu()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("--- Staff menu ---");

            do
            {
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("1. List staff 2.Room availability 3.Create slot 4.Remove slot 5.Exit");
                //check input
                do
                {
                    input = Console.ReadLine();
                }
                while (!UtilTool.checkInput(input));

                sfSelection = int.Parse(input);
                if (sfSelection == 1)
                {
                    foreach (Staffs staff in Program.staffList)
                    {
                        Console.WriteLine(staff.id + " " + staff.name + " " + staff.email);
                    }
                }
                else if (sfSelection == 2)
                {
                    Console.WriteLine("--- Room availability ---");
                    do
                    {
                        Console.WriteLine("Enter date for room availability (dd-mm-yyyy):  ");
                        input = Console.ReadLine();
                    } while (!UtilTool.checkDate(input));
                    
                    UtilTool.printAvailableRoom(input);
                }

                else if (sfSelection == 3)
                {
                    Console.WriteLine("--- Create slots --- ");// Create a slot
                    createSlot();
                    Console.WriteLine("----------------------");
                }
                else if (sfSelection == 4)
                {
                    Console.WriteLine("--- Remove slots --- ");
                    removeSlot();
                }
                else if (sfSelection == 5)
                {
                    Console.WriteLine("--- Back to main menu --- ");
                    break;
                }
            } while (sfSelection != 5);
        }

        public void createSlot()
        {
            string roomName;
            string date;
            string startTime;
            string endTime;
            string staffId;
            string booking = null;
            

            // Check room availability
            do
            {
                Console.WriteLine("Enter room name: ");
                roomName = Console.ReadLine(); // Enter room name
            } while (!UtilTool.checkRoom(roomName));

            do
            {
                Console.WriteLine("Enter date for slot (dd-mm-yyyy): ");
                date = Console.ReadLine();// Enter date
            } while (!UtilTool.checkDate(date));

            if (!checkRoomSlot(roomName, date))
            {
                return;
            }

            do
            {
                Console.WriteLine("Enter time for slot (hh:mm): ");
                startTime = Console.ReadLine();
            } while (!UtilTool.checkTime(startTime));

            endTime = UtilTool.GetEndTime(startTime);

            do
            {
                Console.WriteLine("Enter staff ID: ");
                staffId = Console.ReadLine();
            } while (!checkMaxSlot(staffId,date));

            //check staff id in database
            if (!UtilTool.checkStaffId(staffId))
            {
                return;
            }

            foreach (Slots s in Program.slotList)
            {
                if (roomName == s.room && date == s.date && startTime == s.start)
                {
                    Console.WriteLine("Unable to create because it has been created");
                    return;
                }
            }

            Slots slot = new Slots(roomName, date, startTime, endTime, staffId, booking);
            
            Program.slotList.Add(slot);

            addSlotToDataBase(roomName, date, startTime, endTime, staffId);
            
            Console.WriteLine("You created a new slot");
        }

        public void removeSlot()
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

            foreach(Slots slot in Program.slotList)
            {
                if (slot.room == roomName && slot.date == date && slot.start == time && (slot.booking == null || slot.booking == ""))
                {
                    Program.slotList.Remove(slot);
                    deleteSlotFromDatabase(roomName, date, time);
                    Console.WriteLine("Successfully removed");
                    return;
                }
                if (slot.room == roomName && slot.date == date && slot.start == time && (slot.booking != null || slot.booking != ""))
                {
                    Console.WriteLine("Unable to remove this slot because it has been booked");
                    return;
                }
            }
            Console.WriteLine("Slot does not exist");

        }

        public void addSlotToDataBase(string room, string date, string start, string end, string staffId)
        {
            cmd = Database.Dblink().CreateCommand();
            cmd.CommandText = string.Format("Insert into [Slot] (RoomID, Date, StartTime, EndTime, StaffID) " +
                        "values ('{0}','{1}','{2}','{3}','{4}')", room, date, start, end, staffId);
            Database.executeSql(cmd);
        }

        public void deleteSlotFromDatabase(string room, string date, string time)
        {
            cmd = Database.Dblink().CreateCommand();
            cmd.CommandText = string.Format("Delete from [Slot] Where roomId = '{0}' AND date = '{1}' AND startTime = '{2}'",
                                room, date, time);
            Database.executeSql(cmd);
        }

        //Each staff cannot creat slots more than 4 per day
        public bool checkMaxSlot(string staffid, string date)
        {
            int count = 0;

            foreach(Slots slots in Program.slotList)
            {
                if(staffid == slots.staffId && date == slots.date)
                {
                    count++;
                }
            }

            if (count < maxSlotPerDayStaff)
            {
                return true;
            }
            else
            {
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("Unable to create a slot because " + staffid + " has created 4 slots on " + date);
                return false;
            }
        }



        // Each room can be booked for a maximum of 2 slots per day
        public bool checkRoomSlot(string roomName, string date) 
        {
            int count = 0;

            foreach (Slots s in Program.slotList)
            {
                if (roomName == s.room && date == s.date)
                {
                    count++;
                }
            }

            if (count < maxBookingPerDaySlot)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Unable to add this booking because Room " + roomName + " has been booked for 2 solts on " + date);
                return false;
            }
        }

      
    }
}
