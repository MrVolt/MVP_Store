using System.Collections.Generic;
using Store.Model;

namespace Store.DataAccess
{
    public interface IHistoriesDbAccess
    {
        void CreateHistory(string type, string from, string to, string nameOfCreator, int articleId,
            string articleName, int articleAmount, string dateOfCreation, dynamic invoiceId);

        List<HistoryOfInvoice> GetHistoryByIdOfInvoice(Invoices invoice);
    }
}
