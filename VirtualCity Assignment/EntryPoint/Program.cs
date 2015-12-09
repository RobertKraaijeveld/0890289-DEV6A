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
		public static void MergeSort(float[] listToSort, int begin, int end)
		{
			if (begin < end)
			{
				int mid = (end + begin) / 2;
				MergeSort(listToSort, begin, mid);
				MergeSort(listToSort, mid + 1, end);
				Merge(listToSort, begin, end, mid);
			}
		}

		public static void Merge(float[] arrayToMerge, int begin, int end, int mid)
		{
			//Define end of both halves of the originalArray
			int half1 = mid - begin + 1;
			//Notice half1 gets a +1; This is because we have to appoint an actual element as middle, 
			//we cant just draw a line in space.
			int half2 = end - mid;

			//Create an array for each half of the original array
			float[] arrayLeft = new float[half1 + 1];
			float[] arrayRight = new float[half2 + 1];

<<<<<<< HEAD
			//Fill both halves with the values of original array until their half ends.
			for (int i = 0; i < half1; i++)
			{
				arrayLeft[i] = arrayToMerge[begin + i];
			}
			//Again, +1 here because Left gets the actual middle element; arrayRight thusly has to start one element later.
			for (int i = 0; i < half2; i++)
			{
				arrayRight[i] = arrayToMerge[mid + i + 1];
			}

			//We set the very last element in the arrays to infinity, so that when (not if, when) we end up with a 1 element-array,
			//We make sure the left element is always smaller and the array is therefore considered sorted.
			arrayLeft[half1] = float.MaxValue;
			arrayRight[half2] = float.MaxValue;

		    //Define indexes that we will be using in the later loop
			int leftIndex = 0;
			int rightIndex = 0;

			//Loop the entire length of the original array
			for (int i = begin; i <= end; i++)
			{
				//we check if the element in the left array is smaller or equal to the element in the right array.
				if (arrayLeft[leftIndex] <= arrayRight[rightIndex])
				{
					//If so, originalArray[i] gets set to that element because in order for the array to be sorted properly,
					//The smaller elements go on the lefthand-side
					arrayToMerge[i] = arrayLeft[leftIndex];
					//We increment the leftIndex so we dont look at the same element twice; We go to the next element instead.
					leftIndex++;
				}
				else
				{
					//Same goes if the right value turns out to be smaller than the left value.
					arrayToMerge[i] = arrayRight[rightIndex];
					rightIndex++;
				}
			}
		}

    
    private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house, IEnumerable<Vector2> specialBuildings)
    {
			//converting ienumerable to list
			List<Vector2> SpecialBuildingsList = specialBuildings.ToList();

			//Creating 2 floatarrays, the size of the SpecialBuildingsList. (We are not going to have more distances than the amount of buildings)
			float[] SortedDistances = new float[SpecialBuildingsList.Count];
			float[] UnsortedDistances = new float[SpecialBuildingsList.Count];

			//Fill Both arrays with the distance between the house vector and the iterated specialBuilding
			for (int i = 0; i < SpecialBuildingsList.Count; i++)
			{
				SortedDistances[i] = Vector2.Distance(house, SpecialBuildingsList.ElementAt(i));
				UnsortedDistances[i] =  Vector2.Distance(house, SpecialBuildingsList.ElementAt(i));
			}

			//Sort the values of 1 of the Arrays; SortedDistances.
			MergeSort(SortedDistances, 0, SortedDistances.Length - 1);

			//This array is going to contain our final vectors, sorted by their distance to the house :)
			List<Vector2> finalListOfVectors = new List<Vector2>();

			//We loop through both distance-arrays.
			for (int i = 0; i < SortedDistances.Count(); i++)
			{
				for (int j = 0; j < UnsortedDistances.Count(); j++)
				{
					//Explanation below.
					if (SortedDistances[i] == UnsortedDistances[j])
					{
						//Here comes the interesting part. 
						//Remember: i stands for right place, j stands for right building.
						//At the place in the finalArray where the building is supposed to go if we want it ordered by distance,
						//We insert the building that is at that spot in the unsorted, unchanged array, so we know we have the right building for the right, sorted position.
						//As I like to say: Position i, building j!
						finalListOfVectors.Insert(i, SpecialBuildingsList.ElementAt(j));
						//We sorta "delete" the building J we just looked at, so we don get doubles.
						UnsortedDistances[j] = 0;
					}
				}
			}
			//we make a ienumerable of vectors that are sorted 
			IEnumerable<Vector2> sortedBuildings = finalListOfVectors as IEnumerable<Vector2>;

			//we return the list
			return sortedBuildings;
=======
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
            if (Vector2.Distance(leftpart[i2], house) <= Vector2.Distance(rightpart[j2], house))
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
    
    private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house, IEnumerable<Vector2> specialBuildings)
    {
      Vector2[] b = specialBuildings.ToArray();
      return mergeSort(house, specialBuildings, 0, b.Length - 1);
>>>>>>> 786ed07ba5277eb08b89ed3adf256a4d3db26609
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
