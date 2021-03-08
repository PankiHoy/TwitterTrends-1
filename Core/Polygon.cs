using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Polygon
    {
        private List<Coordinate> _vertices;
        public List<Coordinate> Vertices { get => _vertices; }

        public Polygon(List<Coordinate> coordinates)
        {
            _vertices = coordinates;
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
