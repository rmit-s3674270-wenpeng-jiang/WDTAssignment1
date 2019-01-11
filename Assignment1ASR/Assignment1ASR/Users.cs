using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment1ASR
{
    class Users
    {
        private string _name;
        private string _id;
        private string _email;

        public Users(string _name, string _id, string _email)
        {
            this._name = _name;
            this._id = _id;
            this._email = _email;
        }

        public String name
        {
            get {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }
    }
}
