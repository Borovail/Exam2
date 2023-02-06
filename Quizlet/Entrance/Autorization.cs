using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Quizlet.Entrance.Logins;

namespace Quizlet.Entrance
{
    internal class Autorization
    {
        readonly string _path = $"{Environment.CurrentDirectory}\\Autorizations.json";
        public static string CurrentLogin { get; private set; }
        private static string CurrentPassword {  get;  set; }

        BindingList<Logins> _autorizationList;
        IOServices _ioServices;

        public Autorization()
        {
            _ioServices = new IOServices(_path);

            _autorizationList = _ioServices.LoadDate<Logins>();
            _autorizationList.ListChanged += _autorizationList_ListChanged;
        }

        private void _autorizationList_ListChanged(object? sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemMoved)
            {
                _ioServices.SaveDate(sender);
            }
        }

      

        public AutorizeStatus CheckAuthorizedUsers(string login, string password)
        {
            foreach (var user in _autorizationList)
            {

                if (user.Login == login && user.Password == password)
                {
                    CurrentLogin = login;
                    CurrentPassword = password;
                    return AutorizeStatus.User;
                }

            }

            return AutorizeStatus.Unknown;
        }

        public void AutorizeUser(string login, string passord, string birthdate)
        {
            _autorizationList.Add(new Logins (login, passord, birthdate ));
            Console.WriteLine("New user successfully added");
        }

        public void ChangePassword(string login, string newPassword)
        {
            if (CheckAuthorizedUsers(login, CurrentPassword) != AutorizeStatus.Unknown)
            {
                for (int i = 0; i < _autorizationList.Count; i++)
                {
                    if (_autorizationList[i].Login == login)
                    {
                        _autorizationList[i].Password = newPassword;
                        CurrentPassword= newPassword;
                        Console.WriteLine();
                        Console.WriteLine("Password successfully chenged");
                        _ioServices.SaveDate(_autorizationList);
                        return;
                    }
                }

            }
            Console.WriteLine("Uncorect data");


        }
        public void ChangeBirthDate(string login, string date)
        {
            if (CheckAuthorizedUsers(login, CurrentPassword) != AutorizeStatus.Unknown)
            {
                for (int i = 0; i < _autorizationList.Count; i++)
                {
                    if (_autorizationList[i].Login == login)
                    {
                        _autorizationList[i].BirthDate = date;
                        Console.WriteLine();
                        Console.WriteLine("Birthdate successfully chenged");
                        _ioServices.SaveDate(_autorizationList);
                        return;
                    }
                }

            }
            Console.WriteLine("Uncorect data");

        }




    }
}

