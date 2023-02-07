using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizlet.Entrance
{
    internal class Logins
    {
        public Logins() { }
        public Logins(string login)
        {
            _login=login;
        }
        public Logins(string login,string password,string date)
        {
            _login = login;
            _password=password;
            _birthDate=date;
        }

        private string _login;

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _birthDate;

        public string BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

    }
}
