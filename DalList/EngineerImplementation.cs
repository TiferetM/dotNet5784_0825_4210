using System.Collections.Generic;
using DalApi;
using DO;
using System;
using Dal;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {

        if (DataSource.Engineers.Exists(p => p.Id == item.Id))
        {
            throw new Exception($"Engineer with ID={item.ToString} already exist");
        }

        DataSource.Engineers.Add(item);

        return item.Id;


    }

    public void Delete(int id)
    {
        if (DataSource.Engineers.Exists(p => p.Id != id))
        {
            DataSource.Engineers.RemoveAll(engineer => engineer.Id == id);

        }
        else
        {
            throw new Exception($"Engineer with ID={id} does not exist");

        }

    }

    public Engineer? Read(int id)
    {
        return DataSource.Engineers.FirstOrDefault(p => p.Id == id);

    }

    public List<Engineer> ReadAll()
    {

        return new List<Engineer>(DataSource.Engineers);

    }

    public void Update(Engineer item)
    {
        if (!DataSource.Engineers.Exists(p => p.Id == item.Id))
        {
            throw new Exception($"Engineer with ID={item.Id} does not exist");

        }
        Delete(item.Id);
        Create(item);
    }
}
//namespace Dal;

//using System.Collections.Generic;
//using DalApi;
//using DO;
//public class EngineerImplementation : IEngineer
//{
//    public int Create(Engineer item)
//    {
//        DataSource.Engineers.Add(item);
//        return item.Id;
//      // throw new NotImplementedException();
//      // throw new Exception($"Engineer with ID={item.ToString} does Not exist");
//    }

//    public void Delete(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public Engineer? Read(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public List<Engineer?> ReadAll()
//    {
//        return ();
//        //throw new NotImplementedException();
//    }

//    public void Update(Engineer item)
//    {
//        throw new NotImplementedException();
//    }
//}



