namespace Dal
{
    using System;
    using DalApi;

    internal sealed partial class DalList : IDal
    {
        private static readonly IDal instance = new DalList();

        public static IDal Instance { get { return instance; } }

        private DalList() { }

        public ITask Task => new TaskImplementation();

        public IDependency Dependency => new DependencyImplementation();

        public IEngineer Engineer => new EngineerImplementation();

        public DateTime? StartProjectDate {
            get => DataSource.Config.startProjectDate;
        }

        public DateTime? EndProjectDate
        {
            get => DataSource.Config.endProjectDate;
        }

        public void Reset()
        {
            if (DataSource.Dependencies != null)
                DataSource.Dependencies?.Clear();

        }
    }
}
