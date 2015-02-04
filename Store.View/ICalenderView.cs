using System;
using System.Collections;

namespace Store.View
{
    public interface ICalenderView: IView
    {
        IEnumerable ListOfNotifications { get; set; }
        object SelectedNotification { get; set; }

        DateTime? SelectedDate { get; }

        //Обработка события Создать Уведомление
        event EventHandler<EventArgs> CreateEventClicked;

        ////Обработка события Создать Уведомление
        event EventHandler<EventArgs> CreateNotificationClicked;
        
        //Обработка события нажатия кнопки редактировать
        event EventHandler<EventArgs> UpdateClicked;

        //Обработка события изменения даты выбранной в календаре
        event EventHandler<EventArgs> CalendarSelectedDateChanged;

        //Обработка события нажатия меню смены модуля
        event EventHandler<EventArgs> ChangeModuleClicked;

        //Обработка события нажатия меню смены пользователя
        event EventHandler<EventArgs> ChangeUserClicked;

        //Обработка события нажатия меню отправить письмо
        event EventHandler<EventArgs> SendMailClicked;

        //Обработка события Создать рассылку
        event EventHandler<EventArgs> SendAllClicked;
    }
}
