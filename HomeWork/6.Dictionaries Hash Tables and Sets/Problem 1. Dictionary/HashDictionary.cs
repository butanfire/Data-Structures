namespace Problem_1.Dictionary
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        public const float FillFactor = 0.75f;
        public int Count { get; private set; }
        private LinkedList<KeyValue<TKey, TValue>>[] slots;

        public int Capacity => this.slots.Length;

        public HashTable()
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[16];
            this.Count = 0;
        }

        public HashTable(int capacity)
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
            this.Count = 0;
        }

        public void Add(TKey key, TValue value)
        {
            GrowIfNeeded();
            int slotNumber = this.FindSlotNumber(key);
            if (this.slots[slotNumber] == null)
            {
                this.slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            if (this.slots[slotNumber].Any(element => element.Key.Equals(key)))
            {
                throw new ArgumentException("Key already exists : " + key);
            }

            var newElement = new KeyValue<TKey, TValue>(key, value);
            this.slots[slotNumber].AddLast(newElement);
            this.Count++;
        }

        private int FindSlotNumber(TKey key)
        {
            var slotNumber = Math.Abs(key.GetHashCode()) % this.slots.Length;
            return slotNumber;
        }

        private void GrowIfNeeded()
        {
            if ((float)(this.Count + 1) / this.Capacity > FillFactor)
            {
                this.Grow();
            }
        }

        private void Grow()
        {
            var newTable = new HashTable<TKey, TValue>(this.Capacity * 2);
            foreach (var elements in this)
            {
                newTable.Add(elements.Key, elements.Value);
            }

            this.slots = newTable.slots;
            this.Count = newTable.Count;
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            GrowIfNeeded();
            int slotNumber = this.FindSlotNumber(key);
            if (this.slots[slotNumber] == null)
            {
                this.slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var element in this.slots[slotNumber].Where(element => element.Key.Equals(key)))
            {
                element.Value = value;
                return false;
            }

            var newElement = new KeyValue<TKey, TValue>(key, value);
            this.slots[slotNumber].AddLast(newElement);
            this.Count++;
            return true;
        }

        public TValue Get(TKey key)
        {
            var element = this.Find(key);
            if (element == null)
            {
                throw new KeyNotFoundException("Key does not exist");
            }

            return element.Value;

        }

        public TValue this[TKey key]
        {
            get
            {
                return this.Get(key);
            }
            set
            {
                this.AddOrReplace(key, value);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var element = this.Find(key);
            if (element != null)
            {
                value = element.Value;
                return true;
            }

            value = default(TValue);

            return false;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            int slotNumber = this.FindSlotNumber(key);
            var elements = this.slots[slotNumber];

            return elements?.FirstOrDefault(element => element.Key.Equals(key));
        }

        public bool ContainsKey(TKey key)
        {
            return this.Find(key) != null;
        }

        public bool Remove(TKey key)
        {
            int slotNumber = this.FindSlotNumber(key);
            var elements = this.slots[slotNumber];
            if (elements != null)
            {
                var currentElement = elements.First;
                while (currentElement != null)
                {
                    if (currentElement.Value.Key.Equals(key))
                    {
                        elements.Remove(currentElement);
                        this.Count--;
                        return true;
                    }

                    currentElement = currentElement.Next;
                }
            }

            return false;
        }

        public void Clear()
        {
            this.Count = 0;
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[16];
        }

        public IEnumerable<TKey> Keys => this.Select(element => element.Key);

        public IEnumerable<TValue> Values => this.Select(element => element.Value);

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            return this.slots.Where(elements => elements != null).SelectMany(elements => elements).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}