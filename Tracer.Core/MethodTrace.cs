namespace Core;

public class MethodTrace
{
    public string Name { get; set; }
    public string Class { get; set; }
    public long Time { get; set; }
    public List<MethodTrace> Methods { get; set; } = new List<MethodTrace>();
}