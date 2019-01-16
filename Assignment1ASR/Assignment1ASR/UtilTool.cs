using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Assignment1ASR
{
    class UtilTool
    {
        public static void listRoom()
        {
            foreach (Room room in Program.roomList)
            {
                Console.WriteLine(room.name);
            }
        }

        public static void listSlot()
        {
            string date;
            do
            {
                Console.WriteLine("Enter date for slots (dd-mm-yyyy): ");
                date = Console.ReadLine();
            } while (!checkDate(date));

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Room   Start time   End time   Staff ID   Bookings");
            foreach (Slots slots in Program.slotList)
            {             
                if (date == slots.date)
                {                    
                    Console.WriteLine(slots.room + "       " + slots.start + "        " + slots.end + "     " +
                    slots.staffId + "     " + slots.booking);
                }                
            }
        }

        public static bool checkInput(string input) // Check if input is valid
        {
            int number;
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
            foreach (Room room in Program.roomList)
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
            foreach (Slots slots in Program.slotList)
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
            foreach (Slots slots in Program.slotList)
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
            foreach (Slots slots in Program.slotList)
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

        public static void printAvailableRoom(string date)
        {
            var rooms = Program.slotList.Where(x => x.date == date && (x.booking == null || x.booking == "")).ToList();
            if (!rooms.Any())
            {
                Console.WriteLine("No rooms found!");
            }
            Console.WriteLine("Room");
            foreach (var avaliableRoom in Program.slotList.Where(x => x.date == date && (x.booking == null || x.booking == "")))
            {               
                Console.WriteLine(avaliableRoom.room);
            }
        }

        public static void printAvailableStaff(string date, string staffId)
        {
            var staff = Program.slotList.Where(x => (x.date == date && x.staffId == staffId) && (x.booking == null || x.booking == "")).ToList();
            if (!staff.Any())
            {
                Console.WriteLine("No staffs found!");
                return;
            }

            Console.WriteLine("Room   Start time   End time");
            foreach (var avaliableStaff in Program.slotList.Where(x => (x.date == date && x.staffId == staffId) && (x.booking == null || x.booking == "")))
            {               
                Console.WriteLine(avaliableStaff.room + "       " + avaliableStaff.start + "        " + avaliableStaff.end);
            }
        }

        public static string GetEndTime(string startTime)
        {
            string strRet = startTime;
            int nPos = strRet.IndexOf(":");
            if (nPos != -1)
            {
                string leftStr = strRet.Substring(0, nPos);
                string rightStr = strRet.Substring(nPos);
                int nHour;
                int.TryParse(leftStr, out nHour);
                nHour += 1;
                strRet = nHour.ToString().PadLeft(2, '0');
                strRet += rightStr;
            }
            return strRet;
        }

        public static bool checkStaffId(string staffId)
        {
            foreach (Staffs staff in Program.staffList)
            {
                if (staffId == staff.id)
                {
                    return true;
                }
            }
            Console.WriteLine("This staff does not exist in database");
            return false;
        }

        public static bool checkStudentID(string id)
        {
            foreach (Students s in Program.studentList)
            {
                if (id == s.id)
                {
                    return true;
                }
            }
            Console.WriteLine("This student does not exist in database");
            return false;
        }
    }
}
