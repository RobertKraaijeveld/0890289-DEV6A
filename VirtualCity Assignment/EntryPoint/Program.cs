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
    private static IEnumerable<Vector2> mergeSort
    (Vector2 house, IEnumerable<Vector2>buildings, 
    int bLeft, int bRight)
    {
        //I dont have to do any tricky converting to ints-business, because mergeSort() takes element POSITIONS rather than VALUES.
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
            return merge(house, buildings, bLeft, bMiddle, bRight);
        }
        return null;
    }

    private static IEnumerable<Vector2> merge
    (Vector2 house, IEnumerable<Vector2> buildings,
    int bLeft, int bMiddle, int bRight)
    {
        Vector2[] buildingsArray = buildings.ToArray();
        //Set ends of halfs, like usual
        int n1 = bMiddle - bLeft + 1;
        int n2 = bRight - bMiddle;
        //I can use the .getEnumerator call on an array, so i just use arrays here and make them IENumerable later!
        Vector2[] leftpart = new Vector2[n1 + 1];
        Vector2[] rightpart = new Vector2[n2 + 1];
        //We loop through the left half.
        for (int i = 0; i < n1; i++)
        {
            //We put all of the left hand side  Vector 2elements of the original parameter-array into our new leftArray, 
            //stopping at the end of our iterator n1(which is equal to the middle of the param-array)
            leftpart[i] = buildingsArray[bLeft + i];
        }

        //And we fill the right half in the not-so-exact same way.
        for (int j = 0; j < n2; j++)
        {
            leftpart[j] = buildingsArray[bMiddle + j + 1];
        }
        //Now for something more complicated than the original merge: Setting the last element of left and right to something VERY high.
        leftpart[n1] = new Vector2(Int32.MaxValue, Int32.MaxValue);
        rightpart[n2] = new Vector2(Int32.MaxValue, Int32.MaxValue);
        //placeholders for the first values of leftpart and rightpart. Standard procedure. 
        int i2 = 0;
        int j2 = 0;

        //We (I, actually) loop through the entire array of Vector2
        for (int k = bLeft; k <= bRight; k++)
        {
            //No ordinary check of size, but check of the result of distance()!
            //And I can simply reference the buildingsArray of Vector2. Also, Distance is a static method so we call it on the Vector2 class rather than an instance of Vector2.
            if (Vector2.Distance(leftpart[i2], buildingsArray[i2]) <= Vector2.Distance(rightpart[j2], buildingsArray[i2]))
            {
                //The element in param-array that we are currently iterating on is set to the first element of leftArray.
                //We do this to make sure the array is sorted from small to larger; leftArray[i2] turned out to be smaller than rightArray[j2], so we set array[k] to that (smaller) value and loop again.
                buildingsArray[k] = leftpart[i2];
                //i2 is incremented, so in the next loop we will be looking at the second element of the leftarray versus the first element of the rightarray.
                i2++;
            }
            //If the value of the first element of the leftarray is bigger than or equal to the value of the first element of the left array:
            else
            {
                //The element in param-array that we are currently iterating on is set to the first element of rightArray.
                //We do this to make sure the array is sorted from small to larger; rightArray[j2] turned out to be smaller than leftArray[i2], so we set array[k] to that (smaller) value and loop again.
                buildingsArray[k] = rightpart[j2];
                //j2 is incremented, so in the next loop we will be looking at the second element of the leftarray versus the first element of the rightarray.
                j2++;
            }
        }
        //Last but not least, convert our buildingsArray back to the IEnumerable<Vector2> it once was!
        return buildingsArray.AsEnumerable();
    }
    /*
    static void merge(int[] array, int l, int m, int r)
    {
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
      //int bLeft, int bRight
      Vector2[] b = specialBuildings.ToArray();
      return mergeSort(house, specialBuildings, 0, b.Length - 1);
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
