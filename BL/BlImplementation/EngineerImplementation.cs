using BO;
using System.Net.Mail;
using System.Xml.Linq;
using BlApi;
namespace BlImplementation;
internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;//הגדרת משתנה פרטי מסוג DalApi
    public int AddEngineer(BO.Engineer eng)
    {
        if (eng.ID < 0 || eng.Name == "" || eng.Cost < 0)//תקינות קלט
        {
            Console.WriteLine(" eng תקינות קלט ");
            throw new BO.BLDoesNotExistException("Invalid values");// ערך לא חוקי
           

        }
        try
        {
           MailAddress mail = new MailAddress(eng?.Email ?? "defult@1.com");//****
            //חייביל  לשלוח מבנה אימייל תקין המכיל @
        }
        catch (Exception e)
        {
            throw new BO.BLDoesNotExistException(e.Message);
        }
        DO.Engineer DOEngineer = new()
        {
            Id = eng!.ID,
            Name = eng.Name,
            email = eng.Email,
            level = (DO.EngineerExperience)eng.Level,
        };
        try
        {
            int id = _dal.Engineer.Create(DOEngineer);
            return id;
        }
        catch (DO.DalAlreadyExistsException ex)//DAL-נמצא כבר ב
        {
            throw new BO.BLAlreadyExistsException($"engineer with ID={eng.ID} already exists", ex);
        }
    }

    public void DeleteEngineer(int id)
    {
        if (_dal.Engineer.ReadAll(t => t.Id == id) != null)
        {
            throw new BO.BLDeletionImpossible("can't delete an engineer that is in the middle of a task/ finished task");
        }
        try
        {
           _dal.Engineer.Delete(id);
        }
        catch (DO.DalDoesNotExistException)
        {
          throw new BO.BLDoesNotExistException($"Engineer with ID={id} does not  exists");
        }
        //engineer deleted!
    }

    public Engineer EngineerDetailsRequest(int id)//החזרת פרטי מהנדס
    {
        DO.Engineer? DoEngineer = _dal.Engineer.Read(id);
        if(DoEngineer==null)
            throw new BO.BLDoesNotExistException($"Engineer with ID={id} does not  exists");
        BO.Engineer BoEngineer = new Engineer()
        {
            ID = id,
            Name = DoEngineer.Name,
            Email = DoEngineer.email,
            Level = (EngineerExperience)DoEngineer.level,
            Cost = 0,
            Task = new BO.TaskInEngineer()
            {
                ID = _dal.Task.Read(t => t.Id == id)!.Id,
                Name = _dal.Task.Read(t => t.Id == id)!.Alias ?? ""
            }
        };
        return BoEngineer;
    }

    public IEnumerable<Engineer> ReadAllEngineer(Func<Engineer, bool>? filter = null)
    {
        //var x = _dal.Engineer.ReadAll();
            return from e in _dal.Engineer.ReadAll()
                   let taskEngineer = _dal!.Task!.Read(t => t.EngineerId == e.Id && t.Milestone)
                   select new BO.Engineer()
               {
                   ID = e.Id, // שימוש ב-generate property
                   Name = e.Name,
                   Email = e.email,
                   Level = (EngineerExperience)e.level,
                   Cost = 0,
                       Task = taskEngineer != null ? new BO.TaskInEngineer { ID = taskEngineer.Id, Name = taskEngineer.Alias } : null
                   };
        throw new NotImplementedException();
    }// החזרת משימות המהנדס

    public void UpdateEngineer(Engineer engineer)//עדכון פרטי מהנדס
    {
        if (engineer.ID < 0 || engineer.Name == "" || engineer.Cost < 0)//תקינות קלט
        {
            throw new BO.BLDoesNotExistException("Invalid values");// ערך לא חוקי
        }
        try
        {
            MailAddress mail = new MailAddress(engineer?.Email ?? " ");
        }
        catch (Exception e)
        {
            throw new BO.BLDoesNotExistException(e.Message);
        }
            DO.Engineer DOEngineer = new()
            {
                Id = engineer!.ID,
                Name = engineer.Name,
                email = engineer.Email,
                level = (DO.EngineerExperience)engineer.Level,
            };
            _dal.Engineer.Update(DOEngineer);
    }

    
}