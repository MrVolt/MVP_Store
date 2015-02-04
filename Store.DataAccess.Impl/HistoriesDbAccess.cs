using System;
using System.Collections.Generic;
using System.Linq;
using Store.Model;

namespace Store.DataAccess.Impl
{
    //Класс предоставляющий методы работы с историей
    public class HistoriesDbAccess:IHistoriesDbAccess
    {
        //Метод для создания истории с привязкой к накладной
        public void CreateHistory(string type, string from, string to, string nameOfCreator, int articleId,
            string articleName, int articleAmount, string dateOfCreation, dynamic invoiceId)
        {
            using (var db = new StoreModel())
            {
                try
                {
                    db.HistoryOfInvoice.Add(new HistoryOfInvoice
                    {
                        typeChanged = type,
                        fromChanged = from,
                        toChanged = to,
                        nameOfChanger = nameOfCreator,
                        articleIdChanged = articleId,
                        articleNameChanged = articleName,
                        articleAmountChanged = articleAmount,
                        dataChanged = dateOfCreation,
                        invoiceId = invoiceId
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                db.SaveChanges();
            }
        }

        //Метод предоставляющий список историй по id накладной
        public List<HistoryOfInvoice> GetHistoryByIdOfInvoice(Invoices invoice)
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                //Получаем истории связанные с указанной накладной
                try
                {
                    int id = invoice.invoiceId;
                    var histories = from h in db.HistoryOfInvoice where h.invoiceId == id select h;
                    List<HistoryOfInvoice> list = new List<HistoryOfInvoice>(histories);
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
