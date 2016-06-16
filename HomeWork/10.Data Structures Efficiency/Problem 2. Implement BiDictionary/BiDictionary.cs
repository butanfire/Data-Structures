using System;
using System.Collections.Generic;

namespace Problem_2.Implement_BiDictionary
{
    public class BiDictionary<K1, K2, T>
    {
        private Dictionary<K1, List<T>> valuesByFirstKey;
        private Dictionary<K2, List<T>> valuesBySecondKey;
        private Dictionary<Tuple<K1, K2>, List<T>> valuesByBothKeys;

        public BiDictionary()
        {
            valuesByFirstKey = new Dictionary<K1, List<T>>();
            valuesBySecondKey = new Dictionary<K2, List<T>>();
            valuesByBothKeys = new Dictionary<Tuple<K1, K2>, List<T>>();
        }

        public void Add(K1 key1, K2 key2, T value)
        {
            if (!valuesByFirstKey.ContainsKey(key1))
            {
                valuesByFirstKey.Add(key1, new List<T>());
            }

            valuesByFirstKey[key1].Add(value);

            if (!valuesBySecondKey.ContainsKey(key2))
            {
                valuesBySecondKey.Add(key2, new List<T>());
            }

            valuesBySecondKey[key2].Add(value);

            Tuple<K1, K2> keyForBoth = new Tuple<K1, K2>(key1, key2);
            if (!valuesByBothKeys.ContainsKey(keyForBoth))
            {
                valuesByBothKeys.Add(keyForBoth, new List<T>());
            }

            valuesByBothKeys[keyForBoth].Add(value);
        }

        public IEnumerable<T> Find(K1 key1, K2 key2)
        {
            Tuple<K1, K2> keyForBoth = new Tuple<K1, K2>(key1, key2);
            if (valuesByBothKeys.ContainsKey(keyForBoth))
            {
                return valuesByBothKeys[keyForBoth];
            }

            return new List<T>();
        }

        public IEnumerable<T> FindByKey1(K1 Key1)
        {
            if (valuesByFirstKey.ContainsKey(Key1))
            {
                return valuesByFirstKey[Key1];
            }

            return new List<T>();
        }

        public IEnumerable<T> FindByKey2(K2 Key2)
        {
            if (valuesBySecondKey.ContainsKey(Key2))
            {
                return valuesBySecondKey[Key2];
            }

            return new List<T>();
        }

        public bool Remove(K1 key1, K2 key2)
        {
            Tuple<K1, K2> keyForBoth = new Tuple<K1, K2>(key1, key2);
            if (valuesByBothKeys.ContainsKey(keyForBoth))
            {
                var values = valuesByBothKeys[keyForBoth];
                for(int i = 0; i < values.Count; i++)
                {
                    valuesByFirstKey[key1].Remove(values[i]);
                    valuesBySecondKey[key2].Remove(values[i]);
                }

                valuesByBothKeys.Remove(keyForBoth);

                return true;
            }

            return false;
        }
    }
}
