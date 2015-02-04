using System;
using System.Collections;

namespace Store.View
{
    public interface ISearchWindowView: IView
    {
        bool? SearchByIdChecked { get; }
        bool? SearchByArticleChecked { get; }
        bool? SearchByMakerChecked { get; }
        bool? SearchByTextChecked { get; }

        string SearchingStringById { get; }
        string SearchingStringByArticle { get; }
        string SearchingStringByMaker { get; }
        string SearchingStringByText { get; }
        IEnumerable TableOfInvoices { get; set; }


        //Обработка события нажатия кнопки Поиск
        event EventHandler<EventArgs> SearchClicked;
    }
}
