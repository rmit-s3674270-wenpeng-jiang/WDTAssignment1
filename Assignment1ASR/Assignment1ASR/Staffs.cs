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

        public Staffs() { }

        int maxSlot = 4;

        public void createSlot()
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

            do
            {
                Console.WriteLine("Enter staff ID: ");
                staffId = Console.ReadLine();
            } while (!checkMaxSlot(staffId,date));
                
            //需要在数据库中找staff的id是否存在

            //if (staffSlotPair.ContainsKey(staffId) && staffSlotPair[staffId] < 4 )//如果存在当前staff，且slot小于4，那么slot个数加1
            //{
            //    staffSlotPair[staffId]++;
            //}
            //else if (staffSlotPair.ContainsKey(staffId) && staffSlotPair[staffId] == 4)
            //{
            //    Console.WriteLine(staffId + " has created 4 slots today");    
            //}

            //    staffSlotPair.Add(staffId, 1);//如果不存在当前staff，则添加新的staff，并把slot设置为1

            
            
            
            Slots slot = new Slots(roomName, date, time, staffId);
            Program.slotList.Add(slot);
        }

        public bool checkMaxSlot(string staffid, string date) //检查staff每天创建的slot个数不能超过4
        {
            int count = 0;

            foreach(Slots slots in Program.slotList)
            {
                if(staffid == slots.staffId && date == slots.date)
                {
                    count++;
                }
            }

            if (count < maxSlot)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Unable to create a slot because " + staffid + " has created 4 slots on" + date);
                return false;
            }
        }

    }
}
