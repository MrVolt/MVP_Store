using System;
using System.Collections.Generic;
using System.Linq;
using Store.Model;

namespace Store.DataAccess.Impl
{
    public class EmployeesDbAccess:IEmployeesDbAccess
    {
        public List<string> GetAllUsers()
        {            
            using (var db = new StoreModel())
            {
                try
                {
                    var users = from u in db.Users select u.login;
                    List<string> theList = new List<string>(users);
                    theList.Add("Всем");
                    return theList;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    return null;
                }
            }
        }

        public string FindEmailOfUser(string userName)
        {
            using (var db = new StoreModel())
            {
                try
                {
                    var users = from u in db.Users where u.login == userName select u.userId;
                    if (users.Any())
                    {
                        int currentUser = users.First();
                        var email = from e in db.Employees where e.userId == currentUser select e.e_mail;
                        return email.First();
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    return null;
                }
            }
        }

        public List<string> GetAllEmails()
        {
            using (var db = new StoreModel())
            {
                try
                {
                    var emails = from e in db.Employees select e.e_mail;
                    List<string> list = new List<string>(emails);
                    return list;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    return null;
                }
            }
        }
    }   
}
