namespace Store.View
{
    //Общие методы для всех представлений
    public interface IView
    {
        void Show();
        void Close();
        void Hide();
        void ShowError(string errorMessage);
    }

}
