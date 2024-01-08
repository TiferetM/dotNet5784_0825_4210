using DalApi;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Xml.Linq;


namespace Dal;

////stage 3

internal sealed partial class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    //public DateTime StartProjectDate => new UpdateScheduledDates(int ,m-start,m-end)
    DateTime? IDal.EndProjectDate => Convert.ToDateTime(XDocument.Load(@"..\xml\data-config.xml").Descendants("EndProjectDate").FirstOrDefault()!
   .Value == default ? null : XDocument.Load(@"..\xml\data-config.xml").Descendants("EndProjectDate").FirstOrDefault()!.Value);

    DateTime? IDal.StartProjectDate => Convert.ToDateTime(XDocument.Load(@"..\xml\data-config.xml").Descendants("StartProjectDate").FirstOrDefault()!
        .Value == default ? null : XDocument.Load(@"..\xml\data-config.xml").Descendants("StartProjectDate").FirstOrDefault()!.Value);






    public void Reset()
    {
        Console.WriteLine("I in RESET in IDal");
        Engineer.Reset();
        Task.Reset();
        Dependency.Reset();
        //XMLTools.ResetFile("dependencies");
    }
}




