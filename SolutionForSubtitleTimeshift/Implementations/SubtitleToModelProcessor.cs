using SolutionForSubtitleTimeshift.Interfaces;
using SolutionForSubtitleTimeshift.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolutionForSubtitleTimeshift.Implementations
{
  public class SubtitleToModelProcessor : ISubtitleToModelProcessor
  {
    //private int orderSectionCounter = 0;

    public async Task<IEnumerable<SubtitleModel>> GenerateModel(Stream stream, Encoding encoding, int buffersize, bool leaveOpen)
    {
      //var endFromPreviousSection = TimeSpan.Zero;
      var structureSubtitleModel = new List<SubtitleModel>();
      var detectBOM = true;

      using (var streamReader = new StreamReader(stream, encoding, detectBOM, buffersize, leaveOpen))
      {
        while (!streamReader.EndOfStream)
        {
          var subtitleModel = new SubtitleModel();

          var sectionOrder = await RemoveStartWhiteLines(streamReader);

          if (!streamReader.EndOfStream)
          {
            subtitleModel.Order = int.Parse(sectionOrder);
            (subtitleModel.Start, subtitleModel.End) = await ExtractSubtitleTimeSpan(streamReader);
            subtitleModel.Text = await ExtractSubtitleText(streamReader);


            //if (!isTimeSpanInSync(endFromPreviousSection, subtitleModel.Start, subtitleModel.End))
            //  throw new InvalidDataException("Invalid Data: Date is out of sync.");

            //endFromPreviousSection = subtitleModel.End;

            structureSubtitleModel.Add(subtitleModel);
          }
        }
      }

      return structureSubtitleModel;
    }

    private async Task<(TimeSpan, TimeSpan)> ExtractSubtitleTimeSpan(StreamReader streamReader)
    {
      var timeSpanLine = await streamReader.ReadLineAsync();

      TimeSpan startTimeSpan;
      TimeSpan endTimeSpan;

      timeSpanLine = timeSpanLine.Trim('\r', '\n', ' ');

      try
      {
        var startTime = Regex.Matches(timeSpanLine, @"^[0-9]{1,2}[:][0-9]{1,2}[:][0-9]{1,2}[, | .][0-9]{3,7}")[0].ToString().Replace(",", ".");
        var endTime = Regex.Matches(timeSpanLine, @"[0-9]{1,2}[:][0-9]{1,2}[:][0-9]{1,2}[, | .][0-9]{3,7}$")[0].ToString().Replace(",", ".");

        startTimeSpan = TimeSpan.Parse(startTime);
        endTimeSpan = TimeSpan.Parse(endTime);
      }
      catch (Exception ex)
      {
        throw new InvalidDataException("Invalid Data: Incorrect format for date.", ex);
      }


      return (startTimeSpan, endTimeSpan);
    }

    private async Task<List<string>> ExtractSubtitleText(StreamReader streamReader)
    {
      var textSection = new List<string>();
      string textLine;
      bool emptyLineAdded = false;
      int maxAllowedLinesForText = 0;

      do
      {
        textLine = await streamReader.ReadLineAsync();

        if (textSection.Count == 0)
        {
          textSection.Add(textLine);
          if (textLine == string.Empty)
            emptyLineAdded = true;
        }
        else
          if (textLine != string.Empty)
            textSection.Add(textLine);

        maxAllowedLinesForText++;

      } while (!streamReader.EndOfStream && !emptyLineAdded && maxAllowedLinesForText != 2);

      return textSection;
    }

    //private bool isTimeSpanInSync(TimeSpan endFromPreviousSection, TimeSpan start, TimeSpan end)
    //{
    //  return (end >= start && start >= endFromPreviousSection);
    //}

    private async Task<string> RemoveStartWhiteLines(StreamReader streamReader)
    {
      string line = null;

      while (string.IsNullOrEmpty(line) && !streamReader.EndOfStream)
      {
        line = await streamReader.ReadLineAsync();
      }

      return line;
    }

    private bool IsEnd(int? order, TimeSpan? start, TimeSpan? end, IEnumerable<string> text)
    {
      return (order == null || start == null || end == null || text.Count() == 0);
    }
  }
}
