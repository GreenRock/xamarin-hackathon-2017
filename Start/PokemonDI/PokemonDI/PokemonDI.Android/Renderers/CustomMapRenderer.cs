using System.ComponentModel;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using FFImageLoading;
using PokemonDI.Controls;
using PokemonDI.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]

namespace PokemonDI.Droid.Renderers
{
    public class CustomMapRenderer : MapRenderer
    {
        private GoogleMap GoogleMap => _callBack?.GoogleMap;

        private CustomCallBack _callBack;

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null) return;

            Control.GetMapAsync(_callBack = new CustomCallBack());
        }
        protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(nameof(CustomMap.CustomPins)) && GoogleMap != null)
            {
                GoogleMap.Clear();
                var formsMap = (CustomMap)Element;
                var customPins = formsMap.CustomPins;
                if (customPins == null)
                    return;

                foreach (var pin in customPins)
                {
                    var marker = new MarkerOptions();
                    marker.SetPosition(new LatLng(pin.Latitude, pin.Longitude));
                    marker.SetTitle(pin.Label);
                    marker.SetSnippet(pin.Address);

                    var bitm = await ImageService.Instance.LoadUrl(pin.Url).AsBitmapDrawableAsync();
                    marker.SetIcon(BitmapDescriptorFactory.FromBitmap(bitm.Bitmap));

                    GoogleMap.AddMarker(marker);
                }
            }
        }

        public class CustomCallBack : Java.Lang.Object, IOnMapReadyCallback
        {
            public GoogleMap GoogleMap { get; set; }

            public void OnMapReady(GoogleMap googleMap)
            {
                GoogleMap = googleMap;
            }
        }
    }
}