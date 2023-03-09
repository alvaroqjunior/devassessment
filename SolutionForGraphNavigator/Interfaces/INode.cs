using System;
using System.Collections.Generic;
using System.Text;

namespace SolutionForGraphNavigator.Interfaces
{
  public interface INode<T>
  {
    T Id { get; set; }
    Dictionary<T, INode<T>> Links { get; }
    void PointTo(INode<T> node);
    INode<T> GetNodeLink(T nodeId);
  }
}
