using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
		static void Main(string[] args)
		{
			/*var Tree = new EmptyNode<float>() as MiniTree<float>;
			List<Vector2> listOfBuildings = specialBuildings.ToList();

			foreach(Vector2 v in listOfBuildings)
			{
				float[] XnY = new float[]{ v.X, v.Y };
				insertIntoKD(XnY, true, Tree);
			}
			*/
			//Test this in VS!
			float[] testValuesToFind = new float[]{listOfBuildings[0].X, listOfBuildings[0].Y}; 
			findNode (testValuesToFind, true, Tree);
			return null;
		}
		/*



        static void Main(string[] args)
        {
            Node<int> n =
                new Node<int>
                (
                //root
                    5,
                    //level1 left
                    new Node<int>
                    (
                        3,
                        //level2 left
                        new Node<int>
                        (
                            1,
                            new EmptyNode<int>(),
                            new EmptyNode<int>()
                        )
                        ,
                        //level2 right
                        new Node<int>
                        (
                            4,
                            new EmptyNode<int>(),
                            new EmptyNode<int>()
                        )
                    ),
                    //level1 right
                    new Node<int>
                    (
                        8,
                        //level2 left
                        new Node<int>
                        (
                            6,
                            new EmptyNode<int>(),
                            new EmptyNode<int>()
                        )
                        ,
                        //level2 right
                        new Node<int>
                        (
                            10,
                            new EmptyNode<int>(),
                            new EmptyNode<int>()
                        )
                    )
                );

            Console.WriteLine("Pre-order: ");
            preOrderVisit(n);

            Console.WriteLine("In-order: ");
            inOrderVisit(n);

            Console.WriteLine("find Node: ");
            findNode(10, n);

            Console.Read();
        }


        //pre-order: Root, everything to the left, everything to the right
        static void preOrderVisit(MiniTree<int> root)
        {
            if (root.isEmpty() == false)
            {
                Console.WriteLine(root.getValue());
                preOrderVisit(root.getLeftMTree());
                preOrderVisit(root.getRightMTree());
            }
        }

        //in-order: Exactly like the name suggests. Note to self: Draw these routines out.
        static void inOrderVisit(MiniTree<int> root)
        {
            if (root.isEmpty() == false)
            {
                inOrderVisit(root.getLeftMTree());
                Console.WriteLine(root.getValue());
                inOrderVisit(root.getRightMTree());
            }
        }

        //return true if the specified 
        static bool findNode(int nodeValue, MiniTree<int> root)
        {
            //after empty check, check if value is equal. If not, check if value bigger or smaller and then choose appropriate path
            if (root.isEmpty() == true)
                return false;
            else
            {
                Console.WriteLine("Looking in " + root.getValue());
                if (nodeValue == root.getValue())
                {
                    Console.WriteLine("Got it!");
                    return true;
                }
                else if (nodeValue > root.getValue())
                {
                    //Look in the bigger side, since nodeValue is bigger than the node we are currently looking at.
                    return findNode(nodeValue, root.getRightMTree());
                }
                else
                {
                    //Look in the smaller side, since nodeValue is smaller than the node we are currently looking at.
                    return findNode(nodeValue, root.getLeftMTree());
                }
            }
        }

        interface MiniTree<T>
        {
            Boolean isEmpty();
            T getValue();
            MiniTree<T> getLeftMTree();
            MiniTree<T> getRightMTree();
        }

        //since node and emptynode both inherit from the abstract interface minitree, they can both be used 
        class Node<T> : MiniTree<T>
        {
            T value;
            MiniTree<T> left;
            MiniTree<T> right;

            public Boolean isEmpty()
            {
                return false;
            }

            //getters
            public T getValue()
            {
                return value;
            }

            public MiniTree<T> getLeftMTree()
            {
                return left;
            }

            public MiniTree<T> getRightMTree()
            {
                return right;
            }

            //setters
            public T setValue(T v)
            {
                value = v;
                return value;
            }

            public MiniTree<T> setLeftMTree(MiniTree<T> l)
            {
                left = l;
                return left;
            }

            public MiniTree<T> setRightMTree(MiniTree<T> r)
            {
                right = r;
                return right;
            }


            //constructor
            public Node(T val, MiniTree<T> l, MiniTree<T> r)
            {
                value = val;
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
            public T getValue()
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
            //(explicit) constructor is no specified in the interface contract and thusly not necessary
            //same goes for setters
        }
    }
}
//each node is a minitree! Node is concrete, minitree abstract
//and also concrete empty-node, with boolean functiomn to indicate nullness

*/



