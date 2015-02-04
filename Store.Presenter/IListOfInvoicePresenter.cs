namespace Store.Presenter
{
    public interface IListOfInvoicePresenter:IPresenter
    {
        void _listOfInvoiceView_InvoicesTableMouseClicked(object sender, System.EventArgs e);

        void listOfInvoiceView_RefreshClicked(object sender, System.EventArgs e);

        void _listOfInvoiceView_LoadHistoryClicked(object sender, System.EventArgs e);

        void _listOfInvoiceView_ChangeInvoiceClicked(object sender, System.EventArgs e);

        void _listOfInvoiceView_DoneClicked(object sender, System.EventArgs e);

        void _listOfInvoiceView_CreateNewInvoiceClicked(object sender, System.EventArgs e);

        void _listOfInvoiceView_WindowLoaded(object sender, System.EventArgs e);
    }
}
