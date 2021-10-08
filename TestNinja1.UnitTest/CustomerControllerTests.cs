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
    class CustomerControllerTests
    {
        CustomerController _customerController;

        [SetUp]
        public void SetUp()
        {
            _customerController = new CustomerController();
        }


        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            var result =_customerController.GetCustomer(0);

            //Two modes of doing this test.
            Assert.That(result, Is.TypeOf<NotFound>()); //The result HAS TO BE a Not Found.
            Assert.That(result, Is.InstanceOf<NotFound>()); //The result can be a Not Found or one of its derivatives.
        }

        [Test]
        public void GetCustomer_IdIsNotZero_ReturnOk()
        {
            var result = _customerController.GetCustomer(1);

            Assert.That(result, Is.TypeOf<Ok>());



        }
    }
}
