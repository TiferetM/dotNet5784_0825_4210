


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
                    // ForecastDate = DOTask!.ForecastDate,
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

        public BO.Milestone UpdateMilestoneData(Milestone item)
        {

            DO.Task? prevTask = _dal.Task.Read(item.Id);
            if (prevTask == null)
            {
                throw new BO.BlDoesNotExistException($"Student with ID={item.Id} does not  exists");
            }

            DO.Task DOTask = new DO.Task()
            {
                Id = item.Id,
                description = item.Description,
                Alias = item.Alias,
                //ForCastDate = prevTask.ForCastDate,
                createdat = prevTask.createdat,
                start = prevTask.start,
                schedudalDate = prevTask.schedudalDate,
                DeadLine = prevTask.DeadLine,
                Complete = prevTask.Complete,
                Delivrables = prevTask.Delivrables,
                Remarks = item.Remaks,
                Engineerid = prevTask.Engineerid,
                ComplexityLevel = prevTask.ComplexityLevel

            };
            try
            {
                _dal.Task.Update(DOTask);
                List<BO.TaskInList> dependencies = (from d in _dal.Dependency!.ReadAll(d => d.DependenceTask == DOTask.Id)
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
                    Id = DOTask.Id,
                    Description = DOTask!.description,
                    Alias = DOTask!.Alias,
                    CreatedAtDate = DOTask!.createdat,
                    Status = Tools.DetermineStatus(DOTask),
                    //ForecastDate = DOTask!.ForecastDate,
                    DeadlineDate = DOTask!.DeadLine,
                    CompleteDate = DOTask!.Complete,
                    completionPercentage = 0,
                    Dependencies = dependencies
                };
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"Student with ID={item.Id} does not  exists", ex);
            }


        }

    }
}