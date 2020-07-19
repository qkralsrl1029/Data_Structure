using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure_DoublyLinkedList
{
    public class Node
    {
        public int Data = 0;
        public Node next = null;
        public Node prev = null;
    }

    public class LinkedList
    {
        public int count = 0;           //리스트의 사이즈

        public Node CreateNode(int data)        //노드 생성, 값 대입
        {
            var node = new Node();
            node.Data = data;
            node.next = null;
            node.prev = null;
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

        public Node GetNode(int index,ref Node head,ref Node tail)      //해당 인덱스의 노드를 반환, 인덱스의 크기를 리스트의 사이즈와 비교해서 앞뒤중 빠른쪽으로 탐색
        {
            Node newNode = null;
            if(index<=count/2)
            {
                newNode = head;
                for (int i = 0; i < index; i++)
                {
                    newNode = newNode.next;
                }
            }
            else
            {
                newNode = tail;
                for (int i = 0; i < (count-1)-index; i++)
                {
                    newNode = newNode.prev;
                }
            }

            return newNode;
        }

        public void AppendNode(ref Node head, ref Node tail, Node newNode)     //리스트의 끝에 노드 추가
        {
            if (head == null)                                   //리스트가 비어있으면 새로운 헤드 생성
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                Node temp = tail;              
                temp.next = newNode;
                newNode.prev = temp;
                tail = newNode;
            }

            count++;
        }

        public void InsertNode(ref Node head,ref Node tail, Node newNode, int index)       //리스트 중간에 노드 삽입
        {
           
            if (index == 0)                                                    //그 위치가 리스트의 맨 앞이라면 새로운 헤드 지정
            {
                newNode.next = head;
                head.prev = newNode;
                head = newNode;
                count++;
                return;
            }
            else if(index==count-1)
            {
                newNode.prev = tail;
                tail.next = newNode;
                tail = newNode;
                count++;
                return;
            }

            Node temp = GetNode(index - 1, ref head, ref tail);

            newNode.next = temp.next;
            temp.next = newNode;

            newNode.prev = temp;
            newNode.next.prev = newNode;
            count++;
        }

        public void removeAt(ref Node head, ref Node tail,int index)       //해당위치의 노드 제거
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
            else if(index==count-1)
            {
                tail = tail.prev;
                count--;
                return;
            }
            Node temp = GetNode(index - 1, ref head, ref tail);
           
            temp.next = temp.next.next;
            temp.next.prev = temp;
            count--;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            LinkedList linkedList = new LinkedList();
            Node Head = null;
            Node Tail = null;
            Node newNode = null;

            for (int i = 0; i < 10; i++)
            {
                newNode = linkedList.CreateNode(i * 10);
                linkedList.AppendNode(ref Head,ref Tail, newNode);
            }

            
            newNode = linkedList.CreateNode(65);
            linkedList.InsertNode(ref Head, ref Tail, newNode, 7);

            linkedList.removeAt(ref Head, ref Tail, 9);
           



            linkedList.ShowList(ref Head);
            Console.WriteLine(linkedList.GetSize());
        }
    }
}
