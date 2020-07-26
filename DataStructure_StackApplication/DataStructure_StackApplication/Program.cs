using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure_StackApplication        //수식계산법간 변환
{
    public class Node
    {
        public string Data = null;
        public int order = 0;      // 연산자의 우선순위 결정인자
        public Node next = null;
    }

    public class _Stack
    {
        public int count = 0;

        public Node CreateNode(string data,int _order=0)        //노드 생성, 값 대입
        {
            var node = new Node();
            node.Data = data;
            node.order = _order;
            node.next = null;
            return node;
        }

        public bool CheckStack(ref Node top)
        {
            if (top != null)
                return true;
            else
                return false;
        }

        public void Push(ref Node top, Node newNode)     //푸쉬, 스택의 맨 위에 노드 추가
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

            count++;
        }

        public Node Pop(ref Node top)       //top을 제거 후 반환
        {
            Node temp = top;
            top = top.next;
            count--;
            return temp;
        }

        

        public string equation_LtoM(string equation,ref _Stack stack,ref Node top)      //스택을 이용한 후위연산법에서 중위연산법으로 변환
        {
            string temp = null;
            for (int i = 0; i < equation.Length; i++)
            {
                string _char = equation.Substring(i, 1);        //문자열 길이만큼 한글자씩 비교하면서
                if(_char=="+"|| _char == "-" || _char == "*" || _char == "/" )      //연산자일경우 스택에 있는 두 토큰을 팝한 후 연산자를 포함한 하나의 토큰으로 변경 후 푸쉬
                {
                    string num2 = stack.Pop(ref top).Data;
                    string num1 = stack.Pop(ref top).Data;
                    if(i != equation.Length-1)
                        temp = "("+num1 + _char + num2+")";    //계산식의 순서를 알려주기 위해 괄호 삽입
                    else
                        temp = num1 + _char + num2;

                    Node resultTemp = stack.CreateNode(temp);
                    stack.Push(ref top, resultTemp);
                }
                else                                            //피연산자일경우 스택에 푸쉬
                {
                    Node tempNum = stack.CreateNode(_char);
                    stack.Push(ref top, tempNum);
                }

            }
            temp = stack.Pop(ref top).Data;                     //루프가 끝난 후 스택에 남아있는 토큰(최종 식)을 반환
            return temp;
        }

        public string equation_MtoL(string equation, ref _Stack stack, ref Node top)      //스택을 이용한 중위연산법에서 후위연산법으로 변환
        {
            string temp = null;
            for (int i = 0; i < equation.Length; i++)           //수식의 길이만큼 루프를 돌면서
            {
                string _char = equation.Substring(i, 1);


                if (_char == "+" || _char == "-")               //연산자일경우
                {
                    Node _operator = stack.CreateNode(_char,1); //스택ㅔ 쌓을 노드를 생성, 덧셈뺄셈이므로 우선순위는 가장 낮음
                    
                    if(stack.CheckStack(ref top))               //스택에 노드가 하나이상 존재할때
                    {
                        if(_operator.order>top.order)           //우선순위를 비교        
                            stack.Push(ref top, _operator);     //우선순위가 더 크면 그냥 푸쉬
                        else                                    //작거나 같으면 탑 노드를 반환할문자열에 추가한 후 팝
                        {
                            temp += top.Data;
                            stack.Pop(ref top);
                            stack.Push(ref top, _operator);     //그다음에 푸쉬
                        }
                    }
                    else                                        //스택이 비어있을때는 그냥 푸쉬
                    {
                        stack.Push(ref top, _operator);
                    }
                }
                else if (_char == "*" || _char == "/")          //곱셈과 나눗셈도 우선순위만 높고 나머지 동일
                {
                    Node _operator = stack.CreateNode(_char,2);
                    if (stack.CheckStack(ref top))
                    {
                        if (_operator.order > top.order)
                            stack.Push(ref top, _operator);
                        else
                        {
                            temp += top.Data;
                            stack.Pop(ref top);
                            stack.Push(ref top, _operator);
                        }
                    }
                    else
                    {
                        stack.Push(ref top, _operator);
                    }
                }
                else if (_char == "(")                      //열린괄호를 만났을때는 일단 푸쉬(스택 밖에서는 우선순위가 가장 높음)
                {
                    Node _operator = stack.CreateNode(_char,0); //스택 안에 들어갈때는 우선순위가 가장 낮아짐
                    stack.Push(ref top, _operator);
                }
                else if (_char == ")")                      //닫힌괄호를 만났을때는
                {
                    while(top.Data!="(")                    //열린괄호를 만날때까지 
                    {
                        if (top.Data != "(")
                            temp += top.Data;               //만나는 노드들을 문자열에 붙여주고 없애줌
                        stack.Pop(ref top);
                    }
                    stack.Pop(ref top);                     //마지막으로 열린괄호까지 팝
                }
                else
                {
                    temp += _char;                          //피연산자는 그냥 출력할 문자열에 붙이고 끝
                }

            }

            while (top != null)                             //루프가 끝난후 스택에 남은 연산자들 문자열에 붙이기
            {
                temp += top.Data;
                stack.Pop(ref top);
            }

            return temp;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            _Stack stack = new _Stack();
            Node top = null;

            string test = "da*bc+fe*cg+/q++d/e++";

            Console.WriteLine(stack.equation_LtoM(test, ref stack, ref top));

            string test1 = "d*a+(b+c+f*e/(c+g)+q)/d+e";
            Console.WriteLine(stack.equation_MtoL(test1, ref stack, ref top));
        }
    }
}
