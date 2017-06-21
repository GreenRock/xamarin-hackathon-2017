using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        // Using a DependencyProperty as the backing store for CustomPins.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty CustomPinsProperty =
            BindableProperty.Create("CustomPins", typeof(List<CustomPin>), typeof(CustomMap), null, BindingMode.OneWay);



        public Position Center
        {
            get { return (Position)GetValue(CenterProperty); }
            set { SetValue(CenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Center.  This enables animation, styling, binding, etc...
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
