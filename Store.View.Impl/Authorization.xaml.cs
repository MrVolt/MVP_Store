using System;
using System.ComponentModel;
using System.Windows;

namespace Store.View.Impl
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window, IAuthorizationView
    {
        public event EventHandler<EventArgs> Login;

        public Authorization()
        {
            InitializeComponent();
        }

        public string Username
        {
            get { return LoginBox.Text; }
        }

        public string Password
        {
            get { return PasswordBox.Password; }
        }

        //Кнопка обработки запросов на авторизацию
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Username != null && Password != null)
                Login(this, EventArgs.Empty);
        }

        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Неверный логин или пароль", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Authorization_OnClosing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
