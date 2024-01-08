using DalApi;
namespace Dal;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using DO;
using global::DalXml;


//using global::DalXml;

internal class TaskImplementation : ITask
{
    public int Create(DO.Task item)
    {

        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>("Tasks");

        int newId = Config.NextTaskId;
      
        item.Id = newId;

        tasks.Add(item);
        XMLTools.SaveListToXMLSerializer(tasks, "Tasks");

        return newId;
    }

    public void Delete(int id)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>("Tasks");

        DO.Task? toDelete = tasks.FirstOrDefault(t => t.Id == id);
        if (toDelete != null)
        {
            tasks.Remove(toDelete);
            XMLTools.SaveListToXMLSerializer(tasks, "Tasks");
        }
        else
        {
            throw new DalDoesNotExistException($"Task object with ID {id} does not exist");
        }
    }

    public DO.Task? Read(int id)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>("Tasks");
        return tasks.FirstOrDefault(t => t.Id == id);
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>("Tasks");
        return tasks.FirstOrDefault(filter);
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>("Tasks");
        return filter != null ? tasks.Where(filter) : tasks;
    }

    public void Update(DO.Task item)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>("Tasks");

        DO.Task? toUpdate = tasks.FirstOrDefault(t => t.Id == item.Id);
        if (toUpdate != null)
        {
            int index = tasks.IndexOf(toUpdate);
            tasks[index] = item;
            XMLTools.SaveListToXMLSerializer(tasks, "Tasks");
        }
        else
        {
            throw new DalDoesNotExistException($"Task object with ID {item.Id} does not exist");
        }
    }
    public void Reset()
    {
        XElement root = XMLTools.LoadListFromXMLElement("Tasks");
        root.Descendants("Task").Remove();
        XMLTools.SaveListToXMLElement(root, "Tasks");
    }
}
