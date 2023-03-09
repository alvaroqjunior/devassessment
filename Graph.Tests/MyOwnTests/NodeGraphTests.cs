using Moq;
using SolutionForGraphNavigator.Implementations;
using SolutionForGraphNavigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Graph.Tests.MyOwnTests
{
  public class NodeGraphTests
  {
    [Fact(DisplayName = "[AddNodeRegister] Should Add Node Register")]
    public void AddNodeLink_Should_Add_Node_Register()
    {
      //Arrange
      var mockedNode1 = new Mock<INode<string>>();
      var node2 = new Node<string>("b");
      mockedNode1.Setup(n => n.Id).Returns("a");
      mockedNode1.Setup(n => n.PointTo(node2));

      //Act
      var nodeGraph = new NodeGraph<string>();
      nodeGraph.AddNodeRegister(mockedNode1.Object, node2);

      //Assert
      Assert.True(nodeGraph.NodeRegisters.ContainsKey("a"));
      Assert.True(nodeGraph.NodeRegisters.ContainsKey("b"));
      mockedNode1.Verify(n => n.PointTo(node2), Times.Once);
    }

    [Fact(DisplayName = "[AddNodeRegister] Should Not Throw An Exception When Try To Add Repeated Register")]
    public void AddNodeLink_Should_Not_Throw_An_Exception_When_Try_To_Add_Repeated_Register()
    {
      //Arrange
      var mockedNode1 = new Mock<INode<string>>();
      var node2 = new Node<string>("b");
      mockedNode1.Setup(n => n.Id).Returns("a");
      mockedNode1.Setup(n => n.PointTo(node2));

      //Act
      var nodeGraph = new NodeGraph<string>();
      nodeGraph.AddNodeRegister(mockedNode1.Object, node2);
      var exception = Record.Exception(() => nodeGraph.AddNodeRegister(mockedNode1.Object, node2));

      //Assert
      Assert.Null(exception);
      mockedNode1.Verify(n => n.PointTo(node2), Times.Exactly(2));
    }

    [Fact(DisplayName = "[AddNodeRegister] Should Not Add Repeated Register")]
    public void AddNodeLink_Should_NoUpdate_Node_Register()
    {
      //Arrange
      var expectedNumberOfNodesRegistered = 2;

      var mockedNode1 = new Mock<INode<string>>();
      var node2 = new Node<string>("b");
      mockedNode1.Setup(n => n.Id).Returns("a");
      mockedNode1.Setup(n => n.PointTo(node2));

      //Act
      var nodeGraph = new NodeGraph<string>();
      nodeGraph.AddNodeRegister(mockedNode1.Object, node2);
      nodeGraph.AddNodeRegister(mockedNode1.Object, node2);

      //Assert
      Assert.True(nodeGraph.NodeRegisters.ContainsKey("a"));
      Assert.True(nodeGraph.NodeRegisters.ContainsKey("b"));
      Assert.Equal(expectedNumberOfNodesRegistered, nodeGraph.NodeRegisters.Count);
      mockedNode1.Verify(n => n.PointTo(node2), Times.Exactly(2));
    }

    [Fact(DisplayName = "[Navigate] Should Throw An InvalidOperationException When Try To Navigate From a Not Registered Node")]
    public void Navigate_Should_Throw_An_InvalidOperationException_When_Try_To_Navigate_From_A_Not_Registered_Node()
    {
      //Arrange
      var node1 = new Node<string>("a");
      var node2 = new Node<string>("b");
      var nodeGraph = new NodeGraph<string>();

      //Act
      void act() => nodeGraph.Navigate(node1, node2);

      //Assert
      var exception = Assert.Throws<InvalidOperationException>(act);
      Assert.Equal("Invalid Source Node.", exception.Message);
    }

    [Fact(DisplayName = "[Navigate] Should Throw An InvalidOperationException When Try To Navigate To a Not Registered Node")]
    public void Navigate_Should_Throw_An_InvalidOperationException_When_Try_To_Navigate_To_A_Not_Registered_Node()
    {
      //Arrange
      var node1 = new Node<string>("a");
      var node2 = new Node<string>("b");
      var nodeGraph = new NodeGraph<string>();
      nodeGraph.NodeRegisters["a"] = node1;

      //Act
      void act() => nodeGraph.Navigate(node1, node2);

      //Assert
      var exception = Assert.Throws<InvalidOperationException>(act);
      Assert.Equal("Invalid Target Node.", exception.Message);
    }

    [Fact(DisplayName = "[Navigate] Should Navigate Between Two Nodes And Print The Path (Single Path)")]
    public void Navigate_Should_Navigate_Between_Two_Nodes_And_Print_The_Path__SinglePath()
    {
      //Arrange
      var expectedPath = "a-b";

      var (mocks, nodeGraph) = CreateStringMockedGraph("a-b");
      var mockedNode1 = mocks[0];
      var mockedNode2 = mocks[1];

      var result = nodeGraph.Navigate(mockedNode1.Object, mockedNode2.Object);

      //Assert
      //Assert.Equal(nodeGraph.CurrentNode, mockedNode2.Object);
      Assert.Equal(expectedPath, result.FirstOrDefault());
    }

    [Fact(DisplayName = "[Navigate] Should Navigate Between Three Nodes And Print The Path (Single Path)")]
    public void Navigate_Should_Navigate_Between_Three_Nodes_And_Print_The_Path__SinglePath()
    {
      //Arrange
      var expectedPath = "a-b-c";

      var (mocks, nodeGraph) = CreateStringMockedGraph("a-b-c");
      var mockedNode1 = mocks[0];
      var mockedNode3 = mocks[2];

      //Act
      var result = nodeGraph.Navigate(mockedNode1.Object, mockedNode3.Object);

      //Assert
      //Assert.Equal(nodeGraph.CurrentNode, mockedNode3.Object);
      Assert.Equal(expectedPath, result.FirstOrDefault());
    }

    [Fact(DisplayName = "[Navigate] Should Navigate Between Nodes And Print The Path (Multiple Paths)")]
    public void Navigate_Should_Navigate_Between_Nodes_And_Print_The_Path__MultiplePaths()
    {
      //Arrange
      var expectedPath1 = "a-b-c";
      var expectedPath2 = "a-c";

      var (mocks, nodeGraph) = CreateStringMockedGraph(new List<string>() { expectedPath1, expectedPath2 });
      var mockedNode1 = mocks[0];
      var mockedNode3 = mocks[2];

      //Act
      var result = nodeGraph.Navigate(mockedNode1.Object, mockedNode3.Object).ToList();

      //Assert
      //Assert.Equal(nodeGraph.CurrentNode, mockedNode3.Object);
      Assert.Equal(expectedPath1, result[0]);
      Assert.Equal(expectedPath2, result[1]);
    }

    [Fact(DisplayName = "[Navigate] Should Doens't Find Any Path (Single Paths)")]
    public void Navigate_Should_Doesnt_Find_Any_Path__SinglePath()
    {
      //Arrange
      var path = "a-b-c-d";

      var (mocks, nodeGraph) = CreateStringMockedGraph(new List<string>() { path });
      var mockedNode1 = mocks[0];
      var mockedNode3 = mocks[2];

      //Act
      var result = nodeGraph.Navigate(mockedNode3.Object, mockedNode1.Object).ToList();

      //Assert
      Assert.Empty(result);
    }

    [Fact(DisplayName = "[Navigate] Should Not Navigate Inside Closed Loop Path (Single Closed Loop Path)")]
    public void Navigate_Should_Navigate_Inside_Closed_Loop_Path_Only_Once__SingleClosedLoopPath()
    {
      //Arrange
      var mockPath = "a-b-c-a";
      var expectedPath = "";

      var (mocks, nodeGraph) = CreateStringMockedGraph(new List<string>() { mockPath });
      var mockedNode1 = mocks[0]; //a node.

      //Act
      var result = nodeGraph.Navigate(mockedNode1.Object, mockedNode1.Object).ToList();

      //Assert
      //Assert.Equal(nodeGraph.CurrentNode, mockedNode1.Object);
      Assert.Equal(expectedPath, result[0]);
    }

    [Fact(DisplayName = "[Navigate] Should Find Multiples Paths (Multiple Closed Loop Path)")]
    public void Navigate_Should_Find_Multiples_Path__SingleClosedLoopPath()
    {
      //Arrange
      var mockPath2 = "a-h";
      var mockPath3 = "b-a";
      var mockPath4 = "c-b";
      var mockPath5 = "d-a";
      var mockPath6 = "a-h";
      var mockPath7 = "f-e";
      var mockPath1 = "a-b-c-d-e-h-g-f";

      var expectedPath1 = "a-b-c-d-e";
      var expectedPath2 = "a-h-g-f-e";

      var (mocks, nodeGraph) = CreateStringMockedGraph(new List<string>() { mockPath1, mockPath2, mockPath3, mockPath4, mockPath5, mockPath6, mockPath7 });
      var mockedNode1 = mocks[0]; //a node.
      var mockedNode5 = mocks[4]; //e node.
      //Act
      var result = nodeGraph.Navigate(mockedNode1.Object, mockedNode5.Object).ToList();

      //Assert
      //Assert.Equal(nodeGraph.CurrentNode, mockedNode1.Object);
      Assert.Contains(result, l => l == expectedPath1);
      Assert.Contains(result, l => l == expectedPath2);
    }

    private Tuple<List<Mock<INode<string>>>, NodeGraph<string>> CreateStringMockedGraph(string path)
    {
      var ids = path.Split("-");
      var returnMocks = new List<Mock<INode<string>>>();
      var nodeGraph = new NodeGraph<string>();

      foreach (var id in ids)
      {
        var mock = new Mock<INode<string>>();
        mock.Setup(n => n.Id).Returns(id);
        returnMocks.Add(mock);
      }

      for (int i = 1; i < returnMocks.Count; i++)
      {
        var previousMock = returnMocks[i - 1];
        var currentMock = returnMocks[i];

        previousMock.Setup(n => n.Links).Returns(new Dictionary<string, INode<string>> { { currentMock.Object.Id, currentMock.Object } });
        previousMock.Setup(n => n.GetNodeLink(currentMock.Object.Id)).Returns(currentMock.Object);

        nodeGraph.AddNodeRegister(previousMock.Object, currentMock.Object);
      }

      return Tuple.Create(returnMocks, nodeGraph);
    }

    private Tuple<List<Mock<INode<string>>>, NodeGraph<string>> CreateStringMockedGraph(List<string> paths)
    {
      var returnMocks = new List<Mock<INode<string>>>();
      var mockLinks = new Dictionary<string, Dictionary<string, INode<string>>>();
      var nodeGraph = new NodeGraph<string>();

      paths.ForEach(path =>
      {
        var ids = path.Split("-");

        foreach (var id in ids)
        {
          var isMockCreated = nodeGraph.NodeRegisters.TryGetValue(id, out var registeredMock);

          if (!isMockCreated && !mockLinks.ContainsKey(id))
          {
            var mock = new Mock<INode<string>>();
            mock.Setup(n => n.Id).Returns(id);
            returnMocks.Add(mock);
            mockLinks.Add(id, new Dictionary<string, INode<string>>());
          }
        }

        for (int i = 1; i < ids.Length; i++)
        {
          var previousMock = returnMocks.FirstOrDefault(m => m.Object.Id == ids[i - 1]);
          var currentMock = returnMocks.FirstOrDefault(m => m.Object.Id == ids[i]);

          mockLinks.TryGetValue(previousMock.Object.Id, out var dictionary);
          var existLink = dictionary.TryGetValue(currentMock.Object.Id, out var _);

          if (!existLink)
            dictionary.Add(currentMock.Object.Id, currentMock.Object);

          previousMock.Setup(n => n.Links).Returns(dictionary);
          previousMock.Setup(n => n.GetNodeLink(currentMock.Object.Id)).Returns(currentMock.Object);

          nodeGraph.AddNodeRegister(previousMock.Object, currentMock.Object);
        }
      });

      return Tuple.Create(returnMocks, nodeGraph);
    }
  }
}
