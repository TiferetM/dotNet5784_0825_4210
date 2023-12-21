//using System.Net.Mail;
//using BlApi;
//using BO;

//namespace BlImplementation;
//internal class TaskImplementation : ITask
//{
//    private readonly DalApi.IDal _dal = DalApi.Factory.Get;
//    public int AddTask(BO.Task task)
//    {
//        DO.Task AddedTask = new
//           (task.ID,
//            task.Description,
//           task.Alias,Milestone,
//          task.ForecastDate,
//          task.StartDate,
//          task.ScheduledStartDate,
//          task.DeadlineDate,
//          task.CompleteDate,
//          task.Deliverables,
//          task.Remarks,
//                    task.CopmlexityLevel,);
//        try
//        {
//            int id = _dal.Task.Create(AddedTask);
//            return id;
//        }
//        catch (DO.DalAlreadyExistException ex)
//        {
//            throw new BO.BlAlreadyExistException($"Task with ID={task.ID} already exists", ex);
//        }


//        throw new NotImplementedException();
//    }

//    public void DeleteTask(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public IEnumerable<Task> GetTaskersList(Func<Task, bool>? filter = null)
//    {
//        throw new NotImplementedException();
//    }

//    public Task TaskDetailsRequest(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public void UpdateTaskData(Task engineer)
//    {
//        throw new NotImplementedException();
//    }
//}
