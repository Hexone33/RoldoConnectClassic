using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android;
using Google.Android.Material.Snackbar;
using static Android.Resource;
using Android.Text;
using System.Threading.Tasks;
using AndroidX.Core.Content;
using Xamarin.Essentials;
using AndroidX.Core.App;
using FormsPinView.Droid;
using Android.Views;

namespace RoldoConnectClassic.Droid
{
    [Activity(Label = "Roldo Connect", Icon = "@mipmap/roldoicon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.AddFlags(WindowManagerFlags.TranslucentNavigation);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            PinItemViewRenderer.Init();
            LoadApplication(new App());
          

            await Permissions.RequestAsync<BLEPermission>();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}