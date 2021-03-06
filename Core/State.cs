using System.Collections.Generic;

namespace Core
{
    public class State
    {
        private string _postalCode;
        private List<Polygon> _shape;
       
        public double Sentiment { get; set; } 
        public List<Tweet> Tweets { get; set; }

        public string PostalCode { 
            get => _postalCode;
            set => _postalCode = value;  
        }
        public List<Polygon> Shape { get => _shape; }

        public State(string postalCode, List<Polygon> shape)
        {
            _postalCode = postalCode;
            _shape = shape;
            Tweets = new List<Tweet>();
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
