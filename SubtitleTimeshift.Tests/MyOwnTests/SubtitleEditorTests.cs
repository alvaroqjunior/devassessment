using SolutionForSubtitleTimeshift.Implementations;
using SolutionForSubtitleTimeshift.Interfaces;
using SolutionForSubtitleTimeshift.Models;
using SubtitleTimeshift.Tests.MyOwnTests.Mocks;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

namespace SubtitleTimeshift.Tests.MyOwnTests
{
  public class SubtitleEditorTests
  {
    private readonly ISubtitleEditor _subtitleTimeEditor;

    public SubtitleEditorTests()
    {
      _subtitleTimeEditor = new SubtitleEditor();
    }

    [Theory(DisplayName = "[SyncSubtitle] Should Shift The Subtitle Time")]
    [MemberData(nameof(ShiftLeft1400Ms_ShiftRight700_DoesntShift))]
    public void SyncSubtitle__Should_Shift_The_Subtitle_time(IEnumerable<SubtitleModel> expectedOutputSubtitleModel, int milliseconds)
    {
      //Arrange
      var operationTime = TimeSpan.FromMilliseconds(milliseconds);

      var inputSubtitleModel = SubtitlesMock.SubtitleModelMocked();

      //Act
      _subtitleTimeEditor.SyncSubtitle(inputSubtitleModel, operationTime);

      var inputSubtitleModelJSON = JsonSerializer.Serialize(inputSubtitleModel);
      var expectedOutputSubtitleModelJSON = JsonSerializer.Serialize(expectedOutputSubtitleModel);

      //Assert
      Assert.Equal(expectedOutputSubtitleModelJSON, inputSubtitleModelJSON);
    }

    public static IEnumerable<object[]> ShiftLeft1400Ms_ShiftRight700_DoesntShift()
    {
      return SubtitlesMock.ShiftLeft1400Ms_ShiftRight700_DoesntShift();
    }
  }
}
