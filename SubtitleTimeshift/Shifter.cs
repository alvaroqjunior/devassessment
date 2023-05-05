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
            const string subtitleDivider = " --> ";
            try
            {
                using (var inputReaderData = new StreamReader(input, encoding, true))
                using (var outputData = new StreamWriter(output, encoding))
                {
                    string line;
                    while(!inputReaderData.EndOfStream)
                    {
                        line = inputReaderData.ReadLine();

                        if (!line.Contains(subtitleDivider))
                        {
                            await outputData.WriteLineAsync(line);
                            continue;
                        }

                        var lineParsed = ShifterSubtitlesOnStream(line, timeSpan);
                        await outputData.WriteLineAsync(lineParsed);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error message: {e.Message}");
                throw;
            }

            string ShifterSubtitlesOnStream(string line, TimeSpan timespan)
            {
                var timesSubtitlesSplited = line.Split(new[] { subtitleDivider }, StringSplitOptions.None);
                for (int i = 0; i < timesSubtitlesSplited.Length; i++)
                {
                    var parsedDataSubtitle = TimeSpan.Parse(timesSubtitlesSplited[i]);
                    timesSubtitlesSplited[i] = parsedDataSubtitle.Add(timespan).ToString(@"hh\:mm\:ss\.fff");
                }

                return timesSubtitlesSplited[0] + subtitleDivider + timesSubtitlesSplited[1].ToString();
            }
        }
    }
}
