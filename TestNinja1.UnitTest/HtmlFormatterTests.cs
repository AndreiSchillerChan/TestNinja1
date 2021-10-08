using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja1.UnitTest
{
    [TestFixture]
    public class HtmlFormatterTests
    {
        //Working with Strings

        private HtmlFormatter _htmlFormatter;
        
        [SetUp]
        public void SetUp()
        {
            _htmlFormatter = new HtmlFormatter();
        }

        [Test]
        public void FormatAsBold_WhenCalled_ShouldMakeStringBold()
        {
            var result = _htmlFormatter.FormatAsBold("abc");

            //.IgnoreCase if CaseSensitivity is not important.
            //Specific Test
            Assert.That(result, Is.EqualTo("<strong>abc</strong>"));

            //General Test

            Assert.That(result, Does.StartWith("<strong>"));
            Assert.That(result, Does.EndWith("</strong>"));
            Assert.That(result, Does.Contain("abc"));
        }
    }
}
