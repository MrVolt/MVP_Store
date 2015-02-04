using System;
using System.Collections;

namespace Store.View
{
    public interface INotificationView: IView
    {
        string StringOfMessage { get; set; }
        string StringOfId { get; set; }
        string StringOfSendFrom { get; set; }
        string StringOfType { get; set; }

        string StringOfSendTo { get; set; }

        IEnumerable SendToList { get; set; }

        DateTime DateToSave { get; }

        string DateOfDisplay { get; set; }

        bool CanSave { get; set; }

        //Обработка события загрузки окна
        event EventHandler<EventArgs> WindowLoaded;

        //Обработка события отображения
        event EventHandler<EventArgs> WindowActivated;

        //Обработка события нажатия кнопки Сохранить
        event EventHandler<EventArgs> SaveMessgaeClicked;

        //Обработка события нажатия кнопки Отмена
        event EventHandler<EventArgs> CanselClicked;
    }
}
