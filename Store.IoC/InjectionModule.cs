using Ninject.Extensions.Factory;
using Ninject.Modules;
using Store.Authorization_Service;
using Store.Authorization_Service.Impl;
using Store.DataAccess;
using Store.DataAccess.Factory;
using Store.DataAccess.Impl;
using Store.Model;
using Store.Presenter;
using Store.Presenter.FactoryOfPtresenter;
using Store.Presenter.Impl;
using Store.View;
using Store.View.Impl;

namespace Store.StartApp
{
    internal class InjectionModule : NinjectModule
    {
        public override void Load()
        {
            //View
            Bind<IAuthorizationView>().To<Authorization>();
            Bind<IModuleView>().To<Modules>();
            Bind<IStoreView>().To<StoreModule>();
            Bind<ICalenderView>().To<Calendar>();
            Bind<IContragentsWindowView>().To<ContragentsWindow>();
            Bind<IHistoryView>().To<History>();
            Bind<IListOfInvoiceView>().To<ListOfInvoice>();
            Bind<INewInvoiceView>().To<NewInvoice>();
            Bind<INotificationView>().To<Notification>();
            Bind<ISearchWindowView>().To<SearchWindow>();

            //Presenter
            Bind<ILoginPresenter>().To<LoginPresenter>();
            Bind<ICalenderPresenter>().To<CalenderPresenter>();
            Bind<IContragentsWindowPresenter>().To<ContragentsWindowPresenter>();
            Bind<IHistoryPresenter>().To<HistoryPresenter>();
            Bind<IListOfInvoicePresenter>().To<ListOfInvoicePresenter>();
            Bind<IModulePresenter>().To<ModulePresenter>();
            Bind<INewInvoicePresenter>().To<NewInvoicePresenter>();
            Bind<INotificationPresenter>().To<NotificationPresenter>();
            Bind<ISearchWindowPresenter>().To<SearchWindowPresenter>();
            Bind<IStorePresenter>().To<StorePresenter>();

            //Data Access
            Bind<IArticlesDbAccess>().To<ArticlesDbAcсess>();
            Bind<IContragentsDbAccess>().To<ContragentsDbAcсess>();
            Bind<IEmployeesDbAccess>().To<EmployeesDbAccess>();
            Bind<IHistoriesDbAccess>().To<HistoriesDbAccess>();
            Bind<IInvoicesDbAccess>().To<InvoicesDbAcсess>();
            Bind<INotificationDbAccess>().To<NotificationDbAccess>();

            //Services
            Bind<IAuthorizationService>().To<AuthorizationService>();
            Bind<IDataAccessFactory>().ToFactory();
            Bind<IPresenterFactory>().ToFactory();

            //Singleton  
            Bind<ICurrentUser>().ToMethod(context => CurrentUser.Instance).InSingletonScope();
            Bind<LoginPresenter>().ToSelf();
        }
    }
}
