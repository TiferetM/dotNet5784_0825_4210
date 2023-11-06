namespace Dal;

using System.Collections.Generic;
using DalApi;
using DO;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        throw new NotImplementedException();
        throw new Exception($"Engineer with ID={item.ToString} does Not exist");
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Engineer?> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}

