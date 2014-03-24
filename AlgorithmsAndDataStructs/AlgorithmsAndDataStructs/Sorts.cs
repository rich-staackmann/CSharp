using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAndDataStructs
{
    public class Sorts<T> where T : IComparable<T>
    {
        public static void BubbleSort(T[] arr)
        {
            bool changed = true;
            T temp;

            do
            {
                changed = false;
                for (int i = 1; i < arr.Length; i++)
                {
                    if (arr[i - 1].CompareTo(arr[i]) > 0)
                    {
                        temp = arr[i];
                        arr[i] = arr[i - 1];
                        arr[i - 1] = temp;
                        changed = true;
                    }
                }
            } while (changed);
            //just print the items
            foreach (T n in arr)
                Console.Write(n + " ");
        }
    }
}
