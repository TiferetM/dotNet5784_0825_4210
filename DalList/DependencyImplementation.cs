//namespace Dal;

//using System.Collections.Generic;
//using DalApi;
//using DO;
//internal class DependencyImplementation : IDependency
//{
//   public int Create(Dependency item)
//    { 
//        int newId = DataSource.Config.NextTaskId;
//        Dependency tempdependency = item with { Id = DataSource.Config.NextTaskId };
//        DataSource.Dependencies.Add(item);
//        return newId;
     
//    }

//    public void Delete(int id)
//    {
//        Task deleteDependency = DataSource.Dependencies.FirstOrDefault(obj => obj.Id == id);
//        if (deleteDependency != null)
//        {
//            DataSource.Tasks.Remove(deleteDependency);
//        }
//        else
//            throw new Exception($"Dependency with ID={id} does not exist");
//    }

//    public Dependency? Read(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public List<Dependency?> ReadAll()
//    {
//        throw new NotImplementedException();
//    }

//    public void Update(Dependency item)
//    {
//        throw new NotImplementedException();
//    }
//}
//    public void Delete(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public Dependency? Read(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public List<Dependency?> ReadAll()
//    {
//        throw new NotImplementedException();
//    }

//    public void Update(Dependency item)
//    {
//        throw new NotImplementedException();
//    }
//}
