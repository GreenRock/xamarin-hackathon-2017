using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading;
using FFImageLoading.Forms.Touch;
using Foundation;
using Microsoft.Practices.Unity;
using PokemonDI.Helpers;
using Prism;
using Prism.Unity;
using UIKit;

namespace PokemonDI.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
       
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            Xamarin.FormsMaps.Init();

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

            LoadApplication(new App(new iOSInitializer()));
            

            return base.FinishedLaunching(app, options);
        }

        public class iOSInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IUnityContainer container)
            {
            }
        }
    }
}
