
using BlApi;
using BO;


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

        DO.Task AddedTask = new()
        {
           task.ID,
            task.Description,
           task.Alias,false,
          task.ForecastDate,
          task.StartDate,
          task.ScheduledStartDate,
          task.DeadlineDate,
          task.CompleteDate,
          task.Deliverables,
          task.Remarks,,/*task.Engineer.ID,*/
          task.CopmlexityLevel};
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

    public IEnumerable<BO.Task> GetTaskersList()
    {
        ////return from e in _dal.Engineer.ReadAll()
        ////       select new BO.Engineer()
        ////       {
        ////           ID = e.Id, // שימוש ב-generate property
        ////           Name = e.Name,
        ////           Email = e.email,
        ////           Level = e.level,
        ////           Cost = 0,
        ////           Task = new BO.TaskInEngineer()
        ////           {
        ////               ID = _dal.Task.Read(t => t.Id == e.Id)!.Id,
        ////               Name = _dal.Task.Read(t => t.Id == e.Id)!.Alias ?? ""
        ////           }
        ////       };
        ////throw new NotImplementedException();


        //return from e in _dal.Task.ReadAll()
        //        select new BO.Engineer()
        //        {

        //        };


        throw new NotImplementedException();
    }//**לא עשוי



    public BO.Task TaskDetailsRequest(int id)
    { 
      

        DO.Task? DoTask = _dal.Task.Read(id);
        if (DoTask==null)
            throw new BO.BLDoesNotExistException($"Task with ID={id} does not  exists");

       Status status = Tools.DetermineStatus(DoTask);
        BO.Task BoTask = new BO.Task()
        {
            ID =id,
            Description = DoTask.description,
            Alias = DoTask.Alias,
            Milestone=new MilestoneTask()
            {
                ID=id,
                Alias=DoTask.Alias

            }
            ,//צריך לבדוק מה שמים שם, יש גם בקונסרקטור בעיה עם זה
            CreatedAtDate =DoTask.createdat,
            StartDate = DoTask.start,
            ScheduledStartDate= DoTask.schedudalDate,
             DeadlineDate= DoTask.DeadLine,
             CompleteDate= DoTask.Complete,
            Deliverables= DoTask.Delivrables,
            Remarks = DoTask.Remarks,
            Status = status
           
            // BaselineStartDate=DoTask
            //ForecastDate=DoTask.
            //DoTask.Engineer?.ID=
            //Engineerid ,
            //  CopmlexityLevel = DoTask.ComplexityLevel
        };
        return BoTask;

  


  
 

  

        throw new NotImplementedException();
    }//**לא עשוי



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
            DO.Task updatedTask = new DO.Task()
            {
                Id = task.ID,
                description = task.Description,
                Alias = task.Alias,
                Milestone=false,//צריך לבדוק מה שמים שם, יש גם בקונסרקטור בעיה עם זה
                createdat = (DateTime?)task.CreatedAtDate,
                start = (DateTime?)task.StartDate,
                schedudalDate = (DateTime?)task.ScheduledStartDate,
                DeadLine = (DateTime?)task.DeadlineDate,
                Complete = (DateTime?)task.CompleteDate,
                Delivrables = task.Deliverables,
                Remarks = task.Remarks,
                Engineerid = task.Engineer?.ID,
                ComplexityLevel = task.CopmlexityLevel
            };
            _dal.Task!.Update(updatedTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"task with ID={task.ID} does Not exist", ex);

        }
    }


}







