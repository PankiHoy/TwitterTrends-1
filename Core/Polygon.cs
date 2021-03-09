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

        public bool IsInnerPoint(Coordinate point)
        {
            double k, b;
            int number_crossings = 0;
            //ЕСЛИ ТОЧКА ЛЕЖИТ НА СТОРОНЕ МНОГОУГОЛЬНИКА, СЧИТАЕМ ЕЕ ВНУТРЕННЕЙ ТОЧКОЙ
            for (int i = 0; i < _vertices.Count; i++)
            {
                int k1, k2;
                //НАХОДИМ k И b ДЛЯ ПРЯМЫХ, СОДЕРЖАЩИХ СТОРОНЫ МНОГОУГОЛЬНИКА
                //y = k * x + b
                //k = (y1 - y2) / (x1 - x2)
                //b = y - k * x

                //ЧТОБЫ НЕ ВЫЙТИ ЗА ГРАНИЦЫ МАССИВА
                if (i == _vertices.Count - 1)
                {
                    k1 = i;
                    k2 = 0;
                }
                else
                {
                    k1 = i;
                    k2 = i + 1;
                }

                k = 1.0 * (_vertices[k1].Longitude - _vertices[k2].Longitude) / (_vertices[k1].Latitude - _vertices[k2].Latitude);
                b = _vertices[k1].Longitude - 1.0 * k * _vertices[k1].Latitude;
                //ПРОВЕРЯЕМ ПРИНАДЛЕЖИТ ЛИ ТОЧКА СТОРОНЕ (ОТРЕЗКУ), ЕСЛИ ДА, ТО ОНА ВНУТРЕННЯЯ (ВОЗВРАЩАЕМ true)
                if (k != Double.PositiveInfinity && k != Double.NegativeInfinity)
                {
                    if ((point.Longitude == k * point.Latitude + b) &&
                        ((point.Longitude >= _vertices[k1].Longitude && point.Longitude <= _vertices[k2].Longitude) || (point.Longitude <= _vertices[k1].Longitude && point.Longitude >= _vertices[k2].Longitude)) &&
                        ((point.Latitude >= _vertices[k1].Latitude && point.Latitude <= _vertices[k2].Latitude) || (point.Latitude <= _vertices[k1].Latitude && point.Latitude >= _vertices[k2].Latitude)))
                    {
                        return true;
                    }
                }
                else
                {
                    if (point.Latitude == _vertices[k1].Latitude &&
                    ((point.Longitude >= _vertices[k1].Longitude && point.Longitude <= _vertices[k2].Longitude) || (point.Longitude <= _vertices[k1].Longitude && point.Longitude >= _vertices[k2].Longitude)))
                    {
                        return true;
                    }
                }
            }

            //ПРОВЕРЯЕМ НАХОДИТСЯ ЛИ ТОЧКА ВНУТРИ МНОГОУГОЛЬНИКА, Т.Е.
            //ПРОВЕРЯЕМ СКОЛЬКО РАЗ ЛУЧ, ПРОВЕДЕННЫЙ ИЗ ТОЧКИ ВПРАВО И ПАРАЛЛЕЛЬНЫЙ ОСИ Ох, ПЕРЕСЕКАЕТ СТОРОНЫ МНОГОУГОЛЬНИКА
            //ЧЕТНОЕ КОЛИЧЕСТВО - ТОЧКА СНАРУЖИ
            //НЕЧЕТНОЕ КОЛИЧЕСТВО - ТОЧКА ВНУТРИ
            for (int i = 0; i < _vertices.Count; i++)
            {
                int k1, k2;
                //НАХОДИМ k И b ДЛЯ ПРЯМЫХ, СОДЕРЖАЩИХ СТОРОНЫ МНОГОУГОЛЬНИКА
                //y = k * x + b
                //k = (y1 - y2) / (x1 - x2)
                //b = y - k * x

                //ЧТОБЫ НЕ ВЫЙТИ ЗА ГРАНИЦЫ МАССИВА ПРИ ИСПОЛЬЗОВАНИИ [i + 1]-ГО ЭЛЕМЕНТА ДЕЛАЕМ ПРОВЕРКУ
                //ЕСЛИ ЭЛЕМЕНТ ПОСЛЕДНИЙ (СЛУЧАЙ else) ПРОДЕЛЫВАЕМ ТЕ ЖЕ ОПЕРАЦИИ, ТОЛЬКО ВМЕСТО [i + 1]-ГО ЭЛЕМЕНТА БЕРЕМ 0-Й (МНОГОУГОЛЬНИК ТИПО ЗАМКНУТАЯ ЛОМАНАЯ)
                if (i == _vertices.Count - 1)
                {
                    k1 = i;
                    k2 = 0;
                }
                else
                {
                    k1 = i;
                    k2 = i + 1;
                }

                k = 1.0 * (_vertices[k1].Longitude - _vertices[k2].Longitude) / (_vertices[k1].Latitude - _vertices[k2].Latitude);
                b = _vertices[k1].Longitude - 1.0 * k * _vertices[k1].Latitude;
                //ЕСЛИ ТОЧКА ЛЕЖИТ ВЫШЕ ПРЯМОЙ С k > 0 И ЗНАЧЕНИЕ y ТОЧКИ ЛЕЖИТ МЕЖДУ ЗНАЧЕНИЯМИ [y1, y1] ОТРЕЗКА, ТО
                //ЛУЧ, ВЫХОДЯЩИЙ ИЗ ЭТОЙ ТОЧКИ ВПРАВО И ПАРАЛЕЛЛЬНЫЙ Ох, СТОПРОЦ ПЕРЕСЕКАЕТ ЭТОТ ОТРЕЗОК (В ДАННОМ СЛУЧАЕ СТОРОНУ МНОГОУГОЛЬНИКА)
                //КОЛИЧЕСТВО ПЕРЕСЕЧЕНИЙ ДЛЯ ДАННОГО ЛУЧА В ПЕРЕМЕННОЙ number_crossing
                //k != INFINITY ТАК КАК ДЛЯ k = +- БЕСКОНЕЧНОСТИ ПРОВЕРКА НИЖЕ (k = inf <=> ПРЯМАЯ ПАРАЛЛЕЛЬНА Oy)
                if (k >= 0 && k != Double.PositiveInfinity && point.Longitude >= (1.0 * k * point.Latitude + b) && ((point.Longitude > _vertices[k1].Longitude && point.Longitude < _vertices[k2].Longitude) || (point.Longitude < _vertices[k1].Longitude && point.Longitude > _vertices[k2].Longitude)))
                {
                    number_crossings++;
                }
                //ЕСЛИ ТОЧКА ЛЕЖИТ НИЖЕ ПРЯМОЙ С k < 0 И ЗНАЧЕНИЕ y ТОЧКИ ЛЕЖИТ МЕЖДУ ЗНАЧЕНИЯМИ [y1, y1] ОТРЕЗКА, ТО
                //ЛУЧ, ВЫХОДЯЩИЙ ИЗ ЭТОЙ ТОЧКИ ВПРАВО И ПАРАЛЕЛЛЬНЫЙ Ох, СТОПРОЦ ПЕРЕСЕКАЕТ ЭТОТ ОТРЕЗОК (В ДАННОМ СЛУЧАЕ СТОРОНУ МНОГОУГОЛЬНИКА)
                //k != -INFINITY ТАК КАК ДЛЯ k = +- БЕСКОНЕЧНОСТИ ПРОВЕРКА НИЖЕ (k = inf <=> ПРЯМАЯ ПАРАЛЛЕЛЬНА Oy)
                if (k <= 0 && k != Double.NegativeInfinity && point.Longitude <= (1.0 * k * point.Latitude + b) && ((point.Longitude > _vertices[k1].Longitude && point.Longitude < _vertices[k2].Longitude) || (point.Longitude < _vertices[k1].Longitude && point.Longitude > _vertices[k2].Longitude)))
                {
                    number_crossings++;
                }
                //ЕСЛИ ТОЧКА ЛЕЖИТ СПРАВА ОТ ПРЯМОЙ С k = +- БЕСКОНЕЧНОСТИ И ЗНАЧЕНИЕ y ТОЧКИ ЛЕЖИТ МЕЖДУ ЗНАЧЕНИЯМИ [y1, y1] ОТРЕЗКА, ТО
                //ЛУЧ, ВЫХОДЯЩИЙ ИЗ ЭТОЙ ТОЧКИ ВПРАВО И ПАРАЛЕЛЛЬНЫЙ Ох, СТОПРОЦ ПЕРЕСЕКАЕТ ЭТОТ ОТРЕЗОК (В ДАННОМ СЛУЧАЕ СТОРОНУ МНОГОУГОЛЬНИКА)
                if ((k == Double.PositiveInfinity || k == Double.NegativeInfinity) && point.Latitude < _vertices[k1].Latitude && ((point.Longitude > _vertices[k1].Longitude && point.Longitude < _vertices[k2].Longitude) || (point.Longitude < _vertices[k1].Longitude && point.Longitude > _vertices[k2].Longitude)))
                {
                    number_crossings++;
                }
                //ЕСЛИ СТОРОНА ЛЕЖИТ НА ЛУЧЕ 
                if (k == 0 && point.Longitude == _vertices[k1].Longitude)
                {
                    //конец предыдущей и следующей граней грани
                    int i1, i2;

                    if (k1 == 0)
                    {
                        i1 = _vertices.Count - 1;
                    }
                    else
                    {
                        i1 = i - 1;
                    }

                    if (k2 == (_vertices.Count - 1))
                    {
                        i2 = 0;
                    }
                    else
                    {
                        i2 = i + 2;
                    }

                    //ECЛИ \_  ИЛИ  _/ , ТО number_crossings++, ТАК КАК ОДНО ПЕРЕСЕЧЕНИЕ
                    //       \     /
                    if ((_vertices[i1].Longitude < point.Longitude && _vertices[i2].Longitude > point.Longitude) || (_vertices[i1].Longitude > point.Longitude && _vertices[i2].Longitude < point.Longitude))
                    {
                        number_crossings++;
                    }
                    //ЕСЛИ \_/ ИЛИ  _  , ТО number_crossings += 2, ТАК КАК ДВА ПЕРЕСЕЧЕНИЯ
                    //             / \ 
                    if ((_vertices[i1].Longitude > point.Longitude && _vertices[i2].Longitude > point.Longitude) || (_vertices[i1].Longitude < point.Longitude && _vertices[i2].Longitude < point.Longitude))
                    {
                        number_crossings += 2;
                    }
                }
                //ЕСЛИ ЛУЧ ПЕРЕСЕКАЕТ КОНЕЦ ОТРЕЗКА (СТОРОНЫ МНОГОУГОЛЬНИКА)
                if (point.Longitude == _vertices[k1].Longitude && point.Latitude <= _vertices[k1].Longitude)
                {
                    int i1, i2;

                    if (k1 == 0)
                    {
                        i1 = _vertices.Count - 1;
                    }
                    else
                    {
                        i1 = i - 1;
                    }

                    if (k2 == (_vertices.Count - 1))
                    {
                        i2 = 0;
                    }
                    else
                    {
                        i2 = i + 1;
                    }

                    //ЕСЛИ \ ИЛИ  / , ТО КОЛИЧЕСТВО ПЕРЕСЕЧЕНИЙ СЧИТАЕМ КАК ОДНО 
                    //     /      \ 
                    //ТО ЕСТЬ y СЛЕДУЮЩЕЙ И ПРЕДЫДУЩЕЙ ВЕРШИНЫ ЛЕЖАТ ПО РАЗНЫЕ СТОРОНЫ ОТ ТЕКУЩЕЙ
                    if ((_vertices[i1].Longitude <= point.Longitude && _vertices[i2].Longitude >= point.Longitude) || (_vertices[i1].Longitude >= point.Longitude && _vertices[i2].Longitude <= point.Longitude))
                    {
                        number_crossings++;
                    }
                    //ЕСЛИ \/ ИЛИ  \/ , ТО КОЛИЧЕСТВО ПЕРЕСЕЧЕНИЙ СЧИТАЕМ КАК ДВА  
                    //ТО ЕСТЬ У СЛЕДУЮЩЕЙ И ПРЕДЫДУЩЕЙ ВЕРШИНЫ ЛЕЖАТ ПО ОДНУ СТОРОНУ ОТ ТЕКУЩЕЙ
                    if ((_vertices[i1].Longitude < point.Longitude && _vertices[i2].Longitude < point.Longitude) || (_vertices[i1].Longitude > point.Longitude && _vertices[i2].Longitude > point.Longitude))
                    {
                        number_crossings += 2;
                    }
                }
            }

            //ЧЕТНОЕ КОЛИЧЕСТВО - ТОЧКА СНАРУЖИ
            //НЕЧЕТНОЕ КОЛИЧЕСТВО - ТОЧКА ВНУТРИ
            if (number_crossings % 2 != 0) return true;
            else return false;
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
