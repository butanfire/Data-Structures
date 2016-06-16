using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Problem_7.Linked_Queue;

namespace LinkedQueueTests
{
    [TestClass]
    public class LinkedQueueTests
    {
        [TestMethod]
        public void PushPop_EmptyLinkedQueue_PushPopCorrectExecution1Element()
        {
            LinkedQueue<int> numbers = new LinkedQueue<int>();
            Assert.AreEqual(0, numbers.Count);

            numbers.Enqueue(10);
            Assert.AreEqual(1, numbers.Count);

            int element = numbers.Dequeue();
            Assert.AreEqual(10, element);

            Assert.AreEqual(0, numbers.Count);
        }

        [TestMethod]
        public void PushPop_EmptyLinkedQueue_PushPopCorrectExecution1000Elements()
        {
            LinkedQueue<string> stringStack = new LinkedQueue<string>();

            for (int i = 0; i <= 999; i++)
            {
                stringStack.Enqueue(i.ToString());
                Assert.AreEqual(i + 1, stringStack.Count);
            }

            for (int i = 999; i >= 0; i--)
            {
                var element = stringStack.Dequeue();
                Assert.AreEqual(element, i.ToString());
                Assert.AreEqual(i, stringStack.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Pop_EmptyLinkedQueue_ShouldThrow()
        {
            LinkedQueue<int> someStack = new LinkedQueue<int>();
            var item = someStack.Dequeue();
        }

        [TestMethod]
        public void PushPop_EmptyLinkedQueue_PushPopCorrectExecution()
        {
            LinkedQueue<int> numbers = new LinkedQueue<int>();
            Assert.AreEqual(0, numbers.Count);

            numbers.Enqueue(5);
            Assert.AreEqual(1, numbers.Count);

            numbers.Enqueue(10);
            Assert.AreEqual(2, numbers.Count);

            int element1 = numbers.Dequeue();
            Assert.AreEqual(element1, 10);
            Assert.AreEqual(1, numbers.Count);

            int element2 = numbers.Dequeue();
            Assert.AreEqual(element2, 5);
            Assert.AreEqual(0, numbers.Count);

        }

        [TestMethod]
        public void ToArray_LinkedQueue_ToArrayCopyCorrectly()
        {
            LinkedQueue<int> numbers = new LinkedQueue<int>();
            numbers.Enqueue(1);
            numbers.Enqueue(2);
            numbers.Enqueue(3);
            numbers.Enqueue(4);
            numbers.Enqueue(5);

            var arr = numbers.ToArray();
            Assert.AreEqual(1, arr[4]);
            Assert.AreEqual(2, arr[3]);
            Assert.AreEqual(3, arr[2]);
            Assert.AreEqual(4, arr[1]);
            Assert.AreEqual(5, arr[0]);
        }

        [TestMethod]
        public void ToArray_EmptyLinkedQueue_ResultShouldBeEmpty()
        {
            LinkedQueue<DateTime> dates = new LinkedQueue<DateTime>();
            var arr = dates.ToArray();
            Assert.AreEqual(arr.Length, dates.Count);
        }
    }
}
