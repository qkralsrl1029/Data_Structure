using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure_Queue
{
    class queue
    {
        public int data;
        public queue next;
    }

    class listQueue
    {
        public int queueSize = 0;

        public int getSize() { return queueSize; }

        public queue createQueue(int _data)
        {
            var _queue = new queue();
            _queue.data = _data;
            _queue.next = null;
            return _queue;
        }

        public void ShowQueue(ref queue front)
        {
            queue temp = front;
            for (int i = 0; i < queueSize; i++)
            {
                Console.WriteLine((i + 1) + "번째 큐 : " + temp.data);
                temp = temp.next;
            }
        }

        public void EnQueue(ref queue front, ref queue rear, queue newQueue)
        {
            if (front == null)
            {
                front = newQueue;
                rear = newQueue;
                front.next = null;
                rear.next = null;
            }
            else
            {
                rear.next = newQueue;
                rear = newQueue;
            }

            queueSize++;
        }

        public queue DeQueue(ref queue front, ref queue rear)
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
            }

            queueSize--;
            return temp;
        }

        public queue Peek(ref queue front)
        {
            if (front != null)
                return front;
            else
                return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            queue front = null;
            queue rear = null;
            listQueue listQueue = new listQueue();

            for (int i = 0; i < 5; i++)
            {
                queue temp = listQueue.createQueue(i);
                listQueue.EnQueue(ref front, ref rear, temp);
            }

            listQueue.DeQueue(ref front, ref rear);
            listQueue.ShowQueue(ref front);
        }
    }

}
