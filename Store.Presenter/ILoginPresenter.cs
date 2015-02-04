namespace Store.Presenter
{
    public interface ILoginPresenter: IPresenter
    {
        void Login(string username, string password);
    }
}
