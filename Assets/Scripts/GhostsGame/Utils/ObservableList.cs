using System;
using System.Collections.Generic;

namespace GhostsGame.Utils
{
    public class ObservableList<T>
    {
        private readonly List<T> _items = new List<T>();

        public event Action<T> ItemAdded;
        public event Action<T> ItemRemoved;

        public int Count => _items.Count;
        public IReadOnlyList<T> Items => _items;

        public void Add(T item)
        {
            _items.Add(item);
            ItemAdded?.Invoke(item);
        }

        public void Remove(T item)
        {
            if (_items.Remove(item))
                ItemRemoved?.Invoke(item);
        }

        public void Clear()
        {
            foreach (T item in _items)
                ItemRemoved?.Invoke(item);

            _items.Clear();
        }
    }
}
