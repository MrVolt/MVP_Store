using System;
using System.Collections.Generic;
using System.Linq;
using Stimulsoft.Report;
using Store.DataAccess.Factory;
using Store.Model;
using Store.View;

namespace Store.Presenter.Impl
{
    public class NewInvoicePresenter: INewInvoicePresenter
    {
        readonly List<Articles> _collectionOfArticles = new List<Articles>();

        private readonly INewInvoiceView _newInvoiceView;
        private readonly ICurrentUser _currentUser;
        private readonly IDataAccessFactory _dataAccessFactory;
        private Invoices _invoice;

        //Конструктор
        public NewInvoicePresenter(INewInvoiceView newInvoiceView, ICurrentUser currentUser, 
            IDataAccessFactory dataAccessFactory)
        {
            _newInvoiceView = newInvoiceView;
            _currentUser = currentUser;
            _dataAccessFactory = dataAccessFactory;

            _newInvoiceView.AddArticleClicked += _newInvoiceView_AddArticleClicked;
            _newInvoiceView.ArticlesTableMouseClicked += _newInvoiceView_ArticlesInInvoiceMouseClicked;
            _newInvoiceView.CanselClicked += _newInvoiceView_CanselClicked;
            _newInvoiceView.PrintClicked += _newInvoiceView_PrintClicked;
            _newInvoiceView.RemoveArticleClicked += _newInvoiceView_RemoveArticleClicked;
            _newInvoiceView.SaveChangesClicked += _newInvoiceView_SaveChangesClicked;
            _newInvoiceView.SearchEnterClicked += _newInvoiceView_SearchEnterClicked;
            _newInvoiceView.SearchTextChanged += _newInvoiceView_SearchTextChanged;
            _newInvoiceView.TypeSelectionChanged += _newInvoiceView_TypeSelectionChanged;
            _newInvoiceView.WindowLoaded += _newInvoiceView_WindowLoaded;
            _newInvoiceView.WindowActivated += _newInvoiceView_WindowActivated;
            _newInvoiceView.ArticlesTableMouseClicked += _newInvoiceView_ArticlesTableMouseClicked;
        }

        void _newInvoiceView_ArticlesTableMouseClicked(object sender, EventArgs e)
        {
            if (_newInvoiceView.SelectedArticle != null && _newInvoiceView.CountOfArticlesInInvoice==0)
            {
                _newInvoiceView.IsAddItemEnabled = true;
            }
        }

        public void _newInvoiceView_WindowActivated(object sender, EventArgs e)
        {
            try
            {
                //Если окно было создано с передачей объекта Накладная
                if (_invoice != null)
                {
                    //Присваеваем значения накладной
                    SetValuesOfExistingInvoice();

                    //Обновляем GUI
                    RefreshGUI();

                    //Если текущий пользователь не содатель накладной - кнопка сохранить - неактивна
                    if (_currentUser.AuthorizedUser.UserName != _invoice.nameOfCreator)
                    {
                        _newInvoiceView.CanSave = false;
                    }
                }
                else
                {
                    //Присваеваем значения накладной
                    _newInvoiceView.IdString = "";
                    _newInvoiceView.StringOfText = "";

                    //Обновляем GUI
                    RefreshGUI();
                }
            }
            catch (Exception ex) { _newInvoiceView.ShowError(ex.Message); }
        }

        private void RefreshGUI()
        {
            _newInvoiceView.ListOfArticlesInInvoice = null;
            _newInvoiceView.ListOfArticlesInInvoice = _collectionOfArticles;
            _newInvoiceView.CanPrint = true;
            _newInvoiceView.IsRemoveItemEnabled = false;
            _newInvoiceView.IsAddItemEnabled = false;
        }

        private void SetValuesOfExistingInvoice()
        {
            var articlesDb = _dataAccessFactory.CreateArticlesDbAccess();

            _newInvoiceView.IdString = _invoice.invoiceId.ToString();

            _newInvoiceView.StringOfText = _invoice.text;

            _newInvoiceView.SelectedGetter = _invoice.to;

            _newInvoiceView.SelectedSender = _invoice.@from;

            _newInvoiceView.SelectedType = _invoice.type;

            Articles articleFromDb = articlesDb.FindArticleById(_invoice.articleId);

            articleFromDb.amountFree = _invoice.articleAmount;

            _collectionOfArticles.Add(articleFromDb);
        }

