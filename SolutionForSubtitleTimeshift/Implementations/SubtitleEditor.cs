using SolutionForSubtitleTimeshift.Interfaces;
using SolutionForSubtitleTimeshift.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolutionForSubtitleTimeshift.Implementations
{
  public class SubtitleEditor : ISubtitleEditor
  {
    public void SyncSubtitle(IEnumerable<SubtitleModel> subtitleModelToChange, TimeSpan time)
    {
      subtitleModelToChange.ToList().ForEach(sm => {
        sm.Start = sm.Start.Add(time);
        sm.End = sm.End.Add(time);
      });
    }
  }
}
