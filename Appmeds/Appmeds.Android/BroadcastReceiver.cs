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
    [BroadcastReceiver(Enabled = true, Label = "Medication Alarm Receiver")]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            string title = intent.GetStringExtra("title");
            string message = intent.GetStringExtra("message");

            // Build and show the notification
            var notificationManager = NotificationManagerCompat.From(context);
            var builder = new NotificationCompat.Builder(context, "medication_channel")
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.ic_launcher); // Set your small icon here

            notificationManager.Notify(0, builder.Build());
        }
    }

}