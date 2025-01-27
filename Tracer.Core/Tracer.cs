using System.Diagnostics;

namespace Core;

public class Tracer : ITracer
{
    private readonly Stopwatch _stopwatch = new();
    private StackFrame? _frame;
    private const int StackIndex = 1;

    public void StartTrace()
    {
        _stopwatch.Start();
        var stackTrace = new StackTrace();
        _frame = stackTrace.GetFrame(StackIndex);
    }

    public void StopTrace()
    {
        _stopwatch.Stop();
    }

    public TraceResult GetTraceResult()
    {
        var methodName = string.Empty;
        var className = string.Empty;
        if (_frame == null) return new TraceResult(methodName, className, _stopwatch.ElapsedMilliseconds);
        var method = _frame.GetMethod();
        if (method == null) return new TraceResult(methodName, className, _stopwatch.ElapsedMilliseconds);
        if (method.DeclaringType != null)
        {
            className = method.DeclaringType.Name;
        }
        methodName = method.Name;
        return new TraceResult(methodName, className, _stopwatch.ElapsedMilliseconds);
    }
}