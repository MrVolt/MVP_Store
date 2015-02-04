namespace Store.Model
{
    //Класс, характеризующий авторизованного пользователя для программы
    public class AuthorizedUser
    {
        public string UserName { get; set; }
        public string UserRole { get; set; }

        public AuthorizedUser(string name, string role)
        {
            UserName = name;
            UserRole = role;
        }
    }
}
