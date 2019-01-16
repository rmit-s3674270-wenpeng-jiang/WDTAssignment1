using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment1ASR
{
    public abstract class Users
    {
        private string _name;
        private string _id;
        private string _email;

        public Users(string _id, string _name, string _email)
        {
            this._id = _id;
            this._name = _name;
            this._email = _email;
        }

        public Users() { }

        public string name
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

        public abstract void Menu();
    }
}
