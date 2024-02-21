using System.Reflection;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace BO;

public static class Tools
{
    public static string ToStringProperty<T>(T obj)
    {
        if (obj == null)
            return "";
        Type type = typeof(T);

        // Get all public properties of the type
        PropertyInfo[] properties = type.GetProperties();

        // Create a string representation by concatenating property names and values
        string result = $"{type.Name} properties:\n";

        foreach (var property in properties)
        {
            object propertyValue = property.GetValue(obj);
            result += $"{property.Name}: {propertyValue}\n";
        }

        return result;
    }

    public class DistinctIntList : IEqualityComparer<List<int?>>
    {
        public bool Equals(List<int?>?x, List<int?>? y)
        {
            if (x is not null && y is not null)
                return x.SequenceEqual(y);
            if (x is null && y is null)
                return true;
            return false;
        }
        public int GetHashCode(List<int?> obj)
        {
            int sum = 0;
            foreach(int? num in obj)
            {
                sum += num ?? default(int);
            }
            return sum;
        }
    }

    public static Status DetermineStatus(DO.Task doTask)
    {
        //finsh function
        return (Status)(doTask.SchedulableDate is null ? 0 ://set the status
                    doTask.StartDate is null ? 1 :
                    doTask.CompletedDate is null ? 2
                    : 3);
        
    }

}
