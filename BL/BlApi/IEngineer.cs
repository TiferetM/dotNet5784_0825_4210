namespace BlApi;
public interface IEngineer
{
    public IEnumerable<BO.Engineer> GetEngineersList(Func<BO.Engineer, bool>? filter = null);
    public BO.Engineer EngineerDetailsRequest(int id);
    public void AddEngineer(BO.Engineer engin);
    public void DeleteEngineer(int id);
    public void UpdateEngineer(BO.Engineer engineer);
}