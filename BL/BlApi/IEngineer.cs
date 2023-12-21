namespace BlApi;
public interface IEngineer
{
    public IEnumerable<BO.Engineer> GetEngineersList(Func<BO.Engineer, bool>? filter = null);
    public BO.Engineer EngineerDetailsRequest(int id);
    public int AddEngineer(BO.Engineer eng);
    public void DeleteEngineer(int id);
    public void UpdateEngineer(BO.Engineer engineer);
}