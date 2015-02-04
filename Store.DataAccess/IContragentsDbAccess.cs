using System.Collections.Generic;
using Store.Model;

namespace Store.DataAccess
{
    public interface IContragentsDbAccess
    {
        List<string> GetAllShortNamesOfContrAgents();

        List<ContrAgents> GetAllContrAgents();

        void UpdateExistingContragent(string id, string shortname, string address, string telephone, string bin);

        void AddNewContragent(string shortname, string address, string telephone, string bin);
    }
}
