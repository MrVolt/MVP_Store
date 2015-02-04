using System;
using System.Collections.Generic;
using Store.DataAccess.Factory;
using Store.Model;
using Store.View;

namespace Store.Presenter.Impl
{
    public class NotificationPresenter: INotificationPresenter
    {
        private readonly INotificationView _notificationView;
        private readonly ICurrentUser _currentUser;
        private readonly IDataAccessFactory _dataAccessFactory;
        private Notifications _notification;

        private string _type;
        private List<string> _list;

        //Конструктор
        public NotificationPresenter(INotificationView notificationView, ICurrentUser currentUser,
            IDataAccessFactory dataAccessFactory)
        {
            _notificationView = notificationView;
            _currentUser = currentUser;
            _dataAccessFactory = dataAccessFactory;

            _notificationView.CanselClicked += _notificationView_CanselClicked;
            _notificationView.SaveMessgaeClicked += _notificationView_SaveMessgaeClicked;
            _notificationView.WindowLoaded += _notificationView_WindowLoaded;
            _notificationView.WindowActivated += _notificationView_WindowActivated;
        }

        public void _notificationView_WindowActivated(object sender, EventArgs e)
        {
            if (_notification == null)
            {
                SetNewNotification();               
            }

            //Если это редактируемая накладная - загружаем данные
            else
            {
                SetExistingNotification();
            }        
        }

        private void SetExistingNotification()
        {
            try
            {
                _notificationView.StringOfId = _notification.notificationId.ToString();
                _notificationView.StringOfType = _notification.type;
                _notificationView.StringOfSendFrom = _notification.@from;
                _notificationView.DateOfDisplay = _notification.dateOfShowing;
                _notificationView.StringOfMessage = _notification.text;
                _notificationView.CanSave = true;

                _notificationView.StringOfSendTo = _notification.to;
            }
            catch (Exception ex)
            {
                _notificationView.ShowError(ex.Message);
            }
        }

        private void SetNewNotification()
        {
            try
            {
                _notificationView.StringOfId = "";
                _notificationView.DateOfDisplay = DateTime.Now.ToString("yyyy/MM/dd");
                _notificationView.StringOfMessage = "";

                _notificationView.StringOfSendFrom = _currentUser.AuthorizedUser.UserName;
                _notificationView.StringOfType = _type;
                _notificationView.CanSave = true;
            }
            catch (Exception ex)
            {
                _notificationView.ShowError(ex.Message);
            }
        }

        //Окно загружается
        public void _notificationView_WindowLoaded(object sender, EventArgs e)
        {
            if (_notification == null)
            {
                try
                {
                    //Загружаем список пользователей
                    LoadUsersList();

                    SetNewNotification();
                }
                catch (Exception ex)
                {
                    _notificationView.ShowError(ex.Message);
                }
            }

            //Если это редактируемая накладная - загружаем данные
            else
            {
                try
                {
                    //Загружаем список пользователей
                    LoadUsersList();

                    SetExistingNotification();
                }
                catch (Exception ex)
                {
                    _notificationView.ShowError(ex.Message);
                }
            }
        }

        private void LoadUsersList()
        {
            var userDb = _dataAccessFactory.CreateEmployeesDbAccess();

            _list = userDb.GetAllUsers();
            if (_notificationView.StringOfType != "Событие")
            {
                _list.Remove("Всем");
            }
            _notificationView.SendToList = _list;
        }

        //Сохранить 
        public void _notificationView_SaveMessgaeClicked(object sender, EventArgs e)
        {
            //Если все поля заполнены - создаём накладную
            if (_notificationView.StringOfSendTo != "" && _notificationView.StringOfMessage != "")
            {
                try
                {
                    //Если у документа нет номера, значит создаём новый.
                    if (_notificationView.StringOfId == "")
                    {
                        CreateNewDocument();
                    }

                    //Иначе пересохраняем документ, если создатель - и пользователь программы один и тот же, иначе ошибка.    
                    else
                    {
                        UpdateOldDocument();
                    }
                }
                catch (Exception ex)
                {
                    _notificationView.ShowError(ex.Message);
                }
            }
            //Если есть незаполненные поля - выводим предупреждение
            else
            {
               _notificationView.ShowError("Заполните все доступные поля!");
            }
        }

        private void UpdateOldDocument()
        {
            if (_currentUser.AuthorizedUser.UserName == _notificationView.StringOfSendFrom)
            {
                var notificationDb = _dataAccessFactory.CreateNotificationDbAccess();

                notificationDb.UpdateNotification(_notificationView.StringOfId, _notificationView.StringOfSendTo,
                    _notificationView.StringOfMessage,
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm"),
                    _notificationView.DateToSave.ToString("yyyy/MM/dd"));
            }
            else
            {
                _notificationView.ShowError("У Вас нет прав для редактирования!");
            }
        }

        private void CreateNewDocument()
        {
            var notificationDb = _dataAccessFactory.CreateNotificationDbAccess();

            notificationDb.CreateNotification(_notificationView.StringOfType, _notificationView.StringOfSendFrom,
                _notificationView.StringOfSendTo,
                DateTime.Now.ToString("yyyy/MM/dd HH:mm"),
                _notificationView.DateToSave.ToString("yyyy/MM/dd"), _currentUser.AuthorizedUser.UserName,
                _notificationView.StringOfMessage);

            _notificationView.StringOfId = notificationDb.GetCurrentIdOfNotification();
        }

        //Отмена
        public void _notificationView_CanselClicked(object sender, EventArgs e)
        {
            _notificationView.Hide();
        }

        //Методы запуска
        public void Run(ICurrentUser user, Notifications existingNotification)
        {
            _notification = existingNotification;
            _notificationView.Show();
        }

        public void Run(ICurrentUser user, string document)
        {
            _type = document;
            _notificationView.Show();
        }
    }
}