        //Загружается окно
        public void _newInvoiceView_WindowLoaded(object sender, EventArgs e)
        {
            try
            {
                var contrAgentsDb = _dataAccessFactory.CreateContragentsDbAccess();
                var articlesDb = _dataAccessFactory.CreateArticlesDbAccess();

                //Загружаем список контрагентов и цепляем его к комбо боксам 
                List<string> list = contrAgentsDb.GetAllShortNamesOfContrAgents();
                _newInvoiceView.ListOfGetters = list;
                _newInvoiceView.ListOfSenders = list;

                //Загружаем список типов
                List<String> typeList = new List<String>();
                typeList.Add("Товар в пути");
                typeList.Add("Обычный");
                _newInvoiceView.ListOfTypes = typeList;

                //Отображаем на экране все товары.
                _newInvoiceView.ListOfArticles = articlesDb.GetAllArticles();
            }
            catch (Exception ex) { _newInvoiceView.ShowError(ex.Message); }
        }

        //Изменён тип
        public void _newInvoiceView_TypeSelectionChanged(object sender, EventArgs e)
        {
            //Реакция на изменение типа
            if (_newInvoiceView.SelectedType.ToString() == "Товар в пути")
            {
                SetArticlesOnRoadInvoice();
            }
            else if (_newInvoiceView.SelectedType.ToString() == "Обычный")
            {
                SetUsualInvoice();
            }
        }

        private void SetUsualInvoice()
        {
            _newInvoiceView.IsCheckGetterEnabled = true;
            _newInvoiceView.SelectedSender = "ЦС Астана";
            _newInvoiceView.IsCheckSenderEnabled = false;
        }

        private void SetArticlesOnRoadInvoice()
        {
            _newInvoiceView.IsCheckSenderEnabled = true;
            _newInvoiceView.SelectedGetter = "ЦС Астана";
            _newInvoiceView.IsCheckGetterEnabled = false;
        }

        //Чек бокс
        public void _newInvoiceView_SearchTextChanged(object sender, EventArgs e)
        {
            var articlesDb = _dataAccessFactory.CreateArticlesDbAccess();

            _newInvoiceView.IsSearchChecked = false;

            if (_newInvoiceView.StringOfSearch == "")
            {
                //Отображаем на экране все товары.
                _newInvoiceView.ListOfArticles = articlesDb.GetAllArticles();
            }
        }

        //Нажатие Enter в строке поиска
        public void _newInvoiceView_SearchEnterClicked(object sender, EventArgs e)
        {
            try
            {
                FindArticle();
            }
            catch (Exception ex) { _newInvoiceView.ShowError(ex.Message); }
        }

        private void FindArticle()
        {
            var articlesDb = _dataAccessFactory.CreateArticlesDbAccess();

            List<Articles> list = articlesDb.FindArticlesByRequestString(_newInvoiceView.StringOfSearch);
            _newInvoiceView.ListOfArticles = list;
            _newInvoiceView.IsSearchChecked = true;
        }

        //Сохранить
        public void _newInvoiceView_SaveChangesClicked(object sender, EventArgs e)
        {
            {
                if (_newInvoiceView.StringOfType != "" && _newInvoiceView.StringOfSender != "" &&
                    _newInvoiceView.StringOfGetter != "" && _newInvoiceView.CountOfArticlesInInvoice > 0)
                {
                    try
                    {
                        Articles article = _collectionOfArticles.FirstOrDefault();

                        //Если Номера накладной нет, то создаём новую накладную.
                        if (_newInvoiceView.IdString == "")
                        {
                            CreateNewInvoice(article);
                        }

                        //Если номер накладной есть, обновляем существующую накладную
                        else
                        {
                            UpdateExistingInvoice(article);
                        }

                        //Делаем доступной кнопку печати
                        _newInvoiceView.CanPrint = true;
                    }
                    catch (Exception ex)
                    {
                        _newInvoiceView.ShowError(ex.Message);
                    }
                }
                else
                {
                    _newInvoiceView.ShowError("Заполните все доступные поля!");
                }
            }
        }

        private void UpdateExistingInvoice(Articles article)
        {
            //Получаем дату изменения
            string currentDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

            var invoicesDb = _dataAccessFactory.CreateInvoicesDbAccess();

            //Обновляем существующую накладную
            invoicesDb.UpdateInvoice(_newInvoiceView.IdString, _newInvoiceView.StringOfType,
                _newInvoiceView.StringOfSender, _newInvoiceView.StringOfGetter,
                _currentUser.AuthorizedUser.UserName,
                article.nameOfArticle, article.amountFree, currentDate, article.articleId, _newInvoiceView.StringOfText);
        }

