using System;
using System.Collections.Generic;
using System.Linq;

namespace HandJointsMeasurement
{
    public class FixedSizedList<T>
    {
        List<T> list = new List<T>();

        public int Limit { get; set; }
        public void Add(T obj)
        {
            list.Add(obj);
            lock (this)
            {
                while (list.Count > Limit) { list.RemoveAt(0); }
            }
        }

        public IEnumerable<T> Get(int count)
        {
            if (count > list.Count)
            {
                count = list.Count;
            }

            return list.Skip(list.Count - count);
        }

        public int Count()
        {
            return this.list.Count;
        }

        public T Last()
        {
            return this.list.Last();
        }
    }
}
