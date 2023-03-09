using SolutionForSubtitleTimeshift.Implementations;
using SolutionForSubtitleTimeshift.Interfaces;
using SubtitleTimeshift.Tests.MyOwnTests.Mocks;
using System.IO;
using System.Text;
using Xunit;

namespace SubtitleTimeshift.Tests.MyOwnTests
{
  public class ModelToSubtitleProcessorTests
  {
    private readonly IModelToSubtitleProcessor _modelToSubtitleProcessor;

    public ModelToSubtitleProcessorTests()
    {
      _modelToSubtitleProcessor = new ModelToSubtitleProcessor();
    }

    [Fact(DisplayName = "[GenerateFromModel] Should Generate a Stream From A Provided Model (Comma Separator)")]
    public void GenerateFromModel__Should_Generate_Stream_From_Provided_Model__CommaSeparator()
    {
      //Arrange
      var expectedResult = SubtitlesMock.MockedValidSrtFile_CommaSeparator().ToString();

      var mockedModel = SubtitlesMock.SubtitleModelMocked();
      var fileToBeWritedMock = new MemoryStream();
      var encoding = new UTF8Encoding(false); //Getting rid of BOM characters.
      var bufferize = 1024;
      var leaveOpen = false;
      bool dotSeparator = false;

      //Act
      _modelToSubtitleProcessor.GenerateFromModel(mockedModel, fileToBeWritedMock, encoding, bufferize, leaveOpen, dotSeparator);

      //Assert
      AssertFileWriting(fileToBeWritedMock, expectedResult);
    }

    [Fact(DisplayName = "[GenerateFromModel] Should Generate a Stream From A Provided Model (Dot Separator)")]
    public void GenerateFromModel__Should_Generate_Stream_From_Provided_Model__DotSeparator()
    {
      //Arrange
      var expectedResult = SubtitlesMock.MockedValidSrtFile_DotSeparator().ToString();

      var mockedModel = SubtitlesMock.SubtitleModelMocked();
      var fileToBeWritedMock = new MemoryStream();
      var encoding = new UTF8Encoding(false); //Getting rid of BOM characters.
      var bufferize = 1024;
      var leaveOpen = false;
      bool dotSeparator = true;

      //Act
      _modelToSubtitleProcessor.GenerateFromModel(mockedModel, fileToBeWritedMock, encoding, bufferize, leaveOpen, dotSeparator);

      //Assert
      AssertFileWriting(fileToBeWritedMock, expectedResult);
    }

    private void AssertFileWriting(MemoryStream memoryStream, string expectedFileString)
    {
      string memoryStreamText;

      using (memoryStream)
      {
        memoryStreamText = Encoding.UTF8.GetString(memoryStream.ToArray());
      }

      Assert.Equal(expectedFileString, memoryStreamText);
    }
  }
}
