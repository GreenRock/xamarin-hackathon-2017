using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonDI.Helpers
{
    public class GeoHelper
    {
        public struct Position
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
        public static List<Position> CalculateRandomPositions(int numberOfPoint, double startlat, double startlon, double maxdist)
        {
            var result = new List<Position>();
            var mx = 20020.732;
            var radiusEarth = 6372.796924;
            var rad360 = 2 * Math.PI;

            if (startlat == -999 || startlon == -999)
            {
                return new List<Position>();
            }
            if (startlat == 90)
            {
                startlat = 89.99999999;
            }
            if (startlat == -90)
            {
                startlat = -89.99999999;
            }

            var startlatRad = Rad(startlat);
            var startlonRad = Rad(startlon);
            maxdist = maxdist / radiusEarth;

            var cosdif = Math.Cos(maxdist) - 1;
            var sinstartlat = Math.Sin(startlatRad);
            var cosstartlat = Math.Cos(startlatRad);
            var random = new Random();

            for (int i = 0; i < numberOfPoint; i++)
            {
                var v = random.NextDouble() * cosdif + 1;
                var dist = Math.Acos(v);
                var brg = rad360 * random.NextDouble();
                var lat = Math.Asin(sinstartlat * Math.Cos(dist) + cosstartlat * Math.Sin(dist) * Math.Cos(brg));
                var lon = Deg(NormalizeLongitude(startlonRad * 1 + Math.Atan2(Math.Sin(brg) * Math.Sin(dist) * cosstartlat, Math.Cos(dist) - sinstartlat * Math.Sin(lat))));
                lat = Deg(lat);
                dist = Math.Round(dist * radiusEarth * 10000) / 10000;
                brg = Math.Round(Deg(brg) * 1000) / 1000;
                result.Add(new Position() { Latitude = lat, Longitude = lon });
            }
            return result;
        }
        private static double Deg(double rd)
        {
            return (rd * 180 / Math.PI);
        }
        private static double Rad(double dg)
        {
            return (dg * Math.PI / 180);
        }
        private static double NormalizeLongitude(double lon)
        {
            var n = Math.PI;
            if (lon > n)
            {
                lon = lon - 2 * n;
            }
            else if (lon < -n)
            {
                lon = lon + 2 * n;
            }
            return lon;
        }
    }
}
