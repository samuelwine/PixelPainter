using System;
using System.Drawing;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PaintingPixels.Tests
{
    [TestClass]
    public class PixelPainterTests
    {
        [TestMethod]
        public void PixelPainter_CanCreateGrid()
        {
            //  Arrange - Act
            PixelPainter nineByNinePixelPainter = new PixelPainter(9,9);
            PixelPainter twentyByFiftyPixelPainter = new PixelPainter(20,50);
            
            //  Assert
            Assert.AreEqual(81, nineByNinePixelPainter.Grid.Count);
            Assert.AreEqual(1000, twentyByFiftyPixelPainter.Grid.Count);            
        }

        [TestMethod]
        public void FillPixel_FillsOnlyTheSpecifiedPixel()
        {
            //  Arrange
            PixelPainter pixelPainter = new PixelPainter(9,9);            
            Point pixel = new Point(2, 3);

            //  Act
            pixelPainter.FillPixel(pixel, Color.Red);
            var numberOfUnaffectedPixels = pixelPainter.Grid.Values.Where(x => x.IsEmpty).Count();
            
            //  Assert
            Assert.AreEqual(Color.Red, pixelPainter.Grid[pixel]);
            Assert.AreEqual(80, numberOfUnaffectedPixels);
        }

        [TestMethod]
        public void FillAdjacentCells_FillsOnlyTheSpecifiedColumn()
        {
            //  Arrange
            PixelPainter pixelPainter = new PixelPainter(9,9);            
            Point firstPixel = new Point(2, 3);
            Point endPixel = new Point(2, 7);

            //  Act
            pixelPainter.FillAdjacentCells(firstPixel, endPixel, Color.Red);
            var numberOfUnaffectedPixels = pixelPainter.Grid.Values.Where(x => x.IsEmpty).Count();
            var numberOfAffectedPixels = pixelPainter.Grid.Values.Where(x => x == Color.Red).Count();

            //  Assert
            Assert.AreEqual(5, numberOfAffectedPixels);
            Assert.AreEqual(76, numberOfUnaffectedPixels);
        }

        [TestMethod]
        public void FillAdjacentCells_FillsOnlyTheSpecifiedRow()
        {
            //  Arrange
            PixelPainter pixelPainter = new PixelPainter(9,9);            
            Point firstPixel = new Point(2, 3);
            Point endPixel = new Point(6, 3);

            //  Act
            pixelPainter.FillAdjacentCells(firstPixel, endPixel, Color.Red);
            var numberOfUnaffectedPixels = pixelPainter.Grid.Values.Where(x => x.IsEmpty).Count();
            var numberOfAffectedPixels = pixelPainter.Grid.Values.Where(x => x == Color.Red).Count();

            //  Assert
            Assert.AreEqual(5, numberOfAffectedPixels);
            Assert.AreEqual(76, numberOfUnaffectedPixels);
        }

        [TestMethod]
        public void FloodPixels_ColorsAllAdjacentPixelsOfSameColor()
        {
            //  Arrange
            PixelPainter pixelPainter = new PixelPainter(9,9);            
            pixelPainter.Grid[new Point(2, 3)] = Color.Blue;
            pixelPainter.Grid[new Point(3, 2)] = Color.Green;
            pixelPainter.Grid[new Point(3, 3)] = Color.Green;
            pixelPainter.Grid[new Point(3, 4)] = Color.Green;
            pixelPainter.Grid[new Point(3, 5)] = Color.Green;
            pixelPainter.Grid[new Point(3, 6)] = Color.Green;
            pixelPainter.Grid[new Point(4, 3)] = Color.Blue;
            pixelPainter.Grid[new Point(5, 3)] = Color.Blue;
            pixelPainter.Grid[new Point(5, 4)] = Color.Blue;
            pixelPainter.Grid[new Point(6, 3)] = Color.Blue;
            pixelPainter.Grid[new Point(6, 4)] = Color.Blue;
            pixelPainter.Grid[new Point(7, 3)] = Color.Blue;
            pixelPainter.Grid[new Point(8, 2)] = Color.Yellow;
            pixelPainter.Grid[new Point(8, 4)] = Color.Blue;
            pixelPainter.Grid[new Point(8, 5)] = Color.Blue;

            //  Act
            pixelPainter.FloodPixels(new Point(6, 3), Color.Red);
            var numberOfFloodedPixels = pixelPainter.Grid.Values.Where(x => x == Color.Red).Count();
           
            //  Assert
            Assert.AreEqual(6, numberOfFloodedPixels);
        }

        [TestMethod]
        public void FloodPixels_StopsAtGridBoundaries()
        {
            //  Arrange
            PixelPainter pixelPainter = new PixelPainter(9,9);            

            //  Act
            pixelPainter.FloodPixels(new Point(1, 1), Color.Red);
            var numberOfFloodedPixels = pixelPainter.Grid.Values.Where(x => x == Color.Red).Count();

            //  Assert
            Assert.AreEqual(81, numberOfFloodedPixels);
        }
    }
}
