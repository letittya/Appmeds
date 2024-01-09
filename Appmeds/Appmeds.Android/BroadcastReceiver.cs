using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appmeds.Droid
{
    [BroadcastReceiver(Enabled = true, Label = "Medication Reminder")]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var message = intent.GetStringExtra("message");
            var title = intent.GetStringExtra("title");

            var notificationIntent = new Intent(context, typeof(MainActivity));
            var pendingIntent = PendingIntent.GetActivity(context, 0, notificationIntent, PendingIntentFlags.Immutable);

            var notificationBuilder = new NotificationCompat.Builder(context, MainActivity.CHANNEL_ID)
                                          .SetSmallIcon(Resource.Drawable.plus_green)
                                          .SetContentTitle(title)
                                          .SetContentText(message)
                                          .SetContentIntent(pendingIntent)
                                          .SetAutoCancel(true);

            var notificationManager = NotificationManagerCompat.From(context);
            notificationManager.Notify(1000, notificationBuilder.Build());
        }
    }

}