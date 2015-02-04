using System;

namespace Store.Presenter
{
    public interface ICalenderPresenter:IPresenter
    {
        void _calenderView_CreateNotificationClicked(object sender, EventArgs e);

        void _calenderView_UpdateClicked(object sender, EventArgs e);

        void _calenderView_SendMailClicked(object sender, EventArgs e);

        void _calenderView_SendAllClicked(object sender, EventArgs e);

        void _calenderView_CreateEventClicked(object sender, EventArgs e);

        void _calenderView_ChangeUserClicked(object sender, EventArgs e);

        void _calenderView_ChangeModuleClicked(object sender, EventArgs e);

        void _calenderView_CalendarSelectedDateChanged(object sender, EventArgs e);
    }
}
