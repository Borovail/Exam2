using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Quizlet.Logins;

namespace Quizlet
{
    internal class Autorization
    {
        readonly string _path = $"{Environment.CurrentDirectory}\\Autorizations";

        public Autorization()
        {
            _ioServices = new IOServices(_path);
            _autorizationList = _ioServices.LoadDate<Logins>();
            _autorizationList.ListChanged += _autorizationList_ListChanged;
        }

        private void _autorizationList_ListChanged(object? sender, ListChangedEventArgs e)
        {
           if(e.ListChangedType==ListChangedType.ItemChanged || e.ListChangedType == ListChangedType.ItemDeleted|| e.ListChangedType == ListChangedType.ItemAdded|| e.ListChangedType == ListChangedType.ItemMoved)
            {
                _ioServices.SaveDate(sender);
            }
        }

        BindingList<Logins> _autorizationList;
        IOServices _ioServices;
        
        public AutorizeStatus CheckAuthorizedUsers(string login, string password)
        {
            foreach (var user in _autorizationList)
            {

                if (user.Login == login && user.Password == password)
                {

                    return AutorizeStatus.User;
                }
                if (user.Login == "admin" && user.Password == "Admin")
                {

                    return AutorizeStatus.Admin;
                }

            }

            return AutorizeStatus.Unknown;
        }

        public void AutorizeUser(string login, string passord, string birthdate)
        {
            _autorizationList.Add(new Logins { Login = login, Password = passord, BirthDate = birthdate });
            Console.WriteLine("New user seccesfully added");
        }

        public void ChangePassword(string login, string password)
        {
            if (CheckAuthorizedUsers(login, password) != AutorizeStatus.Admin || CheckAuthorizedUsers(login, password) != AutorizeStatus.Unknown)
            {
                for (int i = 0; i < _autorizationList.Count; i++)
                {
                    if (_autorizationList[i].Login == login)
                    {
                        _autorizationList[i].Password = password;
                        return;
                    }
                }

            }

        }



    }
}

