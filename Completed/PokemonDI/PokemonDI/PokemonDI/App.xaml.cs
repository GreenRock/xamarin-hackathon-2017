using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonDI.Helpers;
using PokemonDI.Services;
using PokemonDI.Views;
using Prism.Unity;
using Xamarin.Forms;
using Microsoft.Practices.Unity;

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

            NavigationService.NavigateAsync("Navigation/Main");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterType(typeof(IDataService), typeof(DataService), new ContainerControlledLifetimeManager());

            Container.RegisterTypeForNavigation<NavigationPage>("Navigation");
            Container.RegisterTypeForNavigation<MainPage>("Main");
            Container.RegisterTypeForNavigation<MapPage>("Map");
            Container.RegisterTypeForNavigation<DetailPage>("Detail");
            Container.RegisterTypeForNavigation<ARPage>("AR");

        }
    }
}
