using System.Collections.Generic;

namespace Store.DataAccess
{
    public interface INotificationDbAccess
    {
        void CreateNotification(string type, string from, string to, string dateOfCreation,
            string dateOfShowing, string nameOfCreator, string text);

        string GetCurrentIdOfNotification();

        void UpdateNotification(string id, string to, string text, string dateOfCreation, string dateOfShowing);

        List<Model.Notifications> GetAllNotificationByDate(string dateOfShowing);

        int GetAllNotificationsForUser(string name);
    }
}
