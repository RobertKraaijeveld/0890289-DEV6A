using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntryPoint
{
#if WINDOWS || LINUX
  public static class Program
  {
    [STAThread]
    static void Main()
    {
      var fullscreen = false;
      read_input:
      switch (Microsoft.VisualBasic.Interaction.InputBox("Which assignment shall run next? (1, 2, 3, 4, or q for quit)", "Choose assignment", VirtualCity.GetInitialValue()))
      {
        case "1":
          using (var game = VirtualCity.RunAssignment1(SortSpecialBuildingsByDistance, fullscreen))
            game.Run();
          break;
        case "2":
          using (var game = VirtualCity.RunAssignment2(FindSpecialBuildingsWithinDistanceFromHouse, fullscreen))
            game.Run();
          break;
        case "3":
          using (var game = VirtualCity.RunAssignment3(FindRoute, fullscreen))
            game.Run();
          break;
        case "4":
          using (var game = VirtualCity.RunAssignment4(FindRoutesToAll, fullscreen))
            game.Run();
          break;
        case "q":
          return;
      }
      goto read_input;
    }

    /*
     * Student Stuff starts here. I made seperate mergeSort and merge methods, just like in the example. 
     * I left all the comments I made to my own brain during coding, for educational- and comic relief-purposes.
     * --------------------------------------------------
     * ROBERT KRAAIJEVELD (0890289@hr.nl) INF2C: 30-11-15 A.D
     * --------------------------------------------------
     */ 

    //Has to return IEnumerable of Vector2. OR DOES IT
    //Sort specialbuildings by their distance to the house, ascending from small to large. -> How? Vector2.Distance(house, sb), which returns a float!
    private static void mergeSort
    (Vector2 house, IEnumerable<Vector2>buildings, 
    int bLeft, int bRight)
    {
        //I dont have to do any tricking converting to ints, because mergeSort() takes element POSITIONS rather than VALUES.
        //Makes sense, since we are comparing positions rather than values.
        if (bLeft > bRight)
        {
            //Knowing this, we run through the method like we always do.
            int bMiddle = (bLeft + bRight) / 2;
            //One exception being that we pass an extra parameter. This parameter is used later in merge() 
            //Left half
            mergeSort(house, buildings, bLeft, bMiddle);
            //Right half and merge!
            mergeSort(house, buildings, bMiddle + 1, bRight);
            merge(house, buildings, bLeft, bMiddle, bRight);
        }
    }

    private static void merge
    (Vector2 house, IEnumerable<Vector2> buildings,
    int bLeft, int bMiddle, int bRight)
    {
        //Set ends of halfs, like usual
        int n1 = bMiddle - bLeft + 1;
        int n2 = bRight - bMiddle;
        //NOW, for the real work, treating IEnumberables as arrays... righhhttt...
        //Oh boy, look at the time!
    }

    /*
    static void merge(int[] array, int l, int m, int r)
    {
        //This number represents the end of the left half.
        int n1 = m - l + 1;
        //This number represents the end of the right half.
        int n2 = r - m;
        //We create a new array, the size of n1 + 1
        int[] leftpart = new int[n1 + 1];
        //We create a new array, the size of n2 + 1
        int[] rightpart = new int[n2 + 1];
        //We loop through the left half.
        for (int i = 0; i < n1; i++)
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
     */

    private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house, IEnumerable<Vector2> specialBuildings)
    {
      //return mergeSort(house, buildings, 
    }

    private static IEnumerable<IEnumerable<Vector2>> FindSpecialBuildingsWithinDistanceFromHouse(
      IEnumerable<Vector2> specialBuildings, 
      IEnumerable<Tuple<Vector2, float>> housesAndDistances)
    {
      return
          from h in housesAndDistances
          select
            from s in specialBuildings
            where Vector2.Distance(h.Item1, s) <= h.Item2
            select s;
    }

    private static IEnumerable<Tuple<Vector2, Vector2>> FindRoute(Vector2 startingBuilding, 
      Vector2 destinationBuilding, IEnumerable<Tuple<Vector2, Vector2>> roads)
    {
      var startingRoad = roads.Where(x => x.Item1.Equals(startingBuilding)).First();
      List<Tuple<Vector2, Vector2>> fakeBestPath = new List<Tuple<Vector2, Vector2>>() { startingRoad };
      var prevRoad = startingRoad;
      for (int i = 0; i < 30; i++)
      {
        prevRoad = (roads.Where(x => x.Item1.Equals(prevRoad.Item2)).OrderBy(x => Vector2.Distance(x.Item2, destinationBuilding)).First());
        fakeBestPath.Add(prevRoad);
      }
      return fakeBestPath;
    }

    private static IEnumerable<IEnumerable<Tuple<Vector2, Vector2>>> FindRoutesToAll(Vector2 startingBuilding, 
      IEnumerable<Vector2> destinationBuildings, IEnumerable<Tuple<Vector2, Vector2>> roads)
    {
      List<List<Tuple<Vector2, Vector2>>> result = new List<List<Tuple<Vector2, Vector2>>>();
      foreach (var d in destinationBuildings)
      {
        var startingRoad = roads.Where(x => x.Item1.Equals(startingBuilding)).First();
        List<Tuple<Vector2, Vector2>> fakeBestPath = new List<Tuple<Vector2, Vector2>>() { startingRoad };
        var prevRoad = startingRoad;
        for (int i = 0; i < 30; i++)
        {
          prevRoad = (roads.Where(x => x.Item1.Equals(prevRoad.Item2)).OrderBy(x => Vector2.Distance(x.Item2, d)).First());
          fakeBestPath.Add(prevRoad);
        }
        result.Add(fakeBestPath);
      }
      return result;
    }
  }
#endif
}
