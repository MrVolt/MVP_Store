using System.Collections.Generic;

namespace Store.DataAccess
{
    public interface IEmployeesDbAccess
    {
        List<string> GetAllUsers();

        string FindEmailOfUser(string userName);

        List<string> GetAllEmails();
    }
}
