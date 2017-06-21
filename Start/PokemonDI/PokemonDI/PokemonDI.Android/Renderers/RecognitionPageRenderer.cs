using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.Content;
using Microsoft.Practices.Unity;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using PokedexNET;
using PokemonDI.Controls;
using PokemonDI.Droid.Renderers;
using PokemonDI.Models;
using PokemonDI.Services;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RecognitionPage), typeof(RecognitionPageRenderer))]

namespace PokemonDI.Droid.Renderers
{
    public class RecognitionPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            Load();
        }

        public static Position Position { get; set; }
        public static List<GeoObject<Pokemon>> Objects { get; set; }
        private bool _load = false;

        public async void Load()
        {
            if (_load)
                return;

            _load = true;

            Position = await CrossGeolocator.Current.GetPositionAsync();
            Objects = new List<GeoObject<Pokemon>>();

            var prismApplication = Xamarin.Forms.Application.Current as PrismApplication;
            if (prismApplication != null)
            {
                var dataService = prismApplication.Container.Resolve<IDataService>();
                var web = new WebClient();
                foreach (var geoObject in dataService.GetNearbyPokemon(Position.Latitude, Position.Longitude, 1, 0.001))
                {
                    var img = $"https://assets.pokemon.com/assets/cms2/img/pokedex/full/{geoObject.Data.Id:D3}.png";
                    var bytes = await web.DownloadDataTaskAsync(img);
                    geoObject.ImageBytes = bytes;
                    Objects.Add(geoObject);
                }

                var mainActivity = Context as Activity;
                if (mainActivity != null)
                {
                    var intent = new Intent(Context, typeof(MyGeoActivity));
                    mainActivity.StartActivityForResult(intent, 123);
                }
            }
        }
    }
}