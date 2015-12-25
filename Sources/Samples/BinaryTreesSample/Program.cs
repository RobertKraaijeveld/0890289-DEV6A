using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreesSample
{
  interface ITree<T>
  {
    bool IsEmpty { get; }
    T Value { get; }
    ITree<T> Left { get; }
    ITree<T> Right { get; }
  }

  class Empty<T> : ITree<T>
  {
    public bool IsEmpty
    {
      get
      {
        return true;
      }
    }

    public ITree<T> Left
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public ITree<T> Right
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public T Value
    {
      get
      {
        throw new NotImplementedException();
      }
    }
  }

  class Node<T> : ITree<T>
  {
    public bool IsEmpty
    {
      get
      {
        return false;
      }
    }

    public ITree<T> Left { get; set; }

    public ITree<T> Right { get; set; }

    public T Value { get; set; }

    public Node(ITree<T> l, T v, ITree<T> r)
    {
      Value = v;
      Left = l;
      Right = r;
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      var t = new Empty<int>() as ITree<int>;
      t = Insert(t, 50);
      t = Insert(t, 25);
      t = Insert(t, 100);
      t = Insert(t, 666);
      t = Insert(t, 12);
      t = Insert(t, 80085);
      //PrintInOrder(t);
      Console.WriteLine(SearchElement(t, 68));
    }

    static ITree<int> Insert(ITree<int> t, int v)
    {
      if (t.IsEmpty)
        return new Node<int>(new Empty<int>(), v, new Empty<int>());

      if (t.Value == v)
        return t;

      if (v < t.Value)
        return new Node<int>(Insert(t.Left, v), t.Value, t.Right);
      else
        return new Node<int>(t.Left, t.Value, Insert(t.Right, v));
    }

    static bool SearchElement(ITree<int> t, int v)
    {
      if (t.IsEmpty)
        return false;
      else
      {
        Console.WriteLine("Looking in " + t.Value);
        if (t.Value == v)
          return true;
        else
        {
          if (v < t.Value)
            return SearchElement(t.Left, v);
          else
            return SearchElement(t.Right, v);
        }
      }

    }

    static void PrintPreOrder<T>(ITree<T> t)
    {
      if (t.IsEmpty) return;
      Console.WriteLine(t.Value);
      PrintPreOrder(t.Left);
      PrintPreOrder(t.Right);
    }

    static void PrintInOrder<T>(ITree<T> t)
    {
      if (t.IsEmpty) return;
      PrintInOrder(t.Left);
      Console.WriteLine(t.Value);
      PrintInOrder(t.Right);
    }

  }
}
