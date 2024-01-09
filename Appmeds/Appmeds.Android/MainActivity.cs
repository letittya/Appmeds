using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Media;
using Android;

namespace Appmeds.Droid
{
    [Activity(Label = "Appmeds", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public const string CHANNEL_ID = "appmeds_notifications_channel";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CreateNotificationChannel();
            LoadApplication(new App());
        }
        protected override void OnStart()
        {
            base.OnStart();
            const int requestLocationId = 0;

            string[] notiPermission =
            {
            Manifest.Permission.PostNotifications
        };

            if ((int)Build.VERSION.SdkInt < 33) return;

            if (this.CheckSelfPermission(Manifest.Permission.PostNotifications) != Permission.Granted)
            {
                this.RequestPermissions(notiPermission, requestLocationId);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelName = GetString(Resource.String.channel_name);
                var channelDescription = GetString(Resource.String.channel_description);
                var channel = new NotificationChannel(CHANNEL_ID, channelName, NotificationImportance.High)
                {
                    Description = channelDescription
                };

                var notificationManager = (NotificationManager)GetSystemService(NotificationService);
                notificationManager.CreateNotificationChannel(channel);
            }
        }


    }
}