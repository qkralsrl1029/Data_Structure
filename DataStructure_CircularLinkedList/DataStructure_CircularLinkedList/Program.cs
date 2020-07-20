using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure_CircularLinkedList
{
    public class Node
    {
        public int Data = 0;
        public Node next = null;
    }

    public class LinkedList
    {
        public int count = 0;           //리스트의 사이즈

        public Node CreateNode(int data)        //노드 생성, 값 대입
        {
            var node = new Node();
            node.Data = data;
            node.next = null;
            return node;
        }

        public void ShowList(ref Node Head)     //현재 사이즈 만큼의 노드 출력, ref를 통해 해당 리스트 참조 가능
        {
            Node temp = Head;
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

        public void AppendNode(ref Node head, Node newNode)     //리스트의 끝에 노드 추가
        {
            if (head == null)                                   //리스트가 비어있으면 새로운 헤드 생성
            {
                head = newNode;

            }
            else
            {
                Node temp = head;
                for (int i = 0; i < count-1; i++)               
                {
                    temp = temp.next;
                }
                temp.next = newNode;
                newNode.next = head;
            }

            count++;
        }

        public void InsertNode(ref Node head, Node newNode, int index)       //리스트 중간에 노드 삽입
        {
            Node temp = head;
            if (index == 0)                                                    //그 위치가 리스트의 맨 앞이라면 새로운 헤드 지정
            {
                newNode.next = head;
                head = newNode;
                count++;
                return;
            }
            for (int i = 0; i < index - 1; i++)
            {
                temp = temp.next;
            }
            newNode.next = temp.next;
            temp.next = newNode;
            count++;
        }

        public void removeAt(ref Node head, int index)       //해당위치의 노드 제거
        {
            if (index >= count || index < 0)
            {
                Console.WriteLine("Error: out of Index");
                return;
            }
            else if (index == 0)
            {
                head = head.next;
                count--;
                return;
            }
            Node temp = head;
            for (int i = 0; i < index - 1; i++)
            {
                temp = temp.next;
            }
            temp.next = temp.next.next;
            count--;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            LinkedList linkedList = new LinkedList();
            Node Head = null;
            Node newNode = null;

            for (int i = 0; i < 5; i++)
            {
                newNode = linkedList.CreateNode(i * 10);
                linkedList.AppendNode(ref Head, newNode);
            }

            newNode = linkedList.CreateNode(-5);
            linkedList.InsertNode(ref Head, newNode, 0);
            newNode = linkedList.CreateNode(25);
            linkedList.InsertNode(ref Head, newNode, 4);


            linkedList.removeAt(ref Head, 0);
            linkedList.ShowList(ref Head);
            Console.WriteLine(linkedList.GetSize());
        }
    }
}
