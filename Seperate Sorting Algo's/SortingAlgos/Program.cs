using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgos
{
    class Program
    {

        /*
         * By Robert Kraaijeveld: 27-11-15 | 28-11-15.
         */ 

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
                //We repeat the above steps for all the elements of the array.
            }
        }

        static void mergeSort(int[] array, int l, int r)
        {
            //If the value of element l isnt smaller than the value of element r, the array has only one element left and it is considered already sorted:
            //An array is considered unsorted if it has 2 or more element, the left hand element being bigger than the right hand element.
            if (l < r)
            {
                //We compute the middle of the array.
                int m = (l + r) / 2;
                //We merge sort the left half of the array. This call will then sort the left half of that left half, etc etc until only 1 element remains of that half.
                mergeSort(array, l, m);
                //The same goes for the right half; we call merge sort on that half, which will call merge sort on the half of that half, etc.
                mergeSort(array, m + 1, r);
                //Finally:

                merge(array, l, m, r);
            }
        }

        static void merge(int[] array, int l, int m, int r)
        {
            //This number represents the end of the left half.
            int n1 = m - l + 1;
            //This number represents the end of the right half.
            int n2 = r -m;
            //We create a new array, the size of n1 + 1
            int[] leftpart = new int[n1 + 1];
            //We create a new array, the size of n2 + 1
            int[] rightpart = new int[n2 + 1];
            //We loop through the left half.
            for (int i = 0 ; i < n1; i++)
            {
                //We put all of the left hand side elements of the original parameter-array into our new leftArray, 
                //stopping at the end of n1(which is equal to the middle of the param-array)
                leftpart[i] = array[l + i];
            }
            //We loop through the right half.
            for (int j = 0; j < n2; j++)
            {
                //We fill the new rightarray with the right half of our param array, just like we did earlier.
                rightpart[j] = array[m + j + 1];
            }
            //We fill the last element of the array with the maximum value a normal 32 bit Integer can take, 2147483648, 
            //in case our array only has one element the last element always needs to be the bigger one in order for the array to be considered sorted; 
            //nothing can be bigger than Int32.MaxValue (as far as 32-bits ints are concerned!)
            leftpart[n1] = Int32.MaxValue;
            rightpart[n2] = Int32.MaxValue;

            //We create placeholders for the (now) first elements of our left- and rightarrays. I declared them as i2 and j2 because i and j are already declared
            //in the child scope of the 2 previous for-loops.
            int i2 = 0;
            int j2 = 0;
            //We loop through the length of the entire original array.
            for (int k = l; k <= r; k++)
            {
                //If the value of the first element of the rightarray is bigger than or equal to the value of the first element of the left array:
                if (leftpart[i2] <= rightpart[j2])
                {
                    //The element in param-array that we are currently iterating on is set to the first element of leftArray.
                    //We do this to make sure the array is sorted from small to larger; leftArray[i2] turned out to be smaller than rightArray[j2], so we set array[k] to that (smaller) value and loop again.
                    array[k] = leftpart[i2];
                    //i2 is incremented, so in the next loop we will be looking at the second element of the leftarray versus the first element of the rightarray.
                    i2++;
                }
                //If the value of the first element of the leftarray is bigger than or equal to the value of the first element of the left array:
                else
                {
                    //The element in param-array that we are currently iterating on is set to the first element of rightArray.
                    //We do this to make sure the array is sorted from small to larger; rightArray[j2] turned out to be smaller than leftArray[i2], so we set array[k] to that (smaller) value and loop again.
                    array[k] = rightpart[j2];
                    //j2 is incremented, so in the next loop we will be looking at the second element of the leftarray versus the first element of the rightarray.
                    j2++;
                }
            }
        }

        static void Main(string[] args)
        {
            int[] myArray = new int[20]{5,3,8,5,1,3,5,7,9,1,2,3,4,6,7,9,0,1,2,3};

            Console.WriteLine("Original array:");
            for (int i = 0; i < myArray.Length; i++)
            {
                Console.Write(" " + myArray[i] + " ");
            }

            mergeSort(myArray, 0, myArray.Length - 1);
            insertionSort(myArray);
            Console.WriteLine("");
            Console.WriteLine("Array after merge sort:");
            for (int i = 0; i < myArray.Length; i++)
            {
                Console.Write(" " + myArray[i] + " ");
            }
            Console.ReadLine();
            //Victory is ours!
        }
    }
}
