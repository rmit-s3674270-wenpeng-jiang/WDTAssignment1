using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment1ASR
{
    class Slots
    {
        private string _room;
        private string _start;
        private string _end;
        private string _staffId;
        private string _booking;
        private string _date;
        
        public Slots (string _room, string _date, string _start, string _end, string _staffId)
        {
            this._room = _room;
            this._date = _date;
            this._start = _start;
            this._end = _end;
            this._staffId = _staffId;
        }

        public Slots(string _room, string _date, string _start, string _end, string _staffId, string _booking)
        {
            this._room = _room;
            this._date = _date;
            this._start = _start;
            this._end = _end;
            this._staffId = _staffId;
            this._booking = _booking;
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

        public string start
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
            }
        }

        public string end
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
            }
        }

        public string staffId
        {
            get
            {
                return _staffId;
            }
            set
            {
                _staffId = value;
            }
        }

        public string booking
        {
            get
            {
                return _booking;
            }
            set
            {
                _booking = value;
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
    }
}
