using System;
using Store.Model;

namespace Store.Presenter
{
    public interface IHistoryPresenter:IPresenter
    {
        void _historyView_WindowActivated(object sender, EventArgs e);

        void _historyView_WindowLoaded(object sender, EventArgs e);

        void Run(Invoices invoice);
    }
}
