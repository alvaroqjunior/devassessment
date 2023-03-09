using SolutionForGraphNavigator.Implementations;
using System;
using Xunit;

namespace Graph.Tests.MyOwnTests
{
  public class NodeTests
  {
    [Fact(DisplayName = "[PointTo(string)] Should Create a Link Between Nodes")]
    public void PointTo_Shoud_Create_A_Link_Between_Nodes()
    {
      //Arrange
      var node1 = new Node<string>("a");
      var node2 = new Node<string>("b");

      //Act
      node1.PointTo(node2);

      //Assert
      Assert.Equal(node2, node1.GetNodeLink("b"));
    }

    [Fact(DisplayName = "[PointTo(string)] Should Create a Circular Link Between Nodes")]
    public void PointTo_Shoud_Create_A_Circular_Link_Between_Nodes()
    {
      //Arrange
      var node1 = new Node<string>("a");
      var node2 = new Node<string>("b");


      //Act
      node1.PointTo(node2);
      node2.PointTo(node1);

      //Assert
      Assert.Equal(node2, node1.GetNodeLink("b"));
      Assert.Equal(node1, node2.GetNodeLink("a"));
    }

    [Fact(DisplayName = "[PointTo(string)] Should Create Multiple Links For The Same Node")]
    public void PointTo_Shoud_Create_Multiple_Link_Between_Nodes()
    {
      //Arrange
      var expectedNumberOfLinksOnNode1 = 3;

      var node1 = new Node<string>("a");
      var node2 = new Node<string>("b");
      var node3 = new Node<string>("c");
      var node4 = new Node<string>("d");

      //Act
      node1.PointTo(node2);
      node1.PointTo(node3);
      node1.PointTo(node4);

      //Assert
      Assert.Equal(node2, node1.GetNodeLink("b"));
      Assert.Equal(node3, node1.GetNodeLink("c"));
      Assert.Equal(node4, node1.GetNodeLink("d"));
      Assert.Equal(expectedNumberOfLinksOnNode1, node1.Links.Count);
    }

    [Fact(DisplayName = "[PointTo] Should Not Throw An Exception When Try Add Null Link Pointer")]
    public void PointTo_Shoud_Not_Throw_An_Exception_When_Try_Add_Null_Link_Pointer()
    {
      //Arrange
      var node1 = new Node<string>("a");

      //Act
      var exception = Record.Exception(() => node1.PointTo(null));

      //Assertion
      Assert.Null(exception);
    }

    [Fact(DisplayName = "[PointTo] Should Not Throw An Exception When Try Add Repeated Link Pointer")]
    public void PointTo_Shoud_Not_Throw_An_Exception_When_Try_Add_Repeated_Link_Pointer()
    {
      //Arrange
      var expectedNumberOfLinksOnNode1 = 1;
      var node1 = new Node<string>("a");
      var node2 = new Node<string>("b");
      node1.PointTo(node2);

      //Act
      var exception = Record.Exception(() => node1.PointTo(node2));

      //Assertion
      Assert.Null(exception);
      Assert.Equal(expectedNumberOfLinksOnNode1, node1.Links.Count);
    }

    [Fact(DisplayName = "[PointTo] Should Update Link Pointer")]
    public void PointTo_Shoud_Update_Link_Pointer()
    {
      //Arrange
      var expectedNumberOfLinksOnNode2 = 2;

      var node1 = new Node<string>("a");
      var node2 = new Node<string>("b");
      var node3 = new Node<string>("c");
      var node4 = new Node<string>("d");

      //'a' points to 'b' and 'b' point to 'c'
      node1.PointTo(node2);
      node2.PointTo(node3);

      //Act

      //But now, 'b' points to 'd' too and it needs to be updated.
      node2.PointTo(node4);

      //Assertion
      //And we can go from 'a' to 'd'.
      var expectedToBeNode4 = node1.GetNodeLink("b").GetNodeLink("d");
      Assert.Equal(expectedToBeNode4, node4);
      Assert.Equal(expectedNumberOfLinksOnNode2, node2.Links.Count);
    }

    [Fact(DisplayName = "[PointTo] Should Join Two Completely Separated Branches of Nodes")]
    public void PointTo_Shoud_Update_Link_Pointer_2()
    {
      //Arrange
      var node1 = new Node<string>("a");
      var node2 = new Node<string>("b");
      var node3 = new Node<string>("c");
      var node4 = new Node<string>("d");
      var node5 = new Node<string>("e");
      var node6 = new Node<string>("f");

      //'a' points to 'b' and 'c'
      node1.PointTo(node2);
      node1.PointTo(node3);

      //'d' points to 'e' and 'f'
      node4.PointTo(node5);
      node4.PointTo(node6);

      //Act
      //Now 'b' points to 'd' and the two branches are now together.
      node2.PointTo(node4);

      //Assertion
      //And we can go from 'a' to 'e'.
      var expectedToBeNode5 = node1.GetNodeLink("b").GetNodeLink("d").GetNodeLink("e");
      Assert.Equal(expectedToBeNode5, node5);
    }

    [Fact(DisplayName = "[GetLink] Should Throw An Exception When Try To Get An Nonexistent Link")]
    public void GetLink_Shoud_Throw_An_Exception_When_Try_To_Get_An_Nonexistent_Link()
    {
      //Arrange
      var node1 = new Node<string>("a");

      //Act
      void act() => node1.GetNodeLink("c");

      //Assert
      var exception = Assert.Throws<InvalidOperationException>(act);
      Assert.Equal("Invalid Link.", exception.Message);
    }
  }
}
