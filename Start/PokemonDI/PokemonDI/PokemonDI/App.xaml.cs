using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonDI.Helpers;
using PokemonDI.Views;
using Prism.Unity;
using Xamarin.Forms;

namespace PokemonDI
{
    public partial class App
    {
        public App() : base(null)
        {

        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
            
        }

        protected override void OnInitialized()
        {
           InitializeComponent();
        }

        protected override void RegisterTypes()
        {           
            Container.RegisterTypeForNavigation<NavigationPage>("Navigation");
            Container.RegisterTypeForNavigation<MainPage>("Main");
            Container.RegisterTypeForNavigation<MapPage>("Map");
            Container.RegisterTypeForNavigation<DetailPage>("Detail");            
        }
    }
}
