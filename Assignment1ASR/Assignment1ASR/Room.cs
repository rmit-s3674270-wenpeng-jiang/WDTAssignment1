using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment1ASR
{
    class Room
    {
        private string _name;

        public Room(string _name)
        {
            this._name = _name;
        }
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

    }
}
