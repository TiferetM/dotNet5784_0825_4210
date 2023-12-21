using BlApi;
using BO;
using System.Net.Mail;
using System.Xml.Linq;

namespace BlImplementation;
internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int AddEngineer(BO.Engineer engin)
    {
        if (engin.ID < 0 || engin.Name == "" || engin.Cost < 0)
        {
            throw new BO.BLDoesNotExistException("the value is not valid");////Bl ערך לא חוקי
        }

        try
        {
            MailAddress mail = new MailAddress(engin.Email);//(engin?.Email??" "//כדי שלא יהיה worinnig
        }
        catch (Exception e)
        {
            throw new BO.BLDoesNotExistException(e.Message);
        }

        DO.Engineer DOEnginner = new()
        {
            Id = engin.ID,
            Name = engin.Name,
            email = engin.Email,
            Level = engin.Level,
        };
        try
        {
            int id = _dal.Engineer.Create(DOEnginner);
            return id;
        }
        catch (DO.DalAlreadyExistsException ex)//דאל כבר קיים חריג
        {
            throw new BO.BLAlreadyExistsException($"engeneer with ID={engin.ID} already exists", ex);
        }
        //throw new NotImplementedException();
    }

    public void DeleteEngineer(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer EngineerDetailsRequest(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Engineer> GetEngineersList(Func<Engineer, bool>? filter = null)
    {
        //return from e in _dal.Engineer.ReadAll()
        //       select new BO.Engineer()
        //       {                     
        //           ID = e.Id, // שימוש ב-generate property
        //           Name = e.Name,
        //           Email = e.Email,
        //           Level = e.Level,
        //           Cost = 0,
        //           Task = new BO.TaskInEngineer()
        //           {
        //               Id = _dal.Task.Read(t => t.Engineerid == e.ID)!.Id,
        //               Alias = _dal.Task.Read(t => t.Engineerid == e.ID)!.Alias ?? ""
        //           }
        //       };
        throw new NotImplementedException();
    }

    public void UpdateEngineer(Engineer engineer)
    {
        throw new NotImplementedException();
    }
}