using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace PokemonDI.Controls
{
    public class CustomMap : Map
    {
        public List<CustomPin> CustomPins
        {
            get => (List<CustomPin>)GetValue(CustomPinsProperty);
            set => SetValue(CustomPinsProperty, value);
        }

        public static readonly BindableProperty CustomPinsProperty =
            BindableProperty.Create("CustomPins", typeof(List<CustomPin>), typeof(CustomMap), null, BindingMode.OneWay);

        public Position Center
        {
            get => (Position)GetValue(CenterProperty);
            set => SetValue(CenterProperty, value);
        }

        public static readonly BindableProperty CenterProperty =
            BindableProperty.Create("Center", typeof(Position), typeof(CustomMap), new Position(0,0), BindingMode.OneWayToSource);

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(VisibleRegion) && VisibleRegion != null)
            {
                Center = VisibleRegion.Center;
            }
        }
    }

    public class CustomPin
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public string Label { get; set; }
        public string Address { get; set; }
    }
}
