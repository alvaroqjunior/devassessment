using SolutionForSubtitleTimeshift.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubtitleTimeshift.Tests.MyOwnTests.Mocks
{
  public class SubtitlesMock
  {
    #region Srt Format

    public static StringBuilder MockedValidSrtFile_CommaSeparator()
    {
      var mockedSrt = new StringBuilder();

      mockedSrt.AppendLine("0");
      mockedSrt.AppendLine("00:00:10,008 --> 00:00:15,424");
      mockedSrt.AppendLine("Subtitle by : Amir Barzagli");
      mockedSrt.AppendLine();

      mockedSrt.AppendLine("1");
      mockedSrt.AppendLine("00:00:49,203 --> 00:00:52,623");
      mockedSrt.AppendLine("<i>- Semua dah sedia?");
      mockedSrt.AppendLine("- Kau tak patut gantikan aku.</i>");
      mockedSrt.AppendLine();

      mockedSrt.AppendLine("2");
      mockedSrt.AppendLine("00:00:52,874 --> 00:00:55,501");
      mockedSrt.AppendLine("<i>Aku tahu, tapi aku rasa");
      mockedSrt.AppendLine("macam kerja shift.</i>");
      mockedSrt.AppendLine();

      return mockedSrt;
    }

    public static StringBuilder MockedValidSrtFile_DotSeparator()
    {
      var mockedSrt = new StringBuilder();

      mockedSrt.AppendLine("0");
      mockedSrt.AppendLine("00:00:10.008 --> 00:00:15.424");
      mockedSrt.AppendLine("Subtitle by : Amir Barzagli");
      mockedSrt.AppendLine();

      mockedSrt.AppendLine("1");
      mockedSrt.AppendLine("00:00:49.203 --> 00:00:52.623");
      mockedSrt.AppendLine("<i>- Semua dah sedia?");
      mockedSrt.AppendLine("- Kau tak patut gantikan aku.</i>");
      mockedSrt.AppendLine();

      mockedSrt.AppendLine("2");
      mockedSrt.AppendLine("00:00:52.874 --> 00:00:55.501");
      mockedSrt.AppendLine("<i>Aku tahu, tapi aku rasa");
      mockedSrt.AppendLine("macam kerja shift.</i>");
      mockedSrt.AppendLine();

      return mockedSrt;
    }

    public static StringBuilder MockedValidSrtFile_WithSequencesOfEmptyLines()
    {
      var mockedSrt = new StringBuilder();

      mockedSrt.AppendLine();
      mockedSrt.AppendLine();
      mockedSrt.AppendLine("0");
      mockedSrt.AppendLine("00:00:10,008 --> 00:00:15,424");
      mockedSrt.AppendLine();
      mockedSrt.AppendLine();

      mockedSrt.AppendLine("1");
      mockedSrt.AppendLine("00:00:49,203 --> 00:00:52,623");
      mockedSrt.AppendLine("<i>- Semua dah sedia?");
      mockedSrt.AppendLine();
      mockedSrt.AppendLine();

      mockedSrt.AppendLine("2");
      mockedSrt.AppendLine("00:00:52,874 --> 00:00:55,501");
      mockedSrt.AppendLine("<i>Aku tahu, tapi aku rasa");
      mockedSrt.AppendLine("macam kerja shift.</i>");
      mockedSrt.AppendLine();
      mockedSrt.AppendLine();
      mockedSrt.AppendLine();
      mockedSrt.AppendLine();

      return mockedSrt;
    }

    public static StringBuilder MockedInvalidSrtFile_TimeSpanFormat()
    {
      var mockedSrt = new StringBuilder();

      mockedSrt.AppendLine("0");
      mockedSrt.AppendLine("00:00:10,008 --> 00:00:15,424");
      mockedSrt.AppendLine("Subtitle by : Amir Barzagli");
      mockedSrt.AppendLine();

      mockedSrt.AppendLine("1");
      mockedSrt.AppendLine("00:00:49,203 --> 00:00:52,62398098098");
      mockedSrt.AppendLine("<i>- Semua dah sedia?");
      mockedSrt.AppendLine("- Kau tak patut gantikan aku.</i>");
      mockedSrt.AppendLine();

      mockedSrt.AppendLine("2");
      mockedSrt.AppendLine("00:00:52,874 --> 00:00:55,501");
      mockedSrt.AppendLine("<i>Aku tahu, tapi aku rasa");
      mockedSrt.AppendLine("macam kerja shift.</i>");
      mockedSrt.AppendLine();

      return mockedSrt;
    }

    public static StringBuilder MockedInvalidSrtFile_TimeSpanOutOfSync()
    {
      var mockedSrt = new StringBuilder();

      mockedSrt.AppendLine("0");
      mockedSrt.AppendLine("00:00:10,008 --> 00:00:08,424");
      mockedSrt.AppendLine("Subtitle by : Amir Barzagli");
      mockedSrt.AppendLine();

      mockedSrt.AppendLine("1");
      mockedSrt.AppendLine("00:00:49,203 --> 00:00:52,623");
      mockedSrt.AppendLine("<i>- Semua dah sedia?");
      mockedSrt.AppendLine("- Kau tak patut gantikan aku.</i>");
      mockedSrt.AppendLine();

      mockedSrt.AppendLine("2");
      mockedSrt.AppendLine("00:00:52,874 --> 00:00:55,501");
      mockedSrt.AppendLine("<i>Aku tahu, tapi aku rasa");
      mockedSrt.AppendLine("macam kerja shift.</i>");
      mockedSrt.AppendLine();

      return mockedSrt;
    }

    public static StringBuilder MockedInvalidSrtFile_TimeSpanOutOfSyncWithPreviousSection()
    {
      var mockedSrt = new StringBuilder();

      mockedSrt.AppendLine("0");
      mockedSrt.AppendLine("00:00:10,008 --> 00:00:15,424");
      mockedSrt.AppendLine("Subtitle by : Amir Barzagli");
      mockedSrt.AppendLine();

      mockedSrt.AppendLine("1");
      mockedSrt.AppendLine("00:00:49,203 --> 00:00:52,623");
      mockedSrt.AppendLine("<i>- Semua dah sedia?");
      mockedSrt.AppendLine("- Kau tak patut gantikan aku.</i>");
      mockedSrt.AppendLine();

      mockedSrt.AppendLine("2");
      mockedSrt.AppendLine("00:00:52,620 --> 00:00:55,501");
      mockedSrt.AppendLine("<i>Aku tahu, tapi aku rasa");
      mockedSrt.AppendLine("macam kerja shift.</i>");
      mockedSrt.AppendLine();

      return mockedSrt;
    }

    public static StringBuilder MockedInvalidSrtFile_OrderCorrupted()
    {
      var mockedSrt = new StringBuilder();

      mockedSrt.AppendLine("4234234234234243423423432234");
      mockedSrt.AppendLine("00:00:10,008 --> 00:00:15,424");
      mockedSrt.AppendLine("Subtitle by : Amir Barzagli");
      mockedSrt.AppendLine();

      mockedSrt.AppendLine("1");
      mockedSrt.AppendLine("00:00:49,203 --> 00:00:52,623");
      mockedSrt.AppendLine("<i>- Semua dah sedia?");
      mockedSrt.AppendLine("- Kau tak patut gantikan aku.</i>");
      mockedSrt.AppendLine();

      mockedSrt.AppendLine("2");
      mockedSrt.AppendLine("00:00:52,874 --> 00:00:55,501");
      mockedSrt.AppendLine("<i>Aku tahu, tapi aku rasa");
      mockedSrt.AppendLine("macam kerja shift.</i>");
      mockedSrt.AppendLine();

      return mockedSrt;
    }

    #endregion


    #region SubtitleModels

    public static IEnumerable<SubtitleModel> SubtitleModelMocked()
    {

      return new List<SubtitleModel>
      {
        new SubtitleModel
        {
          Order = 0,
          Start = TimeSpan.Parse("00:00:10.008"),
          End = TimeSpan.Parse("00:00:15.424"),
          Text = new List<string>
          {
            "Subtitle by : Amir Barzagli"
          }
        },
        new SubtitleModel
        {
          Order = 1,
          Start = TimeSpan.Parse("00:00:49.203"),
          End = TimeSpan.Parse("00:00:52.623"),
          Text = new List<string>
          {
            "<i>- Semua dah sedia?",
            "- Kau tak patut gantikan aku.</i>"
          }
        },
        new SubtitleModel
        {
          Order = 2,
          Start = TimeSpan.Parse("00:00:52.874"),
          End = TimeSpan.Parse("00:00:55.501"),
          Text = new List<string>
          {
            "<i>Aku tahu, tapi aku rasa",
            "macam kerja shift.</i>"
          }
        },
      };
    }

    public static IEnumerable<object[]> ShiftLeft1400Ms_ShiftRight700_DoesntShift()
    {

      yield return new object[]
      {
        new List<SubtitleModel>
        {
          new SubtitleModel
          {
            Order = 0,
            Start = TimeSpan.Parse("00:00:8.608"),
            End = TimeSpan.Parse("00:00:14.024"),
            Text = new List<string>
            {
              "Subtitle by : Amir Barzagli"
            }
          },
          new SubtitleModel
          {
            Order = 1,
            Start = TimeSpan.Parse("00:00:47.803"),
            End = TimeSpan.Parse("00:00:51.223"),
            Text = new List<string>
            {
              "<i>- Semua dah sedia?",
              "- Kau tak patut gantikan aku.</i>"
            }
          },
          new SubtitleModel
          {
            Order = 2,
            Start = TimeSpan.Parse("00:00:51.474"),
            End = TimeSpan.Parse("00:00:54.101"),
            Text = new List<string>
            {
              "<i>Aku tahu, tapi aku rasa",
              "macam kerja shift.</i>"
            }
          },
        },
        -1400
      };

      yield return new object[]
      {
        new List<SubtitleModel>
        {
          new SubtitleModel
          {
            Order = 0,
            Start = TimeSpan.Parse("00:00:10.708"),
            End = TimeSpan.Parse("00:00:16.124"),
            Text = new List<string>
            {
              "Subtitle by : Amir Barzagli"
            }
          },
          new SubtitleModel
          {
            Order = 1,
            Start = TimeSpan.Parse("00:00:49.903"),
            End = TimeSpan.Parse("00:00:53.323"),
            Text = new List<string>
            {
              "<i>- Semua dah sedia?",
              "- Kau tak patut gantikan aku.</i>"
            }
          },
          new SubtitleModel
          {
            Order = 2,
            Start = TimeSpan.Parse("00:00:53.574"),
            End = TimeSpan.Parse("00:00:56.201"),
            Text = new List<string>
            {
              "<i>Aku tahu, tapi aku rasa",
              "macam kerja shift.</i>"
            }
          },
        },
        700
      };

      yield return new object[]
      {
        new List<SubtitleModel>
        {
          new SubtitleModel
          {
            Order = 0,
            Start = TimeSpan.Parse("00:00:10.008"),
            End = TimeSpan.Parse("00:00:15.424"),
            Text = new List<string>
            {
              "Subtitle by : Amir Barzagli"
            }
          },
          new SubtitleModel
          {
            Order = 1,
            Start = TimeSpan.Parse("00:00:49.203"),
            End = TimeSpan.Parse("00:00:52.623"),
            Text = new List<string>
            {
              "<i>- Semua dah sedia?",
              "- Kau tak patut gantikan aku.</i>"
            }
          },
          new SubtitleModel
          {
            Order = 2,
            Start = TimeSpan.Parse("00:00:52.874"),
            End = TimeSpan.Parse("00:00:55.501"),
            Text = new List<string>
            {
              "<i>Aku tahu, tapi aku rasa",
              "macam kerja shift.</i>"
            }
          },
        },
        0
      };
    }
    #endregion
  }
}
