namespace DalTest;

internal class Program
{
    try{
    static void Main(string[] args)
    {
       private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
    private static Engineer? s_dalEngineer = new EngineerIlementation(); //stage 1
    private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    Initialization.Do(s_dalDependency, s_dalEngineer, s_dalTask);

catch{
        Console.WriteLine(e.Message);
}

