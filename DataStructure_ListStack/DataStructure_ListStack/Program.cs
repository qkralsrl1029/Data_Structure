using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataStructure_ListStack
{
    public class Node
    {
        public int Data = 0;
        public Node next = null;
    }

    public class ListStack
    {
        public int count = 0;           //스택의 사이즈

        public Node CreateNode(int data)        //노드 생성, 값 대입
        {
            var node = new Node();
            node.Data = data;
            node.next = null;
            return node;
        }

        public void ShowStack(ref Node top)     //역순 출력, 후입선출(LIFO)
        {
            Node temp = top;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(temp.Data);
                temp = temp.next;
            }
            Console.WriteLine("");
        }

        public int GetSize()
        {
            return count;
        }

        public void Push(ref Node top, Node newNode)     //푸쉬, 스택의 맨 위에 노드 추가
        {
            if(top==null)
            {
                top = newNode;
                top.next = null;
            }
            else
            {
                newNode.next = top;
                top = newNode;
            }

            count++;
        }

        public Node Pop(ref Node top)       //top을 제거 후 반환
        {
            Node temp = top;
            top = top.next;
            count--;
            return temp;
        }

        public Node Peek(ref Node top)      //top위치의 노드 반환
        {
            return top;
        }


    }


    class Program
    {
        static void Main(string[] args)
        {
            ListStack linkedList = new ListStack();
            Node top = null;

            Node stack1 = linkedList.CreateNode(10);
            linkedList.Push(ref top, stack1);

            Node stack2 = linkedList.CreateNode(20);
            linkedList.Push(ref top, stack2);

            Node stack3 = linkedList.CreateNode(30);
            linkedList.Push(ref top, stack3);

            

            Node stack4 = linkedList.CreateNode(40);
            linkedList.Push(ref top, stack4);

            
            linkedList.ShowStack(ref top);

                       
        }
    }
}
