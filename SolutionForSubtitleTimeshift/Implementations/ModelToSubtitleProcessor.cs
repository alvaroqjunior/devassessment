using SolutionForSubtitleTimeshift.Interfaces;
using SolutionForSubtitleTimeshift.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SolutionForSubtitleTimeshift.Implementations
{
  public class ModelToSubtitleProcessor : IModelToSubtitleProcessor
  {
    private bool _dotSeparator;

    public async Task GenerateFromModel(IEnumerable<SubtitleModel> model, Stream stream, Encoding encoding, int bufferSize, bool leaveOpen, bool dotSeparator = false)
    {
      _dotSeparator = dotSeparator;

      using (var streamWriter = new StreamWriter(stream, encoding, bufferSize, leaveOpen))
      {
        foreach (var subtitleModel in model)
        {
          await WriteSubtitleModelToFile(streamWriter, subtitleModel);
        };
      }
    }

    private async Task WriteSubtitleModelToFile(StreamWriter streamWriter, SubtitleModel model)
    {

      await WriteSubtitleOrder(streamWriter, model.Order);

      await WriteSubtitleTimeSpanSection(streamWriter, model.Start, model.End);

      await WriteSubtitleTextSection(streamWriter, model.Text);
    }

    private async Task WriteSubtitleOrder(StreamWriter streamWriter, int orderNumber)
    {
      await streamWriter.WriteLineAsync(orderNumber.ToString());
    }

    private async Task WriteSubtitleTimeSpanSection(StreamWriter streamWriter, TimeSpan startTime, TimeSpan endTime)
    {
      string startTimeSpan;
      string endTimeSpan;

      if (_dotSeparator)
      {
        startTimeSpan = startTime.ToString(@"hh\:mm\:ss\.fff");
        endTimeSpan = endTime.ToString(@"hh\:mm\:ss\.fff");
      }
      else
      {
        startTimeSpan = startTime.ToString(@"hh\:mm\:ss\,fff");
        endTimeSpan = endTime.ToString(@"hh\:mm\:ss\,fff");
      }

      await streamWriter.WriteLineAsync($"{startTimeSpan} --> {endTimeSpan}");
    }

    private async Task WriteSubtitleTextSection(StreamWriter streamWriter, IEnumerable<string> subtitleSectionText)
    {
      foreach (var line in subtitleSectionText)
      {
        await streamWriter.WriteLineAsync(line);
      }

      await streamWriter.WriteLineAsync();
    }
  }
}
