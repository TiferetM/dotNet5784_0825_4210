using DalApi;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Xml.Linq;


namespace Dal;

//stage 3

internal sealed partial class DalXml : IDal
{
    private static string data_config_xml = "data-config";

    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementation();
    public DateTime ?StartProjectDate { get; set; }
    public DateTime? EndProjectDate { get; set; }


    public void Reset()
    {
        Engineer.Reset();
        Task.Reset();
        Dependency.Reset();
    }
}




