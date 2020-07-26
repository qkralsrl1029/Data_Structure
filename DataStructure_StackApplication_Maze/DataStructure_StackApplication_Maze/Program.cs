using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure_StackApplication_Maze
{
    public struct pos                   //이차원 배열 내 위치를 저장할 구조체
    {
        public int posX;
        public int posY;
    }



    public class path               //위치와 이동 방향을 저장하는 노드
    {
        public pos position;
        public pos dir;
        public path next = null;
    }
    public class mazeStack
    {
        public void mazeSolution(int[,] maze,pos currentPos,pos goalPos,ref path top)
        {
            pos myPos = currentPos;             //현위치, 시작값은 인자로 받아옴

            int[,] pastPathed = maze;           //이동경로를 저장할 임시 배열

            pos[] Dir = new pos[4];             //메이즈탐색 방향들
            Dir[0].posX = -1; Dir[0].posY = 0;
            Dir[1].posX = 0; Dir[1].posY = 1;
            Dir[2].posX = 1; Dir[2].posY = 0;   
            Dir[3].posX = 0; Dir[3].posY = -1;
            Console.WriteLine("------------------------------------------");
            while(myPos.posX!=goalPos.posX||myPos.posY!=goalPos.posY)   //도착점에 도착할때까지 반복
            {
                pastPathed[myPos.posX, myPos.posY] = 2;     //지나간 자리는 이동경로를 저장해주는 배열에 따로 저장
                
                for (int i = 0; i < Dir.Length; i++)        //네 방향 탐색
                {
                    int locationX = myPos.posX + Dir[i].posX;
                    int locationY = myPos.posY + Dir[i].posY;
                    if (maze[locationX, locationY] == 0 && pastPathed[locationX, locationY] == 0)   //해당 방향으로의 이동이 가능하다면
                    {
                        path newPath = CreatePath(myPos, Dir[i]);   //현 위치와 이동할 방향을 저장하는 노드 생성 후 푸쉬

                        Console.WriteLine("("+myPos.posX+","+myPos.posY+")");

                        Push(ref top, newPath);
                        //우치 최신화
                        myPos.posX = locationX;
                        myPos.posY = locationY;
                        if(myPos.posX == goalPos.posX && myPos.posY == goalPos.posY)
                        {
                            Console.WriteLine("(" + myPos.posX + "," + myPos.posY + ")");
                            Console.WriteLine("------------------------------------------");
                            return;
                        }
                        //더이상 탐색할 필요 없으니 반복문 종료
                        break;
                    }                   
                }

                //위의반복문이 종료되었을때의 위치에 대하여 미리 탐색

                int blockCount = 0;     //해당 위치를 기준으로 사방이 막혀있는걸 세어줄 변수
                for (int i = 0; i < Dir.Length; i++)
                {
                    //해당 방향이 막혀있다면 카운트 증가
                    if (maze[myPos.posX + Dir[i].posX, myPos.posY + Dir[i].posY] != 0 || pastPathed[myPos.posX + Dir[i].posX, myPos.posY + Dir[i].posY] != 0)                    
                        blockCount++;   
                }
                if(blockCount==4)   //카운트가 4라면, 사방이 막혀있다면
                {
                    pastPathed[myPos.posX, myPos.posY] = 2; //현위치를 왔던 곳으로 저장하고
                    Console.WriteLine("(" + myPos.posX + "," + myPos.posY + ")");
                    if (CheckStack(ref top))                //스택이 존재할때
                    {
                        myPos = Pop(ref top).position;          //이전 위치로 되돌리기
                        Console.WriteLine("Pop, return to " + myPos.posX + "," + myPos.posY);
                    }
                    else                 //스택이 비어있는 상태라면, 돌아갈 경로도 없고 사방이막혀있는 상태이므로 오류 출력
                    {
                        Console.WriteLine("Error : no Available path Exists!");
                        Console.WriteLine("------------------------------------------");
                        return;
                    }
                }
                
            }
            
        }

        public path CreatePath(pos currentPos,pos Dir)        //노드 생성, 값 대입
        {
            var node = new path();
            node.position.posX = currentPos.posX; node.position.posY = currentPos.posY;
            node.dir.posX = Dir.posX; node.dir.posY = Dir.posY;
            node.next = null;
            return node;
        }


        public void Push(ref path top, path newNode)     //푸쉬, 스택의 맨 위에 노드 추가
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

        public path Pop(ref path top)       //top을 제거 후 반환
        {
            path temp = top;
            top = top.next;
            return temp;
        }

        public bool CheckStack(ref path top)
        {
            if (top != null)
                return true;
            else
                return false;
        }




        public void showMaze(int row, int col, int[,] maze)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                    Console.Write(maze[i, j] + "   ");
                Console.WriteLine("");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Program instance = new Program();
            mazeStack sol = new mazeStack();
            path top = null;

            int rowM = 6;
            int colM = 6;

            int[,] maze =
            {
                {0,0,1,0,0,0},
                {0,1,0,0,0,0},
                {0,0,0,1,1,0},
                {0,1,0,1,0,0},
                {1,0,0,0,1,1},
                {0,0,0,0,0,0}
            };


            int[,] newMap = new int[rowM + 2, colM + 2];        //메이즈탐색용 새로운 배열, 상하좌우가 막혀 맵 밖으로의 탐색을 막음
            for (int i = 0; i < rowM + 2; i++)
            {
                for (int j = 0; j < colM + 2; j++)
                {
                    if (i == 0 || i == rowM + 1 || j == 0 || j == colM + 1)
                        newMap[i, j] = 1;
                    else
                        newMap[i, j] = maze[i - 1, j - 1];
                    
                }
            }

            pos POS;
            POS.posX = 1; POS.posY = 1;
            pos GOAL;
            GOAL.posX = 6;GOAL.posY = 6;


            sol.showMaze(8, 8, newMap);
            sol.mazeSolution(newMap, POS, GOAL, ref top);
        }

        
    }
}
