using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Mantin.Controls.Wpf.Notification;

namespace Store.View.Impl
{
    /// <summary>
    /// Логика взаимодействия для Store.xaml
    /// </summary>
    public partial class StoreModule : Window, IStoreView
    {
        public IEnumerable MakersList
        {
            get { return ListOfMakers.ItemsSource; }
            set { ListOfMakers.ItemsSource = value; }
        }

        public object SelectedMaker
        {
            get { return ListOfMakers.SelectedItem; }
        }

        public string SearchString
        {
            get { return SearchBox.Text; }
        }

        public IEnumerable TableOfArticles
        {
            get { return ArticlesTable.ItemsSource; }
            set { ArticlesTable.ItemsSource = value; }
        }

        public bool? SearchIsChecked
        {
            get { return SearchingCheckBox.IsChecked; }
            set { SearchingCheckBox.IsChecked = value; }
        }

        public event EventHandler<EventArgs> ViewLoaded;
        public event EventHandler<EventArgs> Exit;
        public event EventHandler<EventArgs> ChangeUser;
        public event EventHandler<EventArgs> ListOfInvoices;
        public event EventHandler<EventArgs> Refresh;
        public event EventHandler<EventArgs> ListOfMakersMouseClick;
        public event EventHandler<EventArgs> ChangeModul;
        public event EventHandler<EventArgs> CreateInvoice;
        public event EventHandler<EventArgs> Contragents;
        public event EventHandler<EventArgs> SearchEnterClicked;
        public event EventHandler<EventArgs> SearchTextChanged;
        public event EventHandler<EventArgs> SearchInvoice;

        public StoreModule()
        {
            InitializeComponent();
            Loaded +=Store_Loaded;
        }

        //Загрузка контента
        void Store_Loaded(object sender, RoutedEventArgs e)
        {
            ViewLoaded(this, EventArgs.Empty);
        }

        //Обработка события при нажатии меню Выход.
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Exit(this, EventArgs.Empty);
        }

        //Обработка события при нажатии меню Сменить пользователя
        private void Change_User(object sender, RoutedEventArgs e)
        {
            ChangeUser(this, EventArgs.Empty);
        }

        //Открываем реестр накладных
        private void ListOfInvoice_Click(object sender, RoutedEventArgs e)
        {
            ListOfInvoices(this, EventArgs.Empty);
        }

        //Реакция на нажатие кнопки Обновить
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh(this, EventArgs.Empty);
        }

        //Обработка события при нажатии левой кнопки мыши в списке производителей
        private void ListOfMakers_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListOfMakersMouseClick(this, EventArgs.Empty);
        }

        //Обработка щелчка по кнопке Сменить модуль
        private void ModulChange_click(object sender, RoutedEventArgs e)
        {
            ChangeModul(this, EventArgs.Empty);
        }

        //Обрыботка события щелчка по меню Создать накладную
        private void CreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            CreateInvoice(this, EventArgs.Empty);
        }

        //Обрыботка события щелчка по меню Реестр контрагентов
        private void Contragents_Click(object sender, RoutedEventArgs e)
        {
            Contragents(this, EventArgs.Empty);
        }

        //Обработка события нажатия клавиши Enter в поисковой строке
        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            SearchEnterClicked(this, EventArgs.Empty);
        }

        //Если Текст в Поиске изменён - кнопка чек спадает.
        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            SearchTextChanged(this, EventArgs.Empty);
        }

        //Обработка события нажатия меню Поиск накладной
        private void SearchInvoice_Click(object sender, RoutedEventArgs e)
        {
            SearchInvoice(this, EventArgs.Empty);
        }

        //Вывод сообщения об ошибке
        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        //Создаём Всплывающее окно
        public void Toast(string toast)
        {
            new ToastPopUp("Внимание!", toast, NotificationType.Information).Show();
        }

        private void StoreModule_OnClosing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}