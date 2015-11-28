using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgos
{
    class Program
    {

        static void insertionSort(int[] array)
        {
            for (int s = 1; s < array.Length; s++)
            {
                //The 'key' is the holder for the right-hand, smaller value
                int key = array[s];
                //i will be the holder for the left-hand, greater value 
                int i = s - 1;
                //if i is smaller than 0 the array is 1 element or smaller. If i is smaller than key, 
                //the right hand value is bigger and no re-ordering is needed.
                while (i > -1 && array[i] > key)
                {
                    //We copy the first element that we found to be bigger than the second element to the space that was originally meant for the 2nd element.
                    //We can do this because we backed-up the 2nd elements value in 'key'.
                    array[i + 1] = array[i];
                    //We decrement i. if we did not do this, the last step out of the while loop would set the smaller element to the same element as the larger element that we just shifted!
                    i--;
                }
                //finally, we set A[i + 1] to key. If the values were already sorted, this would be equal to saying A[S] =  key; nothing changes! 
                //But if the elements were not sorted, i is decremented and thusly the smaller value shifts one position to the left.
                array[i + 1] = key;
            }
        }

        //static void mergeSort();
        //static void merge();

        static void Main(string[] args)
        {
            
            int[] myArray = new int[20]{5,3,8,5,1,3,5,7,9,1,2,3,4,6,7,9,0,1,2,3};
            
            insertionSort(myArray);

            for (int i = 0; i < myArray.Length; i++)
            {
                Console.WriteLine(myArray[i]);
            }
            Console.ReadLine();
        }
    }
}
