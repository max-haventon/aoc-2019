using System;

namespace Common
{

    public class Layer
    {
        private int[] _pixels;
        private int _width, _height;

        public Layer(char[] pixels, int width, int height)
        {
            _pixels = new int[pixels.Length];
            _width = width;
            _height = height;
            for (int i = 0; i < pixels.Length; i++)
            {
                _pixels[i] = (int)Char.GetNumericValue(pixels[i]);
            }
        }

        public Layer(int[] pixels, int width, int height)
        {
            _pixels = pixels;
            _width = width;
            _height = height;
        }

        public int Count(int needle)
        {
            int found = 0;

            foreach (int px in _pixels)
            {
                if (px == needle) found++;
            }

            //Console.WriteLine($"  found {found} occurances of {needle} in {_pixels.Length} pixels");
            return found;
        }

        public int[] GetPixels() {
            return _pixels;
        }

        public void Apply(Layer otherLayer) {
            if (_pixels.Length != otherLayer.GetPixels().Length) {
                throw new ArgumentException("Inconsistent layer sizes");
            }

            for (var i=0; i<_pixels.Length; i++)
            {
                if (otherLayer.GetPixels()[i] != (int)PixelColor.Transparent)
                {
                    _pixels[i] = otherLayer.GetPixels()[i];
                }
            }
        }

        public override string ToString()
        {
            string s = "";

            for (int i = 0; i < _pixels.Length; i++)
            {
                s += _pixels[i].ToString();

                if (i + 1 < _pixels.Length)
                {
                    if ((i + 1) % _width == 0)
                    {
                        s += "\r\n";
                    }
                    else
                    {
                        s += "\t";
                    }
                }
            }

            return s;
        }
    }
}