using DalApi;
using System.Diagnostics;


namespace Dal
{
    ////stage 3

    internal sealed partial class DalXml : IDal
    {
        public static IDal Instance { get; } = new DalXml();
        private DalXml() { }
        public ITask Task => new TaskImplementation();

        public IDependency Dependency => new DependencyImplementation();

        public IEngineer Engineer => new EngineerImplementation();

        //public DateTime StartProjectDate => new UpdateScheduledDates(int ,m-start,m-end)
        public DateTime StartProjectDate { get; set; } 
        public DateTime EndProjectDate { get; set; }

        public void Reset()
        {
            Engineer.Reset();
            Task.Reset();
            Dependency.Reset();
            //XMLTools.ResetFile("dependencies");
        }
    }
}




