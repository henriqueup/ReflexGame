using System;
using System.Drawing;
using NUnit.Framework;

namespace ReflexGame.UnitTests
{
    [TestFixture]
    public class Form1Tests
    {
        [Test]
        public void GetRandomSquare_FakeGenerator_ValueEquals20()
        {
            //Arrange
            FakeRandomNumberGenerator rnd = new FakeRandomNumberGenerator();
            Form1 form = new Form1();
            //Act
            Rectangle rect = form.GetRandomSquare(rnd);
            //Assert
            Assert.That(rect.Location == new Point(20, 20) && rect.Size == new Size(20, 20));
        }

        [Test]
        public void CreateNewCircle_AddsToCircles()
        {
            //Arrange
            Form1 form = new Form1();
            int initialCount = form.circles.Count;
            //Act
            form.CreateNewCircle();
            //Assert
            Assert.That(form.circles.Count > initialCount && form.circles[0].GetType() == typeof(Rectangle));
        }

        [Test]
        public void GrowAllCircles_SizesIncreased()
        {
            //Arrange
            Form1 form = new Form1();
            int firstCircle_initialSize = 20;
            //Act
            form.CreateNewCircle();
            form.GrowAllCircles();
            //Assert
            Assert.That(form.circles[0].Height > firstCircle_initialSize);
        }
    }
}
