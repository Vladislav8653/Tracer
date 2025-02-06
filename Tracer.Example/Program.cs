using Core;
using Tracer.Serialization;

namespace Example;
class Program
{
    private static void Main()
    {
        var tracer = new Core.Tracer();
        var bar = new Bar(tracer);
        
        /*var threads = new Thread[3];

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(() =>
            {
                tracer.StartTrace();
                Test1(300, bar);
                tracer.StopTrace();
            });
            threads[i].Start();
        }


        foreach (var thread in threads)
        {
            thread.Join();
        }*/
        
        var tasks = new List<Task>();
        for (var i = 0; i < 4; i++)
        {
            var task = Task.Run(() =>
            {
                Foo foo = new Foo(tracer);
                foo.MyMethod();
            });
            tasks.Add(task);
        }
        Task.WhenAll(tasks).Wait();
        var result = tracer.GetTraceResult();
        var serializers = PluginLoader.LoadPlugins();
        foreach (var serializer in serializers)
        {
            var fileName = Path.Combine("Tracers", $"tracer.{serializer.Format}");
            using var fileStream = new FileStream(fileName, FileMode.Create);
            serializer.Serialize(result, fileStream);
        }
    }

    private static void Test1(int time, Bar bar)
    {
        Console.WriteLine("Test1 start");
        Thread.Sleep(time);
        bar.InnerMethod(200);
        Console.WriteLine("Test1 finish");
    }
}

public class Foo
{
    private Bar _bar;
    private ITracer _tracer;

    internal Foo(ITracer tracer)
    {
        _tracer = tracer;
        _bar = new Bar(_tracer);
    }
    
    public void MyMethod()
    {
        _tracer.StartTrace();
        Console.WriteLine("Outter method start");
         Thread.Sleep(100);
        _bar.InnerMethod(200);
        _tracer.StopTrace();
        Console.WriteLine("Outter method finish");
    }
}

public class Bar
{
    private readonly ITracer _tracer;

    public Bar(ITracer tracer)
    {
        _tracer = tracer;
    }
    
    public void InnerMethod(int time)
    {
        _tracer.StartTrace();
        Console.WriteLine("Inner method start");
        Thread.Sleep(time);
        _tracer.StopTrace();
        Console.WriteLine("Inner method finish");
    } 
}