        private void CreateNewInvoice(Articles article)
        {
            //Получаем дату создания
            string currentDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

            var invoicesDb = _dataAccessFactory.CreateInvoicesDbAccess();

            //Создаём накладную по данным
            invoicesDb.CreateInvoice(_newInvoiceView.StringOfType, _newInvoiceView.StringOfSender,
                _newInvoiceView.StringOfGetter, _currentUser.AuthorizedUser.UserName,
                article.nameOfArticle, article.amountFree, currentDate, article.articleId, _newInvoiceView.StringOfText);

            //Получаем и задаём Номер накладной.
            _newInvoiceView.IdString = invoicesDb.GetCurrentIdOfInvoice();
        }

        //Убираем товар 
        public void _newInvoiceView_RemoveArticleClicked(object sender, EventArgs e)
        {
            _collectionOfArticles.Remove((Articles)_newInvoiceView.SelectedArticleInInvoice);
            _newInvoiceView.ListOfArticlesInInvoice = null;
            _newInvoiceView.ListOfArticlesInInvoice = _collectionOfArticles;
            _newInvoiceView.IsRemoveItemEnabled = false;
            _newInvoiceView.IsAddItemEnabled = true;
        }
        
        //Печать
        public void _newInvoiceView_PrintClicked(object sender, EventArgs e)
        {
            //Получаем товар из накладной
            Articles article = _collectionOfArticles.FirstOrDefault();

            //Создаём объект report и загружаем в него нашу модель
            StiReport rep = new StiReport();
            rep.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\Report.mrt");

            //Компилируем report и вкладываем в него наши значения
            rep.Compile();
            rep["OrganizationFrom"] = _newInvoiceView.StringOfSender;
            rep["OrganizationTo"] = _newInvoiceView.StringOfGetter;
            rep["ArticleId"] = article.articleId.ToString();
            rep["ArticleName"] = article.nameOfArticle;
            rep["ArticleAmount"] = article.amountFree.ToString();
            rep["NumberOfInvoice"] = _newInvoiceView.IdString;
            rep["DateOfInvoice"] = DateTime.Now.ToString("yyyy/MM/dd");

            //Отображаем
            rep.ShowWithWpf();
        }

        //При нажатии Отмена
        public void _newInvoiceView_CanselClicked(object sender, EventArgs e)
        {
            _newInvoiceView.Hide();
        }

        //Щелчёк по таблице товаров в накладной
        public void _newInvoiceView_ArticlesInInvoiceMouseClicked(object sender, EventArgs e)
        {
            if (_newInvoiceView.SelectedArticleInInvoice != null)
            {
                _newInvoiceView.IsRemoveItemEnabled = true;
            }
            else { _newInvoiceView.IsRemoveItemEnabled = false; }
        }

        public void _newInvoiceView_AddArticleClicked(object sender, EventArgs e)
        {
            if (_newInvoiceView.SelectedArticle != null)
            {
                try
                {
                    Articles selectedItem = (Articles)_newInvoiceView.SelectedArticle;

                    //Находим по Id товар и создаём новый объект
                    FindArticleByIdAndSet(selectedItem);

                    //Обновляем GUI
                    _newInvoiceView.ListOfArticlesInInvoice = null;
                    _newInvoiceView.ListOfArticlesInInvoice = _collectionOfArticles;
                    _newInvoiceView.IsAddItemEnabled = false;
                }
                catch (Exception ex) { _newInvoiceView.ShowError(ex.Message);}
            }
            else { _newInvoiceView.ShowError("Выберите товар!"); }
        }        

        private void FindArticleByIdAndSet(Articles selectedItem)
        {
            var articlesDb = _dataAccessFactory.CreateArticlesDbAccess();

            Articles articleFromDb = articlesDb.FindArticleById(selectedItem.articleId);
            articleFromDb.amountFree = 0;
            _collectionOfArticles.Add(articleFromDb);
        }


        //Методы запуска без существующей накладной и с ней
        public void Run()
        {
            _newInvoiceView.Show();
        }
     
        public void Run(Invoices invoice)
        {
            _invoice = invoice;
            _newInvoiceView.Show();
        }
    }
}
