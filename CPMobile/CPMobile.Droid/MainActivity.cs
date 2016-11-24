using Android.App;
using Android.Content.PM;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace CPMobile.Droid
{
    [Activity(Label = "FindMe", Icon = "@drawable/face", //MainLauncher = false,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {

        //GPSServiceBinder _binder;
        //GPSServiceConnection _gpsServiceConnection;
        //Intent _gpsServiceIntent;
        //private GPSServiceReciever _receiver;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            
            global::Xamarin.Forms.Forms.Init(this, bundle);

            
            ImageCircleRenderer.Init();
            LoadApplication(new App());
        }
        //private void RegisterService()
        //{
        //    _gpsServiceConnection = new GPSServiceConnection(_binder);
        //    _gpsServiceIntent = new Intent(Android.App.Application.Context, typeof(GPSService));
        //    BindService(_gpsServiceIntent, _gpsServiceConnection, Bind.AutoCreate);
        //}
        //private void RegisterBroadcastReceiver()
        //{
        //    IntentFilter filter = new IntentFilter(GPSServiceReciever.LOCATION_UPDATED);
        //    filter.AddCategory(Intent.CategoryDefault);
        //    _receiver = new GPSServiceReciever();
        //    RegisterReceiver(_receiver, filter);
        //}

        //private void UnRegisterBroadcastReceiver()
        //{
        //    UnregisterReceiver(_receiver);
        //}
        //public void UpdateUI(Intent intent)
        //{
        //    //_locationText.Text = intent.GetStringExtra("Location");
        //   // _addressText.Text = intent.GetStringExtra("Address");
        //   // _remarksText.Text = intent.GetStringExtra("Remarks");
        //}

        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    RegisterBroadcastReceiver();
        //}

        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    UnRegisterBroadcastReceiver();
        //}

        //[BroadcastReceiver]
        //internal class GPSServiceReciever : BroadcastReceiver
        //{
        //    public static readonly string LOCATION_UPDATED = "LOCATION_UPDATED";
        //    public override void OnReceive(Context context, Intent intent)
        //    {
        //        if (intent.Action.Equals(LOCATION_UPDATED))
        //        {
        //            MainActivity.Instance.UpdateUI(intent);
        //        }

        //    }
        //}

        //public override void OnBackPressed()
        //{
        //    if (App.DoBack)
        //    {
        //        base.OnBackPressed();
        //    }
        //}
    }
}

