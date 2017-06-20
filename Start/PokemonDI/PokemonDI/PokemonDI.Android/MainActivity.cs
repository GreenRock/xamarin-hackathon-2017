using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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

        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IUnityContainer container)
            {

            }
        }
    }
}

