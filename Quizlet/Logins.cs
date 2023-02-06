using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizlet
{
    internal class Logins
    {


        private string _login;

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        private string _password;

        public string Password
        {
            get { return _login; }
            set { _login = value; }
        }

        private string _birthDate;

        public string BirthDate
        {
            get { return _login; }
            set { _login = value; }
        }

        public enum AutorizeStatus
        {
            Admin, User, Unknown
        }

    }
}
