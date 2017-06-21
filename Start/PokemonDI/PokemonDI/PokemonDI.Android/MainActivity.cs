using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using FFImageLoading;
using FFImageLoading.Forms.Droid;
using Microsoft.Practices.Unity;
using PokemonDI.Helpers;
using Prism.Unity;

namespace PokemonDI.Droid
{
    [Activity(Label = "PokemonDI", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            CachedImageRenderer.Init();

            var config = new FFImageLoading.Config.Configuration()
            {
                VerboseLogging = false,
                VerbosePerformanceLogging = false,
                VerboseMemoryCacheLogging = false,
                VerboseLoadingCancelledLogging = false,
                Logger = new CustomLogger(),
            };

            ImageService.Instance.Initialize(config);

            LoadApplication(new App(new AndroidInitializer()));
        }
        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 123)
            {
                var application = Xamarin.Forms.Application.Current as PrismApplication;
                if (application != null)
                    await application
                        .MainPage.Navigation.PopAsync();
            }
        }
        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IUnityContainer container)
            {

            }
        }
    }
}

