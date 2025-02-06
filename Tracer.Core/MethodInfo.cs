using System.Diagnostics;
using System.Text;

namespace Core;

public class MethodInfo
{
    public required Stopwatch Stopwatch { get; set; }
    public required string MethodName { get; init; } 
    public required string ClassName { get; init; }
    public long Time { get; set; }
    public List<MethodInfo> Methods { get; set; } = [];

    public void StartTimer()
    {
        Stopwatch.Start();
    }

    public void StopTimer()
    {
        Stopwatch.Stop();
        Time = Stopwatch.ElapsedMilliseconds;
    }
    
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append($"MethodName {MethodName}, ClassName: {ClassName}, Time: {Time}");
        foreach (var method in Methods)
        {
            builder.Append($"\n{method}");
        } 
        return builder.ToString();
    }
}