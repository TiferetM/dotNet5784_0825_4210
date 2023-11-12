using System.Collections.Generic;
using DalApi;
using DO;
using System;
using Dal;

internal  class EngineerImplementation : IEngineer
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




