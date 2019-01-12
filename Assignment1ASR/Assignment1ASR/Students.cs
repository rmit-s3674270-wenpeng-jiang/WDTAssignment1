using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment1ASR
{
    class Students : Users
    {
        public Students(string _name, string _id, string _email) : base(_name, _id, _email)
        {
        }

        public static void makeBooking()
        {
            string roomName;
            string date;
            string time;
            string studentId;

            // Check room availability, if room is in slot list
            do
            {
                Console.WriteLine("Enter room name: ");
                roomName = Console.ReadLine(); // Enter room name
            } while (!Program.checkSlotRoom(roomName));

            do
            {
                Console.WriteLine("Enter date for slot (dd-mm-yyyy)");
                date = Console.ReadLine();
            } while (!Program.checkSlotDate(date));

            do
            {
                Console.WriteLine("Enter time for slot (hh:mm): ");
                time = Console.ReadLine();
            } while (!Program.checkSlotTime(time));

        //   do
          //  {
                Console.WriteLine("Enter student ID: ");
                studentId = Console.ReadLine();
            //需要在数据库找student的id是否存在
            //  } while ();
            Bookings booking = new Bookings(roomName, date, time, studentId);
            foreach (Slots slots in Program.slotList){
                if (booking.room == slots.room && booking.date == slots.date && booking.time == slots.start)
                {
                    slots.booking = studentId;
                }
            }
            Program.bookingList.Add(booking);

        }
    }
}
