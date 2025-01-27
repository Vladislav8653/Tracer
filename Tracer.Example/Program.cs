using Core;

namespace Example;
class Program
{
    private static void Main(string[] args)
    {
        var tracer = new Tracer();
        tracer.StartTrace();
        Test1(69);
        tracer.StopTrace();
        /*tracer.StartTrace();
        Test2(52);
        tracer.StopTrace();*/
        var result = tracer.GetTraceResult();
        Console.WriteLine($"{result.ClassName}---{result.MethodName}---{result.Time}");
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
    
    private static void Test2(int time)
    {
        Thread.Sleep(time);
        Test21(time / 2);
        Test22(time / 2);
    }
    
    private static void Test21(int time)
    {
        Thread.Sleep(time);
    }
    
    private static void Test22(int time)
    {
        Thread.Sleep(time);
    }
    
    
}
