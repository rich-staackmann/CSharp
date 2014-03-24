using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;  

namespace AlgorithmsAndDataStructs
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedListNode<int> first = new LinkedListNode<int> (3);
            LinkedListNode<int> middle = new LinkedListNode<int> (5);
            LinkedListNode<int> last = new LinkedListNode<int> (7);

            first.Next = middle;
            middle.Next = last;

            PrintList(first);
            Console.WriteLine();
            
            LinkedList<int> list = new LinkedList<int>();
            list.AddFirst(5);
            list.AddFirst(7);
            list.RemoveFirst();
            Console.WriteLine(list.First());
            Console.WriteLine();

            PostFix();

            Console.Read(); //keeps console open
        }

        static void PrintList(LinkedListNode<int> node)
        {
            while (node != null)
            {
                Console.WriteLine(node.Value);
                node = node.Next;
            }
        }

        //Postfix: 5 2 +
        static void PostFix()
        {
            string exp = "567*+1-";
            Stack<int> stack = new Stack<int>();
            int value = 0;

            foreach (char s in exp)
            {
                if (int.TryParse(s.ToString(), out value))
                {
                    stack.Push(value);
                }
                else
                {
                    int rhs = stack.Pop();
                    int lhs = stack.Pop();

                    switch (s.ToString())
                    {
                        case "+":
                            stack.Push(lhs + rhs);
                            break;
                        case "-":
                            stack.Push(lhs - rhs);
                            break;
                        case "*":
                            stack.Push(lhs * rhs);
                            break;
                        case "/":
                            stack.Push(lhs / rhs);
                            break;
                        case "%":
                            stack.Push(lhs % rhs);
                            break;
                        default:
                            throw new ArgumentException(string.Format("Unrecognized token: {0}", s.ToString()));
                    }
                }
            }

            Console.WriteLine(stack.Peek());
        }
    }
}
