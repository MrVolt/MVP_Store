using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;

namespace Store.View.Impl
{
    /// <summary>
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Window, IHistoryView
    {
        public IEnumerable ListOfHistory
        {
            get { return HistoriesTable.ItemsSource; }
            set { HistoriesTable.ItemsSource = value; }
        }

        public event EventHandler<EventArgs> WindowLoaded;
        public event EventHandler<EventArgs> WindowActivated;

        public History()
        {

            InitializeComponent();

            Loaded += History_Loaded;
        }

        //При загрузке окна загружаем компоненты
        void History_Loaded(object sender, RoutedEventArgs e)
        {
            WindowLoaded(this, EventArgs.Empty);
        }

        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void History_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void History_OnActivated(object sender, EventArgs e)
        {
            WindowActivated(this, EventArgs.Empty);
        }
    }
}