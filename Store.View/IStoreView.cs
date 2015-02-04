using System;
using System.Collections;

namespace Store.View
{
    public interface IStoreView: IView
    {
        IEnumerable MakersList { get; set; }
        object SelectedMaker { get; }
        string SearchString { get; }
        IEnumerable TableOfArticles { get; set; }
        bool? SearchIsChecked { get; set; }

        //Событие для прогрузки контента
        event EventHandler<EventArgs> ViewLoaded;

        //Событие при нажатии Выход
        event EventHandler<EventArgs> Exit;

        //Событие для запуска склада
        event EventHandler<EventArgs> ChangeUser;
        
        //Событие при нажатии меню Список накладных
        event EventHandler<EventArgs> ListOfInvoices;

        //Событие при нажатии кнопки Обновить
        event EventHandler<EventArgs> Refresh;

        //Событие при щелчке по списку производителей
        event EventHandler<EventArgs> ListOfMakersMouseClick;
        
        //Событие при нажатии кнопки Сменить Модуль
        event EventHandler<EventArgs> ChangeModul;

        //Событие при нажатии меню Создать накладную
        event EventHandler<EventArgs> CreateInvoice;

        //Событие при нажатии меню Контрагенты
        event EventHandler<EventArgs> Contragents;

        //Событие при нажатии кнопки Enter в поле поиска товара
        event EventHandler<EventArgs> SearchEnterClicked;

        //Событие, возникающее при изменении текста в поиске товара
        event EventHandler<EventArgs> SearchTextChanged;

        //Событие, возникающее при нажатии меню Поиск накладной
        event EventHandler<EventArgs> SearchInvoice;

        void Toast(string toast);
    }
}
