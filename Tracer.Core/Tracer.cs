using System.Diagnostics;

namespace Core;

public class Tracer : ITracer
{
    private readonly Stack<MethodTrace> _methodStack = new Stack<MethodTrace>();
    private readonly Stopwatch _stopwatch = new Stopwatch();
    private readonly ThreadTrace _currentThread;

    public Tracer()
    {
        _currentThread = new ThreadTrace { Id = Thread.CurrentThread.ManagedThreadId.ToString() };
    }

    public void StartTrace()
    {
        _stopwatch.Restart();
    }

    public void StopTrace()
    {
        _stopwatch.Stop();
        var elapsedTime = _stopwatch.ElapsedMilliseconds;

        var currentMethod = new MethodTrace
        {
            Name = new StackTrace().GetFrame(1).GetMethod().Name,
            Class = new StackTrace().GetFrame(1).GetMethod().DeclaringType.Name,
            Time = elapsedTime
        };

        if (_methodStack.Count > 0)
        {
            _methodStack.Peek().Methods.Add(currentMethod);
        }
        else
        {
            _currentThread.Methods.Add(currentMethod);
        }
    }

    public TraceResult GetTraceResult()
    {
        return new TraceResult
        {
            Threads = new List<ThreadTrace> { _currentThread }
        };
    }
}

