﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure_ArrayStack
{
    class _stack
    {
        public int data;
    }

    class ArrayStack
    {
        
        int currentIndex = 0;

        public _stack CreateNode(int _data)
        {
            _stack stack = new _stack();
            stack.data = _data;

            return stack;
        }

        public void Push(ref _stack[] arrStack,ref _stack data)
        {
            if(currentIndex>9)
            {
                Console.WriteLine("Error: Out of Index");
                return;
            }

            arrStack[currentIndex++] = data;
        }

        public _stack Pop(ref _stack[] arrStack)
        {
            _stack temp = arrStack[currentIndex];
            arrStack[currentIndex--] = null;
            return temp;
        }

        public _stack Peek(ref _stack[] arrStack)
        {
            _stack temp = arrStack[currentIndex];
            return temp;
        }

        public void ShowStack(ref _stack[] stacks)
        {
            for (int i = 0; i < currentIndex; i++)
            {
                Console.WriteLine(stacks[i].data);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ArrayStack arrayStack = new ArrayStack();
            _stack[] Stack = new _stack[10];

            _stack stack1 = arrayStack.CreateNode(10);
            _stack stack2 = arrayStack.CreateNode(20);
            _stack stack3 = arrayStack.CreateNode(30);

            arrayStack.Push(ref Stack, ref stack1);
            arrayStack.Push(ref Stack, ref stack2);
            arrayStack.Push(ref Stack, ref stack3);

            arrayStack.Pop(ref Stack);

            arrayStack.ShowStack(ref Stack);
        }
    }
}
