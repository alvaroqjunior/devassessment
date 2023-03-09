using SolutionForSubtitleTimeshift.Models;
using System;
using System.Collections.Generic;

namespace SolutionForSubtitleTimeshift.Interfaces
{
  public interface ISubtitleEditor
  {
    void SyncSubtitle(IEnumerable<SubtitleModel> subtitleModelToChange, TimeSpan time);
  }
}
