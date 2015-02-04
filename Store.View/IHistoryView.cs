using System;
using System.Collections;

namespace Store.View
{
    public interface IHistoryView: IView
    {
        IEnumerable ListOfHistory { get; set; }

        //Обработка события загрузки окна
        event EventHandler<EventArgs> WindowLoaded;

        event EventHandler<EventArgs> WindowActivated;
    }
}
