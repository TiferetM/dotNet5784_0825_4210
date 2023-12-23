using BlApi;
using BO;
using DalApi;

namespace BlImplementation
{
    internal class MilestoneImplementation : IMilestone
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        public Milestone? ReadMilestoneData(int id)
        {
            try
            {
                DO.Task? DOTask = _dal.Task.Read(id);
                List<BO.TaskInList> dependencies = (from d in _dal.Dependency!.ReadAll(d => d.DependenceTask == id)
                                                    where true
                                                    select new BO.TaskInList()
                                                    {
                                                        Id = d.DependenceOnTask,
                                                        Alias = _dal.Task!.Read(d.DependenceOnTask)?.Alias,
                                                        Description = _dal.Task!.Read(d.DependenceOnTask)?.description,
                                                        Status = Tools.DetermineStatus(_dal.Task!.Read(d.DependenceOnTask))
                                                    }
                                                    ).ToList();
                return new BO.Milestone()
                {
                    Id = id,
                    Description = DOTask!.description,
                    Alias = DOTask!.Alias,
                    CreatedAtDate = DOTask!.createdat,
                    Status = Tools.DetermineStatus(DOTask),
                    ForecastDate = DOTask!.ForecastDate,
                    DeadlineDate = DOTask!.DeadLine,
                    CompleteDate = DOTask!.Complete,
                    completionPercentage = 0,
                    Dependencies = dependencies
                };
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"task with ID={id} does not  exists", ex);
            }
        }

        public Milestone UpdateMilestoneData(Milestone item)
        {
            throw new NotImplementedException();
        }
    }
}