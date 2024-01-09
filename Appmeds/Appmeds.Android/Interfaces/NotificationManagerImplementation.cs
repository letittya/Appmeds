using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Appmeds.Droid;
using Appmeds.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationManagerImplementation))]
namespace Appmeds.Droid
{
    public class NotificationManagerImplementation : INotificationManager
    {
        public void ScheduleNotification(string title, string message, DateTime notifyTime)
        {
            Intent alarmIntent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
            alarmIntent.PutExtra("message", message);
            alarmIntent.PutExtra("title", title);

            // Set a unique action for the intent
            var uniqueAction = "com.appmeds.NOTIFY" + notifyTime.GetHashCode(); // Example unique action
            alarmIntent.SetAction(uniqueAction);

            var pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, alarmIntent, PendingIntentFlags.Immutable | PendingIntentFlags.UpdateCurrent);
            var alarmManager = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);

            var triggerTime = ConvertDateTimeToMilliseconds(notifyTime);
            alarmManager.Set(AlarmType.RtcWakeup, triggerTime, pendingIntent);
        }


        private long ConvertDateTimeToMilliseconds(DateTime dateTime)
        {
            DateTime reference = new DateTime(1970, 1, 1);
            TimeSpan diff = dateTime.ToUniversalTime() - reference;
            return (long)diff.TotalMilliseconds;
        }
    }

}
