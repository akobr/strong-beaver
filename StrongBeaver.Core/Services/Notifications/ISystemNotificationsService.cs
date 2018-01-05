using System;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Notification
{
    public interface ISystemNotificationsService : IService
    {
        void Configure(string notificationKey, Type notificationType);

        Task CreateNotificationByName(string notificationName);

        void CreateNotification(string title, string message);

        void CreateNotification(string title, string message, Uri imageUri);
    }
}