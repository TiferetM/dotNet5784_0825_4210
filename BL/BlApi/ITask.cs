namespace BlApi;
public interface ITask
{
    public IEnumerable<Task> GetTaskersList(Func<Task, bool>? filter = null);
    public Task TaskDetailsRequest(int id);
    public int AddTask(BO.Task task);
    public void DeleteTask(int id);
    public void UpdateTaskData(Task engineer);
}
