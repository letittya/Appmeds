using System;
using System.Collections.Generic;
using System.Text;

namespace Appmeds.Services
{
    public interface INotificationManager
    {
        void ScheduleNotification(string title, string message, DateTime notifyTime);
    }

}
