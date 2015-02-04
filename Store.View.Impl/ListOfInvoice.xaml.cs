using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Store.View.Impl
{
    /// <summary>
    /// Примерное отображение новых вкладок при вызове Реестра накладных и Товара в пути (Аналогичная ситуация с
    /// контрагентами, небольшие вариации с полями. Слева список контрагентов - справа возможность их редактирования или же создания).
    /// </summary>
    public partial class ListOfInvoice : Window, IListOfInvoiceView
    {
        public bool IsCreateEnabled
        {
            get { return CreateButton.IsEnabled; }
            set { CreateButton.IsEnabled = value; }
        }

        public bool IsEditEnabled
        {
            get { return ChangeButton.IsEnabled; }
            set { ChangeButton.IsEnabled = value; }
        }

        public bool IsDoneEnabled
        {
            get { return DoneButton.IsEnabled; }
            set { DoneButton.IsEnabled = value; }
        }

        public bool IsHistoryEnabled
        {
            get { return HistoryButton.IsEnabled; }
            set { HistoryButton.IsEnabled = value; }
        }

        public IEnumerable ListOfInvoices
        {
            get { return InvoicesTable.ItemsSource; }
            set { InvoicesTable.ItemsSource = value; }
        }

        public object SelectedInvoice
        {
            get { return InvoicesTable.SelectedItem; }
        }

        public event EventHandler<EventArgs> WindowLoaded;
        public event EventHandler<EventArgs> CreateNewInvoiceClicked;
        public event EventHandler<EventArgs> InvoicesTableMouseClicked;
        public event EventHandler<EventArgs> ChangeInvoiceClicked;
        public event EventHandler<EventArgs> RefreshClicked;
        public event EventHandler<EventArgs> LoadHistoryClicked;
        public event EventHandler<EventArgs> DoneClicked;

        public ListOfInvoice()
        {
            InitializeComponent();

            Loaded += ListOfInvoice_Loaded;
        }

        void ListOfInvoice_Loaded(object sender, RoutedEventArgs e)
        {
            WindowLoaded(this, EventArgs.Empty);
        }

        //Обработка события при нажатии кнопки для открытия окна по созданию новой накладной.
        private void Create_New_Invoice(object sender, RoutedEventArgs e)
        {
            CreateNewInvoiceClicked(this, EventArgs.Empty);
        }


        //При выборе Накладной кнопка Изменить - становится активной. 
        private void InvoicesTable_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            InvoicesTableMouseClicked(this, EventArgs.Empty);
        }

        //Реакция на событие нажатия кнопки Изменить
        private void ChangeInvoice_Click(object sender, RoutedEventArgs e)
        {
            ChangeInvoiceClicked(this, EventArgs.Empty);
        }

        //Реакция на событие нажатия кнопки Обновить
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshClicked(this, EventArgs.Empty);
        }

        //Реакция на событие нажатия кнопки История
        private void LoadHistory_Click(object sender, RoutedEventArgs e)
        {
            LoadHistoryClicked(this, EventArgs.Empty);
        }

        //Реакция на событие нажатия кнопки Списать
        private void Done_click(object sender, RoutedEventArgs e)
        {
            DoneClicked(this, EventArgs.Empty);
        }

        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ListOfInvoice_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}