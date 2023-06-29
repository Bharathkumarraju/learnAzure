using System;
using System.Collections;
using System.Collections.Generic;

namespace Benday.YamlDemoApp.WebUi
{
    public class ViewModelRowCollection<T> : IList<T> where T : new()
    {
        private readonly List<T> _list;
        private T _templateItem;
        public const int TEMPLATE_ROW_INDEX = int.MaxValue;

        public ViewModelRowCollection()
        {
            _list = new List<T>();
        }

        public Action<T> OnNewTemplateItem;

        public T TemplateItem
        {
            get
            {
                if (_templateItem == null)
                {
                    _templateItem = new T();

                    OnNewTemplateItem?.Invoke(_templateItem);
                }

                return _templateItem;
            }
            set => _templateItem = value;
        }

        public T this[int index]
        {
            get
            {
                if (index == TEMPLATE_ROW_INDEX)
                {
                    return TemplateItem;
                }
                else
                {
                    return _list[index];
                }
            }
            set => _list[index] = value;
        }

        public int Count => _list.Count;

        public bool IsReadOnly
        {
            get
            {
                if (_list is not IList<T> temp)
                {
                    throw new InvalidOperationException($"Could not convert to IList<T>.");
                }
                else
                {
                    return temp.IsReadOnly;
                }
            }
        }

        public void Add(T item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
