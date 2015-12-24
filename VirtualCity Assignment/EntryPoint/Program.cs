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
						//We insert the building that is at that spot in the unsorted, unchanged array, so we know we have the right building 							for the right, sorted position.
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
    }


    /*
    * TREES
    */

        /********************
        * CLASS DEFINITIONS 
        *********************/

        interface MiniTree<T>
        {
            Boolean isEmpty();
            T getValue();
            MiniTree<T> getLeftMTree();
            MiniTree<T> getRightMTree();
            T getXValue();
            T getYValue();
        }

        //since node and emptynode both inherit from the abstract interface minitree, they can both be used 
        class Node<T> : MiniTree<T>
        {
            T Xvalue;
            T Yvalue;
            MiniTree<T> left;
            MiniTree<T> right;

            public Boolean isEmpty()
            {
                return false;
            }

            //getters
            public T getXValue()
            {
                return Xvalue;
            }

            //getters
            public T getYValue()
            {
                return Yvalue;
            }

            public MiniTree<T> getLeftMTree()
            {
                return left;
            }

            public MiniTree<T> getRightMTree()
            {
                return right;
            }


            //constructor
            public Node(T xval, T yval, MiniTree<T> l, MiniTree<T> r)
            {
                Xvalue = xval;
                Yvalue = yval;
                left = l;
                right = r;
            }

        }

        //since node and emptynode both inherit from the abstract interface minitree, they can both be used 
        class EmptyNode<T> : MiniTree<T>
        {
            MiniTree<T> left;
            MiniTree<T> right;

            public Boolean isEmpty()
            {
                return true;
            }

            //getters
            public T getXValue()
            {
                throw new NotImplementedException();
            }

            public T getYValue()
            {
                throw new NotImplementedException();
            }


            public MiniTree<T> getLeftMTree()
            {
                throw new NotImplementedException();
            }

            public MiniTree<T> getRightMTree()
            {
                throw new NotImplementedException();
            }
            //(explicit) constructor is not specified in the interface contract and thusly not necessary
            //same goes for setters
        }

    public static MiniTree<float> insertIntoKD(float valueX, float valueY, MiniTree<float> root)
    {
        bool sortOnX = true;
        if(root.isEmpty() == false)
        {
            //its important that we always insert BOTH values, but sort on only one of them.
            if(sortOnX == true)
            {
                if(valueX > root.getXValue())
                {
                    return new Node<float>(root.getXValue(), insertIntoKD(valueX, root.getLeftMTree()), root.getRightMTree());
                }
                else 
                {
                    return new Node<float>(root.getXValue(), root.getLeftMTree(), insertIntoKD(valueX, root.getRightMTree()));
                }
            }
            else
            {
                if(valueY > root.getXValue())
                {
                    return new Node<float>(root.getYValue(), insertIntoKD(valueY, root.getLeftMTree()), root.getRightMTree());
                }
                else
                {
                    return new Node<float>(root.getYValue(), root.getLeftMTree(), insertIntoKD(valueY, root.getRightMTree()));
                }
            }
        }
        else
            return root;
    }

    private static IEnumerable<IEnumerable<Vector2>> FindSpecialBuildingsWithinDistanceFromHouse(
      IEnumerable<Vector2> specialBuildings, 
      IEnumerable<Tuple<Vector2, float>> housesAndDistances)
    {
        var Tree;
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
