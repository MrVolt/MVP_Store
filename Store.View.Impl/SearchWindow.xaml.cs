using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;

namespace Store.View.Impl
{
    /// <summary>
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window, ISearchWindowView
    {
        public bool? SearchByIdChecked
        {
            get { return Id.IsChecked; }
        }

        public bool? SearchByArticleChecked
        {
            get { return Article.IsChecked; }
        }

        public bool? SearchByMakerChecked
        {
            get { return Maker.IsChecked; }
        }

        public bool? SearchByTextChecked
        {
            get { return Text.IsChecked; }
        }

        public string SearchingStringById
        {
            get { return IdText.Text; }
        }

        public string SearchingStringByArticle
        {
            get { return ArticleText.Text; }
        }

        public string SearchingStringByMaker
        {
            get { return MakerText.Text; }
        }

        public string SearchingStringByText
        {
            get { return TextText.Text; }
        }

        public IEnumerable TableOfInvoices
        {
            get { return InvoicesTable.ItemsSource; }
            set { InvoicesTable.ItemsSource = value; }
        }

        public event EventHandler<EventArgs> SearchClicked;

        public SearchWindow()
        {
            InitializeComponent();
        }

        //Обработка события нажатия на кнопку Найти
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            SearchClicked(this, EventArgs.Empty);
        }

        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void SearchWindow_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}