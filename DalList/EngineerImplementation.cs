using System.Collections.Generic;
using DalApi;
using DO;
using System;
using Dal;
using System.Collections;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        var query = from Engineer in DataSource.Engineers//A query that
                                                         //returns all objects whose item.Id ==Engineer.Id
                    where item.Id ==Engineer.Id
                    select Engineer.Id;

        if (query.Any())//if exists
        {
            throw new DalAlreadyExistsException($"Engineer with ID={item.ToString} already exist");


        }
        else//if not exists
        {
            DataSource.Engineers?.Add(item);

            return item.Id;
        }
    }
    public void Delete(int id)
    {
        var query = from Engineer in DataSource.Engineers////A query that
                                                         //returns all objects whose  Engineer.Id !=id
                    where Engineer.Id !=id
                    select Engineer.Id;

        if (query.Any())////if exists
        {
            DataSource.Engineers?.RemoveAll(engineer => engineer.Id == id);


        }
        else//if not exists
        {
            throw new DalDoesNotExistException($"Engineer with ID={id} does not exist");
        }
    }

    public Engineer? Read(int id)
    {
        var query = (from engineer in DataSource.Engineers//A query that returns the
                     where engineer.Id == id
                     select engineer).FirstOrDefault();
        return query;
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {

        if (filter != null)
        {
            return (from item in DataSource.Engineers
                    where filter(item)
                    select item).FirstOrDefault();
        }
        return (from item in DataSource.Engineers
                select item).FirstOrDefault();
        //return new Engineer;
    }

    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null)
    {
        if (filter != null)
        {
            return from item in DataSource.Engineers
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Engineers
               select item;
    }

    public void Update(Engineer item)
    {
        var query = from Engineer in DataSource.Engineers//A query that
                                                         //returns all objects whose item.Id ==Engineer.Id
                    where item.Id ==Engineer.Id
                    select Engineer.Id;

        if (query.Any())//if exists
        {
            Delete(item.Id);
            Create(item);

        }
        else//if not exists
        {
            throw new DalAlreadyExistsException($"Engineer with ID={item.ToString} already exist");

        }
    }
    public void Reset()
    {
        if (DataSource.Engineers != null)
            DataSource.Engineers?.Clear();
        
    }


}





