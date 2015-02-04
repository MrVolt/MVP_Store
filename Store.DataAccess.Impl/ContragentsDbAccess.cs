using System;
using System.Collections.Generic;
using System.Linq;
using Store.Model;

namespace Store.DataAccess.Impl
{
    public class ContragentsDbAcсess:IContragentsDbAccess
    {  
        //Получаем список коротких названий контрагентов
        public List<string> GetAllShortNamesOfContrAgents()
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                //Пытаемся достать короткие названия всех контрагентов
                try
                {
                    List<string> theList = new List<string>(from с in db.ContrAgents select с.shortName);
                    //Возвращаем в список
                    return theList;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    return null;
                }
            }
        }

        //Получаем список всех контрагентов
        public List<ContrAgents> GetAllContrAgents()
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                //Пытаемся достать всех контрагентов
                try
                {
                    var contragents = from с in db.ContrAgents select с;
                    List<ContrAgents> theList = new List<ContrAgents>(contragents);
                    //Возвращаем в список
                    return theList;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    return null;
                }
            }
        }

        //Метод для обновления существующего контрагента
        public void UpdateExistingContragent(string id, string shortname, string address, string telephone, string bin)
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                try
                {
                    //Конвертируем строку в число и ищем по этому числу существующего контрагента
                    int currentId = Convert.ToInt32(id);
                    ContrAgents currentContragent = db.ContrAgents.Find(currentId);

                    //Вносим изменения
                    currentContragent.shortName = shortname;
                    currentContragent.address = address;
                    currentContragent.telephone = telephone;
                    currentContragent.bin = bin;

                    //Сохраняем изменения
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        //Метод для создания нового контрагента
        public void AddNewContragent(string shortname, string address, string telephone, string bin)
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                try
                {
                    //Добавляем контрагент по полученным данным
                    db.ContrAgents.Add(new ContrAgents
                    {
                        fullName = "",
                        shortName = shortname,
                        address = address,
                        telephone = telephone,
                        bin = bin
                    });

                    //Сохраняем изменения
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }
    }
}
