namespace CodingProblem
{
    public class CustomDictionary<TKey, TValue>
    {
        private const int DefaultSize = 5;
        private readonly List<KeyValuePair<TKey, TValue>>[] _bucket;

        public CustomDictionary(int size = DefaultSize)
        {
            _bucket = new List<KeyValuePair<TKey, TValue>>[size];
        }

        private int GetIndex(TKey key)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));

            return Math.Abs(key.GetHashCode() % _bucket.Length);
        }

        public void Add(TKey key, TValue value)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));

            int index = GetIndex(key);

            if (_bucket[index] == null)
            {
                _bucket[index] = new List<KeyValuePair<TKey, TValue>>();
            }

            foreach (var pair in _bucket[index])
            {
                if (pair.Key.Equals(key))
                    throw new ArgumentException($"Key '{key}' already exists");
            }

            _bucket[index].Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public TValue Get(TKey key)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));

            int index = GetIndex(key);

            if (_bucket[index] != null)
            {
                foreach (var pair in _bucket[index])
                {
                    if (pair.Key.Equals(key))
                        return pair.Value;
                }
            }

            throw new ArgumentException($"Key '{key}' does not exist");
        }

        public void Remove(TKey key)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));

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

            throw new ArgumentException($"Key '{key}' does not exist");
        }

        public bool ContainsKey(TKey key)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));

            int index = GetIndex(key);

            if (_bucket[index] != null)
            {
                foreach (var pair in _bucket[index])
                {
                    if (pair.Key.Equals(key))
                        return true;
                }
            }

            return false;
        }
    }
}
