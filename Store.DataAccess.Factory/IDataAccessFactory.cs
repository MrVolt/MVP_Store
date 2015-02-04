namespace Store.DataAccess.Factory
{
    public interface IDataAccessFactory
    {
        IArticlesDbAccess CreateArticlesDbAccess();
        IContragentsDbAccess CreateContragentsDbAccess();
        IEmployeesDbAccess CreateEmployeesDbAccess();
        IHistoriesDbAccess CreateHistoriesDbAccess();
        IInvoicesDbAccess CreateInvoicesDbAccess();
        INotificationDbAccess CreateNotificationDbAccess();
    }
}
