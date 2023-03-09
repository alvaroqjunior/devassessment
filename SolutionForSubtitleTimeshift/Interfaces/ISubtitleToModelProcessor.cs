using SolutionForSubtitleTimeshift.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SolutionForSubtitleTimeshift.Interfaces
{
  public interface ISubtitleToModelProcessor
  {
    Task<IEnumerable<SubtitleModel>> GenerateModel(Stream stream, Encoding encoding, int buffersize, bool leaveOpen);
  }
}
