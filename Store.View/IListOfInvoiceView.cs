using System;
using System.Collections;

namespace Store.View
{
    public interface IListOfInvoiceView: IView
    {
        bool IsCreateEnabled{ get; set; }
        bool IsEditEnabled { get; set; }
        bool IsDoneEnabled { get; set; }
        bool IsHistoryEnabled { get; set; }

        IEnumerable ListOfInvoices { get; set; }
        object SelectedInvoice{ get; }

        //Обработка события загрузки окна
        event EventHandler<EventArgs> WindowLoaded;

        //Обработка события нажатия кнопки Создать новую накладную
        event EventHandler<EventArgs> CreateNewInvoiceClicked;

        //Обработка события нажатия кнопки мыши по таблице накладных
        event EventHandler<EventArgs> InvoicesTableMouseClicked;

        //Обработка события кнопки Редакторовать
        event EventHandler<EventArgs> ChangeInvoiceClicked;

        //Обработка события нажатия кнопки Обновить
        event EventHandler<EventArgs> RefreshClicked;

        //Обработка события нажатия кнопки История
        event EventHandler<EventArgs> LoadHistoryClicked;

        //Обработка события нажатия кнопки Списать
        event EventHandler<EventArgs> DoneClicked;
    }
}
