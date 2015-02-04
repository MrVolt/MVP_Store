using System;
using Store.DataAccess.Factory;
using Store.Model;
using Store.Presenter.FactoryOfPtresenter;
using Store.View;

namespace Store.Presenter.Impl
{
    public class ListOfInvoicePresenter: IListOfInvoicePresenter
    {
        private readonly IListOfInvoiceView _listOfInvoiceView;
        private readonly IPresenterFactory _presenterFactory;
        private readonly IDataAccessFactory _dataAccessFactory;
        private readonly ICurrentUser _currentUser;

        //Конструктор
        public ListOfInvoicePresenter(IListOfInvoiceView listOfInvoiceView, IPresenterFactory presenterFactory,
            IDataAccessFactory dataAccessFactory, ICurrentUser currentUser)
        {
            _listOfInvoiceView = listOfInvoiceView;
            _presenterFactory = presenterFactory;
            _dataAccessFactory = dataAccessFactory;
            _currentUser = currentUser;

            _listOfInvoiceView.WindowLoaded += _listOfInvoiceView_WindowLoaded;
            _listOfInvoiceView.CreateNewInvoiceClicked += _listOfInvoiceView_CreateNewInvoiceClicked;
            _listOfInvoiceView.DoneClicked += _listOfInvoiceView_DoneClicked;
            _listOfInvoiceView.ChangeInvoiceClicked += _listOfInvoiceView_ChangeInvoiceClicked;
            _listOfInvoiceView.LoadHistoryClicked += _listOfInvoiceView_LoadHistoryClicked;
            listOfInvoiceView.RefreshClicked += listOfInvoiceView_RefreshClicked;
            _listOfInvoiceView.InvoicesTableMouseClicked += _listOfInvoiceView_InvoicesTableMouseClicked;
        }

        //Щелчёк по таблице накладных
        public void _listOfInvoiceView_InvoicesTableMouseClicked(object sender, EventArgs e)
        {
            if (_listOfInvoiceView.SelectedInvoice != null)
            {
                _listOfInvoiceView.IsEditEnabled = true;
                _listOfInvoiceView.IsDoneEnabled = true;
                
                //Если роль - администратор или Начальник склада, История - активна
                if (_currentUser.AuthorizedUser.UserRole == "Administrator" || (_currentUser.AuthorizedUser.UserRole == "HighStoreManager"))
                {
                    _listOfInvoiceView.IsHistoryEnabled = true;
                }
            }
            else { _listOfInvoiceView.IsEditEnabled = false; _listOfInvoiceView.IsDoneEnabled = false; }
        }

        //Обновить
        public void listOfInvoiceView_RefreshClicked(object sender, EventArgs e)
        {
            RefreshWindow();
        }

        //Метод обновления view
        private void RefreshWindow()
        {
            var invoicesDbAcсess = _dataAccessFactory.CreateInvoicesDbAccess();

            _listOfInvoiceView.ListOfInvoices = null;
            _listOfInvoiceView.ListOfInvoices = invoicesDbAcсess.GetAllInvoices();
            _listOfInvoiceView.IsHistoryEnabled = false;
            _listOfInvoiceView.IsEditEnabled = false;
            _listOfInvoiceView.IsDoneEnabled = false;
        }

        //История
        public void _listOfInvoiceView_LoadHistoryClicked(object sender, EventArgs e)
        {
            Invoices invoice = (Invoices)_listOfInvoiceView.SelectedInvoice;
            var historyPresenter = _presenterFactory.CreateHistoryPresenter();
            historyPresenter.Run(invoice);
        }

        //Редактировать
        public void _listOfInvoiceView_ChangeInvoiceClicked(object sender, EventArgs e)
        {
            Invoices selectedItem = (Invoices)_listOfInvoiceView.SelectedInvoice;

            //Если накладная не подтверждена
            if (selectedItem.done != "Подтверждена")
            {
                var newInvoicePresenter = _presenterFactory.CreateNewInvoicePresenter();
                newInvoicePresenter.Run(selectedItem);
            }

            else
            {
                _listOfInvoiceView.ShowError("Накладная уже подтверждена! Обратитесь к администратору.");
            }
        }

        //Подтвердить
        public void _listOfInvoiceView_DoneClicked(object sender, EventArgs e)
        {
            Invoices invoice = (Invoices)_listOfInvoiceView.SelectedInvoice;

            try
            {
                if (invoice.done == "Не подтверждена")
                {
                    var invoicesDbAcсess = _dataAccessFactory.CreateInvoicesDbAccess();
                    invoicesDbAcсess.DoneTheInvoice(invoice);
                }
                else
                {
                    _listOfInvoiceView.ShowError("Накладная уже подтверждена! Чтобы снять подтверждение обратитесь к администратору.");
                }
            }
            catch (Exception ex) {_listOfInvoiceView.ShowError(ex.Message); }

            RefreshWindow();
        }

        //Создание новой накладной
        public void _listOfInvoiceView_CreateNewInvoiceClicked(object sender, EventArgs e)
        {
            var newInvoicePresenter = _presenterFactory.CreateNewInvoicePresenter();
            newInvoicePresenter.Run();
        }

        //Загружаем окно
        public void _listOfInvoiceView_WindowLoaded(object sender, EventArgs e)
        {
            try
            {
                var invoicesDbAcсess = _dataAccessFactory.CreateInvoicesDbAccess();
                //Отображаем на экране все накладные.
                _listOfInvoiceView.ListOfInvoices = invoicesDbAcсess.GetAllInvoices();
            }
            catch (Exception ex)
            {
                _listOfInvoiceView.ShowError(ex.Message);
            }
        }

        //Запуск
        public void Run()
        {
            _listOfInvoiceView.Show();
        }     
    }
}
