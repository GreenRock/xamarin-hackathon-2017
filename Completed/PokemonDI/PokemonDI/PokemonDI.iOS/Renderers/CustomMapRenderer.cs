using System;
using System.ComponentModel;
using CoreGraphics;
using CoreLocation;
using Foundation;
using MapKit;
using PokemonDI.Controls;
using PokemonDI.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]

namespace PokemonDI.iOS.Renderers
{
    public class CustomMapRenderer : MapRenderer
    {


        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                if (nativeMap != null)
                    nativeMap.GetViewForAnnotation = null;
            }

            if (e.NewElement != null)
            {

                var nativeMap = Control as MKMapView;
                if (nativeMap != null)
                    nativeMap.GetViewForAnnotation = GetViewForAnnotation;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if ((e.PropertyName.Equals(nameof(CustomMap.CustomPins))))
            {
                var formsMap = (CustomMap)Element;
                var nativeMap = Control as MKMapView;
                if (nativeMap == null)
                    return;

                nativeMap.RemoveAnnotations(nativeMap.Annotations);

                foreach (var pin in formsMap.CustomPins)
                {
                    nativeMap.AddAnnotation(new MKPointAnnotation()
                    {
                        Coordinate = new CLLocationCoordinate2D(pin.Latitude, pin.Longitude)
                    });
                }
            }
        }

        private MKAnnotationView GetViewForAnnotation(MKMapView mapview, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;

            if (annotation is MKUserLocation)
                return null;

            var customPin = GetCustomPin(annotation.Coordinate);
            if (customPin == null)
            {
                throw new Exception("Custom pin not found");
            }

            annotationView = mapview.DequeueReusableAnnotation(customPin.Id);
            if (annotationView == null)
            {
                var imag = FromUrl(customPin.Url);
                imag = ResizeUIImage(imag, 100, 100);
                annotationView = new MKAnnotationView(annotation, customPin.Id)
                {
                    Image = imag,
                    CalloutOffset = new CGPoint(0, 0),
                    CanShowCallout = true
                };
            }

            return annotationView;

        }

        public UIImage ResizeUIImage(UIImage sourceImage, float width, float height)
        {

            UIGraphics.BeginImageContext(new CGSize(width, height));
            sourceImage.Draw(new CGRect(0, 0, width, height));
            var resultImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return resultImage;
        }
        static UIImage FromUrl(string uri)
        {
            using (var url = new NSUrl(uri))
            using (var data = NSData.FromUrl(url))
                return UIImage.LoadFromData(data);
        }

        CustomPin GetCustomPin(CLLocationCoordinate2D position)
        {
            var formsMap = (CustomMap)Element;
            var customPins = formsMap.CustomPins;
            foreach (var pin in customPins)
            {
                if (pin.Latitude == position.Latitude && pin.Longitude == position.Longitude)
                {
                    return pin;
                }
            }
            return null;
        }

    }
}
