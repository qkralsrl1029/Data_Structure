using System;
using System.Collections.Generic;
using System.Globalization;
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

    public class Poly       //스택의 응용: 수식의 괄호 에서 쓸 노드
    {
        public string Data = "";
        public Poly next = null;
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
        ///////////////////////////////////////////////////////////////////////////////
        public Poly CreatePoly(string data)        
        {
            var node = new Poly();
            node.Data = data;
            node.next = null;
            return node;
        }

        public void PushPoly(ref Poly top, Poly newNode)    
        {
            if (top == null)
            {
                top = newNode;
                top.next = null;
            }
            else
            {
                newNode.next = top;
                top = newNode;
            }
        }

        public Poly PopPoly(ref Poly top)       
        {
            Poly temp = top;
            top = top.next;
            count--;
            return temp;
        }
        ///////////////////////////////////////////////////////////////////////////////


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


        //스택 응용

        public bool EquationCheck(string equation,ref ListStack listStack,ref Poly top)      //수식의 올바른 괄호 활용 반환
        {
                    
            int pushCount = 0;  //열린괄호 수 체크 변수

            int _length = equation.Length;  
            bool isCorrect = true;

            for (int i = 0; i < _length; i++)   //인자로 받은 문자열의 길이만큼 탐색( ().{},[]  )
            {

                if (equation.Substring(i,1) == "(" || equation.Substring(i,1) == "{" || equation.Substring(i,1) == "[")      //괄호의 시작을 스택에 저장
                {
                    Poly temp1 = CreatePoly(equation.Substring(i,1));
                    listStack.PushPoly(ref top, temp1);
                    pushCount++;
                }
                else if (equation.Substring(i,1) == ")" || equation.Substring(i,1) == "}" || equation.Substring(i,1) == "]")       //괄호의 끝을 만나면 스택에서 팝, 스택이 비어있거나 괄호의 종류가 맞지 않으면 false반환
                {
                    if (top == null)
                    {
                        isCorrect = false;
                        return isCorrect;
                    }
                    Poly temp1 = PopPoly(ref top);
                    pushCount--;
                    switch (equation.Substring(i,1))
                    {
                        case ")":
                            if (temp1.Data != "(")
                            {
                                isCorrect = false;
                                return isCorrect;
                            }
                            break;
                        case "}":
                            if (temp1.Data != "{")
                            {
                                isCorrect = false;
                                return isCorrect;
                            }
                            break;
                        case "]":
                            if (temp1.Data != "[")
                            {
                                isCorrect = false;
                                return isCorrect;
                            }
                            break;

                    }
                }
                else
                    Console.WriteLine(equation.Substring(i,1));
            }

            if (pushCount != 0)     //수식 검사가 끝났는데 남아있는 스택이 있을때(괄호가 시작하고 끝나지 않았을 때, false반환)
                isCorrect = false;
            return isCorrect;
        }


    }


    class Program
    {
        static void Main(string[] args)
        {
            ListStack linkedList = new ListStack();
            Poly top = null;
            //Node top = null;

            //Node stack1 = linkedList.CreateNode(10);
            //linkedList.Push(ref top, stack1);

            //Node stack2 = linkedList.CreateNode(20);
            //linkedList.Push(ref top, stack2);

            //Node stack3 = linkedList.CreateNode(30);
            //linkedList.Push(ref top, stack3);



            //Node stack4 = linkedList.CreateNode(40);
            //linkedList.Push(ref top, stack4);


            //linkedList.ShowStack(ref top);

            string equation1 = "(1+2+3+4)+{(1+2)+3+4}";
            Console.WriteLine(linkedList.EquationCheck(equation1,ref linkedList,ref top));
           
        }
    }
}
