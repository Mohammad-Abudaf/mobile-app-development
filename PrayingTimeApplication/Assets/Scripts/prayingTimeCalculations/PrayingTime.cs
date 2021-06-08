using System;
namespace PrayingTime{
    public static class PrayingTable
    {
        private static double DegToRad(double degree)
        {
            return ((Math.PI / 180) * degree);
        }

        private static double RadToDeg(double radian)
        {
            return (radian * (180 / Math.PI));
        }
        private static double MoreOrLess360(double value)
        {
            while (value > 360 || value < 0)
            {
                if (value > 360)
                {
                    value -= 360; 
                }
                else
                {
                    value += 360; 
                }
            }

            return value;
        }
        private static double MoreOrLess24(double value)
        {
            while (value > 24 || value < 0)
            {
                if (value > 24)
                {
                    value -= 24; 
                }
                else
                {
                    value += 0; 
                }
            }
            return value;
        }
private static double[] CalculatePrayerTime(int year, int month, int day, double longitude,
            double latitude, int timeZone, double fajrTwilight, double ishaTwilight){
            var d1 = (367 * year) - ((year +  ((month + 9) / 12)) * 7 / 4) +
                     (((275 * month / 9)) + day - 730531.5);
            var l = 280.461 + 0.9856474 * d1;
            l = MoreOrLess360(l);
            var m = 357.528 + (0.9856003) * d1;
            m = MoreOrLess360(m);
            var lambda = l + 1.915 * Math.Sin(DegToRad(m)) + 0.02 * Math.Sin(DegToRad(2 * m));
            lambda = MoreOrLess360(lambda);
            var obliquity = 23.439 - 0.0000004 * d1;
            var alpha = RadToDeg(Math.Atan((Math.Cos(DegToRad(obliquity)) * Math.Tan(DegToRad(lambda)))));
            alpha = MoreOrLess360(alpha);
            alpha -= (360 * (int) (alpha / 360));
            alpha += 90 * (Math.Floor(lambda / 90) - Math.Floor(alpha / 90));
            var st = 100.46 + 0.985647352 * d1;
            var dec = RadToDeg(Math.Asin(Math.Sin(DegToRad(obliquity)) * Math.Sin(DegToRad(lambda))));
            var durinalArc = RadToDeg(Math.Acos(
                (Math.Sin(DegToRad(-0.8333)) - Math.Sin(DegToRad(dec)) * Math.Sin(DegToRad(latitude))) / (
                    Math.Cos(DegToRad(dec)) * Math.Cos(DegToRad(latitude)))));
            var noon = alpha - st;
            noon = MoreOrLess360(noon);
            var utNoon = noon - longitude;

            // 2) ZuhrTime[Localnoon]
            var zuhrTime1 = utNoon / 15 + timeZone;
            var asrAlt = RadToDeg(Math.Atan(2 + Math.Tan(DegToRad(latitude - dec))));
            var asrArc = RadToDeg(Math.Acos(
                (Math.Sin(DegToRad(90 - asrAlt)) - Math.Sin(DegToRad(dec)) * Math.Sin(DegToRad(latitude))) / (
                    Math.Cos(DegToRad(dec)) * Math.Cos(DegToRad(latitude)))));

            asrArc /= 15;
            // 3) Asr Time
            var asrTime1 = zuhrTime1 + asrArc - 1;
            // 1) Shorouq Time
            var sunRiseTime1 = zuhrTime1 - (durinalArc / (15));
            // 4) MaghribTime
            var maghribTime1 = zuhrTime1 + (durinalArc / (15));
            var ishaArc = RadToDeg(Math.Acos(
                (Math.Sin(DegToRad(ishaTwilight)) - Math.Sin(DegToRad(dec)) * Math.Sin(DegToRad(latitude))) / (
                    Math.Cos(DegToRad(dec)) * Math.Cos(DegToRad(latitude)))));
            // 5) IshaTime
            var ishaTime1 = zuhrTime1 + (ishaArc / 15);
            // 0) FajrTime
            var fajrArc = RadToDeg(Math.Acos(
                (Math.Sin(DegToRad(fajrTwilight)) - Math.Sin(DegToRad(dec)) * Math.Sin(DegToRad(latitude))) / (
                    Math.Cos(DegToRad(dec)) * Math.Cos(DegToRad(latitude)))));
            var fajrTime1 = zuhrTime1 - (fajrArc / 15);

            // ReSharper disable once HeapView.ObjectAllocation.Evident
            var prayingTime = new double[6];
            prayingTime[0] = (fajrTime1);
            prayingTime[1] = (sunRiseTime1);
            prayingTime[2] = (zuhrTime1);
            prayingTime[3] = (asrTime1);
            prayingTime[4] = (maghribTime1);
            prayingTime[5] = (ishaTime1);

            return prayingTime;
        }
        private static double[] DoubleToHrMin(double value){
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            var list = new double[2];
            var hour = Math.Floor((double) MoreOrLess24(value));
            var min = Math.Floor((double) MoreOrLess24(value - hour) * 60);
            list[0] =  hour;
            list[1] =  min;
            return list;
        }

        private static int year = System.DateTime.Now.Year;
        private static int month = System.DateTime.Now.Month;
        private static int day = System.DateTime.Now.Day; 
        public static double longitude=34.3088, latitude=31.3547, fajrTwilight=-18, ishaTwilight=-17.1;
        public static int timeZone = 3; 
        
        private static double[] getPrayingTime =
            CalculatePrayerTime(year, month, day, longitude, latitude, timeZone, fajrTwilight, ishaTwilight);
        
        public static double[] fajetTime = DoubleToHrMin(getPrayingTime[0]);
        public static double[] dohaTime = DoubleToHrMin(getPrayingTime[1]);
        public static double[] dohorTime = DoubleToHrMin(getPrayingTime[2]);
        public static double[] aserTime = DoubleToHrMin(getPrayingTime[3]);
        public static double[] magrebTime = DoubleToHrMin(getPrayingTime[4]);
        public static double[] ishaTime = DoubleToHrMin(getPrayingTime[5]);
    }
}