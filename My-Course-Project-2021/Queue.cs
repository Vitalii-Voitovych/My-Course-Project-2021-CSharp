﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Course_Project_2021
{
    class Queue<T> : IEnumerable
    {
        public Node<T> Front { get; private set; } = null;
        public Node<T> Back { get; private set; } = null;
        public Queue() { }
        public Queue(T element) => Enqueue(element);
        public Queue(params T[] elements)
        {
            foreach (T item in elements)
                Enqueue(item);
        }
        public void Enqueue(T element)
        {
            Node<T> n = new Node<T>(element);
            if(Back == null)
            {
                Front = Back = n;
            }
            else
            {
                Back.Next = n;
                Back = n;
            }
        }
        public void Dequeue()
        {
            if (Front == null)
                throw new NullReferenceException("Queue empty");
            Front = Front.Next;
            if (Front == null)
                Back = null;
        }
        public bool IsEmpty()
        {
            if (Front == null)
                return true;
            return false;
        }
        public void Clear() => Front = Back = null;
        public void Clone(Queue<T> Q)
        {
            Node<T> ptr = Q.Front;
            while (ptr != null)
            {
                Enqueue(ptr.Data);
                ptr = ptr.Next;
            }
        }
        public T Peek()
        {
            if (!IsEmpty())
                return Front.Data;
            else
                throw new NullReferenceException("Queue empty!");
        }
        public void Print()
        {
            Node<T> ptr = Front;
            while (ptr != null)
            {
                Console.Write("{0} <- ", ptr.Data);
                ptr = ptr.Next;
            }
            Console.WriteLine();
        }
        /// <summary>
        /// Метод "Sort" базується на алгоритмі сортування бульбашкою
        /// </summary>
        /// <param name="Q">Черга яка сортується</param>
        public static void Sort<V>(Queue<V> Q) where V : IComparable<V>
        {
            Node<V> ptr1 = Q.Front;
            Node<V> ptr2;
            while (ptr1 != null)
            {
                ptr2 = Q.Front;
                while (ptr2.Next != null)
                {
                    if (ptr2.Data.CompareTo(ptr2.Next.Data) > 0)
                    {
                        var tmp = ptr2.Next;
                        Node<T>.Swap(ptr2, tmp);
                    }
                    ptr2 = ptr2.Next;
                }
                ptr1 = ptr1.Next;
            }
        }
        public static Queue<T> operator+(Queue<T> Q1, Queue<T> Q2)
        {
            Queue<T> tmp = new Queue<T>();
            tmp.Clone(Q1);
            Node<T> ptr = tmp.Front;
            while (ptr.Next != null)
                ptr = ptr.Next;
            ptr.Next = Q2.Front;
            return tmp;
        }
        /// <summary>
        /// Реалізація інтерфейсу IEnumerable, для ітерації в циклі foreach
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            Node<T> current = Front;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        /// <summary>
        /// Операція явного перетворення. Наприклад:"(int)a"
        /// </summary>
        /// <param name="L">Список який перетворюєм</param>
        public static explicit operator Queue<T>(LinkedList<T> L)
        {
            Queue<T> Q = new Queue<T>();
            foreach (T item in L)
                Q.Enqueue(item);
            return Q;
        }
    }
}
