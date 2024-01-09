using Android.App;
using Android.Content;
using Android.OS;
using Xamarin.Forms;
using Appmeds.Droid;
using System;
using Appmeds.Services;

[assembly: Dependency(typeof(NotificationManagerImplementation))]
namespace Appmeds.Droid
{
    public class NotificationManagerImplementation : INotificationManager
    {
        public void ScheduleNotification(string title, string message, DateTime notifyTime)
        {
            Intent intent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
            intent.PutExtra("message", message);
            intent.PutExtra("title", title);

            // Adjust flags based on Android version
            PendingIntentFlags flags = PendingIntentFlags.UpdateCurrent;
            if (Build.VERSION.SdkInt >= BuildVersionCodes.S)
            {
                flags |= PendingIntentFlags.Immutable;
            }

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, intent, flags);
            AlarmManager alarmManager = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);

            // Convert DateTime to milliseconds
            var triggerTime = (long)(notifyTime - DateTime.Now).TotalMilliseconds;

            alarmManager.Set(AlarmType.RtcWakeup, SystemClock.ElapsedRealtime() + triggerTime, pendingIntent);
        }

    }
}
