namespace Store.Authorization_Service
{
    public interface IAuthorizationService
    {
        //Проверка корректности введёных данных
        bool IsValid(string login, string pass);

        //Метод для высчитывания хэш-значения
        string CalculateHash(string pass, string salt);

        //Метод получения пользовательской роли
        string GetUserRole(string userName);
    }
}
