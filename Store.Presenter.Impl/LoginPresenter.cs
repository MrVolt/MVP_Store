using Store.Authorization_Service;
using Store.Model;
using Store.Presenter.FactoryOfPtresenter;
using Store.View;

namespace Store.Presenter.Impl
{
    public class LoginPresenter: ILoginPresenter
    {
        private readonly IAuthorizationView _loginView;
        private readonly IAuthorizationService _authorizationService;
        private readonly ICurrentUser _currentUser;
        private readonly IPresenterFactory _factory;

        //Конструктор
        public LoginPresenter(IAuthorizationView view, IAuthorizationService authorizationService,
            IPresenterFactory factory, ICurrentUser currentUser)
        {
            _loginView = view;
            _authorizationService = authorizationService;
            _factory = factory;
            _currentUser = currentUser;

            _loginView.Login += delegate { Login(_loginView.Username, _loginView.Password); };
        }

        //Метод проверки авторизации
        public void Login(string username, string password)
        {
            _loginView.Hide();

            //Проверка корректности данных и дальнейшие действия
            if (_authorizationService.IsValid(username, password))
            {   

                //Создаём пользователя
                _currentUser.AuthorizedUser = new AuthorizedUser(username, _authorizationService.GetUserRole(username));

                //Запускаем модуль
                var modulePresenter = _factory.CreateModulePresenter();
                modulePresenter.Run();
            }
            else
            {
                _loginView.ShowError("Введите корректные данные.");
                _loginView.Show();
            }
        }

        //Запуск отображения
        public void Run()
        {
            _loginView.Show();
        }      
    }
}
