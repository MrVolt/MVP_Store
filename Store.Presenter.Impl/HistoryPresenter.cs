using System;
using Store.DataAccess.Factory;
using Store.Model;
using Store.View;

namespace Store.Presenter.Impl
{
    public class HistoryPresenter: IHistoryPresenter
    {
        private readonly IHistoryView _historyView;
        private readonly IDataAccessFactory _dataAccessFactory;
        private Invoices _invoices;

        //Конструктор
        public HistoryPresenter(IHistoryView historyView, IDataAccessFactory dataAccessFactory)
        {
            _historyView = historyView;
            _dataAccessFactory = dataAccessFactory;

            _historyView.WindowLoaded += _historyView_WindowLoaded;
            _historyView.WindowActivated += _historyView_WindowActivated;
        }

        public void _historyView_WindowActivated(object sender, EventArgs e)
        {
            try
            {
                var historiesDbAccess = _dataAccessFactory.CreateHistoriesDbAccess();

                _historyView.ListOfHistory = null;
                var histories = historiesDbAccess.GetHistoryByIdOfInvoice(_invoices);
                _historyView.ListOfHistory = histories;
            }
            catch (Exception ex) { _historyView.ShowError(ex.Message); }
        }

        //Обработка загрузки view
        public void _historyView_WindowLoaded(object sender, EventArgs e)
        {
            try
            {
                var historiesDbAccess = _dataAccessFactory.CreateHistoriesDbAccess();
                var histories = historiesDbAccess.GetHistoryByIdOfInvoice(_invoices);
                _historyView.ListOfHistory = histories;
            }
            catch (Exception ex) { _historyView.ShowError(ex.Message); }
        }

        //Запускаем
        public void Run(Invoices invoice)
        {
            _invoices = invoice;
            _historyView.Show();            
        }

        public void Run()
        {
            _historyView.Show();
        }
    }
}
