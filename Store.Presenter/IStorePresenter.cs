using System;

namespace Store.Presenter
{
    public interface IStorePresenter:IPresenter
    {
        void _storeView_SearchInvoice(object sender, EventArgs e);

        void _storeView_SearchTextChanged(object sender, EventArgs e);

        void _storeView_SearchEnterClicked(object sender, EventArgs e);

        void _storeView_Contragents(object sender, EventArgs e);

        void _storeView_CreateInvoice(object sender, EventArgs e);

        void _storeView_ChangeModul(object sender, EventArgs e);

        void _storeView_ListOfMakersMouseClick(object sender, EventArgs e);

        void _storeView_Refresh(object sender, EventArgs e);

        void _storeView_ListOfInvoices(object sender, EventArgs e);

        void _storeView_ChangeUser(object sender, EventArgs e);

        void _storeView_Exit(object sender, EventArgs e);

        void _storeView_ViewLoaded(object sender, EventArgs e);
    }
}
