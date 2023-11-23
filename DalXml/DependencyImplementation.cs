﻿using DalApi;
using DalXml;
using DO;
using System.Xml.Linq;
namespace Dal;
internal class DependencyImplementation : IDependency
{
    public int Create(Dependency item)//done
    {
        XElement dependenciesDoc = XMLTools.LoadListFromXMLElement("Dependencies.xml");
        int newId = Config.NextDependencyId;//new config number
        XElement newDep = new XElement("dependency",
            new XElement ("Id",newId),
            new XElement("DependenceTask", item.DependenceTask),
            new XElement("DependenceOnTask", item.DependenceOnTask));
        dependenciesDoc.Add(newDep);//added to the Xml file
        XMLTools.SaveListToXMLElement(dependenciesDoc, "Dependencies.xml");
           return newId;
    }

    public void Delete(int id)//done
    {
        XElement dependenciesDoc = XMLTools.LoadListFromXMLElement("Dependencies.xml");
        XElement? elemToDel = (XElement)dependenciesDoc.Elements("Dependency").FirstOrDefault(d => d!.Element("Id")!.Value.Equals(id));
        if(elemToDel!=null)
        {
            dependenciesDoc.Remove();
            XMLTools.SaveListToXMLElement(dependenciesDoc, "Dependencies.xml");

        }
        else
            throw new DalDoesNotExistException($"אובייקט מסוג Dependency עם ID {id} לא קיים");

    }
    public Dependency? Read(int id)//done!!
    {
        XElement dependenciesDoc = XMLTools.LoadListFromXMLElement("Dependencies.xml");
        XElement? elemToRead = (XElement)dependenciesDoc.Elements("Dependency").FirstOrDefault(d => d.Element("Id")!.Value.Equals(id));
        if (elemToRead != null)
        {
            Dependency dependencyToShow = new Dependency();
            dependencyToShow.Id = Convert.ToInt32(elemToRead.Element("Id")!.Value);
            dependencyToShow.DependenceTask   = Convert.ToInt32(elemToRead.Element("DependenceTask")!.Value);
            dependencyToShow.DependenceOnTask = Convert.ToInt32(elemToRead.Element("DependenceOnTask")!.Value);
        }
        throw new NotImplementedException();
    }

    public Dependency? Read(Func<Dependency, bool> filter)//done
    {
        XElement dependenciesDoc = XMLTools.LoadListFromXMLElement("Dependencies.xml");
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
        XElement dependenciesDoc = XMLTools.LoadListFromXMLElement("Dependencies.xml");
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
            List<Dependency> list = dependenciesDoc!.Elements("Dependency")
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

    public void Update(Dependency item)
    {
        XElement dependenciesDoc = XMLTools.LoadListFromXMLElement("Dependencies.xml");
        XElement? elemToUpdate = (XElement)dependenciesDoc?.Elements("Dependency").FirstOrDefault(d => d!.Element("Id")!.Value.Equals(item.Id));
        if (elemToUpdate != null)
        {
            dependenciesDoc.Remove();
            XElement update;
            XElement updateDetailes=new XElement("dependency",
            new XElement("Id", item.Id),
            new XElement("DependenceTask", item.DependenceTask),
            new XElement("DependenceOnTask", item.DependenceOnTask));
            dependenciesDoc.Add(updateDetailes);//added to the Xml file
            XMLTools.SaveListToXMLElement(dependenciesDoc, "Dependencies.xml");
        }
        else
            throw new DalDoesNotExistException($"אובייקט מסוג Dependency עם ID {item.Id} לא קיים");
    }
}
