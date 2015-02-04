using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Store.View.Impl
{
    /// <summary>
    /// Логика взаимодействия для Calendar.xaml
    /// </summary>
    public partial class Calendar : Window, ICalenderView
    {
        public IEnumerable ListOfNotifications
        {
            get { return NotificationList.ItemsSource; }
            set { NotificationList.ItemsSource = value; }
        }

        public object SelectedNotification
        {
            get { return NotificationList.SelectedItem; }
            set { NotificationList.SelectedItem = value; }
        }

        public DateTime? SelectedDate
        {
            get { return CalendarOfNotifications.SelectedDate; }
        }

        public event EventHandler<EventArgs> CreateEventClicked;
        public event EventHandler<EventArgs> CreateNotificationClicked;
        public event EventHandler<EventArgs> UpdateClicked;
        public event EventHandler<EventArgs> CalendarSelectedDateChanged;
        public event EventHandler<EventArgs> ChangeModuleClicked;
        public event EventHandler<EventArgs> ChangeUserClicked;
        public event EventHandler<EventArgs> SendMailClicked;
        public event EventHandler<EventArgs> SendAllClicked;

        public Calendar()
        {
            InitializeComponent();
        }

        //Создание события или уведомления. Ключевое отличие - событие для всех. Уведомление для кого-то одного.
        private void Create_Event(object sender, RoutedEventArgs e)
        {
            CreateEventClicked(this, EventArgs.Empty);
        }

        //Создание события или уведомления. Ключевое отличие - событие для всех. Уведомление для кого-то одного.
        private void Create_Notification(object sender, RoutedEventArgs e)
        {
            CreateNotificationClicked(this, EventArgs.Empty);
        }

        //Обработка события нажатия на Редактировать документ
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            UpdateClicked(this, EventArgs.Empty);
        }

        //Обработка события нажатия на дату
        private void CalendarOfNotifications_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            CalendarSelectedDateChanged(this, EventArgs.Empty);
        }


        //Обработка выбора меню Сменить модуль
        private void ModuleChange_Click(object sender, RoutedEventArgs e)
        {
            ChangeModuleClicked(this, EventArgs.Empty);
        }

        //Обработка выбора меню Сменить пользователя
        private void UserChange_Click(object sender, RoutedEventArgs e)
        {
            ChangeUserClicked(this, EventArgs.Empty);
        }

        //Обработка события щелчка на меню Написать письмо
        private void SendMail_Click(object sender, RoutedEventArgs e)
        {
            SendMailClicked(this, EventArgs.Empty);
        }

        //Обработка события нажатия меню Создать рассылку
        private void SendAll_Click(object sender, RoutedEventArgs e)
        {
            SendAllClicked(this, EventArgs.Empty);
        }

        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Calendar_OnClosing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}