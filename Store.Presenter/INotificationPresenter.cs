using System;
using Store.Model;

namespace Store.Presenter
{
    public interface INotificationPresenter
    {
        void _notificationView_WindowActivated(object sender, EventArgs e);

        void _notificationView_WindowLoaded(object sender, EventArgs e);

        void _notificationView_SaveMessgaeClicked(object sender, EventArgs e);

        void _notificationView_CanselClicked(object sender, EventArgs e);

        void Run(ICurrentUser user, Notifications existingNotification);
        void Run(ICurrentUser user, string document);
    }
}
