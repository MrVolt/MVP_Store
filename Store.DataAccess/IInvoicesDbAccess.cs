using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Store.DataAccess
{
    public interface IInvoicesDbAccess
    {
        void CreateInvoice(string type, string from, string to, string nameOfCreator,
            string articleName, int articleAmount, string dateOfCreation, int articleId,
            string text, string done = "Не подтверждена");

        string GetCurrentIdOfInvoice();

        void UpdateInvoice(string id, string type, string from, string to, string nameOfCreator,
            string articleName, int articleAmount, string dateOfChange, int articleId,
            string text, string done = "Не подтверждена");

        ObservableCollection<Model.Invoices> GetAllInvoices();

        void DoneTheInvoice(Model.Invoices invoice);

        List<Model.Invoices> FindInvoiceById(string id);

        List<Model.Invoices> FindInvoiceByArticle(string articleName);

        List<Model.Invoices> FindInvoiceByCreator(string nameOfCreator);

        List<Model.Invoices> FindInvoiceByText(string text);
    }
}
