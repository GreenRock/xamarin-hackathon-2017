using System.Collections.Generic;
using System.Linq;
using System.Net;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Locations;
using Android.OS;
using Android.Widget;
using Com.Pikkart.AR.Geo;
using Microsoft.Practices.Unity;
using PokemonDI.Droid.Renderers;
using PokemonDI.Services;
using Prism.Unity;
using View = Android.Views.View;

namespace PokemonDI.Droid
{
    [Activity(Icon = "@drawable/icon", ConfigurationChanges = Android.Content.PM.ConfigChanges.ScreenSize | Android.Content.PM.ConfigChanges.Orientation, Theme = "@style/MainTheme")]
    public class MyGeoActivity : GeoActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Init();
        }

        public override void OnBackPressed()
        {
            SetResult(Result.Ok);
            Finish();
        }

        private void Init()
        {
            MyMarkerViewAdapter arMyMarkerViewAdapter = new MyMarkerViewAdapter(this, 250, 250);

            MyMarkerViewAdapter mapMyMarkerViewAdapter = new MyMarkerViewAdapter(this, 50, 50);

            InitGeoFragment(arMyMarkerViewAdapter, mapMyMarkerViewAdapter);

            List<GeoElement> geoElementList = new List<GeoElement>();

            foreach (var p in RecognitionPageRenderer.Objects)
            {
                geoElementList.Add(new GeoElement(new Location(p.Data.Number) { Latitude = p.Latitude, Longitude = p.Longitude }, p.Data.Id.ToString(), p.Data.Name));
            }
            SetGeoElements(geoElementList);

        }
        public override void OnGeoElementClicked(GeoElement p0)
        {
            base.OnGeoElementClicked(p0);
        }

        public override void OnMapOrCameraClicked()
        {
            base.OnMapOrCameraClicked();
        }
        public override void OnGeolocationChanged(Location p0)
        {
            base.OnGeolocationChanged(p0);

        }
    }

    public class MyMarkerViewAdapter : MarkerViewAdapter
    {
        public MyMarkerViewAdapter(Context context, int width, int height) : base(context, width, height)
        {
            IsDefaultMarker = false;
        }

        public override void SetMarkerView(View p0)
        {
            base.SetMarkerView(p0);
        }

        public override View GetView(GeoElement p0)
        {
            ImageView imageView = (ImageView)MarkerView.FindViewById(Resource.Id.image);
            var o = RecognitionPageRenderer.Objects.FirstOrDefault(x => x.Data.Id.ToString() == p0.Id);
            var bitmap = BitmapFactory.DecodeByteArray(o.ImageBytes, 0, o.ImageBytes.Length);
            imageView.SetImageBitmap(bitmap);
            imageView.Invalidate();
            return MarkerView;
        }
    }
}