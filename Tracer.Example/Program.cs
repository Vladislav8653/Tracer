using Core;

namespace Example;
class Program
{
    private static void Main(string[] args)
    {
        var tracer = new Tracer();
        var bar = new Bar(tracer);  
        tracer.StartTrace();
        Test1(1000);
        bar.InnerMethod(2000);
        tracer.StopTrace();
        tracer.StartTrace();
        Test1(500);
        tracer.StopTrace();
        var result = tracer.GetTraceResult();
        foreach (var threadInfo in result.Threads)
        {
            Console.WriteLine(threadInfo.ToString());
        }
    }

    private static void Test1(int time)
    {
        Thread.Sleep(time);
        //Test11(time / 2);
    }
    
    private static void Test11(int time)
    {
        Thread.Sleep(time);
    }
}

class Bar
{
    private ITracer _tracer;

    internal Bar(ITracer tracer)
    {
        _tracer = tracer;
    }
    
    public void InnerMethod(int time)
    {
        _tracer.StartTrace();
        Thread.Sleep(time);
        _tracer.StopTrace();
    } 
}
