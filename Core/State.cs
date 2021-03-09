using System.Collections.Generic;

namespace Core
{
    public class State
    {
        private string _postalCode;
        private List<Polygon> _shape;

        public string PostalCode { get => _postalCode; }
        public List<Polygon> Shape { get => _shape; }

        public State(string postalCode, List<Polygon> shape)
        {
            _postalCode = postalCode;
            _shape = shape;
        }

        public bool IsInnerPoint(Coordinate point)
        {
            foreach (Polygon polygon in _shape)
            {
                if (polygon.IsInnerPoint(point))
                    return true;
            }

            return false;
        }
    }
}
