using System;
using static System.Math;

namespace Moments.Helper
{
    public static class DistanceUtils
    {
        /// <summary>
        ///     Calculates the distance in miles
        /// </summary>
        /// <returns>The distance.</returns>
        /// <param name="latitudeStart">Latitude start.</param>
        /// <param name="longitudeStart">Longitude start.</param>
        /// <param name="latitudeEnd">Latitude end.</param>
        /// <param name="longitudeEnd">Longitude end.</param>
        public static double CalculateDistance(double latitudeStart, double longitudeStart,
            double latitudeEnd, double longitudeEnd)
        {
            if (latitudeEnd == latitudeStart && longitudeEnd == longitudeStart)
                return 0;

            var rlat1 = PI * latitudeStart / 180.0;
            var rlat2 = PI * latitudeEnd / 180.0;
            var theta = longitudeStart - longitudeEnd;
            var rtheta = PI * theta / 180.0;
            var dist = Sin(rlat1) * Sin(rlat2) + Cos(rlat1) * Cos(rlat2) * Cos(rtheta);
            dist = Acos(dist);
            dist = dist * 180.0 / PI;
            var final = dist * 60.0 * 1.1515;
            if (double.IsNaN(final) || double.IsInfinity(final) || double.IsNegativeInfinity(final) ||
                double.IsPositiveInfinity(final) || final < 0)
                return 0;

            return final;
        }

        public static double MilesToKilometers(double miles) => miles * 1.609344;


        public enum DistanceUnit { Miles, Kilometers };
        public static double ToRadian(this double value)
        {
            return (Math.PI / 180) * value;
        }
        public static double HaversineDistance(LatLng coord1, LatLng coord2, DistanceUnit unit)
        {
            double R = (unit == DistanceUnit.Miles) ? 3960 : 6371;
            var lat = (coord2.Latitude - coord1.Latitude).ToRadian();
            var lng = (coord2.Longitude - coord1.Longitude).ToRadian();

            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                     Math.Cos(coord1.Latitude.ToRadian()) * Math.Cos(coord2.Latitude.ToRadian()) *
                     Math.Sin(lng / 2) * Math.Sin(lng / 2);

            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));

            return R * h2;
        }
    }
}
