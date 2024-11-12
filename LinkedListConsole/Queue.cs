using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinkedListsConsole
{
    public class Queue
    {
        private LinkedList<object> list = new LinkedList<object>();

        public void Enqueue(object data)
        {
            list.AddLast(data);
        }

        public object Dequeue()
        {
            if (list.Count == 0) return null;
            var value = list.First.Value;
            list.RemoveFirst();
            return value;
        }

        public object Peek()
        {
            return list.Count == 0 ? null : list.First.Value;
        }

        public bool IsEmpty()
        {
            return list.Count == 0;
        }

        public string Print()
        {
            return string.Join(", ", list);
        }

        public int Size()
        {
            return list.Count;
        }
    }
}
