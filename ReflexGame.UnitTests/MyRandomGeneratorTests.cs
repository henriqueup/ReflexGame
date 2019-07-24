using System;
using NUnit.Framework;

namespace ReflexGame.UnitTests
{
    [TestFixture]
    public class MyRandomGeneratorTests
    {
        [Test]
        public void Constructor_IsFake()
        {
            //Arrange
            MyRandomGenerator rnd = new MyRandomGenerator(true);
            //Act
            //Assert
            Assert.That(rnd.isFake == true);
        }

        [Test]
        public void NextInt_Fake()
        {
            //Arrange
            MyRandomGenerator rnd = new MyRandomGenerator(true);
            //Act
            int result = rnd.NextInt(0, 100, 20);
            //Assert
            Assert.That(result == 20);
        }
    }
}
