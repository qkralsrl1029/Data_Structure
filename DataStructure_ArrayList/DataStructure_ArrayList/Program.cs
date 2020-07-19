using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure_ArrayList
{
    class Program
    {
        static void Main(string[] args)
        {
            int size=10;
            _arrayList node = new _arrayList(size);


            node.addLast(10);
            node.addLast(20);
            node.addLast(30);
            node.addLast(40);
            node.addLast(50);
            node.addLast(60);

           

            node.insert(3, 35);
            node.showList();

            Console.WriteLine("");

            node.deleteFrom(3);
            node.showList();
            Console.WriteLine("");

            node.deleteFrom(3);
            node.showList();
            Console.WriteLine(node.sizeOf());


        }
    }

    class _arrayList
    {
        int[] data;
        int currentIndex = 0;


        public _arrayList(int size)     //생성자
        {
            data = new int[size];
        }

        public void showList()          //배열리스트 정보 출력
        {
            for (int i = 0; i <currentIndex; i++)
            {
                Console.WriteLine(i + "번째 인자" + data[i]);
            }
        }

        public int sizeOf()             //리스트의 크기 반환
        {
            return currentIndex;
        }
        public void addLast(int _data)      //리스트의 끝에 데이터 추가
        {
            data[currentIndex++] = _data;
        }

        public void insert(int index,int _data)     //리스트 중간에 데이터 추가
        {
            for (int i = currentIndex; i>index; i--)
            {
                data[i] = data[i - 1];
            }
            data[index] = _data;
            currentIndex++;
        }

        public void delete()                    //리스트 마지막에 있는 데이터 삭제
        {
            data[--currentIndex] = 0;
        }

        public void deleteFrom(int index)       //리스트 중간에 있는 원소 제거
        {
            for (int i = index; i < currentIndex-1; i++)
            {
                data[i] = data[i + 1];
            }
            currentIndex--;
        }

    }
}
