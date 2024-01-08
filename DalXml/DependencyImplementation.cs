using DalApi;
using DalXml;
using DO;
using System.Xml.Linq;
namespace Dal;
internal class DependencyImplementation : IDependency
{
    public int Create(Dependency item)//done
    {
        XElement dependenciesDoc = XMLTools.LoadListFromXMLElement("Dependencies");
        int newId = Config.NextDependencyId;//new config number
        XElement newDep = new XElement("dependency",
            new XElement("Id", newId),
            new XElement("DependenceTask", item.DependenceTask),
            new XElement("DependenceOnTask", item.DependenceOnTask));
        dependenciesDoc.Add(newDep);//added to the Xml file
        XMLTools.SaveListToXMLElement(dependenciesDoc, "Dependencies");
        return newId;
    }

    public void Delete(int id)//done
    {
        XElement dependenciesDoc = XMLTools.LoadListFromXMLElement("Dependencies");
        if (dependenciesDoc != null)
        {
            XElement? elemToDel = dependenciesDoc.Elements("Dependency").FirstOrDefault(d => d!.Element("Id")!.Value.Equals(id));
            if (elemToDel != null)
            {
                dependenciesDoc.Remove();
                XMLTools.SaveListToXMLElement(dependenciesDoc, "Dependencies");

            }
        }
        else
            throw new DalDoesNotExistException($"אובייקט מסוג Dependency עם ID {id} לא קיים");

    }
    public Dependency? Read(int id)//done!!
    {
        XElement dependenciesDoc = XMLTools.LoadListFromXMLElement("Dependencies");
        XElement? elemToRead = dependenciesDoc.Elements("Dependency").FirstOrDefault(d => d.Element("Id")!.Value.Equals(id));
        if (elemToRead != null)
        {
            Dependency dependencyToShow = new Dependency
            {
                Id = Convert.ToInt32(elemToRead.Element("Id")!.Value),
                DependenceTask = Convert.ToInt32(elemToRead.Element("DependenceTask")!.Value),
                DependenceOnTask = Convert.ToInt32(elemToRead.Element("DependenceOnTask")!.Value)
            };
        }
        throw new NotImplementedException();
    }

    public Dependency? Read(Func<Dependency, bool> filter)//done
    {
        XElement dependenciesDoc = XMLTools.LoadListFromXMLElement("Dependencies");
        Dependency? elementToRead = dependenciesDoc?.Elements("Dependency")
      .Select(d =>
      {
          Dependency dependency = new Dependency();
          dependency.Id = int.TryParse(d.Element("Id")?.Value, out int id) ? id : 0;
          dependency.DependenceTask = int.TryParse(d.Element("DependenceTask")?.Value, out int dependentTask) ? dependentTask : 0;
          dependency.DependenceOnTask = int.TryParse(d.Element("DependenceOnTask")?.Value, out int dependsOnTask) ? dependsOnTask : 0;
          return dependency;
      })
      .FirstOrDefault(dependency => filter(dependency));
        if (elementToRead != null)
            return elementToRead;
        else
            return null;
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        XElement dependenciesDoc = XMLTools.LoadListFromXMLElement("Dependencies");
        if (filter != null)
        {
            List<Dependency> list = dependenciesDoc!.Elements("Dependency")
            .Select(d =>
            {
                Dependency dependency = new Dependency();
                dependency.Id = int.TryParse(d.Element("Id")?.Value, out int id) ? id : 0;
                dependency.DependenceTask = int.TryParse(d.Element("DependenceTask")?.Value, out int dependentTask) ? dependentTask : 0;
                dependency.DependenceOnTask = int.TryParse(d.Element("DependenceOnTask")?.Value, out int dependsOnTask) ? dependsOnTask : 0;
                return dependency;
            })
          .Where(dependency => filter(dependency)).ToList();
            return list;
        }
        else
        {
            List<Dependency> list = dependenciesDoc!.Elements("dependency")
            .Select(d =>
            {
                Dependency dependency = new Dependency();
                dependency.Id = int.TryParse(d.Element("Id")?.Value, out int id) ? id : 0;
                dependency.DependenceTask = int.TryParse(d.Element("DependenceTask")?.Value, out int dependentTask) ? dependentTask : 0;
                dependency.DependenceOnTask = int.TryParse(d.Element("DependenceOnTask")?.Value, out int dependsOnTask) ? dependsOnTask : 0;
                return dependency;
            }).ToList();
            return list;
        }
        throw new NotImplementedException();
    }

    public void Reset()
    {
   
        XElement root = XMLTools.LoadListFromXMLElement("Dependencies");
        root.Descendants("dependency").Remove();
        XMLTools.SaveListToXMLElement(root, "Dependencies");
    }

    public void Update(Dependency item)
    {
        XElement dependenciesDoc = XMLTools.LoadListFromXMLElement("Dependencies");
        XElement? elemToUpdate = dependenciesDoc?.Elements("Dependency").FirstOrDefault(d => d!.Element("Id")!.Value.Equals(item.Id));
        if (elemToUpdate != null)
        {
            dependenciesDoc?.Remove();
            // XElement update;
            XElement updateDetailes = new XElement("dependency",
            new XElement("Id", item.Id),
            new XElement("DependenceTask", item.DependenceTask),
            new XElement("DependenceOnTask", item.DependenceOnTask));
            dependenciesDoc?.Add(updateDetailes);//added to the Xml file
            if (dependenciesDoc != null)
                XMLTools.SaveListToXMLElement(dependenciesDoc, "Dependencies");
        }
        else
            throw new DalDoesNotExistException($"אובייקט מסוג Dependency עם ID {item.Id} לא קיים");
    }
}
