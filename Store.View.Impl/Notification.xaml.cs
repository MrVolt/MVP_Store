using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;

namespace Store.View.Impl
{
    /// <summary>
    /// Логика взаимодействия для Notification.xaml
    /// </summary>
    public partial class Notification : Window, INotificationView
    {
        public string StringOfMessage
        {
            get { return Message.Text; }
            set { Message.Text = value; }
        }

        public string StringOfId
        {
            get { return IdOfDocument.Text; }
            set { IdOfDocument.Text = value; }
        }

        public string StringOfSendFrom
        {
            get { return From.Text; }
            set { From.Text = value; }
        }

        public string StringOfType
        {
            get { return Type.Text; }
            set { Type.Text = value; }
        }

        public string StringOfSendTo
        {
            get { return To.Text; }
            set { To.Text = value; }
        }

        public IEnumerable SendToList
        {
            get { return To.ItemsSource; }
            set { To.ItemsSource = value; }
        }

        public DateTime DateToSave
        {
            get { return DateOfShowing.DisplayDate; }
        }

        public string DateOfDisplay
        {
            get { return DateOfShowing.Text; }
            set { DateOfShowing.Text = value; }
        }

        public bool CanSave
        {
            get { return SaveButton.IsEnabled; }
            set { SaveButton.IsEnabled = value; }
        }

        public event EventHandler<EventArgs> WindowLoaded;
        public event EventHandler<EventArgs> WindowActivated;
        public event EventHandler<EventArgs> SaveMessgaeClicked;
        public event EventHandler<EventArgs> CanselClicked;

        //Конструктор для создания нового уведомления
        public Notification()
        {
            InitializeComponent();

            Loaded += Notification_Loaded;
        }

        //Загружаем данные для окна
        void Notification_Loaded(object sender, RoutedEventArgs e)
        {
            WindowLoaded(this, EventArgs.Empty);
        }

        //Обработка события нажатия на кнопку Создать
        private void SaveMessage_Click(object sender, RoutedEventArgs e)
        {
            SaveMessgaeClicked(this, EventArgs.Empty);
        }

        //Обработчик события при нажатии кнопки Отмена
        private void Cancel_click(object sender, RoutedEventArgs e)
        {
            CanselClicked(this, EventArgs.Empty);
        }

        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Notification_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void Notification_OnActivated(object sender, EventArgs e)
        {
            WindowActivated(this, EventArgs.Empty);
        }
    }
}