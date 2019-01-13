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

        int maxBookingPerDayStudent = 1;
        int maxBookingPerDaySlot = 2;

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
                // A slot can have a maximum of 1 student booked into it
                if (booking.room == slots.room && booking.date == slots.date && booking.time == slots.start && slots.booking == null)
                {
                    slots.booking = studentId;
                    Program.bookingList.Add(booking);
                    Console.WriteLine("You add a new booking");
                }
                else
                {
                    Console.WriteLine("This slot has been booked");
                    break;
                }
            }
        }

        public bool checkMaxBooking(string studentId, string date)
        {
            int count = 0;

            foreach(Bookings booking in Program.bookingList)
            {
                if (studentId == booking.studentId && date == booking.date)
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

        public bool checkRoomSlot(string roomName, string date) // Each room can be booked for a maximum of 2 slots per day
        {
            int count = 0;

            foreach (Bookings booking in Program.bookingList)
            {
                if (roomName == booking.room && date == booking.date)
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
