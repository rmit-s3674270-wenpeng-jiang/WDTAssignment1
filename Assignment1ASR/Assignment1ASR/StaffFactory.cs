using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment1ASR
{
    public class StaffFactory : Factory
    {
        public override Users CreateUser()
        {
            return new Staffs();
        }
    }
}
