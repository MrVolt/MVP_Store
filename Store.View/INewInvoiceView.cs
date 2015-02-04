using System;
using System.Collections;

namespace Store.View
{
    public interface INewInvoiceView: IView
    {
        string IdString { get; set; }
        string StringOfSearch { get; set; }
        string StringOfText { get; set; }

        object SelectedType { get; set; }
        IEnumerable ListOfTypes { get; set; }
        string StringOfType { get; set; }
       
        object SelectedSender { get; set; }
        IEnumerable ListOfSenders { get; set; }
        string StringOfSender { get; set; }
        bool IsCheckSenderEnabled { get; set; }


        object SelectedGetter { get; set; }
        IEnumerable ListOfGetters { get; set; }
        string StringOfGetter { get; set; }
        bool IsCheckGetterEnabled { get; set; }

        bool? IsSearchChecked { get; set; }

        bool IsAddItemEnabled { get; set; }
        bool IsRemoveItemEnabled { get; set; }
        bool CanSave { get; set; }
        bool CanPrint { get; set; }

        object SelectedArticle { get; set; }
        IEnumerable ListOfArticles { get; set; }

        IEnumerable ListOfArticlesInInvoice { get; set; }
        int CountOfArticlesInInvoice { get; }
        object SelectedArticleInInvoice { get; set; }


        event EventHandler<EventArgs> WindowActivated;

        //Обработка события загрузки окна
        event EventHandler<EventArgs> WindowLoaded;

        //Обработка события нажатия кнопки Отмена
        event EventHandler<EventArgs> CanselClicked;

        //Обработка события нажатия Добавить товар
        event EventHandler<EventArgs> AddArticleClicked;

        //Обработка события нажатия Убрать товар
        event EventHandler<EventArgs> RemoveArticleClicked;

        //Обработка события кнопки Сохранить
        event EventHandler<EventArgs> SaveChangesClicked;

        //Обработка события изменения Типа
        event EventHandler<EventArgs> TypeSelectionChanged;

        //Обработка события нажатия кнопки Печать
        event EventHandler<EventArgs> PrintClicked;

        //Обработка события нажатия кнопки мыши по таблице товаров в накладной
        event EventHandler<EventArgs> ArticlesInInvoiceMouseClicked;

        //Обработка события нажатия кнопки Enter в строке поиска товара
        event EventHandler<EventArgs> SearchEnterClicked;

        //Обработка события изменения текста в строке поиска товара
        event EventHandler<EventArgs> SearchTextChanged;

        //Обработка события нажатия кнопки мыши по таблице товаров
        event EventHandler<EventArgs> ArticlesTableMouseClicked;
    }
}
