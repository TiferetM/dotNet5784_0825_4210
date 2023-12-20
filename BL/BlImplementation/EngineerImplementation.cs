using BlApi;
using BO;

namespace BlImplementation;
internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void AddEngineer(Engineer engineer)
    {            
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public void UpdateEngineer(Engineer engineer)
    {
        throw new NotImplementedException();
    }
}
