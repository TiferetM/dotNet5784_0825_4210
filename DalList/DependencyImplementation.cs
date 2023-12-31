namespace Dal;

using System.Collections.Generic;
using DalApi;
using DO;
internal class DependencyImplementation : IDependency
{
    public int Create(Dependency item)//done
    {
        int newId = DataSource.Config.NextTaskId;
        Dependency  tempdependency = item with { Id = DataSource.Config.NextTaskId };
        DataSource.Dependencies?.Add(item);
        return newId;

    }
    public void Delete(int id)//done
    {
        var deleteDependency = from Dependency in DataSource.Dependencies////A query that
                                                                         //returns all objects whose  Engineer.Id !=id
                               where Dependency.Id !=id
                               select Dependency.Id;

        if (deleteDependency.Any())////if exists
        {
            DataSource.Dependencies?.RemoveAll(Dependency => Dependency.Id == id);
        }
        else//if not exists
        {
            throw new DalDoesNotExistException($"Dependency with ID={id} does not exist");
        }

    }
    public Dependency? Read(int id)
    {
        var query = (from Dependency in DataSource.Dependencies//A query that returns the
                                                               //first one that meets the condition:
                                                               //engineer.Id == id
                     where Dependency.Id == id
                     select Dependency).FirstOrDefault();
        return query;

    }
    public Dependency? Read(Func<Dependency, bool> filter)
    { 
        if (filter != null)
        {
            return (from item in DataSource.Dependencies
                    where filter(item)
                    select item).FirstOrDefault();
        }
        return (from item in DataSource.Dependencies
                select item).FirstOrDefault();
    }
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)//done
    {
        if (filter != null)
        {
            return from item in DataSource.Dependencies
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependencies
               select item;

    }
    public void Reset()
    {
        if(DataSource.Dependencies!=null)
        {
            DataSource.Dependencies.RemoveAll(dependency => true);
        }
    }
    public void Update(Dependency item)//done
    {
        var query = from Dependency in DataSource.Dependencies//A query that
                                                            //returns all objects whose item.Id ==Engineer.Id
                    where item.Id ==Dependency.Id
                    select Dependency.Id;

        if (query.Any())//if exists
        {
            Delete(item.Id);
            Create(item);

        }
        else//if not exists
        {
            throw new DalAlreadyExistsException($"Dependency with ID={item.ToString} already exist");

        }
    }
}

