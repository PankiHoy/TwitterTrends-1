using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class State
    {
        private string postalCode;
        private List<Polygon> shape;

        public string PostalCode { get => postalCode; }
        public List<Polygon> Shape { get => shape; }

        public State(string postalCode, List<Polygon> shape)
        {
            this.postalCode = postalCode;
            this.shape = shape;
        }
    }
}
