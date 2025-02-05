namespace Core;

public class TraceResult(IReadOnlyList<ThreadInfo> threads)
{
    public IReadOnlyList<ThreadInfo> Threads { get; } = threads;
} 