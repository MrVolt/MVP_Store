using System.Windows;
using Ninject;
using Store.Presenter.Impl;

namespace Store.StartApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            IKernel module = new StandardKernel(new InjectionModule());
            var loginPresenter = module.Get<LoginPresenter>();
            loginPresenter.Run();         
        }
    }
}
