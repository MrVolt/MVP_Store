using System;
using System.Collections.Generic;
using System.Linq;
using Store.Model;

namespace Store.DataAccess.Impl
{
    //Класс предоставляющий основные методы работы с уведомлениями
    public class NotificationDbAccess:INotificationDbAccess
    {
        //Метод создания нового уведомления
        public void CreateNotification(string type, string from, string to, string dateOfCreation,
            string dateOfShowing, string nameOfCreator, string text)
        {
            //Соединяемся с БД
            using (var db = new StoreModel())
            {
                //Пытаемся добавить новое уведомление
                try
                {

                    db.Notifications.Add(new Notifications
                    {
                        type = type,
                        @from = from,
                        to = to,
                        dateOfCreation = dateOfCreation,
                        dateOfShowing = dateOfShowing,
                        nameOfCreator = nameOfCreator,
                        text = text
                    });

                    db.SaveChanges();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        //Метод возвращающий последний созданный номер уведомления
        public string GetCurrentIdOfNotification()
        {
            using (var db = new StoreModel())
            {
                try
                {
                    var lastNotificationId = (from l in db.Notifications select l.notificationId).Max();
                    string currentId = lastNotificationId.ToString();
                    return currentId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        //Метод для обновления существующего уведомления
        public void UpdateNotification(string id, string to, string text, string dateOfCreation, string dateOfShowing)
        {
            using (var db = new StoreModel())
            {
                try
                {
                    //Конвертируем строку id в число и ищем по этому числу номер существующего уведомления
                    int currentId = Convert.ToInt32(id);
                    var existingNotification = db.Notifications.Find(currentId);

                    //Меняем информацию
                    existingNotification.to = to;
                    existingNotification.text = text;
                    existingNotification.dateOfCreation = dateOfCreation;
                    existingNotification.dateOfShowing = dateOfShowing;

                    //Сохраняем изменения
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        //Метод для загрузки всех уведомлений по указанной дате
        public List<Notifications> GetAllNotificationByDate(string dateOfShowing)
        {
            //Соединяемся с бд и ищем уведомления по дате
            using (var db = new StoreModel())
            {
                try
                {
                    var notifications = from n in db.Notifications where n.dateOfShowing == dateOfShowing select n;
                    List<Notifications> list = new List<Notifications>(notifications);
                    return list;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        //Получаем все уведомления для пользователя на текущую дату
        public int GetAllNotificationsForUser(string name)
        {
            //Соединяемся с бд и ищем уведомления по дате
            using (var db = new StoreModel())
            {
                try
                {
                    string date = DateTime.Now.ToString("yyyy/MM/dd");
                    var notifications = from n in db.Notifications
                                        where n.dateOfShowing == date
                                            && (n.to == name || n.to == "Всем")
                                        select n;
                    List<Notifications> list = new List<Notifications>(notifications);
                    return list.Count;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }
    }
}