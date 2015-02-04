using System;
using System.Collections.Generic;
using System.Diagnostics;
using Store.DataAccess.Factory;
using Store.Model;
using Store.Presenter.FactoryOfPtresenter;
using Store.View;

namespace Store.Presenter.Impl
{
    public class CalenderPresenter: ICalenderPresenter
    {
        private readonly ICalenderView _calenderView;
        private readonly IPresenterFactory _presenterFactory;
        private readonly IDataAccessFactory _dataAccessFactory;
        private readonly ICurrentUser _currentUser;

        //Конструктор
        public CalenderPresenter(ICalenderView calenderView, IPresenterFactory presenterFactory,
            IDataAccessFactory dataAccessFactory, ICurrentUser currentUser)
        {
            _calenderView = calenderView;
            _presenterFactory = presenterFactory;
            _dataAccessFactory = dataAccessFactory;
            _currentUser = currentUser;

            _calenderView.CalendarSelectedDateChanged += _calenderView_CalendarSelectedDateChanged;
            _calenderView.ChangeModuleClicked += _calenderView_ChangeModuleClicked;
            _calenderView.ChangeUserClicked += _calenderView_ChangeUserClicked;
            _calenderView.CreateEventClicked += _calenderView_CreateEventClicked;
            _calenderView.SendAllClicked += _calenderView_SendAllClicked;
            _calenderView.SendMailClicked += _calenderView_SendMailClicked;
            _calenderView.UpdateClicked += _calenderView_UpdateClicked;
            _calenderView.CreateNotificationClicked += _calenderView_CreateNotificationClicked;
        }

        public void _calenderView_CreateNotificationClicked(object sender, EventArgs e)
        {
            var notificationPresenter = _presenterFactory.CreateNotificationPresenter();
            notificationPresenter.Run(_currentUser, "Уведомление");
        }

        //Редактировать
        public void _calenderView_UpdateClicked(object sender, EventArgs e)
        {
            if (_calenderView.SelectedNotification != null)
            {
                Notifications existingNotification = (Notifications)_calenderView.SelectedNotification;
                var notificationPresenter = _presenterFactory.CreateNotificationPresenter();
                notificationPresenter.Run(_currentUser, existingNotification);
            }
            else { _calenderView.ShowError("Выберите документ для редактирования!"); }
        }

        //Отправить сообщение
        public void _calenderView_SendMailClicked(object sender, EventArgs e)
        {
            //Просим ввести имя пользователя, для отправки письма
            string userName = Microsoft.VisualBasic.Interaction.InputBox("Введите имя пользователя, которому хотите отправить сообщения и нажмите ОК.",
                "Укажите пользователя.");

            //Пытаемся найти этого пользователя, его е-майл и открыть аутлук.
            try
            {
                var userAccess = _dataAccessFactory.CreateEmployeesDbAccess();
                
                string email = userAccess.FindEmailOfUser(userName);
                if (email != null)
                {
                    string mailString = "mailto:" + email;
                    Process.Start(mailString);
                }
                else { _calenderView.ShowError("Введите существующего пользователя!");
                }
            }
            catch (Exception ex) { _calenderView.ShowError(ex.Message);}
        }

        //Сделать рассылку
        public void _calenderView_SendAllClicked(object sender, EventArgs e)
        {
            //Пытаемся найти этого пользователя, его е-майл и открыть аутлук.
            try
            {
                var userAccess = _dataAccessFactory.CreateEmployeesDbAccess();
                List<string> list = userAccess.GetAllEmails();

                string allEmails = "";
                foreach (string element in list)
                {
                    allEmails += element + "; ";
                }

                string mailString = "mailto:" + allEmails;
                Process.Start(mailString);
            }
            catch (Exception ex) { _calenderView.ShowError(ex.Message); }
        }

        //Создать событие или уведомление
        public void _calenderView_CreateEventClicked(object sender, EventArgs e)
        {
            var notificationPresenter = _presenterFactory.CreateNotificationPresenter();
            notificationPresenter.Run(_currentUser, "Событие");
        }

        public void _calenderView_ChangeUserClicked(object sender, EventArgs e)
        {
            _calenderView.Hide();
            var authorizationPresenter = _presenterFactory.CreateAuthorizationPresenter();
            authorizationPresenter.Run();
        }

        public void _calenderView_ChangeModuleClicked(object sender, EventArgs e)
        {
            _calenderView.Hide();
            var modulePresenter = _presenterFactory.CreateModulePresenter();
            modulePresenter.Run();
        }

        //Дата изменена
        public void _calenderView_CalendarSelectedDateChanged(object sender, EventArgs e)
        {
            try
            {
                var notificationDb = _dataAccessFactory.CreateNotificationDbAccess();
                if (_calenderView.SelectedDate != null)
                {
                    _calenderView.ListOfNotifications = null;
                    _calenderView.ListOfNotifications = notificationDb.GetAllNotificationByDate(_calenderView.SelectedDate.Value.ToString("yyyy/MM/dd"));
                }
            }
            catch (Exception ex) { _calenderView.ShowError(ex.Message); }
        }

        //Запуск
        public void Run()
        {
            _calenderView.Show();
        }      
    }
}
