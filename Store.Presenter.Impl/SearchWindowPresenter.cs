using System;
using Store.DataAccess.Factory;
using Store.View;

namespace Store.Presenter.Impl
{
    public class SearchWindowPresenter: ISearchWindowPresenter
    {
        private readonly ISearchWindowView _searchWindowView;
        private readonly IDataAccessFactory _dataAccessFactory;

        //Конструктор
        public SearchWindowPresenter(ISearchWindowView searchWindowView, IDataAccessFactory dataAccessFactory)
        {
            _searchWindowView = searchWindowView;
            _dataAccessFactory = dataAccessFactory;

            _searchWindowView.SearchClicked += _searchWindowView_SearchClicked;
        }

        public void _searchWindowView_SearchClicked(object sender, EventArgs e)
        {
            try
            {
                //Проверяем какая кнопка выбрана
                //по номеру
                if (_searchWindowView.SearchByIdChecked == true && _searchWindowView.SearchingStringById != "")
                {
                    SearchExistingInvoiceById();
                }

                //по товару
                else if (_searchWindowView.SearchByArticleChecked == true && _searchWindowView.SearchingStringByArticle != "")
                {
                    SearchExistingInvoiceByArticle();
                }

                //по создателю
                else if (_searchWindowView.SearchByMakerChecked == true && _searchWindowView.SearchingStringByMaker != "")
                {
                    SearchExistingInvoiceByMaker();
                }
                
                // Забавно хД
                //По примечанию
                else if (_searchWindowView.SearchByTextChecked == true && _searchWindowView.SearchingStringByText != "")
                {
                    SearchExistingInvoiceByText();
                }
                else { _searchWindowView.ShowError("Введите параметры для поиска"); }
            }
            catch (Exception ex) { _searchWindowView.ShowError(ex.Message); }        
        }

        private void SearchExistingInvoiceByText()
        {
            var invoicesDb = _dataAccessFactory.CreateInvoicesDbAccess();

            if (invoicesDb.FindInvoiceByText(_searchWindowView.SearchingStringByText) != null)
            {
                _searchWindowView.TableOfInvoices = null;
                _searchWindowView.TableOfInvoices = invoicesDb.FindInvoiceByText(_searchWindowView.SearchingStringByText);
            }
            else
            {
                _searchWindowView.ShowError("Накладные по заданному параметру не найдены.");
            }
        }

        private void SearchExistingInvoiceByMaker()
        {
            var invoicesDb = _dataAccessFactory.CreateInvoicesDbAccess();

            if (invoicesDb.FindInvoiceByCreator(_searchWindowView.SearchingStringByMaker) != null)
            {
                _searchWindowView.TableOfInvoices = null;
                _searchWindowView.TableOfInvoices = invoicesDb.FindInvoiceByCreator(_searchWindowView.SearchingStringByMaker);
            }
            else
            {
                _searchWindowView.ShowError("Накладные по заданному параметру не найдены");
            }
        }

        private void SearchExistingInvoiceByArticle()
        {
            var invoicesDb = _dataAccessFactory.CreateInvoicesDbAccess();

            if (invoicesDb.FindInvoiceByArticle(_searchWindowView.SearchingStringByArticle) != null)
            {
                _searchWindowView.TableOfInvoices = null;
                _searchWindowView.TableOfInvoices = invoicesDb.FindInvoiceByArticle(_searchWindowView.SearchingStringByArticle);
            }
            else
            {
                _searchWindowView.ShowError("Накладные по заданному параметру не найдены");
            }
        }

        private void SearchExistingInvoiceById()
        {
            var invoicesDb = _dataAccessFactory.CreateInvoicesDbAccess();

            if (invoicesDb.FindInvoiceById(_searchWindowView.SearchingStringById) != null)
            {
                _searchWindowView.TableOfInvoices = null;
                _searchWindowView.TableOfInvoices = invoicesDb.FindInvoiceById(_searchWindowView.SearchingStringById);
            }

            else
            {
                _searchWindowView.ShowError("Накладные по заданному параметру не найдены");
            }
        }

        //Запуск
        public void Run()
        {
            _searchWindowView.Show();
        }     
    }
}
