using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;
using Math = TestNinja.Fundamentals.Math;

namespace TestNinja1.UnitTest
{
    [TestFixture]
    public class MathTests
    {
        private Math _math;
        //NUnit has a SetUp Attribute - which can help to call a method before each test.
        //And a TearDown Method

        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }

        [Test]
        public void Add_WhenCalled_ReturnTheSumOfArguements()
        {
           //var math = new Math() - Because of the SetUp attribute we no longer need to call this class on each test. The SetUp will call it immediately before each test is run!

            var result = _math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(2,1,2)]
        [TestCase(1,2,2)]
        [TestCase(1,1,1)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
        {

            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(5);

            //Too General
            Assert.That(result, Is.Not.Empty);

            //Specfic
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result, Does.Contain(1));
            Assert.That(result, Does.Contain(3));
            Assert.That(result, Does.Contain(5));

            //Better when testing for non-dynamic arrays - and is equivalent to writing the three tests above.
            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));


            //Some other methods:
            //Assert.That(result, Is.Ordered);
            //Assert.That(result, Is.Unique);
        }
    }
}
