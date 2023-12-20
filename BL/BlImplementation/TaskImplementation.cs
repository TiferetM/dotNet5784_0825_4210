using BlApi;

namespace BlImplementation;
internal class TaskImplementation : ITask

{ private DalApi.IDal _dal = DalApi.Factory.Get;
        
    public void AddTask(Task engineer)
    {
        throw new NotImplementedException();
    }

    public void DeleteTask(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task> GetTaskersList(Func<Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public Task TaskDetailsRequest(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateTaskData(Task engineer)
    {
        throw new NotImplementedException();
    }
}
