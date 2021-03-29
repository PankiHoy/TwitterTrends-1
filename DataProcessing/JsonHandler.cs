using Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DataProcessing
{
    public class JsonHandler
    {
        // private static JsonHandler _jsonHandler;

        public JsonHandler()
        {
            using (StreamReader streamReader = new StreamReader(@"..\DataProcessing\DataToProcess\states.json"))
            {
                string json = streamReader.ReadToEnd();
                States.CreateStatesInstance(DeserializeStates(json));
            }
        }

        //public JsonHandler()
        //{

        //}

        //public static JsonHandler GetJsonInstance()
        //{
        //    if (_jsonHandler == null)
        //        _jsonHandler = new JsonHandler();

        //    return _jsonHandler;
        //}

        private List<State> DeserializeStates(string jsonString)
        {
            List<State> states = new List<State>();
            //variable to join postal code with state's shape
            int stateNumber = -1;
            //enable point as separator in double
            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

            //remove '{' and '}' symbols in jsonString
            StringBuilder str = new StringBuilder(jsonString);
            str.Remove(0, 1);
            str.Remove(str.Length - 1, 1);
            jsonString = str.ToString();

            //regex for finding postal code
            //which is two letters inside quotes
            string postalCodePattern = "\"\\w\\w\"";

            //regex for spliting input string on blocks
            //that represent state's shape as collection of polygons, that are collection of coordinates,
            //which are two double values recursively
            string shapeSeparatorPattern = "\"\\w\\w\":\\s";

            //collection of postal codes
            MatchCollection postalCodes = Regex.Matches(jsonString, postalCodePattern);
            //array of unparsed state shapes
            string[] shapes = Regex.Split(jsonString, shapeSeparatorPattern);

            //process of parsing 
            foreach (string shapeString in shapes)
            {
                if (shapeString != String.Empty)
                {
                    stateNumber++;

                    Coordinate coordinate;
                    coordinate.Latitude = 0; coordinate.Longitude = 0;

                    List<Coordinate> coordinates = null;
                    Polygon polygon = null;
                    List<Polygon> shape = null;
                    State state = null;

                    bool isShapeOpened = false;
                    bool isPolygonOpened = false;
                    bool isCoordinateOpened = false;

                    StringBuilder bufferNumber = new StringBuilder();

                    //calculate number of parentheses for each of the state's shapes
                    int maxParentheses = 0;
                    while (shapeString[maxParentheses] == '[')
                    {
                        maxParentheses++;
                    }


                    for (int i = 0; i < shapeString.Length; ++i)
                    {
                        if (shapeString[i] == '[')
                        {
                            //situation when we read first element of the shape string
                            if (!isShapeOpened)
                            {
                                isShapeOpened = true;
                                //create shape instance for new state
                                shape = new List<Polygon>();
                            }
                            else
                            {
                                //situation when we read second and sometimes third parenthes
                                //or situation when shape is still opened and
                                //still some polygons are there
                                if (!isPolygonOpened)
                                {
                                    //if there four or more parentheses in current shape string
                                    //we just skip them until the parentheses which are related
                                    //to the cooordinates
                                    if (maxParentheses > 3)
                                    {
                                        i = i + maxParentheses - 3;
                                        isPolygonOpened = true;
                                        coordinates = new List<Coordinate>();
                                    }
                                    else
                                    {
                                        isPolygonOpened = true;
                                        coordinates = new List<Coordinate>();
                                    }
                                }
                                else
                                {
                                    //there we each time when just open a new polygon
                                    //or close previous coordinate's parenthes
                                    if (!isCoordinateOpened)
                                    {
                                        isCoordinateOpened = true;
                                        coordinate = new Coordinate();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (shapeString[i] == ']')
                            {
                                //if it is first ']' in a row it means we just read coordinate
                                //then will be next coordinate or
                                //we should close polygon in case when next symbol is ']' too
                                if (isCoordinateOpened)
                                {
                                    isCoordinateOpened = false;
                                    //it means that we have read latitude and we have longtitude in the buffer
                                    coordinate.Latitude = Double.Parse(bufferNumber.ToString(), formatter);
                                    //clear buffer and add coordinate into collection
                                    bufferNumber.Clear();
                                    coordinates.Add(coordinate);
                                }
                                else
                                {
                                    //when we closed coordinate block but there is still one more ']'
                                    //we go here and close polygon
                                    //next will be opened a new polygon or closed state's shape
                                    //if there was more than 3 parenthes in the start of this polygon
                                    //then we should take into consideration this fact and add needed value to counter
                                    if (isPolygonOpened)
                                    {
                                        if (maxParentheses > 3)
                                        {
                                            i = i + maxParentheses - 3;
                                        }
                                        isPolygonOpened = false;
                                        //still we closed polygon section we can crate polygon instance
                                        //based on collection of coordinates
                                        //and add the polygon into state's shape
                                        polygon = new Polygon(coordinates);
                                        shape.Add(polygon);
                                    }
                                    else
                                    {
                                        //in case when shape is closed
                                        //we should create State instance based on polygon collection (shape)
                                        //and postal code from according collection
                                        if (isShapeOpened)
                                        {
                                            isShapeOpened = false;
                                            var postalCode = new StringBuilder(postalCodes[stateNumber].Value);
                                            postalCode.Remove(0, 1);
                                            postalCode.Remove(postalCode.Length - 1, 1);
                                            state = new State(postalCode.ToString(), shape);
                                            //and add the state into states collection
                                            states.Add(state);
                                        }
                                    }
                                }
                            }
                            //in case when we have digit or '-' or decimal separator '.' we read into buffer
                            else if (shapeString[i] == '-' ||
                            shapeString[i] == '0' ||
                            shapeString[i] == '1' ||
                            shapeString[i] == '2' ||
                            shapeString[i] == '3' ||
                            shapeString[i] == '4' ||
                            shapeString[i] == '5' ||
                            shapeString[i] == '6' ||
                            shapeString[i] == '7' ||
                            shapeString[i] == '8' ||
                            shapeString[i] == '9' ||
                            shapeString[i] == '.')
                            {
                                bufferNumber.Append(shapeString[i]);
                            }
                            //this case means we have read latitude of coordinate into buffer
                            else if (shapeString[i] == ',' && isCoordinateOpened)
                            {
                                if (bufferNumber.Length > 0)
                                {
                                    //so we write into needed field and clear buffer
                                    coordinate.Longitude = Convert.ToDouble(bufferNumber.ToString(), formatter);
                                    bufferNumber.Clear();
                                }
                            }
                        }
                    }
                }
            }

            //var statesByName = states.OrderBy(s => s.PostalCode);
            //int id_counter = 1;
            //var id = new StringBuilder();
            //foreach (State s in statesByName)
            //{
            //    if (id_counter < 10)
            //        id.Append("0");

            //    id.Append(id_counter);

            //    s.PostalCode = id.ToString();

            //    id.Clear();
            //    id_counter++;
            //}

            return states;
        }
    }
}
