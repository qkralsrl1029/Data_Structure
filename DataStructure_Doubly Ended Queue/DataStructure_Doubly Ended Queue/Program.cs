using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure_Doubly_Ended_Queue
{
    class queue
    {
        public int data;
        public queue next = null;
        public queue prev = null;
    }

    class Deque
    {
        public int DequeSize = 0;

        public int getSize() { return DequeSize; }

        public queue createQueue(int _data)
        {
            var _queue = new queue();
            _queue.data = _data;
            _queue.next = null;
            _queue.prev = null;
            return _queue;
        }

        public void ShowDeque(ref queue front)
        {
            queue temp = front;
            for (int i = 0; i < DequeSize; i++)
            {
                Console.WriteLine((i + 1) + "번째 큐 : " + temp.data);
                temp = temp.next;
            }
        }

        public void EnQueue_front(ref queue front, ref queue rear, queue newQueue)
        {
            if (front == null)
            {
                front = newQueue;
                rear = newQueue;
            }
            else
            {
                newQueue.next = front;
                front.prev = newQueue;
                front = newQueue;
            }

            DequeSize++;
        }

        public void EnQueue_rear(ref queue front, ref queue rear, queue newQueue)
        {
            if (rear == null)
            {
                front = newQueue;
                rear = newQueue;
            }
            else
            {
                rear.next = newQueue;
                newQueue.prev = rear;
                rear = newQueue;
            }

            DequeSize++;
        }

        public queue DeQueue_front(ref queue front, ref queue rear)
        {
            if (front == null)
            {
                Console.WriteLine("Error: Queue is Empty!");
                return null;
            }

            queue temp = front;
            if (front.next == null)
            {
                front = null;
                rear = null;
            }
            else
            {
                front = front.next;
                front.prev = null;
            }

            DequeSize--;
            return temp;
        }

        public queue DeQueue_rear(ref queue front, ref queue rear)
        {
            if (rear == null)
            {
                Console.WriteLine("Error: Queue is Empty!");
                return null;
            }

            queue temp = rear;
            if (rear.prev == null)
            {
                front = null;
                rear = null;
            }
            else
            {
                rear = rear.prev;
                rear.next = null;
            }

            DequeSize--;
            return temp;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            queue front = null;
            queue rear = null;

            Deque deque = new Deque();

            for (int i = 0; i < 3; i++)
            {
                queue temp = deque.createQueue(i);
                deque.EnQueue_front(ref front, ref rear, temp);
            }


            for (int i = 3; i < 6; i++)
            {
                queue temp = deque.createQueue(i);
                deque.EnQueue_rear(ref front, ref rear, temp);
            }

            deque.DeQueue_rear(ref front, ref rear);

            deque.ShowDeque(ref front);


        }
    }

}
