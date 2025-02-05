using System.Diagnostics;
using System.Text;

namespace Core;

public class MethodInfo(Stopwatch stopwatch)
{
    private readonly Stopwatch _stopwatch = stopwatch;
    public required string MethodName { get; init; } 
    public required string ClassName { get; init; }
    public long Time { get; private set; }
    public List<MethodInfo> Methods { get; set; } = [];

    public void StartTimer()
    {
        _stopwatch.Start();
    }

    public void StopTimer()
    {
        _stopwatch.Stop();
        Time = _stopwatch.ElapsedMilliseconds;
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