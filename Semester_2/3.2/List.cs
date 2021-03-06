using System;

namespace HashTable
{
    /// Contains integer values, gets them with the "Add" method,
    /// deletes element by value using "DeleteElement" method,
    /// deletes first element with "Pop" method,
    /// returns the first element with "Top",
    /// searches for element by value with "Search",
    /// prints the whole List with "Print" method.
    public class List
    {
        private BasicArrayList list = new BasicArrayList();

        /// Adds new element to the list
        public void Add(int value)
        {
            list.Add(value);
        }

        /// Deletes element with chosen value
        public void DeleteElement(int value)
        {
            list.SetIteratorFirst();
            while (!list.IsIteratorNull() && list.GetIteratorValue() != value)
            {
                list.MoveIteratorForward();
            }
            if (!list.IsIteratorNull())
            {
                list.DeleteElement();
            }
        }

        /// Deletes first element and returns its value
        public int Pop()
        {
            list.SetIteratorFirst();
            int value = list.GetIteratorValue();
            list.DeleteElement();
            return value;
        }

        /// Returns value of the first element
        public int Top()
        {
            list.SetIteratorFirst();
            return list.GetIteratorValue();
        }

        /// Prints the whole list
        public void Print()
        {
            list.SetIteratorFirst();
            while (!list.IsIteratorNull())
            {
                Console.Write(list.GetIteratorValue());
                Console.Write(' ');
                list.MoveIteratorForward();
            }
            Console.WriteLine();
            list.SetIteratorFirst();
        }

        /// Returns boolean value after searching for an element
        public bool Search(int value)
        {
            list.SetIteratorFirst();
            while (!list.IsIteratorNull())
            {
                if (list.GetIteratorValue() == value)
                {
                    list.SetIteratorFirst();
                    return true;
                }
                list.MoveIteratorForward();
            }
            list.SetIteratorFirst();
            return false;
        }
    }
}