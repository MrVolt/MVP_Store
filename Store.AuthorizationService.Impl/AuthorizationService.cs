using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Store.Model;

namespace Store.Authorization_Service.Impl
{
    public class AuthorizationService : IAuthorizationService
    {
        public bool IsValid(string login, string pass)
        {
            //Открываем соединение
            using (var db = new StoreModel())
            {
                try
                {
                    //Собираем пароль из введёных данных
                    string password = CalculateHash(pass, login);
                    var query = from u in db.Users where u.login == login && u.password == password select u;
                    return Enumerable.Count(query) > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public string CalculateHash(string pass, string salt)
        {
            // Конвертируем солёный пароль в байты
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(pass + salt);
            // Используем хэш алгоритм для подсчёта хэша
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Возвращаем хэш как base64 строку для сравнения с паролем в бд
            return Convert.ToBase64String(hash);
        }

        //Получаем роль
        public string GetUserRole(string userName)
        {
            using (var db = new StoreModel())
            {
                try
                {
                    var user = from u in db.Users where u.login == userName select u.role;
                    return user.First();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
    }
}
