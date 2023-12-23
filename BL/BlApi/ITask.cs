namespace BlApi;
public interface ITask
{
    public IEnumerable<BO.Task> GetTaskersList();
    public BO.Task TaskDetailsRequest(int id);
    public int AddTask(BO.Task task);
    public void DeleteTask(int id);
    public void UpdateTaskData(BO.Task task);
}
