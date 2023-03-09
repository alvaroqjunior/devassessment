using System.Collections.Generic;

namespace SolutionForGraphNavigator.Interfaces
{
  public interface INodeGraph<T>
  {
    INode<T> CurrentNode { get; }
    Dictionary<T, INode<T>> NodeRegisters { get; }
    HashSet<T> VisitedNodes { get; }
    IEnumerable<string> Navigate(INode<T> source, INode<T> target);
    void AddNodeRegister(INode<T> source, INode<T> target);
  }
}
