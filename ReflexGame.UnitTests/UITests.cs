using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using NUnit.Framework;
using System.Threading;

namespace ReflexGame.UnitTests
{
    [TestFixture]
    public class UITests
    {
        [Test]
        public void UI_GameOver_ControlsAreCreated()
        {
            //Arrange
            Form1 form = new Form1();
            form.UI = new UI(form);
            Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject pvt = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(form.UI);
            //Act
            form.UI.GameOver();
            Label labelGameOver = (Label)pvt.GetField("labelGameOver");
            //Assert
            Assert.That(form.Controls.Contains(labelGameOver) && labelGameOver.Tag.ToString() == "GameOver");
        }
    }
}
