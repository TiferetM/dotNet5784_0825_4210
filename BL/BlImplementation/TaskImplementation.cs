
using BlApi;
using BO;
using DO;
using System.Xml.Linq;



namespace BlImplementation;
internal class TaskImplementation : ITask
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;
    public int AddTask(BO.Task task)
    {
        //if(task.ID<0||  task.Description==" "|| task.Alias==" ")//
        //  {
        //      throw new BO.BLDoesNotExistException("Invalid values");// ערך לא חוקי
        //  }

        DO.Task AddedTask = new DO.Task(task.ID)
        {
           
            Description = task.Description,
            Alias = task.Alias,
            Milestone = false,
            ForCastDate = task.ForecastDate,
            StartDate = task.StartDate,
            SchedulableDate = task.ScheduledStartDate,
            DeadLine = task.DeadlineDate,
            CompletedDate = task.CompleteDate,
            Deliverables = task.Deliverables,
            Remarks = task.Remarks,
            EngineerId = task.Engineer?.ID,
            ComplexityLevel = task.ComplexityLevel
        };
        try
        {
            int id = _dal.Task.Create(AddedTask);
            return id;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BLAlreadyExistsException($"Task with ID={task.ID} already exists", ex);
        }
    }

    public void DeleteTask(int id)
    {

        if (_dal.Task.ReadAll(t => t.Id == id) != null)
        {
            throw new BO.BLDeletionImpossible("can't delete an task that is in the middle of a task/ finished task");
        }
        try
        {
            _dal.Task.Delete(id);
        }
        catch (DO.DalDoesNotExistException)
        {
            throw new BO.BLDoesNotExistException($"task with ID={id} does not  exists");
        }

    }

    public IEnumerable<BO.Task> GetTasksList()
    {
        return from t in _dal.Task.ReadAll()
               let dependencies = _dal.Dependency!.ReadAll(d => d.DependenceTask == t.Id)
                                .Select(d => new BO.TaskInList()
                                {
                                    Id = (int)d.DependenceOnTask!,
                                    Alias = _dal.Task!.Read((int)d.DependenceOnTask)?.Alias,
                                    Description = _dal.Task!.Read((int)d.DependenceTask!)?.Description,
                                    Status = Tools.DetermineStatus(_dal.Task!.Read((int)d.DependenceTask))
                                })
                                .ToList()
               select new BO.Task()
               {
                   ID = t.Id,
                   Description = t.Description,
                   Alias = t.Alias,
                   CreatedAtDate = t.CreateDate,
                   Status = Tools.DetermineStatus(t),
                   Dependencies = dependencies,
                   Milestone = dependencies
                   .Where(d => _dal.Task!.Read(d.Id)!.Milestone == true)
                   .Select(d => new BO.MilestoneTask()
                   {
                       ID = d.Id,
                       Alias = d.Alias
                   })
                    .FirstOrDefault(),
                   StartDate = t.StartDate,
                   ScheduledStartDate = t.SchedulableDate,
                   ForecastDate = DateTime.MinValue,
                   DeadlineDate = t.DeadLine,
                   CompleteDate = t.CreateDate,
                   Deliverables = t.Deliverables,
                   Remarks = t.Remarks,
                   Engineer = new BO.EngineerInTask()
                   {
                       ID = _dal.Engineer!.Read(t.Id)!.Id,
                       Name = _dal.Engineer!.Read(t.Id)!.Name
                   },
                   ComplexityLevel = t.ComplexityLevel
               };
        throw new NotImplementedException();
    }
    public BO.Task TaskDetailsRequest(int id)
    {
        DO.Task? DoTask = _dal.Task.Read(id);
        if (DoTask == null)
            throw new BO.BLDoesNotExistException($"Task with ID={id} does not  exists");

        List<BO.TaskInList> dependencies = (from d in _dal.Dependency!.ReadAll(d => d.DependenceTask == id)
                                            where true
                                            select new BO.TaskInList()
                                            {
                                                Id = (int)d.DependenceOnTask,
                                                Alias = _dal.Task!.Read((int)d.DependenceOnTask)?.Alias,
                                                Description = _dal.Task!.Read((int)d.DependenceOnTask)?.Description,
                                                Status = Tools.DetermineStatus(_dal.Task!.Read((int)d.DependenceOnTask))
                                            }
                                              ).ToList();


        Status status = Tools.DetermineStatus(DoTask);
        MilestonesInTask? milestone = dependencies
              .Where(d => _dal.Task!.Read(d.Id)!.Milestone == true)
              .Select(d => new BO.MilestonesInTask()
              {
                  ID = d.Id,
                  Alias = d.Alias
              })
              .FirstOrDefault();

        BO.Task BoTask = new BO.Task()
        {
            ID = id,
            Description = DoTask.Description,
            Alias = DoTask.Alias,
            Milestone = new MilestoneTask()
            {
                ID = id,
                Alias = DoTask.Alias

            },
            CreatedAtDate = DoTask.CreateDate,
            StartDate = DoTask.StartDate,
            ScheduledStartDate = DoTask.SchedulableDate,
            DeadlineDate = DoTask.DeadLine,
            CompleteDate = DoTask.CompletedDate,
            Deliverables = DoTask.Deliverables,
            Remarks = DoTask.Remarks,
            Status = status,
            ForecastDate = DoTask.ForCastDate,
            //  BaselineStartDate=DoTask.
            Engineer = new BO.EngineerInTask
            {
                ID = _dal.Engineer!.Read(DoTask.Id)!.Id,
                Name = _dal.Engineer!.Read(DoTask.Id)!.Name
            },

            ComplexityLevel = (DO.EngineerExperience?)DoTask.ComplexityLevel
        };
        return BoTask;
    }
    public void UpdateTaskData(BO.Task task)//
    {

        //if(task.ID<0||  task.Description==" "|| task.Alias==" ")//
        //  {
        //      throw new BO.BLDoesNotExistException("Invalid values");// ערך לא חוקי
        //  }
        if (_dal.Task.Read(task.ID) == null)
            throw new BO.BlDoesNotExistException($"Student with ID={task.ID} does Not exist");
        try
        {
            DO.Task updatedTask = new DO.Task(task.ID)
            {
              
                Description = task.Description,
                Alias = task.Alias,
                Milestone = false,//צריך לבדוק מה שמים שם, יש גם בקונסרקטור בעיה עם זה
                CreateDate = (DateTime?)task.CreatedAtDate,
                StartDate = (DateTime?)task.StartDate,
                SchedulableDate = (DateTime?)task.ScheduledStartDate,
                DeadLine = (DateTime?)task.DeadlineDate,
                CompletedDate = (DateTime?)task.CompleteDate,
                Deliverables = task.Deliverables,
                Remarks = task.Remarks,
                EngineerId = task.Engineer?.ID,
                ComplexityLevel = task.ComplexityLevel
            };
            _dal.Task!.Update(updatedTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"task with ID={task.ID} does Not exist", ex);

        }
    }
}