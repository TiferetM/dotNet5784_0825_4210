using DO;


namespace BLTest;
internal class Program
{

    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    static void Main(string[] args)
    {
        BO.Engineer engineer = new BO.Engineer()
        {
           
      //  Level = (int)EngineerExperience.Beginner,
                       
        };

        s_bl.Engineer.AddEngineer(engineer);

        Console.Write("Would you like to create Initial data? (Y/N)");
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        //if (ans == "Y")
            //Initialization.Do(s_bl);
    }

}


