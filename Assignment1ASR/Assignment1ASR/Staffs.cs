using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment1ASR
{
    class Staffs : Users
    {
        public Staffs(string _name, string _id, string _email) : base(_name, _id, _email)
        {
        }

        public static void createSlot()
        {
            string roomName;
            string date;
            string time;
            string staffId;

            // Check room availability
            do
            {
                Console.WriteLine("Enter room name: ");
                roomName = Console.ReadLine(); // Enter room name
            } while (!Program.checkRoom(roomName));

            do
            {
                Console.WriteLine("Enter date for slot (dd-mm-yyyy): ");
                date = Console.ReadLine();// Enter date
            } while (!Program.checkDate(date));

            do
            {
                Console.WriteLine("Enter time for slot (hh:mm): ");
                time = Console.ReadLine();
            } while (!Program.checkTime(time));

            Console.WriteLine("Enter staff ID: ");
            staffId = Console.ReadLine();

            Slots slot = new Slots(roomName, date, time, staffId);
            Program.slotList.Add(slot);
        }


    }
}
