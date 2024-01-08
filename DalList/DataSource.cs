namespace Dal;

using System.Globalization;
using DO;
internal static class DataSource
{
    internal static class Config
    {
        internal const int startTaskId = 1000;
        private static int nextTaskId = startTaskId;
        public static int NextTaskId { get => nextTaskId++; }
        internal const int startDependencyId = 1000;
        private static int nextDependencyId = startDependencyId;
        public static int NextDependencyId { get => nextDependencyId++; }
        internal static DateTime startProjectDate = DateTime.ParseExact("2020 - 01 - 01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
        internal static DateTime endProjectDate = DateTime.ParseExact("2028 - 01 - 01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
    }
    internal static List<Task>? Tasks { get; } = new();//
    internal static List<Engineer> ?Engineers { get; } = new();
    internal static List<Dependency>? Dependencies { get; } = new();
 
}
