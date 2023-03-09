using SolutionForGraphNavigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolutionForGraphNavigator.Implementations
{
  public class Node<T> : INode<T>
  {
    public T Id { get; set; }
    public Dictionary<T, INode<T>> Links { get; set; }

    public Node(T nodeId)
    {
      Id = nodeId;
      Links = new Dictionary<T, INode<T>>();
    }

    public INode<T> GetNodeLink(T nodeId)
    {
      var containKey = Links.TryGetValue(nodeId, out INode<T> node);

      if (containKey)
        return node;
      else
        throw new InvalidOperationException("Invalid Link.");
    }

    public void PointTo(INode<T> node)
    {
      if (node == null)
        return;

      if (!Links.ContainsKey(node.Id))
        Links.Add(node.Id, node);
    }
  }
}
