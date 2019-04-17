using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Mocker
{
    public class FakeDbDataParameterCollection : IDataParameterCollection
    {
        public IList<object> ActualParameters { get; }

        public FakeDbDataParameterCollection()
        {
            ActualParameters = new List<object>();
        }

        public bool Contains(string parameterName)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(string parameterName)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(string parameterName)
        {
            throw new NotImplementedException();
        }

        public object this[string parameterName]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public int Add(object value)
        {
            ActualParameters.Add(value);
            return 1;
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public bool IsFixedSize => throw new NotImplementedException();
        public bool IsReadOnly => throw new NotImplementedException();

        public object this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count => throw new NotImplementedException();
        public bool IsSynchronized => throw new NotImplementedException();
        public object SyncRoot => throw new NotImplementedException();

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}
