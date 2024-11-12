using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListsConsole
{
    public class Stack
    {
        private LinkedList<object> list = new LinkedList<object>();

        public void Push(object data)
        {
            list.AddFirst(data);
        }

        public object Pop()
        {
            if (list.Count == 0) return null;
            var value = list.First.Value;
            list.RemoveFirst();
            return value;
        }

        public object Top()
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
