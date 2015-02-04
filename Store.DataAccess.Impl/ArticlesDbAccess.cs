using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Store.Model;

namespace Store.DataAccess.Impl
{
    //Класс предоставляющий основные методы работы с товаром
    public class ArticlesDbAcсess:IArticlesDbAccess
    {
        //Получаем список производителей
        public List<string> GetListOfMakers()
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                //Пытаемся достать всех производителей
                try
                {
                    //Загружаем данные в хэш набор, чтобы не было повторяющихся имён
                    HashSet<string> hashSet = new HashSet<string>(from a in db.Articles select a.nameOfMaker);
                    //Возвращаем в список
                    List<string> theList = hashSet.ToList();
                    return theList;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    return null;
                }
            }
        }

        //Добавление нового товара
        public void AddArticle(string nameOfMaker, string nameOfArticle, int amountAll, int price, int amountBusy = 0, int amountFree = 0)
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                //Пытаемся добавить новый товар
                try
                {
                    db.Articles.Add(new Articles
                    {
                        nameOfMaker = nameOfMaker,
                        nameOfArticle = nameOfArticle,
                        amountAll = amountAll,
                        amountBusy = amountBusy,
                        amountFree = amountFree,
                        price = price
                    });
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        //Удаление товара
        public void DeleteArticle(string nameOfMaker, string nameOfArticle)
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                //Пытаемся получить из БД товар, по данным и удалить его
                try
                {
                    var article = from a in db.Articles
                                  where a.nameOfMaker == nameOfMaker && a.nameOfArticle == nameOfArticle
                                  select a;
                    Articles result = (Articles) article;
                    db.Articles.Remove(result);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        //Товар отпускается со склада
        public void ArticleInReserv(int articleId, int articleAmount)
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                //Пытаемся получить из БД товар, по данным и поставить данное количество в резерв
                try
                {
                    var article = db.Articles.Find(articleId);

                    article.amountBusy = article.amountBusy + articleAmount;
                    article.amountFree = article.amountAll - article.amountBusy;

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        //Товар приходит на склад
        public void ArrivalOfTheArticle(int articleId, int articleAmount)
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                //Пытаемся получить из БД товар, по данным и добавить к товару в пути даное количество товара
                try
                {
                    var article = db.Articles.Find(articleId);

                    article.waiting += articleAmount;

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        //Возврат значений при редактировании
        public void ArticleInReservInvert(int articleId, int articleAmount)
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                //Пытаемся получить из БД товар, по данным и убрать данное количество с резерва
                try
                {
                    var article = db.Articles.Find(articleId);

                    article.amountBusy = article.amountBusy - articleAmount;
                    article.amountFree = article.amountAll - article.amountBusy;

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        //Возврат значений при редактировании
        public void ArrivalOfTheArticleInvert(int articleId, int articleAmount)
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                //Пытаемся получить из БД товар, по данным и убрать с товара в пути даное количество товара
                try
                {
                    var article = db.Articles.Find(articleId);

                    article.waiting -= articleAmount;

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        //Получение всех товаров
        public ObservableCollection<Articles> GetAllArticles()
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                try
                {
                    var article = from a in db.Articles where a.amountAll > 0 select a;
                    ObservableCollection<Articles> articlesList = new ObservableCollection<Articles>(article);
                    return articlesList;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    return null;
                }
            }
        }

        public Articles FindArticleById(int articleId)
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                //Пытаемся получить из БД товар, по данным и убрать данное количество с резерва
                try
                {
                    var article = db.Articles.Find(articleId);
                    return article;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    return null;
                }
            }
        }

        //Метод для возврата всех товаров по производителю
        public List<Articles> GetArticlesByMaker(string maker)
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                //Пытаемся получить из БД товары по производителю и упаковать их в список
                try
                {
                    var article = from a in db.Articles where a.nameOfMaker == maker select a;
                    List<Articles> list = new List<Articles>(article);
                    return list;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    return null;
                }
            }
        }

        //Метод подтверждения прихода товара с Товар в пути
        public void GrantTheArrival(int id, int amount)
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                //Пытаемся получить из БД товары по id и вносим изменения
                try
                {
                    Articles article = db.Articles.Find(id);
                    article.amountAll += amount;
                    article.amountFree += amount;
                    article.waiting -= amount;

                    //Сохраняем изменения
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        //Метод списывающий товар со склада
        public void DebitItems(int id, int amount)
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                //Пытаемся получить из БД товары по id и вносим изменения
                try
                {
                    Articles article = db.Articles.Find(id);
                    article.amountAll -= amount;
                    article.amountBusy -= amount;
                    article.amountFree = article.amountAll - article.amountBusy;

                    //Сохраняем изменения
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        //Ищет товары по заданной строке
        public List<Articles> FindArticlesByRequestString(string searchText)
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                //Пытаемся получить из БД товары 
                try
                {
                    var articles = from a in db.Articles where a.nameOfArticle == searchText || a.nameOfMaker == searchText select a;
                    List<Articles> list = new List<Articles>(articles);
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
