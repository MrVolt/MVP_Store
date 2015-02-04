using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Store.View.Impl
{
    /// <summary>
    /// Вкладка создание накладной, В панели справа - дерево товаров
    /// </summary>
    public partial class NewInvoice : Window, INewInvoiceView
    {
        public string IdString
        {
            get { return IdOfInvoice.Text; }
            set { IdOfInvoice.Text = value; }
        }

        public string StringOfSearch
        {
            get { return SearchBox.Text; }
            set { SearchBox.Text = value; }
        }

        public string StringOfText
        {
            get { return Text.Text; }
            set { Text.Text = value; }
        }

        public object SelectedType
        {
            get { return Type.SelectedItem; }
            set { Type.SelectedItem = value; }
        }

        public IEnumerable ListOfTypes
        {
            get { return Type.ItemsSource; }
            set { Type.ItemsSource = value; }
        }

        public string StringOfType
        {
            get { return Type.Text; }
            set { Type.Text = value; }
        }

        public object SelectedSender
        {
            get { return Sender.SelectedItem; }
            set { Sender.SelectedItem = value; }
        }

        public IEnumerable ListOfSenders
        {
            get { return Sender.ItemsSource; }
            set { Sender.ItemsSource = value; }
        }

        public string StringOfSender
        {
            get { return Sender.Text; }
            set { Sender.Text = value; }
        }

        public bool IsCheckSenderEnabled
        {
            get { return Sender.IsEnabled; }
            set { Sender.IsEnabled = value; }
        }

        public object SelectedGetter
        {
            get { return Getter.SelectedItem; }
            set { Getter.SelectedItem = value; }
        }

        public IEnumerable ListOfGetters
        {
            get { return Getter.ItemsSource; }
            set { Getter.ItemsSource = value; }
        }

        public string StringOfGetter
        {
            get { return Getter.Text; }
            set { Getter.Text = value; }
        }

        public bool IsCheckGetterEnabled
        {
            get { return Getter.IsEnabled; }
            set { Getter.IsEnabled = value; }
        }

        public bool? IsSearchChecked
        {
            get { return CheckBox.IsChecked; }
            set { CheckBox.IsChecked = value; }
        }

        public bool IsAddItemEnabled
        {
            get { return AddButton.IsEnabled; }
            set { AddButton.IsEnabled = value; }
        }

        public bool IsRemoveItemEnabled
        {
            get { return RemoveButton.IsEnabled; }
            set { RemoveButton.IsEnabled = value; }
        }

        public bool CanSave
        {
            get { return SaveButton.IsEnabled; }
            set { SaveButton.IsEnabled = value; }
        }

        public bool CanPrint
        {
            get { return PrintButton.IsEnabled; }
            set { PrintButton.IsEnabled = value; }
        }

        public object SelectedArticle
        {
            get { return ArticlesTable.SelectedItem; }
            set { ArticlesTable.SelectedItem = value; }
        }

        public IEnumerable ListOfArticles
        {
            get { return ArticlesTable.ItemsSource; }
            set { ArticlesTable.ItemsSource = value; }
        }

        public IEnumerable ListOfArticlesInInvoice
        {
            get { return ArticlesInInvoice.ItemsSource; }
            set { ArticlesInInvoice.ItemsSource = value; }
        }

        public int CountOfArticlesInInvoice
        {
            get { return ArticlesInInvoice.Items.Count; }
        }

        public object SelectedArticleInInvoice
        {
            get { return ArticlesInInvoice.SelectedItem; }
            set { ArticlesInInvoice.SelectedItem = value; }
        }


        public event EventHandler<EventArgs> WindowActivated;
        public event EventHandler<EventArgs> WindowLoaded;
        public event EventHandler<EventArgs> CanselClicked;
        public event EventHandler<EventArgs> AddArticleClicked;
        public event EventHandler<EventArgs> RemoveArticleClicked;
        public event EventHandler<EventArgs> SaveChangesClicked;
        public event EventHandler<EventArgs> TypeSelectionChanged;
        public event EventHandler<EventArgs> PrintClicked;
        public event EventHandler<EventArgs> ArticlesInInvoiceMouseClicked;
        public event EventHandler<EventArgs> ArticlesTableMouseClicked;
        public event EventHandler<EventArgs> SearchEnterClicked;
        public event EventHandler<EventArgs> SearchTextChanged;

        public NewInvoice()
        {
            InitializeComponent();

            Loaded += Invoice_Loaded;
        }

        void Invoice_Loaded(object sender, RoutedEventArgs e)
        {
            WindowLoaded(this, EventArgs.Empty);
        }

        //Обработка события нажатия кнопки Отмена
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            CanselClicked(this, EventArgs.Empty);
        }

        /*Обработка события кнопки Добавления товара. Заполняем таблицу ArticlesInInvoice выбранными значениями
         из таблицы ArticlesTable*/
        private void AddArticle_Click(object sender, RoutedEventArgs e)
        {
            AddArticleClicked(this, EventArgs.Empty);
        }

        //Обработка события кнопки Удаления товара.
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            RemoveArticleClicked(this, EventArgs.Empty);
        }

        //Обработка события кнопки Сохранить.
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            SaveChangesClicked(this, EventArgs.Empty);
        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TypeSelectionChanged(this, EventArgs.Empty);
        }

        //Обработка события нажатия кнопки печати
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintClicked(this, EventArgs.Empty);
        }


        //Обработка включения кнопки Удалить товар из накладной
        private void ArticlesInInvoice_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ArticlesTableMouseClicked(this, EventArgs.Empty);
        }

        //Если нажат Enter - начинаем поиск
        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            SearchEnterClicked(this, EventArgs.Empty);
        }

        //Если изменён текст - чек бокс снимается
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchTextChanged(this, EventArgs.Empty);
        }

        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void NewInvoice_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void NewInvoice_OnActivated(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            WindowActivated(this, EventArgs.Empty);
        }

        private void ArticlesTable_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ArticlesTableMouseClicked(this, EventArgs.Empty);
        }
    }
}