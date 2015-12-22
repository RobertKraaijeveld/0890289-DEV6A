using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        /********************
        * TREE AND METHOD DEFINITIONS
        *********************/

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


        /********************
        * CLASS DEFINITIONS 
        *********************/

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
