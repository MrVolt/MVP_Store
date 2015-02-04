using System;
using Store.Model;

namespace Store.Presenter
{
    public interface INewInvoicePresenter:IPresenter
    {
        void _newInvoiceView_WindowActivated(object sender, EventArgs e);

        void _newInvoiceView_WindowLoaded(object sender, EventArgs e);

        void _newInvoiceView_TypeSelectionChanged(object sender, EventArgs e);

        void _newInvoiceView_SearchTextChanged(object sender, EventArgs e);

        void _newInvoiceView_SearchEnterClicked(object sender, EventArgs e);

        void _newInvoiceView_SaveChangesClicked(object sender, EventArgs e);

        void _newInvoiceView_RemoveArticleClicked(object sender, EventArgs e);

        void _newInvoiceView_PrintClicked(object sender, EventArgs e);

        void _newInvoiceView_CanselClicked(object sender, EventArgs e);

        void _newInvoiceView_ArticlesInInvoiceMouseClicked(object sender, EventArgs e);

        void _newInvoiceView_AddArticleClicked(object sender, EventArgs e);

        void Run(Invoices invoice);
    }
}
