using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintingPixels
{
    public class PixelPainter
    {
        //Grid structure - each pixel is represented by a key-value pair; key is a Point(x and y properties), value is a Color
        public Dictionary<Point, Color> Grid { get; set; }
        private int NumberOfColumns { get; set; }
        private int NumberOfRows { get; set; }


        //  Creates a grid to specified size
        public PixelPainter(int columns, int rows)
        {
            Grid = new Dictionary<Point, Color>();
            NumberOfColumns = columns;
            NumberOfRows = rows;

            // create multiples of ....
            for (int x = 1; x <= columns; x++)
            {
                // ... rows
                for (int y = 1; y <= rows; y++)
                {
                    Grid.Add(new Point(x, y), Color.Empty);
                }
            }
        }
        

        //  Assigns Color value to the selected pixel
        public void FillPixel(Point selectedPixel, Color selectedColor)
        {
            Grid[selectedPixel] = selectedColor;
        }


        //  Fills a row or column of cells with a Color
        public void FillAdjacentCells(Point firstPixel, Point endPixel, Color selectedColor)
        {
            //  if the X values are identical, then it is a column fill
            if (firstPixel.X == endPixel.X)
            {
                //  cycles through the columns
                for (int x = 1; x <= NumberOfColumns; x++)
                {
                    //  cycles through the rows
                    for (int y = 1; y <= NumberOfRows; y++)
                    {
                        Point pixel = new Point(x, y);

                        //  if the pixel falls between firstpixel and endpixel
                        if (pixel.Y >= firstPixel.Y && pixel.Y <= endPixel.Y && pixel.X == firstPixel.X)
                        {
                            Grid[pixel] = Color.Red;
                        }
                    }                    
                }
            }

            //  if the Y values are identical, then it is a row fill
            if (firstPixel.Y == endPixel.Y)
            {
                //  cycles through the columns
                for (int x = 1; x <= NumberOfColumns; x++)
                {
                    //  cycles through the rows
                    for (int y = 1; y <= NumberOfRows; y++)
                    {
                        Point pixel = new Point(x, y);

                        //  if the pixel falls between firstpixel and endpixel
                        if (pixel.X >= firstPixel.X && pixel.X <= endPixel.X && pixel.Y == firstPixel.Y)
                        {
                            Grid[pixel] = Color.Red;
                        }
                    }
                }
            }

            // if neither values are identical then the input parameters are invalid
            else return;
        }


        public void FloodPixels(Point startPixel, Color newColor)
        {
            //  gets the current Color of the start pixel
            var originalColor = Grid[startPixel];

            // Flood() has to be split out into a seperate method to prevent "originalColor" being reset to newColor during recursion
            Flood(startPixel, originalColor, newColor);
        }


        private void Flood(Point startPixel, Color originalColor, Color newColor)
        {
            //  list of the 4 adjacent pixels to the selected pixel
            var adjacentPixels = new List<Point>()
            {
                new Point(startPixel.X+1,startPixel.Y),
                new Point(startPixel.X-1,startPixel.Y),
                new Point(startPixel.X,startPixel.Y+1),
                new Point(startPixel.X,startPixel.Y-1)
            };

            foreach (var pixel in adjacentPixels)
            {
                //  checks that the adjacent pixel is within the grid
                if (pixel.X <= NumberOfColumns && pixel.Y <= NumberOfRows && pixel.X > 0 && pixel.Y > 0)
                {
                    //  checks if the adjacent pixel has same Color as the startpixel
                    if (Grid[pixel] == originalColor)
                    {
                        //  sets the color of the adjacent pixel to the new Color
                        Grid[pixel] = newColor;

                        //  calls Flood on the adjacent pixel currently being worked on to flood to its adjacent pixels
                        Flood(pixel, originalColor, newColor);
                    }
                }
            }
        }
    }
}
