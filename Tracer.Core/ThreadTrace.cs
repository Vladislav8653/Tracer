namespace Core;

public class ThreadTrace
{
    public string Id { get; set; }
    public long Time { get; set; }
    public List<MethodTrace> Methods { get; set; } = new List<MethodTrace>();
}