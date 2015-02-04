using System;

namespace Store.View
{
    //Контракт, по которому представитель будет взаимодействовать с отображением
    public interface IAuthorizationView : IView
    {
        string Username { get; }

        string Password { get; }

        //Событие "пользователь пытается авторизоваться"
        event EventHandler<EventArgs> Login;
    }
}
