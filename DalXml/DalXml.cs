using DalApi;
using System.Diagnostics;


namespace Dal
{
    ////stage 3

    internal sealed partial class DalXml : IDal
    {
        private static string data_config_xml="data-config";

        public static IDal Instance { get; } = new DalXml();
        private DalXml() { }
        public ITask Task => new TaskImplementation();

        public IDependency Dependency => new DependencyImplementation();

        public IEngineer Engineer => new EngineerImplementation();
        public DateTime? EndProjectDate
        {
            get => XMLTools.GetDates("data-config", "endProjectDate");

        }
        public DateTime? StartProjectDate
        {
            get => XMLTools.GetDates("data-config", "startProjectDate");
        }

        //public DateTime StartProjectDate => new UpdateScheduledDates(int ,m-start,m-end)
        //internal static DateTime StartProjectDate { get => XMLTools.GetDates(data_config_xml, "startProjectDate"); }
        //internal static DateTime EndProjectDate { get => XMLTools.GetDates(data_config_xml, "endProjectDate"); }


        public void Reset()
        {
            Engineer.Reset();
            Task.Reset();
            Dependency.Reset();
        }
    }
}




