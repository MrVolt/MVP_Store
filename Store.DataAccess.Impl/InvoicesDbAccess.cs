using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using Store.Model;

namespace Store.DataAccess.Impl
{
    //Класс представляющий основные методы для работы с накладными
    public class InvoicesDbAcсess:IInvoicesDbAccess
    {
        //Метод создания накладной
        public void CreateInvoice(string type, string from, string to, string nameOfCreator,
            string articleName, int articleAmount, string dateOfCreation, int articleId,
            string text, string done = "Не подтверждена")
        {
            using (var db = new StoreModel())
            {
                try
                {                    
                    db.Invoices.Add(new Invoices
                    {
                        type = type,
                        @from = from,
                        to = to,
                        nameOfCreator = nameOfCreator,
                        articleId = articleId,
                        articleName = articleName,
                        articleAmount = articleAmount,
                        dataCreated = dateOfCreation,
                        done = done,
                        text = text
                    });

                    db.SaveChanges();

                    ArticlesDbAcсess articleDb = new ArticlesDbAcсess();

                    //если склад отпускает товар
                    if (from == "ЦС Астана")
                    {
                        articleDb.ArticleInReserv(articleId, articleAmount);
                    }

                    //если склад получает товар
                    else
                    {
                        articleDb.ArrivalOfTheArticle(articleId, articleAmount);
                    }

                    //сохраняем изменения
                    db.SaveChanges();

                    //Создаём историю накладной. Ищем максимальный Id и связываем с ним.
                    var lastInvoiceId = (from l in db.Invoices select l.invoiceId).Max();

                    HistoriesDbAccess historyAccess = new HistoriesDbAccess();
                    historyAccess.CreateHistory(type, from, to, nameOfCreator, articleId, articleName, articleAmount,
                       dateOfCreation, lastInvoiceId);

                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                ve.PropertyName,
                                eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }

        //Получение максимального номера накладной, используется при создании новой накладной и истории.
        public string GetCurrentIdOfInvoice()
        {
            using (var db = new StoreModel())
            {
                try
                {
                    var lastInvoiceId = (from l in db.Invoices select l.invoiceId).Max();
                    string currentId = lastInvoiceId.ToString();
                    return currentId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        //Обновление существующей накладной и создание истории изменения
        public void UpdateInvoice(string id, string type, string from, string to, string nameOfCreator,
            string articleName, int articleAmount, string dateOfChange, int articleId,
            string text, string done = "Не подтверждена")
        {
            using (var db = new StoreModel())
            {
                try
                {
                    //Конвертируем строку id в число и ищем по этому числу номер существующей накладной
                    int currentId = Convert.ToInt32(id);
                    var existingInvoice = db.Invoices.Find(currentId);

                    //Изменяем найденную накладную                    
                    if (existingInvoice.from != from)
                    {
                        ArticlesDbAcсess articleDb = new ArticlesDbAcсess();

                        //Если старое значение Отправитель другое - инвертируем колличесто в Товар в пути или Резерве
                        if (existingInvoice.from == "ЦС Астана")
                        {
                            articleDb.ArticleInReservInvert(existingInvoice.articleId, existingInvoice.articleAmount);
                            articleDb.ArrivalOfTheArticle(articleId, articleAmount);
                        }
                        else
                        {
                            articleDb.ArrivalOfTheArticleInvert(existingInvoice.articleId, existingInvoice.articleAmount);
                            articleDb.ArticleInReserv(articleId, articleAmount);
                        }
                        existingInvoice.from = from;
                        existingInvoice.type = type;
                        existingInvoice.to = to;
                        existingInvoice.articleName = articleName;
                        existingInvoice.articleAmount = articleAmount;
                        existingInvoice.text = text;
                        existingInvoice.dataCreated = dateOfChange;

                        //сохраняем изменения
                        db.SaveChanges();
                    }

                    existingInvoice.to = to;
                    existingInvoice.articleId = articleId;
                    existingInvoice.articleName = articleName;
                    existingInvoice.articleAmount = articleAmount;
                    existingInvoice.text = text;
                    existingInvoice.dataCreated = dateOfChange;

                    //сохраняем изменения
                    db.SaveChanges();

                    HistoriesDbAccess historyAccess = new HistoriesDbAccess();
                    historyAccess.CreateHistory(type, from, to, nameOfCreator, articleId, articleName, articleAmount,
                       dateOfChange, currentId);

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public ObservableCollection<Invoices> GetAllInvoices()
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                try
                {
                    var invoices = from i in db.Invoices select i;
                    ObservableCollection<Invoices> invoicesList = new ObservableCollection<Invoices>(invoices);
                    return invoicesList;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    return null;
                }
            }
        }

        //Метод для подтверждения накладной
        public void DoneTheInvoice(Invoices invoice)
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                try
                {
                    //Ищем накладную в бд
                    Invoices existingInvoice = db.Invoices.Find(invoice.invoiceId);

                    //Подтверждаем найденную накладную
                    existingInvoice.done = "Подтверждена";

                    ArticlesDbAcсess articleDb = new ArticlesDbAcсess();

                    //Проверяем тип накладной и если Товар в пути - ставим товар на приход
                    if (existingInvoice.type == "Товар в пути")
                    {
                        articleDb.GrantTheArrival(existingInvoice.articleId, existingInvoice.articleAmount);
                        db.SaveChanges();
                    }

                    //Иначе - списываем товар
                    else
                    {
                        articleDb.DebitItems(existingInvoice.articleId, existingInvoice.articleAmount);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        //Метод для поиска по ID
        public List<Invoices> FindInvoiceById(string id)
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                try
                {
                    int currentId = Convert.ToInt32(id);
                    var invoices = db.Invoices.Find(currentId);

                    if (invoices != null)
                    {
                        List<Invoices> list = new List<Invoices> { invoices };
                        return list;
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

        //Метод для поиска по названию товара
        public List<Invoices> FindInvoiceByArticle(string articleName)
        {
            {
                //Открываем соединение
                using (var db = new StoreModel())
                {
                    try
                    {
                        var invoices = from i in db.Invoices where i.articleName == articleName select i;
                        if (invoices.Any())
                        {
                            List<Invoices> list = new List<Invoices>(invoices);
                            return list;
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
        }

        //Метод для поиска по создателю
        public List<Invoices> FindInvoiceByCreator(string nameOfCreator)
        {
            {
                //Открываем соединение
                using (var db = new StoreModel())
                {
                    try
                    {
                        var invoices = from i in db.Invoices where i.nameOfCreator == nameOfCreator select i;
                        if (invoices.Any())
                        {
                            List<Invoices> list = new List<Invoices>(invoices);
                            return list;
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
        }

        //Метод для поиска по примечанию
        public List<Invoices> FindInvoiceByText(string text)
        {
            {
                //Открываем соединение
                using (var db = new StoreModel())
                {
                    try
                    {
                        var invoices = from i in db.Invoices where i.text.Contains(text) select i;
                        if (invoices.Any())
                        {
                            List<Invoices> list = new List<Invoices>(invoices);
                            return list;
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
        }
    }    
}
