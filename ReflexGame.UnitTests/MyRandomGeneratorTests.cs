using System;
using NUnit.Framework;

namespace ReflexGame.UnitTests
{
    [TestFixture]
    public class MyRandomGeneratorTests
    {
        [Test]
        public void NextInt_Fake_ValueEquals20()
        {
            //Arrange
            FakeRandomNumberGenerator rnd = new FakeRandomNumberGenerator();
            //Act
            int result = rnd.NextInt(0, 100);
            //Assert
            Assert.That(result == 20);
        }
    }
}
