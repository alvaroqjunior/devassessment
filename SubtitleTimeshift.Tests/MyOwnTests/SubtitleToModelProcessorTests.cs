using SolutionForSubtitleTimeshift.Implementations;
using SolutionForSubtitleTimeshift.Interfaces;
using SubtitleTimeshift.Tests.MyOwnTests.Mocks;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace SubtitleTimeshift.Tests.MyOwnTests
{
  public class SubtitleToModelProcessorTests
  {
    private readonly ISubtitleToModelProcessor _subtitleProcessor;

    public SubtitleToModelProcessorTests()
    {
      _subtitleProcessor = new SubtitleToModelProcessor();
    }

    [Fact(DisplayName = "[Process] Should Process Srt File To SubtitleModel (Success)")]
    public async void Process__Should_Process_Srt_File_To_SubtitleModel__Success()
    {
      //Arrange
      var expectedOrderRecord1 = 0;
      var expectedOrderRecord2 = 1;
      var expectedOrderRecord3 = 2;
      var expectedStartTimeSpanRecord1 = TimeSpan.ParseExact("00:00:10,008", @"hh\:mm\:ss\,fff", CultureInfo.InvariantCulture);
      var expectedStartTimeSpanRecord2 = TimeSpan.ParseExact("00:00:49,203", @"hh\:mm\:ss\,fff", CultureInfo.InvariantCulture);
      var expectedStartTimeSpanRecord3 = TimeSpan.ParseExact("00:00:52,874", @"hh\:mm\:ss\,fff", CultureInfo.InvariantCulture);
      var expectedEndTimeSpanRecord1 = TimeSpan.ParseExact("00:00:15,424", @"hh\:mm\:ss\,fff", CultureInfo.InvariantCulture);
      var expectedEndTimeSpanRecord2 = TimeSpan.ParseExact("00:00:52,623", @"hh\:mm\:ss\,fff", CultureInfo.InvariantCulture);
      var expectedEndTimeSpanRecord3 = TimeSpan.ParseExact("00:00:55,501", @"hh\:mm\:ss\,fff", CultureInfo.InvariantCulture);
      var expectedTextRecord1 = "Subtitle by : Amir Barzagli";
      var expectedTextRecord2 = "<i>- Semua dah sedia?\n\r" +
                                "- Kau tak patut gantikan aku.</i>";
      var expectedTextRecord3 = "<i>Aku tahu, tapi aku rasa\n\r" +
                                "macam kerja shift.</i>";

      var encoding = Encoding.UTF8;

      var mockedSrt = SubtitlesMock.MockedValidSrtFile_CommaSeparator();
      var stream = MockedFileUsingMemoryStream(mockedSrt);
      var bufferSize = 1024;
      var leaveOpen = false;

      //Act
      var result = await _subtitleProcessor.GenerateModel(stream, encoding, bufferSize, leaveOpen);

      //Assert
      var subtitleModel1 = result.ToList()[0];
      var subtitleModel2 = result.ToList()[1];
      var subtitleModel3 = result.ToList()[2];

      Assert.Equal(expectedOrderRecord1, subtitleModel1.Order);
      Assert.Equal(expectedOrderRecord2, subtitleModel2.Order);
      Assert.Equal(expectedOrderRecord3, subtitleModel3.Order);
      Assert.Equal(expectedStartTimeSpanRecord1, subtitleModel1.Start);
      Assert.Equal(expectedStartTimeSpanRecord2, subtitleModel2.Start);
      Assert.Equal(expectedStartTimeSpanRecord3, subtitleModel3.Start);
      Assert.Equal(expectedEndTimeSpanRecord1, subtitleModel1.End);
      Assert.Equal(expectedEndTimeSpanRecord2, subtitleModel2.End);
      Assert.Equal(expectedEndTimeSpanRecord3, subtitleModel3.End);
      Assert.Equal(expectedTextRecord1, subtitleModel1.Text[0]);
      Assert.Equal(expectedTextRecord1.Split('\n')[0].Trim(' ', '\r', '\n'), subtitleModel1.Text[0]);
      Assert.Equal(expectedTextRecord2.Split('\n')[0].Trim(' ', '\r', '\n'), subtitleModel2.Text[0]);
      Assert.Equal(expectedTextRecord2.Split('\n')[1].Trim(' ', '\r', '\n'), subtitleModel2.Text[1]);
      Assert.Equal(expectedTextRecord3.Split('\n')[0].Trim(' ', '\r', '\n'), subtitleModel3.Text[0]);
      Assert.Equal(expectedTextRecord3.Split('\n')[1].Trim(' ', '\r', '\n'), subtitleModel3.Text[1]);
    }

    [Fact(DisplayName = "[Process] Should Ignore Sequences Of Empty Lines")]
    public async void Process__Should_Ignore_Sequences_Of_Empty_Lines()
    {
      //Arrange
      var expectedOrderRecord1 = 0;
      var expectedOrderRecord2 = 1;
      var expectedOrderRecord3 = 2;
      var expectedStartTimeSpanRecord1 = TimeSpan.ParseExact("00:00:10,008", @"hh\:mm\:ss\,fff", CultureInfo.InvariantCulture);
      var expectedStartTimeSpanRecord2 = TimeSpan.ParseExact("00:00:49,203", @"hh\:mm\:ss\,fff", CultureInfo.InvariantCulture);
      var expectedStartTimeSpanRecord3 = TimeSpan.ParseExact("00:00:52,874", @"hh\:mm\:ss\,fff", CultureInfo.InvariantCulture);
      var expectedEndTimeSpanRecord1 = TimeSpan.ParseExact("00:00:15,424", @"hh\:mm\:ss\,fff", CultureInfo.InvariantCulture);
      var expectedEndTimeSpanRecord2 = TimeSpan.ParseExact("00:00:52,623", @"hh\:mm\:ss\,fff", CultureInfo.InvariantCulture);
      var expectedEndTimeSpanRecord3 = TimeSpan.ParseExact("00:00:55,501", @"hh\:mm\:ss\,fff", CultureInfo.InvariantCulture);
      var expectedTextRecord1 = "";
      var expectedTextRecord2 = "<i>- Semua dah sedia?\n\r";
      var expectedTextRecord3 = "<i>Aku tahu, tapi aku rasa\n\r" +
                                "macam kerja shift.</i>";

      var mockedSrt = SubtitlesMock.MockedValidSrtFile_WithSequencesOfEmptyLines();
      var stream = MockedFileUsingMemoryStream(mockedSrt);

      var encoding = Encoding.UTF8;
      var bufferSize = 1024;
      var leaveOpen = false;

      //Act
      var result = await _subtitleProcessor.GenerateModel(stream, encoding, bufferSize, leaveOpen);

      //Assert
      var subtitleModel1 = result.ToList()[0];
      var subtitleModel2 = result.ToList()[1];
      var subtitleModel3 = result.ToList()[2];

      Assert.Equal(expectedOrderRecord1, subtitleModel1.Order);
      Assert.Equal(expectedOrderRecord2, subtitleModel2.Order);
      Assert.Equal(expectedOrderRecord3, subtitleModel3.Order);
      Assert.Equal(expectedStartTimeSpanRecord1, subtitleModel1.Start);
      Assert.Equal(expectedStartTimeSpanRecord2, subtitleModel2.Start);
      Assert.Equal(expectedStartTimeSpanRecord3, subtitleModel3.Start);
      Assert.Equal(expectedEndTimeSpanRecord1, subtitleModel1.End);
      Assert.Equal(expectedEndTimeSpanRecord2, subtitleModel2.End);
      Assert.Equal(expectedEndTimeSpanRecord3, subtitleModel3.End);
      Assert.Equal(expectedTextRecord1, subtitleModel1.Text[0]);
      Assert.Equal(expectedTextRecord1.Split('\n')[0].Trim(' ', '\r', '\n'), subtitleModel1.Text[0]);
      Assert.Equal(expectedTextRecord2.Split('\n')[0].Trim(' ', '\r', '\n'), subtitleModel2.Text[0]);
      Assert.Equal(expectedTextRecord3.Split('\n')[0].Trim(' ', '\r', '\n'), subtitleModel3.Text[0]);
      Assert.Equal(expectedTextRecord3.Split('\n')[1].Trim(' ', '\r', '\n'), subtitleModel3.Text[1]);
    }

    //[Fact(DisplayName = "[Process] Should Throw An InvalidDataException When The Srt File Is Corrupted. (Section Order Out Of Sync)")]
    //public async void Process__Should_Throw_An_InvalidDataException_When_The_Srt_File_Is_Corrupted__SectionOrderOutOfSync()
    //{
    //  //Arrange
    //  var encoding = Encoding.UTF8;

    //  var mockedSrt = SubtitlesMock.MockedInvalidSrtFile_OrderCorrupted();
    //  var stream = MockedFileUsingMemoryStream(mockedSrt);
    //  var bufferSize = 1024;
    //  var leaveOpen = false;

    //  //Act
    //  var result = await Assert.ThrowsAsync<InvalidDataException>(async () => await _subtitleProcessor.GenerateModel(stream, encoding, bufferSize, leaveOpen));

    //  //Assert
    //  Assert.IsType<InvalidDataException>(result);
    //  Assert.Equal("Invalid Data: The file srt is corrupted.", result.Message);
    //}

    [Fact(DisplayName = "[Process] Should Throw An InvalidDataException When The Srt File Is Corrupted. (Invalid TimeSpan Format)")]
    public async void Process__Should_Throw_An_InvalidDataException_When_The_Srt_File_Is_Corrupted__InvalidTimeSpanFormat()
    {
      //Arrange
      var encoding = Encoding.UTF8;

      var mockedSrt = SubtitlesMock.MockedInvalidSrtFile_TimeSpanFormat();
      var stream = MockedFileUsingMemoryStream(mockedSrt);
      var bufferSize = 1024;
      var leaveOpen = false;

      //Act
      var result = await Assert.ThrowsAsync<InvalidDataException>(async () => await _subtitleProcessor.GenerateModel(stream, encoding, bufferSize, leaveOpen));

      //Assert
      Assert.IsType<InvalidDataException>(result);
      Assert.Equal("Invalid Data: Incorrect format for date.", result.Message);
    }

    private MemoryStream MockedFileUsingMemoryStream(StringBuilder mockedSrt)
    {
      return new MemoryStream(Encoding.UTF8.GetBytes(mockedSrt.ToString()));
    }

    //[Fact(DisplayName = "[Process] Should Throw An InvalidDataException When The Srt File Is Corrupted. (TimeSpan Out Of Sync)")]
    //public async void Process__Should_Throw_An_InvalidDataException_When_The_Srt_File_Is_Corrupted__TimeSpanOutOfSync()
    //{
    //  //Arrange
    //  var encoding = Encoding.UTF8;

    //  var mockedSrt = SubtitlesMock.MockedInvalidSrtFile_TimeSpanOutOfSync();
    //  var stream = MockedFileUsingMemoryStream(mockedSrt);
    //  var bufferSize = 1024;
    //  var leaveOpen = false;

    //  //Act
    //  var result = await Assert.ThrowsAsync<InvalidDataException>(async () => await _subtitleProcessor.GenerateModel(stream, encoding, bufferSize, leaveOpen));

    //  //Assert
    //  Assert.IsType<InvalidDataException>(result);
    //  Assert.Equal("Invalid Data: Date is out of sync.", result.Message);
    //}

    //[Fact(DisplayName = "[Process] Should Throw An InvalidDataException When The Srt File Is Corrupted. (TimeSpan Out Of Sync With Previous Section)")]
    //public async void Process__Should_Throw_An_InvalidDataException_When_The_Srt_File_Is_Corrupted__TimeSpanOutOfSyncWithPreviousSection()
    //{
    //  //Arrange
    //  var encoding = Encoding.UTF8;

    //  var mockedSrt = SubtitlesMock.MockedInvalidSrtFile_TimeSpanOutOfSyncWithPreviousSection();
    //  var stream = MockedFileUsingMemoryStream(mockedSrt);
    //  var bufferSize = 1024;
    //  var leaveOpen = false;

    //  //Act
    //  var result = await Assert.ThrowsAsync<InvalidDataException>(async () => await _subtitleProcessor.GenerateModel(stream, encoding, bufferSize, leaveOpen));

    //  //Assert
    //  Assert.IsType<InvalidDataException>(result);
    //  Assert.Equal("Invalid Data: Date is out of sync.", result.Message);
    //}
  }
}