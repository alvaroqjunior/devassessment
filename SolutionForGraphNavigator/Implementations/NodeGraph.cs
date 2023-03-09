using SolutionForGraphNavigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolutionForGraphNavigator.Implementations
{
  public class NodeGraph<T> : INodeGraph<T>
  {
    public INode<T> CurrentNode { get; private set; }
    public Dictionary<T, INode<T>> NodeRegisters { get; private set; }
    public HashSet<T> VisitedNodes { get; private set; }

    public NodeGraph()
    {
      NodeRegisters = new Dictionary<T, INode<T>>();
      VisitedNodes = new HashSet<T>();
    }

    public void AddNodeRegister(INode<T> source, INode<T> target)
    {
      if (!NodeRegisters.ContainsKey(source.Id))
        NodeRegisters.Add(source.Id, source);
      if (!NodeRegisters.ContainsKey(target.Id))
        NodeRegisters.Add(target.Id, target);

      source.PointTo(target);
    }

    public IEnumerable<string> Navigate(INode<T> source, INode<T> target)
    {
      var paths = new HashSet<string>();
      Navigate(source, target, paths, "");

      return paths;
    }

    private void Navigate(INode<T> source, INode<T> target, HashSet<string> paths, string path)
    {
      var isRegisteredSource = NodeRegisters.TryGetValue(source.Id, out INode<T> _);
      var isRegisteredTarget = NodeRegisters.TryGetValue(target.Id, out INode<T> _);

      if (!isRegisteredSource)
        throw new InvalidOperationException("Invalid Source Node.");
      else if (!isRegisteredTarget)
        throw new InvalidOperationException("Invalid Target Node.");

      if (source.Id.Equals(target.Id))
      {
        paths.Add(path);
        return;
      }

      if (VisitedNodes.Contains(source.Id))
        return;
      else
        VisitedNodes.Add(source.Id);

      if (source.Links == null)
        return;

      foreach (var link in source?.Links)
      {
        var next = source.GetNodeLink(link.Key);
        var currentPath = WritePath(path, source.Id, next.Id);

        Navigate(next, target, paths, currentPath);
      }

      return;
    }

    private string WritePath(string currentPath, T currentNodeId, T nextNodeId)
    {
      if (string.IsNullOrEmpty(currentPath))
        return $"{currentNodeId}-{nextNodeId}";

      else return $"{currentPath}-{nextNodeId}";
    }
  }
}
