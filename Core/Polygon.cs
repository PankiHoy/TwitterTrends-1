using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Polygon
    {
        private List<Coordinate> vertices;
        public List<Coordinate> Vertices { get => vertices; }

        public Polygon(List<Coordinate> coordinates)
        {
            vertices = coordinates;
        }
    }

    public struct Coordinate
    {
        public double Latitude;
        public double Longitude;
        public Coordinate(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;

        }
    }
}
