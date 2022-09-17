using Core.Entities;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utilities
{
    public static class IncidentUtilityClass
    {
        public static double CalculateDistance(this Point obj, Point newPoint, char unit)
        {
            if ((obj.Y == newPoint.Y) && (obj.X == newPoint.X))
            {
                return 0;
            }
            else
            {
                double theta = obj.X - newPoint.X;
                double dist = Math.Sin(Deg2Rad(obj.Y)) * Math.Sin(Deg2Rad(newPoint.Y)) + Math.Cos(Deg2Rad(obj.Y)) * Math.Cos(Deg2Rad(newPoint.Y)) * Math.Cos(Deg2Rad(theta));
                dist = Math.Acos(dist);
                dist = Rad2Deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == 'K')
                {
                    dist = dist * 1.609344;
                }
                else if (unit == 'N')
                {
                    dist = dist * 0.8684;
                }
                return (dist);
            }
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static double Deg2Rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static double Rad2Deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }


        public static Point CreatePoint(double longitude, double latitude)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            // see https://docs.microsoft.com/en-us/ef/core/modeling/spatial
            // Longitude and Latitude
            var newLocation = geometryFactory.CreatePoint(new Coordinate(longitude, latitude));

            return newLocation;
        }

        public static DateTime FromEpochToDateTime(long epoch)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddMilliseconds(epoch * 1000).ToLocalTime();
        }

        public static double CalculateGrade(Point masterPoint, List<IncidentDetail> details)
        {
            int countCat1 = 0, countCat2 = 0, countCat3 = 0;

            foreach (var incidentDetail in details)
            {
                double distance = masterPoint.CalculateDistance(incidentDetail.Coords, 'K');

                if(distance < 0.5)
                {
                    countCat1++;
                }
                else if(distance >= 0.5 && distance < 1)
                {
                    countCat2++;
                }
                else if(distance < 2)
                {
                    countCat3++;
                }
            }
            return countCat1 * 1 + countCat2 * 0.75 + countCat3 * 0.5;
        }
    }
}
