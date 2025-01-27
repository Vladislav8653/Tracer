namespace Core;

public class TraceResult
{
    public TraceResult(string methodName, string className, long time)
    {
        MethodName = methodName;
        ClassName = className;
        Time = time;
    }
    
    public string MethodName { get; }
    public string ClassName { get; }
    public long Time { get; }
    //public List<TraceResult>? Children { get; } = null;
}