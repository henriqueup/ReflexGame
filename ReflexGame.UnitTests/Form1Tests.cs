using System;
using System.Diagnostics;
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
        public void CreateNewCircle_AddsToCircles_CirclesCountIncreases()
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

        [Test]
        public void PointIsInsideCircle_IsInside_ReturnsTrue()
        {
            //Arrange
            Form1 form = new Form1();
            Point clicked = new Point(2, 2);    //Point on center
            Rectangle circle = new Rectangle(new Point(0, 0), new Size(4, 4));
            //Act
            bool ret = form.PointIsInsideCircle(clicked, circle);
            //Assert
            Assert.That(ret == true);
        }

        [Test]
        public void PointIsInsideCircle_IsOnBorder_ReturnsTrue()
        {
            //Arrange
            Form1 form = new Form1();
            Point clicked = new Point(0, 2);    //Point on left-most border
            Rectangle circle = new Rectangle(new Point(0, 0), new Size(4, 4));
            //Act
            bool ret = form.PointIsInsideCircle(clicked, circle);
            //Assert
            Assert.That(ret == true);
        }

        [Test]
        public void PointIsInsideCircle_IsOutside_ReturnsFalse()
        {
            //Arrange
            Form1 form = new Form1();
            Point clicked = new Point(0, 0);    //Point on top left corner of surrounding square
            Rectangle circle = new Rectangle(new Point(0, 0), new Size(4, 4));
            //Act
            bool ret = form.PointIsInsideCircle(clicked, circle);
            //Assert
            Assert.That(ret == false);
        }

        //ProcessClick
        //Given a point, determine if it is within the area of one of the circles
        //Test: create circle and give points with certain positions
        [Test]
        public void ProcessClick_PointInOneCircle_OnlyOneRemoved()
        {
            //Arrange
            Form1 form = new Form1();
            Rectangle circle1 = new Rectangle(new Point(0, 0), new Size(20, 20));
            form.circles.Add(circle1);
            Rectangle circle2 = new Rectangle(new Point(20, 20), new Size(20, 20));
            form.circles.Add(circle2);
            Rectangle circle3 = new Rectangle(new Point(40, 40), new Size(20, 20));
            form.circles.Add(circle3);
            Point clicked = new Point(10, 10);
            //Act
            form.ProcessClick(clicked);
            //Assert
            //Added 3 circles, clicked on first one
            Assert.That(form.circles.Count == 2);
        }
    }
}
