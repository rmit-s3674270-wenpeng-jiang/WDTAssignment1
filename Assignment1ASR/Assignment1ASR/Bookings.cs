using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment1ASR
{
    class Bookings
    {
        private string _room;
        private string _date;
        private string _time;
        private string _studentId;

        public Bookings(string _room, string _date, string _time, string _studentId)
        {
            this._room = _room;
            this._date = _date;
            this._time = _time;
            this._studentId = _studentId;
        }

        public string room
        {
            get
            {
                return _room;
            }
            set
            {
                _room = value;
            }
        }

        public string date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }

        public string time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
            }
        }

        public string studentId
        {

            get
            {
                return _studentId;
            }
            set
            {
                _studentId = value;
            }
        }
    }
}
