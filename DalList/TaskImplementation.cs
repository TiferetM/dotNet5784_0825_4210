namespace Dal;

using System.Collections.Generic;
using DalApi;
using DO;
public class TaskImplementation : ITask
{
    public int Create(Task item)//done!!!
    {
       int newId = DataSource.Config.NextTaskId;
        Task tempTask = item with { Id = DataSource.Config.NextTaskId };
       DataSource.Tasks.Add(item);
       return newId;
       throw new NotImplementedException();
    } 
    //
    public void Delete(int id)///done
    {
        Task deleteTask = DataSource.Tasks.FirstOrDefault(obj => obj.Id == id);
      if(deleteTask!=null)
        {
            DataSource.Tasks.Remove(deleteTask);
        }
      else
        throw new Exception($"Task with ID={id} does not exist");
    }//done

    public Task? Read(int id)//done
    {
        return DataSource.Tasks.FirstOrDefault(p => p.Id == id);
        throw new NotImplementedException();
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }//done

    public void Update(Task item)//done
    {
        if (!DataSource.Tasks.Exists(p => p.Id == item.Id))
        {
            throw new Exception($"Task with ID={item.Id} does not exist");

        }
        Delete(item.Id);
        Create(item);
       // throw new NotImplementedException();
    }
}
