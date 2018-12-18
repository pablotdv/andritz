using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SubtitleTimeshift
{
    public static class Shifter
    {
        async static public Task Shift(Stream input, Stream output, TimeSpan timeSpan, Encoding encoding, int bufferSize = 1024, bool leaveOpen = false)
        {
            using (var reader = new StreamReader(input, encoding, true, bufferSize, leaveOpen))
            using (var writer = new StreamWriter(output, encoding, bufferSize, leaveOpen))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var times = Regex.Split(line, @"\s-->\s");
                    if (times.Length == 2)
                    {
                        var start = TimeSpan.Parse(times[0]).Add(timeSpan).ToString("hh':'mm':'ss'.'fff");
                        var end = TimeSpan.Parse(times[1]).Add(timeSpan).ToString("hh':'mm':'ss'.'fff");
                        line = $"{start} --> {end}";
                    }

                    await writer.WriteLineAsync(line);
                }
            }
        }
    }
}
