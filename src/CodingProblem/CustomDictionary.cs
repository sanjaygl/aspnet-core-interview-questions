namespace CodingProblem
{
    public class CustomDictionary<TKey, TValue>
    {
        private const int size = 5;
        private List<KeyValuePair<TKey, TValue>>[] _bucket;

        public CustomDictionary()
        {
            _bucket = new List<KeyValuePair<TKey, TValue>>[size];
        }

        private int GetIndex(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % size;
        }

        public void Add(TKey key, TValue value)
        {
            int index = GetIndex(key);

            if (_bucket[index] == null)
            {
                _bucket[index] = new List<KeyValuePair<TKey, TValue>>();
            }

            for (int i = 0; i < _bucket[index].Count; i++)
            {
                if (_bucket[index][i].Key.Equals(key))
                {
                    _bucket[index][i] = new KeyValuePair<TKey, TValue>(key, value);

                    return;
                }
            }

            _bucket[index].Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public TValue Get(TKey key)
        {
            int index = GetIndex(key);
            if (_bucket[index] != null)
            {
                foreach (var kvp in _bucket[index])
                {
                    if (kvp.Key.Equals(key))
                    {
                        return kvp.Value;
                    }
                }
            }

            throw new KeyNotFoundException($"Key '{key}' not found.");
        }

        public void Remove(TKey key)
        {
            int index = GetIndex(key);
            if (_bucket[index] != null)
            {
                for (int i = 0; i < _bucket[index].Count; i++)
                {
                    if (_bucket[index][i].Key.Equals(key))
                    {
                        _bucket[index].RemoveAt(i);
                        return;
                    }
                }
            }
            throw new KeyNotFoundException($"Key '{key}' not found.");
        }

        public int Count
        {
            get
            {
                int count = 0;
                foreach (var bucket in _bucket)
                {
                    if (bucket != null)
                    {
                        count += bucket.Count;
                    }
                }
                return count;
            }
        }
    }
}
