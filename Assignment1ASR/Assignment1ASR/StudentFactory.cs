using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment1ASR
{
    public class StudentFactory : Factory
    {
        public override Users CreateUser()
        {
            return new Students();
        }
    }
}
