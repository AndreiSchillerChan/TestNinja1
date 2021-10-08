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
    class StackTests
    {
        private TestNinja.Fundamentals.Stack<string> _stack;

        [SetUp]
        public void SetUp()
        {
            _stack = new TestNinja.Fundamentals.Stack<string>();
        }

        [Test]
        public void Push_WhenArgsIsNull_ReturnsArgumentNullException()
        {

            Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);

        }

        [Test]
        public void Push_ValidArgument_AddsTheObject()
        {
            _stack.Push("a");

            Assert.That(_stack.Count, Is.EqualTo(1));

        }

        [Test]
        public void Count_EmptyStack_ReturnZero()
        {
            Assert.That(_stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Pop_NoElementsInTheList_ReturnsAnException()
        {
            Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);

        }

        [Test]
        public void Pop_RemoveElementsInTheList_ReturnsRemovedObject()
        {
            _stack.Push("a");
            _stack.Push("b");

            var result = _stack.Pop();

            Assert.That(result, Is.EqualTo("b"));

        }

        [Test]
        public void Pop_RemoveElementsInTheList_ReturnsListLessOne()
        {
            _stack.Push("a");
            _stack.Push("b");

            _stack.Pop();

            Assert.That(_stack.Count, Is.EqualTo(1));

        }

        [Test]
        public void Peek_EmptyStack_ThrowNewInvalidOperationException()
        {
            Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackWithObjects_ReturnObjectOnTopOfTheStack()
        {
            _stack.Push("a");
            _stack.Push("b");

            var result = _stack.Peek();

            Assert.That(result, Is.EqualTo("b"));

        }

        [Test]
        public void Peek_Stack_DoesNotRemoveObjectfromStack()
        {
            _stack.Push("a");
            _stack.Push("b");

           _stack.Peek();

            Assert.That(_stack.Count, Is.EqualTo(2));
        }
    }
}
