using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja1.UnitTest
{
    //How to test void Methods. Notice that because it is a void method, it will not return a result and hence I have not used the result variable.


    [TestFixture]
    class ErrorLoggerTests
    {
        private ErrorLogger _errorLogger;

        [SetUp]
        public void SetUp()
        {
            _errorLogger = new ErrorLogger();
        }

        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            _errorLogger.Log("abc");

            //On the ErrorLogger class - to test the above, it is worthwhile to comment out the property fields and see if the test still passes with ABC. If it passes, it means I had an untrustworthy test.

            Assert.That(_errorLogger.LastError, Is.EqualTo("abc"));
        }

        
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowNullArgumentException(string error)
        {
            Assert.That(() => _errorLogger.Log(error), Throws.ArgumentNullException);

            //To test the above, also try commenting out the IF clause that throws the exception on the ErrorLogger class.
        }


        //How to verify that the event is being raised. Frist, we have to subscribe to the event.
        [Test]
        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            var id = Guid.Empty;
            _errorLogger.ErrorLogged += (sender , args) => { id = args; }; //Sender is the source of the event, args is the event argument. You have to subscribe before the "Act"

            _errorLogger.Log("a");

            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}
