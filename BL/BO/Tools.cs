

using System.Reflection;

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

    public static Status DetermineStatus(DO.Task doTask)
    {
        //finsh function
        throw new NotImplementedException();
    }
}
