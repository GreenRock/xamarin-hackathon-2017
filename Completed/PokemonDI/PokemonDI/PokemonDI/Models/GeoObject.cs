using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonDI.Models
{
    public class GeoObject<T> where T : class
    {
        public T Data { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public byte[] ImageBytes { get; set; }
        public GeoObject(T value, double lat, double lng)
        {
            Data = value;
            Latitude = lat;
            Longitude = lng;
        }
    }
}
