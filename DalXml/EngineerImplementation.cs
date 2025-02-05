﻿using DalApi;
using DO;
using System.Xml.Linq;
namespace Dal;
using System.IO;
using System.Xml.Serialization;
internal class EngineerImplementation : IEngineer
{
    
    public int Create(DO.Engineer item)
    {

        List<DO.Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>("Engineers");

        Engineers.Add(item);
        XMLTools.SaveListToXMLSerializer(Engineers, "Engineers");

        return item.Id;
    }

    public void Delete(int id)
    {
        List<DO.Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>("Engineers");
        if (Engineers != null)
        {
            DO.Engineer? toDelete = Engineers.FirstOrDefault(t => t.Id == id);
            if (toDelete != null)
            {
                Engineers.Remove(toDelete);
                XMLTools.SaveListToXMLSerializer(Engineers, "Engineers");
            }
            else
            {
                throw new DalDoesNotExistException($"Task object with ID {id} does not exist");
            }
        }
        else
        {
            throw new DalDoesNotExistException($"Task object with ID {id} does not exist");
        }
    }

    public DO.Engineer? Read(int id)
    {
        List<DO.Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>("Engineers");
        return Engineers.FirstOrDefault(t => t.Id == id);
    }

    public DO.Engineer? Read(Func<DO.Engineer, bool> filter)
    {
        List<DO.Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>("Engineers");
        return Engineers.FirstOrDefault(filter);
    }

    public IEnumerable<DO.Engineer?> ReadAll(Func<DO.Engineer, bool>? filter = null)
    {
        List<DO.Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>("Engineers");
        return filter != null ? Engineers.Where(filter) : Engineers;
    }

    public void Update(DO.Engineer item)
    {
        List<DO.Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>("Engineers");

        DO.Engineer? toUpdate = Engineers.FirstOrDefault(t => t.Id == item.Id);
        if (toUpdate != null)
        {
            int index = Engineers.IndexOf(toUpdate);
            Engineers[index] = item;
            XMLTools.SaveListToXMLSerializer(Engineers, "Engineers");
        }
        else
        {
            throw new DalDoesNotExistException($"Engineer object with ID {item.Id} does not exist");
        }
    }
    public void Reset()
    {
        XElement root = XMLTools.LoadListFromXMLElement("Engineers");
        root.Descendants("Engineer").Remove();
        XMLTools.SaveListToXMLElement(root, "Engineers");
    }
}
