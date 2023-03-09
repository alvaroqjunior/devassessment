using SolutionForSubtitleTimeshift.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SolutionForSubtitleTimeshift.Interfaces
{
  public interface IModelToSubtitleProcessor
  {
    Task GenerateFromModel(IEnumerable<SubtitleModel> model, Stream stream, Encoding encoding, int buffersize, bool leaveOpen, bool dotSeparator = false);
  }
}
