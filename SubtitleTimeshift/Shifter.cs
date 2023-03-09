using SolutionForSubtitleTimeshift.Implementations;
using SolutionForSubtitleTimeshift.Interfaces;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleTimeshift
{
  public class Shifter
  {
    async static public Task Shift(Stream input, Stream output, TimeSpan timeSpan, Encoding encoding, int bufferSize = 1024, bool leaveOpen = false)
    {
      var dotSeparator = true;

      ISubtitleToModelProcessor subtitleToModelProcessor = new SubtitleToModelProcessor();
      ISubtitleEditor editor = new SubtitleEditor();
      IModelToSubtitleProcessor modelToSubtitleProcessor = new ModelToSubtitleProcessor();

      var subtitleModel = await subtitleToModelProcessor.GenerateModel(input, encoding, bufferSize, leaveOpen);

      editor.SyncSubtitle(subtitleModel, timeSpan);

      await modelToSubtitleProcessor.GenerateFromModel(subtitleModel,output, encoding, bufferSize, leaveOpen, dotSeparator);
    }
  }
}
