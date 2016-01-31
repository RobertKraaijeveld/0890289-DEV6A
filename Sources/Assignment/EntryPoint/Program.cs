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
     * Student Stuff starts here.
     * --------------------------------------------------
     * ROBERT KRAAIJEVELD (0890289@hr.nl) INF2C: 30-11-15 A.D
     * --------------------------------------------------
     */



        /* *************
        * MERGESORT
        * ************
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
                UnsortedDistances[i] = Vector2.Distance(house, SpecialBuildingsList.ElementAt(i));
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
                        //We insert the building that is at that spot in the unsorted, unchanged array, so we know we have the right building                           for the right, sorted position.
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


        /********************
        * TREES OF ALL SHAPES AND SIZES (not really though)
        *********************/

        interface MiniTree<T>
        {
            Boolean isEmpty();

            MiniTree<T> getLeftMTree();

            MiniTree<T> getRightMTree();

            Boolean sortedOnX();

            T getVector();
        }

        //since node and emptynode both inherit from the abstract interface minitree, they can both be used
        class Node<T> : MiniTree<T>
        {
            T Vector;
            MiniTree<T> left;
            MiniTree<T> right;
            Boolean sortage;

            public Boolean sortedOnX()
            {
                return sortage;
            }

            public Boolean isEmpty()
            {
                return false;
            }

            //getters
            public T getVector()
            {
                return Vector;
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
            public Node(T V, MiniTree<T> l, MiniTree<T> r, Boolean s)
            {
                Vector = V;
                left = l;
                right = r;
                sortage = s;
            }

        }

        //since node and emptynode both inherit from the abstract interface minitree, they can both be used
        class EmptyNode<T> : MiniTree<T>
        {
            public Boolean sortedOnX()
            {
                return false;
            }

            public Boolean isEmpty()
            {
                return true;
            }

            //getters
            public T getVector()
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
            //(explicit) constructor for an emptynode is not specified in the interface contract and thusly not necessary
            //same goes for setters
        }


        //Call this with isParentX =true!
        static MiniTree<Vector2> insertIntoKD(Vector2 Vector, MiniTree<Vector2> root, bool isParentX)
        {
            if (root.isEmpty())
            {
                if (isParentX)
                    return new Node<Vector2>(Vector, new EmptyNode<Vector2>(), new EmptyNode<Vector2>(), false);
                else
                    return new Node<Vector2>(Vector, new EmptyNode<Vector2>(), new EmptyNode<Vector2>(), true);
            }
            if (root.sortedOnX())
            {
                if (root.getVector() == Vector)
                    return root;

                if (Vector.X < root.getVector().X)
                    return new Node<Vector2>(root.getVector(), insertIntoKD(Vector, root.getLeftMTree(), root.sortedOnX()), root.getRightMTree(), true);
                else
                    return new Node<Vector2>(root.getVector(), root.getLeftMTree(), insertIntoKD(Vector, root.getRightMTree(), root.sortedOnX()), true);
            }
            else
            {
                if (root.getVector() == Vector)
                    return root;

                if (Vector.Y < root.getVector().Y)
                    return new Node<Vector2>(root.getVector(), insertIntoKD(Vector, root.getLeftMTree(), root.sortedOnX()), root.getRightMTree(), false);
                else
                    return new Node<Vector2>(root.getVector(), root.getLeftMTree(), insertIntoKD(Vector, root.getRightMTree(), root.sortedOnX()), false);
            }
        }
        
        //Rangesearch
        static void rangeSearch(MiniTree<Vector2> root, Vector2 houseVector, float radius, List<Vector2> returnList)
        {
            if (root.isEmpty() == false)
            {
                if (root.sortedOnX() == true)
                {
                    if(Math.Abs(houseVector.X - root.getVector().X) <= radius)
                    {
                        //Euclidean check for good measure (haha)
                        if(Vector2.Distance(root.getVector(), houseVector) <= radius)
                            returnList.Add(root.getVector());

                        //Be thorough and searche the rest too
                        rangeSearch(root.getLeftMTree(), houseVector, radius, returnList);
                        rangeSearch(root.getRightMTree(), houseVector, radius, returnList);
                    }  
                    else if (root.getVector().X >= (houseVector.X + radius))
                    {
                        Console.WriteLine(root.getVector().X + " is bigger than " + (houseVector.X + radius)  + " so we go left");
                        rangeSearch(root.getLeftMTree(), houseVector, radius, returnList);
                    }
                    else if (root.getVector().X <= (houseVector.X - radius))
                    {
                        Console.WriteLine(root.getVector().X + " is smaller than " + (houseVector.X + radius) + " so we go right");
                        rangeSearch(root.getRightMTree(), houseVector, radius, returnList);
                    }
                    else
                    {
                        Console.WriteLine("Not a single matching node found");
                    }
                }
                else 
                {
                    if(Math.Abs(houseVector.Y - root.getVector().Y) <= radius)
                    {
                        //Euclidean check for good measure (haha)
                        if(Vector2.Distance(root.getVector(), houseVector) <= radius)
                            returnList.Add(root.getVector());

                        //Be thorough and searche the rest too
                        rangeSearch(root.getLeftMTree(), houseVector, radius, returnList);
                        rangeSearch(root.getRightMTree(), houseVector, radius, returnList);
                    }
                    else if (root.getVector().Y > (houseVector.Y + radius) )
                    {
                        Console.WriteLine(root.getVector().Y + " is bigger than " + (houseVector.Y + radius) + " so we go left");
                        rangeSearch(root.getLeftMTree(), houseVector, radius, returnList);
                    }
                    else if (root.getVector().Y < (houseVector.Y - radius))
                    {
                        Console.WriteLine(root.getVector().Y + " is smaller than " + (houseVector.Y + radius) + " so we go right");
                        rangeSearch(root.getRightMTree(), houseVector, radius, returnList);
                    }
                    else
                    {
                        Console.WriteLine("Not a single matching node found");
                    }
                }
            }
            else
            {
                Console.WriteLine("Empty tree");
            }
        }
            


        /**********************
        * END TREES
        ***********************/


        /*************************
        * GRAPH  HELPERS
        **************************/

        public class writableTuple<T1, T2>
        {
            public T1 Item1;
            public T2 Item2;

            public writableTuple(T1 Item1Param, T2 Item2Param)
            {
                Item1 = Item1Param; 
                Item2 = Item2Param;
            }
        }

        //PATHFINDERS
        public static List<Tuple<Vector2, Vector2>> createPathListReverse(List<Vector2> nodes)
        {
            List<Tuple<Vector2, Vector2>> result = new List<Tuple<Vector2, Vector2>>();

            // For each node in the nodelist
            for (int i = nodes.Count() - 1; i > 0; i--)
            {
                result.Add(new Tuple<Vector2, Vector2>(nodes[i - 1], nodes[i]));
            }
            return result;
        }

        static void pathcalc(int[] pred, int startbuilding, int curbuilding, List<int> result)
        {
            if (pred[curbuilding] != startbuilding)
            {
                Console.WriteLine(curbuilding);
                result.Add(pred[curbuilding]);
                pathcalc(pred, startbuilding, pred[curbuilding], result);
            }
            else
            {
                result.Add(pred[startbuilding]);
            }
        }

        //Aanpassen voor gebruik in list of tuples of ints and floats
        static int getSmallestItem(List<writableTuple<int, float>> list)
        {
            List<float> distancesList = new List<float>();
            foreach (writableTuple<int, float> tuple in list)
            {
                distancesList.Add(tuple.Item2);
            }
            //De index van het kleinste element in de distancelist, staat gelijk aan de index van het de tuple met kleinste floatvalue in list
            int nodeWithSmallestDistanceInCache = 
                allNodes.FirstOrDefault(x => x.Value == list.ElementAt
                (distancesList.IndexOf(distancesList.Min())).Item1).Value;
            
            return nodeWithSmallestDistanceInCache;
        }

        static void removeSmallestItem(List<writableTuple<int, float>> list, int identifierOfItem)
        {
            //Remove each (will be only one) element that matches the lambda predicate.
            list.RemoveAll(item => item.Item1 == identifierOfItem);
        }

        static void setActualDistanceOfNeighbour(List<writableTuple<int, float>> list, int tupleToBeModified, float valueToBeSet)
        {
            foreach (writableTuple<int, float> tuple in list)
            {
                if (tuple.Item1 == tupleToBeModified)
                    tuple.Item2 = valueToBeSet;
            }
        }

        /*************************
        * GRAPH SETUP
        ***************************/

        static Dictionary<Vector2, int> allNodes = new Dictionary<Vector2, int>();
        //Id and Road Connection
        static void createGraph(List<Tuple<Vector2, Vector2>> roads)
        {
            int roadNumber = 0;
            foreach (var road in roads)
            {
                if (!allNodes.ContainsKey(road.Item1))
                {
                    allNodes.Add(road.Item1, roadNumber++);
                }
                if (!allNodes.ContainsKey(road.Item2))
                {
                    allNodes.Add(road.Item2, roadNumber++);
                }
            }
        }

        private static IEnumerable<Tuple<Vector2, Vector2>> Dijkstras
        (Vector2 startingBuilding, Vector2 destinationBuilding, IEnumerable<Tuple<Vector2, Vector2>> roads)
        {
            Console.WriteLine("Startingbuilding: " + startingBuilding);

            createGraph(roads.ToList());
            int amountOfNodes = allNodes.Count;

            Console.WriteLine("Total amount of nodes: " + amountOfNodes);

            //Create adjacency matrix and set staring values and set nodes as unvisited.
            //Our matrixs' rows are made up of arrays of ints and will only contain neighbours, drastically improving performance
            int[][] neighBoursMatrix = new int[amountOfNodes][];
            int[] pred = new int[amountOfNodes];
            pred[allNodes[startingBuilding]] = allNodes[startingBuilding];
            float[] actualDistances = new float[amountOfNodes];

            List<writableTuple<int, float>> unVisitedNodesAndDistances = new List<writableTuple<int, float>>();
           
            foreach (var cachedNode in allNodes)
            {
                //This list represents the 'row' in the matrix. It only contains neighbours.
                List<int> neighBoursRow = new List<int>();

                //Add each non-starting node to unvisited nodes, set its distance to infinite
                if (cachedNode.Key != startingBuilding)
                {
                    unVisitedNodesAndDistances.Add(new writableTuple<int, float>(cachedNode.Value, float.MaxValue));
                    actualDistances[cachedNode.Value] = float.MaxValue;
                }
                else
                {
                    unVisitedNodesAndDistances.Add(new writableTuple<int, float>(cachedNode.Value, 0));
                    actualDistances[cachedNode.Value] = 0;
                }

                foreach (var roadSection in roads)
                {
                    //If they are neighbours
                    if (cachedNode.Key == roadSection.Item1)
                    {
                        neighBoursRow.Add(allNodes[roadSection.Item2]);
                    }
                }
                //Neighboursrow is the array in dimension 2 at the index of the currentNode in dimension 1
                neighBoursMatrix[cachedNode.Value] = neighBoursRow.ToArray();
            }


            /*************************
            * END GRAPH SETUP
            **************************/

           
            /*************************
            * ACTUAL GRAPH ALGO
            **************************/
            

            //We go through all unvisited nodes
            while (unVisitedNodesAndDistances.Count > 0)
            {
                //this should steadily decrease
                Console.WriteLine("Amount of unvisited nodes: " + unVisitedNodesAndDistances.Count);

                //start with the currentNode, the node that has the smallest distance to the starting node.
                int CurrentNodeIdentifier = getSmallestItem(unVisitedNodesAndDistances);
                //Get the key for value currentNodeIdentifier
                Vector2 currentNode = allNodes.FirstOrDefault(x => x.Value == CurrentNodeIdentifier).Key;
                Console.WriteLine("Current node id, vector: " + CurrentNodeIdentifier + "," + currentNode);

                
                //Remove currentNode from unvisitedNodes
                removeSmallestItem(unVisitedNodesAndDistances, CurrentNodeIdentifier);


                //Create a list of neighbours containing ints, taken from the neighboursmatrix, pointing to values in allNodes
                List<int> currentNodesNeighbours = new List<int>();
                int[] row = neighBoursMatrix[CurrentNodeIdentifier];


                foreach (int neighBourIdentifier in row)
                {
                    currentNodesNeighbours.Add(neighBourIdentifier);
                }
                    
                foreach (int neighBourIdentifier in currentNodesNeighbours)
                {
                    //Get the vector associated with this neighbour. (since vectors are keys)
                    Vector2 neighbourVector = allNodes.FirstOrDefault(x => x.Value == neighBourIdentifier).Key;
                    int indexForNeighbour = allNodes[neighbourVector];  





                    float neighBoursDistance = actualDistances[neighBourIdentifier];
                    Console.WriteLine("Neighbour node: " + neighbourVector);

                    //Add the length from our currentNode to the start + the distance between currentNode and the neighbour
                    float newPotentialPathLength = actualDistances[CurrentNodeIdentifier] + Vector2.Distance(currentNode, neighbourVector);
                    Console.WriteLine("Path length inc. neighbour: " + newPotentialPathLength + ", path at distancesToStartingNode: " + neighBoursDistance);

                    //EVALUATE the actual distance to the neighbours. Set Item2 of the tuple that has Item1==neighbouridentifier to the actual distance
                    setActualDistanceOfNeighbour
                    (unVisitedNodesAndDistances, neighBourIdentifier, 
                        Vector2.Distance(startingBuilding, neighbourVector)); 
                    
                    actualDistances[neighBourIdentifier] = Vector2.Distance(startingBuilding, neighbourVector);

                    //if the new path, including the detour through the neighbour is shorter than the direct distance between the neighbour and start,
                    //as contained within distancesToStartingNode, we should update the potentialpath.
                    if (newPotentialPathLength < neighBoursDistance)
                    {
                        Console.WriteLine("Found a beter path!");
                        actualDistances[neighBourIdentifier] = newPotentialPathLength;

                        pred[neighBourIdentifier] = CurrentNodeIdentifier;                    
                    }
                }
            }


            List<int> pred_results = new List<int>();
            pred_results.Add(allNodes[destinationBuilding]);

            pathcalc(pred, allNodes[startingBuilding], allNodes[destinationBuilding], pred_results);

            List<Vector2> vectors = new List<Vector2>();

            for (int i = 0; i < pred_results.Count; i++)
            {
                vectors.Add(allNodes.FirstOrDefault(x => x.Value == pred_results[i]).Key);
            }

            var path = createPathListReverse(vectors);

            return path;


            /*************************
            * END ACTUAL GRAPH ALGO
            **************************/

        }
            
        /**********************
        * ASSIGNMENT METHODS 
        ***********************/

        private static IEnumerable<IEnumerable<Vector2>> FindSpecialBuildingsWithinDistanceFromHouse(
            IEnumerable<Vector2> specialBuildings, 
            IEnumerable<Tuple<Vector2, float>> housesAndDistances)
        {
            var Tree = new EmptyNode<Vector2>() as MiniTree<Vector2>;
            List<Vector2> listOfBuildings = specialBuildings.ToList();
           
            foreach(Vector2 v in listOfBuildings)
            {
                Tree = insertIntoKD(v, Tree, Tree.sortedOnX());
            }

            List<Tuple<Vector2, float>> housesAndDistancesList = housesAndDistances.ToList();
            List<List<Vector2>> returnList = new List<List<Vector2>>();

            foreach(Tuple<Vector2, float> t in housesAndDistancesList)
            {
                List<Vector2> listForHouse = new List<Vector2>();
                rangeSearch(Tree, t.Item1, t.Item2, listForHouse);
                returnList.Add(listForHouse);
            }

            return returnList.AsEnumerable();
        }


        /********************
        * END OF TREES
        *********************/

        private static IEnumerable<Tuple<Vector2, Vector2>> FindRoute(Vector2 startingBuilding, 
        Vector2 destinationBuilding, IEnumerable<Tuple<Vector2, Vector2>> roads)
        {
            return Dijkstras(startingBuilding, destinationBuilding, roads);
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