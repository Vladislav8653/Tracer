using System.Collections.Immutable;
using System.Diagnostics;

namespace Core;

public class Tracer : ITracer
{
    private readonly HashSet<ThreadInfo> _threads;
    private readonly Stack<MethodInfo> _methods;
   

    public Tracer()
    {
        _methods = new Stack<MethodInfo>();
        _threads = [];
    }

    public void StartTrace()
    {
        var stackTrace = new StackTrace();
        var methodName = stackTrace.GetFrame(1)?.GetMethod()?.Name;
        if (methodName == null) throw new NullReferenceException("Method name was null");
        var className = stackTrace.GetFrame(1)?.GetMethod()?.DeclaringType?.Name;
        if (className == null) throw new NullReferenceException("Class name was null");
        var methodInfo = new MethodInfo(new Stopwatch())
        {
            ClassName = className,
            MethodName = methodName
        };
        _methods.Push(methodInfo);
        methodInfo.StartTimer();
    }

    public void StopTrace()
    {
        var methodInfo = _methods.Pop();
        methodInfo.StopTimer();
        var currentThreadId = Environment.CurrentManagedThreadId;
        _threads.Add(new ThreadInfo(currentThreadId));
        var currentThread = _threads.First(s => s.ThreadId == currentThreadId);
        currentThread.Time += methodInfo.Time;
        if (_methods.Count > 0)
        {
            var parentMethodInfo = _methods.Peek();
            parentMethodInfo.Methods.Add(methodInfo);
        }
        else
        {
            currentThread.Methods.Add(methodInfo);
        }
    }

    public TraceResult GetTraceResult()
    {
        return new TraceResult(_threads.ToImmutableList());
    }
}

