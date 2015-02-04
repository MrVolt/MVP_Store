namespace Store.Presenter.FactoryOfPtresenter
{
    public interface IPresenterFactory
    {
        ILoginPresenter CreateAuthorizationPresenter();
        IModulePresenter CreateModulePresenter();
        ICalenderPresenter CreateCalenderPresenter();
        IContragentsWindowPresenter CreateContragentsWindowPresenter();
        IHistoryPresenter CreateHistoryPresenter();
        IListOfInvoicePresenter CreateListOfInvoicePresenter();
        INewInvoicePresenter CreateNewInvoicePresenter();
        INotificationPresenter CreateNotificationPresenter();
        ISearchWindowPresenter CreateSearchWindowPresenter();
        IStorePresenter CreateStorePresenter();
    }
}
