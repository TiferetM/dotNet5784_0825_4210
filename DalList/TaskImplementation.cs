namespace Dal;

using System.Collections.Generic;
using DalApi;
using DO;
internal class TaskImplementation : ITask
{
    public int Create(Task item)//done!!!
    {
       int newId = DataSource.Config.NextTaskId;
       Task tempTask = item with { Id = DataSource.Config.NextTaskId };
       DataSource.Tasks?.Add(item);
       return newId;
      // throw new NotImplementedException();
    }
    public void Reset()
    {
        if (DataSource.Tasks != null)
             DataSource.Tasks?.Clear();
    }
    public void Delete(int id)///done
    {
        var query = from Task in DataSource.Tasks////A query that
                                                 //returns all objects whose  Engineer.Id !=id
                    where Task.Id !=id
                    select Task.Id;

        if (query.Any())////if exists
        {
            DataSource.Tasks?.RemoveAll(Task => Task.Id == id);
        }
        else//if not exists
        {
            throw new DalDoesNotExistException($"Task with ID={id} does not exist");
        }

    }
    public Task? Read(Func<Task, bool> filter)
    {
        if (filter != null)
        {
            return (from item in DataSource.Tasks
                    where filter(item)
                    select item).FirstOrDefault();
        }
        return (from item in DataSource.Tasks
                select item).FirstOrDefault();
        //return new Engineer;
    }
    public Task? Read(int id)//done
    {

        var query = (from Task in DataSource.Tasks//A query that returns the
                                                  //first one that meets the condition:
                                                  //engineer.Id == id
                     where Task.Id == id
                     select Task).FirstOrDefault();

        return query;
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        if (filter != null)
        {
            return from item in DataSource.Tasks
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Tasks
               select item;


    }

    public void Update(Task item)//done
    {
        var query = from Task in DataSource.Tasks//A query that
                                                //returns all objects whose item.Id ==Engineer.Id
                    where item.Id ==Task.Id
                    select Task.Id;

        if (query.Any())//if exists
        {
            Delete(item.Id);
            Create(item);
        }
        else//if not exists
        {
            throw new DalAlreadyExistsException($"Task with ID={item.ToString} already exist");

        }
    }
}

