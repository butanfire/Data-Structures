namespace LinkedStackTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Problem_5.Linked_Stack;

    [TestClass]
    public class LinkedStackTests
    {
        [TestMethod]
        public void PushPop_EmptyLinkedStack_PushPopCorrectExecution1Element()
        {
            LinkedStack<int> numbers = new LinkedStack<int>();
            Assert.AreEqual(0, numbers.Count);

            numbers.Push(10);
            Assert.AreEqual(1, numbers.Count);

            int element = numbers.Pop();
            Assert.AreEqual(10, element);

            Assert.AreEqual(0, numbers.Count);
        }

        [TestMethod]
        public void PushPop_EmptyLinkedStack_PushPopCorrectExecution1000Elements()
        {
            LinkedStack<string> stringStack = new LinkedStack<string>();

            for (int i = 0; i <= 999; i++)
            {
                stringStack.Push(i.ToString());
                Assert.AreEqual(i + 1, stringStack.Count);
            }

            for (int i = 999; i >= 0; i--)
            {
                var element = stringStack.Pop();
                Assert.AreEqual(element, i.ToString());
                Assert.AreEqual(i, stringStack.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Pop_EmptyLinkedStack_ShouldThrow()
        {
            LinkedStack<int> someStack = new LinkedStack<int>();
            var item = someStack.Pop();
        }

        [TestMethod]
        public void PushPop_StackInitiCapacity1_PushPopCorrectExecution()
        {
            LinkedStack<int> numbers = new LinkedStack<int>();
            Assert.AreEqual(0, numbers.Count);

            numbers.Push(5);
            Assert.AreEqual(1, numbers.Count);

            numbers.Push(10);
            Assert.AreEqual(2, numbers.Count);

            int element1 = numbers.Pop();
            Assert.AreEqual(element1, 10);
            Assert.AreEqual(1, numbers.Count);

            int element2 = numbers.Pop();
            Assert.AreEqual(element2, 5);
            Assert.AreEqual(0, numbers.Count);

        }

        [TestMethod]
        public void ToArray_LinkedStack_ToArrayCopyCorrectly()
        {
            LinkedStack<int> numbers = new LinkedStack<int>();
            numbers.Push(1);
            numbers.Push(2);
            numbers.Push(3);
            numbers.Push(4);
            numbers.Push(5);

            var arr = numbers.ToArray();
            Assert.AreEqual(1, arr[0]);
            Assert.AreEqual(2, arr[1]);
            Assert.AreEqual(3, arr[2]);
            Assert.AreEqual(4, arr[3]);
            Assert.AreEqual(5, arr[4]);
        }

        [TestMethod]
        public void ToArray_EmptyLinkedStack_ResultShouldBeEmpty()
        {
            LinkedStack<DateTime> dates = new LinkedStack<DateTime>();
            var arr = dates.ToArray();
            Assert.AreEqual(arr.Length, dates.Count);
        }
    }
}
