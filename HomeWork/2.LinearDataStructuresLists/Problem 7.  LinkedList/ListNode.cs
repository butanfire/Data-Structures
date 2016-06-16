namespace Problem_7.LinkedList
{
    public class ListNode<T>
    {
        public T Value { get; private set; }

        public ListNode<T> NextNode;

        public ListNode(T value)
        {
            this.Value = value;
        }
    }
}