/********************
		* TREES OF ALL SHAPES AND SIZES (not really though)
		*********************/

/********************
* CLASS DEFINITIONS 
*********************/

interface MiniTree<T>
{
	Boolean isEmpty();
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


//Call this with nextLevelSortedOnX =true!
static MiniTree<float> insertIntoKD(float[] XY, bool nextLevelSortedOnX, MiniTree<float> root)
{
	//If the root is empty we cant do squat.	
	if(root.isEmpty() == false)
	{
		//We are in a level that is sorted by X values.
		if(nextLevelSortedOnX == true)
		{
			//Node already present!
			if (XY [0] == root.getXValue() && XY [1] == root.getYValue()) 
			{
				Console.WriteLine("Node already there!");
				return root;
			}	
			//Node to be inserted has bigger X value, so we look in the right tree.
			else if(XY[0] > root.getXValue())
			{
				return new Node<float>(XY[0], XY[1], root.getLeftMTree(), insertIntoKD(XY, false, root.getRightMTree()));
			}
			//Node to be inserted has smaller X value, so we look in the right tree.
			else 
			{
				return new Node<float>(XY[0], XY[1], insertIntoKD(XY, false, root.getLeftMTree()), root.getRightMTree());
			}
		}
		//Next level sorted on Y
		else
		{
			//Node already present!
			if (XY [0] == root.getXValue() && XY [1] == root.getYValue()) 
			{
				Console.WriteLine("Node already there!");
				return root;
			}	
			//Node to be inserted has bigger Y value, so we look in the right tree.
			else if(XY[1] > root.getYValue())
			{
				//TODO: ANDERE TREE MOET OOK INGEVULD WORDEN!
				return new Node<float>(XY[0], XY[1], root.getLeftMTree(), insertIntoKD(XY, false, root.getRightMTree()));
			}
			//Node to be inserted has smaller Y value, so we look in the left tree.
			else 
			{
				return new Node<float>(XY[0], XY[1], insertIntoKD(XY, false, root.getLeftMTree()), root.getRightMTree());
			}
		}
	}
	else
		return root;
}


static bool findNode(float[] XY, bool nextLevelSortedOnX, MiniTree<float> root)
{
	//If the root is empty we cant do squat.	
	if(root.isEmpty() == false)
	{
		//We are in a level that is sorted by X values.
		if(nextLevelSortedOnX == true)
		{
			//Node found!
			if (XY [0] == root.getXValue() && XY [1] == root.getYValue()) 
			{
				Console.WriteLine("Node found!");
				return true;
			}	
			//Node has bigger X value, so we look in the right tree.
			else if(XY[0] > root.getXValue())
			{
				return findNode(XY, false, root.getRightMTree());
			}
			//Node has smaller X value, so we look in the right tree.
			else 
			{
				return findNode(XY, false, root.getLeftMTree());
			}
		}
		//Next level sorted on Y
		else
		{
			//Node found!
			if (XY [0] == root.getXValue() && XY [1] == root.getYValue()) 
			{
				Console.WriteLine("Node already there!");
				return true;
			}	
			//Node has bigger Y value, so we look in the right tree.
			else if(XY[1] > root.getYValue())
			{
				return findNode(XY, true, root.getRightMTree());
			}
			//Node thas smaller Y value, so we look in the left tree.
			else 
			{
				return findNode(XY, true, root.getLeftMTree());
			}
		}
	}
	else
		return false;
}
	
