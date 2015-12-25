using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExploration
{
  enum ExplorationStatus
  {
    Explored,
    Unexplored,
    Frontier
  }

  class Program
  {
    static void Main(string[] args)
    {
      var V = new List<string> { "A", "B", "C", "D", "E", "F", "G" };

      List<int>[] E =
      {
        new List<int>() { 1, 2, 3 },
        new List<int>() { 2, 3 },
        new List<int>() { 1, 3 },
        new List<int>() { 0, 1, 2, 4, 5, 6 },
        new List<int>() {},
        new List<int>() {},
        new List<int>() {},
      };

      BFS(V, E);

    }

    private static void XFS(List<string> V, List<int>[] E,
      Action<int> addToFrontier, Func<bool> isFrontierEmpty, Func<int> getNextFromFrontier)
    {
      var status =
        (from v in Enumerable.Range(0, V.Count)
         select ExplorationStatus.Unexplored).ToArray();

      addToFrontier(0);
      status[0] = ExplorationStatus.Frontier;

      while (!isFrontierEmpty())
      {
        var v = getNextFromFrontier();
        Console.WriteLine("Now exploring " + V[v]);
        Console.ReadLine();
        status[v] = ExplorationStatus.Explored;
        foreach (var c in E[v])
        {
          if (status[c] == ExplorationStatus.Unexplored)
          {
            addToFrontier(c);
            status[c] = ExplorationStatus.Frontier;
          }
        }
      }
    }

    private static void DFS(List<string> V, List<int>[] E)
    {
      var frontier = new Stack<int>();
      XFS(V, E, v => frontier.Push(v), () => frontier.Count == 0, () => frontier.Pop());
    }

    private static void BFS(List<string> V, List<int>[] E)
    {
      var frontier = new Queue<int>();
      XFS(V, E, v => frontier.Enqueue(v), () => frontier.Count == 0, () => frontier.Dequeue());
    }
  }
}
