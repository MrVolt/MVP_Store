using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Store.View.Impl
{
    /// <summary>
    /// Логика взаимодействия для ContragentsWindow.xaml
    /// </summary>
    public partial class ContragentsWindow : Window, IContragentsWindowView
    {
        public bool IsCopyEnabled
        {
            get { return CopyButton.IsEnabled; }
            set { CopyButton.IsEnabled = value; }
        }

        public bool IsEditEnabled
        {
            get { return EditButton.IsEnabled; }
            set { EditButton.IsEnabled = value; }
        }

        public bool IsPrintEnabled
        {
            get { return PrintButton.IsEnabled; }
            set { PrintButton.IsEnabled = value; }
        }

        public bool IsSaveEnabled
        {
            get { return SaveButton.IsEnabled; }
            set { SaveButton.IsEnabled = value; }
        }

        public string StringOfId
        {
            get { return IdBox.Text; }
            set { IdBox.Text = value; }
        }

        public string StringOfName
        {
            get { return NameBox.Text; }
            set { NameBox.Text = value; }
        }

        public string StringOfTel
        {
            get { return TelBox.Text; }
            set { TelBox.Text = value; }
        }

        public string StringOfAddress
        {
            get { return AddressBox.Text; }
            set { AddressBox.Text = value; }
        }

        public string StringOfBin
        {
            get { return BinBox.Text; }
            set { BinBox.Text = value; }
        }

        public IEnumerable ListOfcontragents
        {
            get { return ContragentsTable.ItemsSource; }
            set { ContragentsTable.ItemsSource = value; }
        }

        public object SelectedContragents
        {
            get { return ContragentsTable.SelectedItem; }
            set { ContragentsTable.SelectedItem = value; }
        }

        public event EventHandler<EventArgs> WindowLoaded;
        public event EventHandler<EventArgs> ContragentsTableMouseClicked;
        public event EventHandler<EventArgs> EditContragentClicked;
        public event EventHandler<EventArgs> CopyContragentClicked;
        public event EventHandler<EventArgs> SaveContragentClicked;
        public event EventHandler<EventArgs> PrintContragentClicked;

        public ContragentsWindow()
        {
            InitializeComponent();

            Loaded += ContragentsWindow_Loaded;
        }

        //Загружаем компоненты в окно
        void ContragentsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowLoaded(this, EventArgs.Empty);
        }

        //Обработка события выбора элемента из таблицы контрагентов
        private void ContragentsTable_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ContragentsTableMouseClicked(this, EventArgs.Empty);
        }

        //Обработка события щелчка по кнопке Редактировать
        private void EditContragent_Click(object sender, RoutedEventArgs e)
        {
            EditContragentClicked(this, EventArgs.Empty);
        }

        //Обработка события щелчка по кнопке Копировать
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            CopyContragentClicked(this, EventArgs.Empty);
        }

        //Обработка события щелчка по кнопке Сохранить
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveContragentClicked(this, EventArgs.Empty);
        }

        //Обработка события щелчка по кнопке Печатать
        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintContragentClicked(this, EventArgs.Empty);
        }

        public void ShowInformMessage(string message)
        {
             MessageBox.Show(message, "Операция завершена.", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ContragentsWindow_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}