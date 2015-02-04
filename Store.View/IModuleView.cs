using System;

namespace Store.View
{
    public interface IModuleView: IView
    {
        //Событие для запуска склада
        event EventHandler<EventArgs> StoreStart;

        //Событие для запуска календаря
        event EventHandler<EventArgs> CalenderStart;        
    }
}
