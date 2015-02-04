using System;
using System.Collections;

namespace Store.View
{
    public interface IContragentsWindowView: IView
    {
        bool IsCopyEnabled { get; set; }
        bool IsEditEnabled { get; set; }
        bool IsPrintEnabled { get; set; }
        bool IsSaveEnabled { get; set; }

        string StringOfId{ get; set; }
        string StringOfName { get; set; }
        string StringOfTel { get; set; }
        string StringOfAddress { get; set; }
        string StringOfBin { get; set; }

        IEnumerable ListOfcontragents { get; set; }

        object SelectedContragents { get; set; }


        //Обработка события загрузки окна
        event EventHandler<EventArgs> WindowLoaded;

        //Обработка события при нажатии кнопки мыши по таблице контрагентов
        event EventHandler<EventArgs> ContragentsTableMouseClicked;

        //Обработка события нажатия кнопки Редактивровать 
        event EventHandler<EventArgs> EditContragentClicked;

        //Обработка события нажатия кнопки Копировать
        event EventHandler<EventArgs> CopyContragentClicked;

        //Обработка события нажатия кнопки Сохранить
        event EventHandler<EventArgs> SaveContragentClicked;

        //Обработка события нажатия кнопки распечатать
        event EventHandler<EventArgs> PrintContragentClicked;

        void ShowInformMessage(string message);
    }
}
