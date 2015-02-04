using System;
using System.Collections.Generic;
using System.Windows.Threading;
using Store.DataAccess.Factory;
using Store.Model;
using Store.Presenter.FactoryOfPtresenter;
using Store.View;

namespace Store.Presenter.Impl
{
    public class StorePresenter: IStorePresenter
    {
        private readonly IStoreView _storeView;
        private readonly IPresenterFactory _presenterFactory;
        private readonly IDataAccessFactory _dataAccessFactory;
        private readonly ICurrentUser _currentUser;

        //Конструктор
        public StorePresenter(IStoreView storeView, IPresenterFactory presenterFactory, 
            IDataAccessFactory dataAccessFactory, ICurrentUser currentUser)
        {                        
            _storeView = storeView;
            _presenterFactory = presenterFactory;
            _dataAccessFactory = dataAccessFactory;
            _currentUser = currentUser;

            _storeView.ViewLoaded += _storeView_ViewLoaded;
            _storeView.Exit += _storeView_Exit;
            _storeView.ChangeUser += _storeView_ChangeUser;
            _storeView.ListOfInvoices += _storeView_ListOfInvoices;
            _storeView.Refresh += _storeView_Refresh;
            _storeView.ListOfMakersMouseClick += _storeView_ListOfMakersMouseClick;
            _storeView.ChangeModul += _storeView_ChangeModul;
            _storeView.CreateInvoice += _storeView_CreateInvoice;
            _storeView.Contragents += _storeView_Contragents;
            _storeView.SearchEnterClicked += _storeView_SearchEnterClicked;
            _storeView.SearchTextChanged += _storeView_SearchTextChanged;
            _storeView.SearchInvoice += _storeView_SearchInvoice;
        }

        //Запуск поиска накладной
        public void _storeView_SearchInvoice(object sender, EventArgs e)
        {
            var searchInvoicePresenter = _presenterFactory.CreateSearchWindowPresenter();
            searchInvoicePresenter.Run();
        }

        //Изменение в поле поиска
        public void _storeView_SearchTextChanged(object sender, EventArgs e)
        {
            _storeView.SearchIsChecked = false;
        }

        //Нажата клавиша Ввод в строке поиска
        public void _storeView_SearchEnterClicked(object sender, EventArgs e)
        {                
            SearchArticles();
        }

        private void SearchArticles()
        {
            try
            {
                var articlesDb = _dataAccessFactory.CreateArticlesDbAccess();
                List<Articles> list = articlesDb.FindArticlesByRequestString(_storeView.SearchString);
                _storeView.TableOfArticles = list;
                _storeView.SearchIsChecked = true;
            }
            catch (Exception ex) { _storeView.ShowError(ex.Message); }
        }

        //Контрагенты
        public void _storeView_Contragents(object sender, EventArgs e)
        {
            var contragentsPresenter = _presenterFactory.CreateContragentsWindowPresenter();
            contragentsPresenter.Run();
        }

        //Cоздать накладную
        public void _storeView_CreateInvoice(object sender, EventArgs e)
        {
            var newInvoicePresenter = _presenterFactory.CreateNewInvoicePresenter();
            newInvoicePresenter.Run();
        }

        //Меняем модуль
        public void _storeView_ChangeModul(object sender, EventArgs e)
        {
            _storeView.Hide();
            var modulePresenter = _presenterFactory.CreateModulePresenter();
            modulePresenter.Run();
        }

        //Обработка щелчка по Производителям
        public void _storeView_ListOfMakersMouseClick(object sender, EventArgs e)
        {
            try
            {
                var articlesDb = _dataAccessFactory.CreateArticlesDbAccess();

                //Отображаем всех производителей
                if ((string) _storeView.SelectedMaker == "Все производители")
                {                    
                    //Отображаем на экране все товары.
                    _storeView.TableOfArticles = articlesDb.GetAllArticles();
                }

                //Отображаем товары по производителю
                else if (_storeView.SelectedMaker != null && (string)_storeView.SelectedMaker != "Все производители")
                {
                    string selectedMaker = (string)_storeView.SelectedMaker;
                    _storeView.TableOfArticles = articlesDb.GetArticlesByMaker(selectedMaker);
                }
            }
            catch (Exception ex) { _storeView.ShowError(ex.Message); }
        }

        //Кнопка обновить
        public void _storeView_Refresh(object sender, EventArgs e)
        {
            var articlesDb = _dataAccessFactory.CreateArticlesDbAccess();
            _storeView.TableOfArticles = null;
            try { _storeView.TableOfArticles = articlesDb.GetAllArticles(); }
            catch (Exception ex) { _storeView.ShowError(ex.Message); }
        }

        //Список накладных
        public void _storeView_ListOfInvoices(object sender, EventArgs e)
        {
            var listOfInvoicesPresenter = _presenterFactory.CreateListOfInvoicePresenter();
            listOfInvoicesPresenter.Run();
        }

        //Меняем пользователя
        public void _storeView_ChangeUser(object sender, EventArgs e)
        {
            _storeView.Hide();
            var authorizationPresenter = _presenterFactory.CreateAuthorizationPresenter();
            authorizationPresenter.Run();
        }

        //Выход
        public void _storeView_Exit(object sender, EventArgs e)
        {
            _storeView.Close();
        }

        //Загрузка окна
        public void _storeView_ViewLoaded(object sender, EventArgs e)
        {
            try
            {
                var articlesDb = _dataAccessFactory.CreateArticlesDbAccess();
                //Загружаем список производителей и цепляем его к списку ListOfMakers
                List<string> list = articlesDb.GetListOfMakers();
                list.Add("Все производители");
                _storeView.MakersList = list;

                //Отображаем на экране все товары.
                _storeView.TableOfArticles = articlesDb.GetAllArticles();

                //Параллельно ищем - есть ли уведомления для текущего пользователя
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => SearchNotifications(_currentUser.Instance)));

            }
            catch (Exception ex) { _storeView.ShowError(ex.Message); }
        }

        //Метод поиска уведомлений
        private void SearchNotifications(ICurrentUser user)
        {
            //Получаем количество уведомлений
            var notificationDb = _dataAccessFactory.CreateNotificationDbAccess();
            int countOfNotifies = notificationDb.GetAllNotificationsForUser(user.AuthorizedUser.UserName);
            const string toast = "На сегодня у Вас есть уведомления. Их можно посмотреть в модуле Календарь.";

            if (countOfNotifies > 0)
            {
                //Создаём Всплывающее окно
                _storeView.Toast(toast);
            }
        }

        //Запуск отображения
        public void Run()
        {
            _storeView.Show();
        }
    }
}
