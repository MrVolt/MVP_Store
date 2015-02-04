using System;
using System.ComponentModel;
using System.Windows;

namespace Store.View.Impl
{
    /// <summary>
    /// Логика взаимодействия для Modules.xaml
    /// </summary>
    public partial class Modules : Window, IModuleView
    {
        public event EventHandler<EventArgs> StoreStart;
        public event EventHandler<EventArgs> CalenderStart;
        public Modules()
        {
            InitializeComponent();
        }

        //Кнопка для запуска модуля Календарь
        private void Open_Calendar(object sender, RoutedEventArgs e)
        {
            CalenderStart(this, EventArgs.Empty);
        }

        //Кнопка для запуска модуля Склад
        private void Open_Store(object sender, RoutedEventArgs e)
        {
            StoreStart(this, EventArgs.Empty);
        }

        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Modules_OnClosing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}