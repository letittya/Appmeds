using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using AndroidX.Core.App;
using Appmeds.Droid;
using System;

namespace Appmeds.Droid
{
    [Service]
    public class NotificationService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            var message = intent.GetStringExtra("message");
            var title = intent.GetStringExtra("title");

            var notificationIntent = new Intent(this, typeof(MainActivity));
            var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, 0);

            var notification = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
                                    .SetContentTitle(title)
                                    .SetContentText(message)
                                    .SetSmallIcon(Resource.Drawable.notification_green)
                                    .SetContentIntent(pendingIntent)
                                    .Build();

            StartForeground(1, notification);

            return StartCommandResult.NotSticky;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        // Call this method to schedule a notification
        public void ScheduleNotification(string title, string message, DateTime notifyTime)
        {
            var intent = new Intent(this, typeof(NotificationService));
            intent.PutExtra("message", message);
            intent.PutExtra("title", title);

            var pendingIntent = PendingIntent.GetService(this, 0, intent, PendingIntentFlags.UpdateCurrent);
            var alarmManager = (AlarmManager)GetSystemService(AlarmService);
            alarmManager.Set(AlarmType.RtcWakeup, GetNotifyTime(notifyTime), pendingIntent);
        }

        private long GetNotifyTime(DateTime notifyTime)
        {
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
            var epochDifference = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
            return utcTime.AddSeconds(-epochDifference).Ticks / 10000;
        }
    }
}
