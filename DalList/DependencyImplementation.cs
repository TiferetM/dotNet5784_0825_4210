namespace Dal;

using System.Collections.Generic;
using DalApi;
using DO;
public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)//done
    {
        int newId = DataSource.Config.NextTaskId;
        Dependency  tempdependency = item with { Id = DataSource.Config.NextTaskId };
        DataSource.Dependencies.Add(item);
        return newId;

    }

    public void Delete(int id)//done
    {
        Dependency ? deleteDependency = DataSource.Dependencies.FirstOrDefault(obj => obj.Id == id);
        if (deleteDependency != null)
        {
            DataSource.Dependencies.Remove(deleteDependency);
        }
        else
            throw new Exception($"Dependency with ID={id} does not exist");
    }

    public Dependency? Read(int id)//done
    {
        return DataSource.Dependencies.FirstOrDefault(p => p.Id == id);
       
    }

    public List<Dependency> ReadAll()//done
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    public void Update(Dependency item)//done
    {

        if (!DataSource.Dependencies.Exists(p => p.Id == item.Id))
        {
            throw new Exception($"Dependency with ID={item.Id} does not exist");

        }
        Delete(item.Id);
        Create(item);
    }
}
