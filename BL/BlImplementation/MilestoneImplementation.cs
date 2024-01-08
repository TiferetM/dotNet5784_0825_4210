using System.Runtime.Intrinsics.Arm;
using BlApi;
using BO;
using DalApi;
using DO;
using System.Data;
using System.Xml.Linq;
using static BO.Tools;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;
    #region functions
    public Milestone? ReadMilestoneData(int id)
    {
        try
        {
            DO.Task? DOTask = _dal.Task.Read(id);
            List<BO.TaskInList> dependencies = (from d in _dal.Dependency!.ReadAll(d => d.DependenceTask == id)
                                                where true
                                                select new BO.TaskInList()
                                                {
                                                    Id = (int)d.DependenceOnTask!,
                                                    Alias = _dal.Task!.Read((int)d.DependenceOnTask)?.Alias,
                                                    Description = _dal.Task!.Read((int)d.DependenceOnTask)?.Description,
                                                    Status = Tools.DetermineStatus(_dal.Task!.Read((int)d.DependenceOnTask))
                                                }
                                                ).ToList();
            BO.Milestone returnedMil = new()
            {
                Id = id,
                Description = DOTask!.Description,
                Alias = DOTask!.Alias,
                CreatedAtDate = DOTask!.CreateDate,
                Status = Tools.DetermineStatus(DOTask),
                // ForecastDate = DOTask!.ForecastDate,
                DeadlineDate = DOTask!.DeadLine,
                CompleteDate = DOTask!.CompletedDate,
                completionPercentage = 0,
                Dependencies = dependencies
            };
            return returnedMil;
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

        DO.Task DOTask = new DO.Task(item.Id)
        {

            Description = item.Description,
            Alias = item.Alias,
            //ForecastDate = prevTask.ForecastDate,
            CreateDate = prevTask.CreateDate,
            StartDate = prevTask.StartDate,
            SchedulableDate = prevTask.SchedulableDate,
            DeadLine = prevTask.DeadLine,
            CompletedDate = prevTask.CompletedDate,
            Deliverables = prevTask.Deliverables,
            Remarks = item.Remarks,
            EngineerId = prevTask.EngineerId,
            ComplexityLevel = prevTask.ComplexityLevel

        };
        try
        {
            _dal.Task.Update(DOTask);
            List<BO.TaskInList> dependencies = (from d in _dal.Dependency!.ReadAll(d => d.DependenceTask == DOTask.Id)
                                                where true
                                                select new BO.TaskInList()
                                                {
                                                    Id = (int)d.DependenceOnTask!,
                                                    Alias = _dal.Task!.Read((int)d.DependenceOnTask)?.Alias,
                                                    Description = _dal.Task!.Read((int)d.DependenceOnTask)?.Description,
                                                    Status = Tools.DetermineStatus(_dal.Task!.Read((int)d.DependenceOnTask))
                                                }
                                                ).ToList();
            return new BO.Milestone()
            {
                Id = DOTask.Id,
                Description = DOTask!.Description,
                Alias = DOTask!.Alias,
                CreatedAtDate = DOTask!.CreateDate,
                Status = Tools.DetermineStatus(DOTask),
                //ForecastDate = DOTask!.ForecastDate,
                DeadlineDate = DOTask!.DeadLine,
                CompleteDate = DOTask!.CompletedDate,
                completionPercentage = 0,
                Dependencies = dependencies
            };
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Student with ID={item.Id} does not  exists", ex);
        }


    }

    #endregion 
    public void CreateScheduledProject()
    {
        List<DO.Dependency?> dependenciesList = _dal.Dependency.ReadAll().ToList();
        List<DO.Dependency> newDepsList = CreateMilestones(dependenciesList);
        _dal.Dependency.Reset();
        foreach (var dep in newDepsList)
        {
            _dal.Dependency.Create(dep);
        }

        //List<DO.Dependency> newDepsList = _dal.Dependency.ReadAll().ToList();
        List<DO.Task?> allTasks = _dal.Task.ReadAll().ToList();
        int startMilestoneId = allTasks.Where(task => task!.Alias == "start").Select(task => task!.Id).First();

        DO.Task startMilestone = _dal.Task.Read(startMilestoneId)!;
        if (startMilestone is not null)
        {
            startMilestone = startMilestone with { SchedulableDate = _dal.StartProjectDate };
            _dal.Task.Update(startMilestone);
        }

        int endMilestoneId = allTasks.Where(task => task!.Alias == "end").Select(task => task!.Id).First();

        DO.Task endMilestone = _dal.Task.Read(endMilestoneId)!;
        if (endMilestone is not null)
            endMilestone = endMilestone with { DeadLine = _dal.EndProjectDate };

        startMilestone = startMilestone! with { DeadLine = UpdateDeadlines(startMilestoneId, endMilestoneId, newDepsList) };
        //_dal.Task.Update(startMilestone!);

        endMilestone = endMilestone! with { SchedulableDate = UpdateScheduledDates(endMilestoneId, startMilestoneId, newDepsList) };
        renameMilestonesAlias(newDepsList);
    }
    #region  help fuctions
    //recommend to move to tools;)
    private List<DO.Dependency> CreateMilestones(List<DO.Dependency?> dependencies)//2->0,1->0, 3->[1,2], 4->3, 5->3
    {
        List<DO.Task?> tasks = _dal.Task.ReadAll().ToList();
        var groupDependencies = (from dep in dependencies
                                 where dep.DependenceTask is not null && dep.DependenceOnTask is not null
                                 group dep by dep.DependenceTask into GroupByDependentTask
                                 let depList = (from dep in GroupByDependentTask
                                                select dep.DependenceOnTask).Order()
                                 select new { _key = GroupByDependentTask.Key, _value = depList });//1->0,2->0, 3->[1,2], 4->3, 5->3

        var FilteredDependenciesList = (from dep in groupDependencies
                                        select dep._value.ToList()).Distinct().ToList();//1->0, 3->[1,2], 4->3//

        List<DO.Dependency> newDepsList = new List<DO.Dependency>();

        int i = 1;
        foreach (var FilteredDependency in FilteredDependenciesList)    //1->0, 3->[1,2], 4->3 value: parent
        {
            int idMilestone = _dal.Task.Create(new DO.Task(-1)
            {
                Description = "Description",
                Alias = $"M{i}",
                Milestone = true,
                CreateDate = DateTime.Now,
                TimeSpan = new TimeSpan(0)
            });
            foreach (var taskItemList in groupDependencies)//1->0,2->0, 3->[1,2], 4->3, 5->3
            {

                var tsk = taskItemList._value.ToList();
                if (tsk.SequenceEqual(FilteredDependency))
                    //  if (taskItemList._value == FilteredDependency._value)
                    newDepsList.Add(new DO.Dependency(-1, taskItemList._key!.Value, idMilestone));
            }

            foreach (var dep in FilteredDependency)
            {
                newDepsList.Add(new DO.Dependency(-1, idMilestone, dep));//check logic again
            }
            i++;
        }
        //  create milestone of start
        int IdStartMilestone = CreateStartMilestone(newDepsList);


        // find tasks that depend on start

        var IndependentsTasksList = (from task in tasks
                                     where !(from taskDep in groupDependencies
                                             select taskDep._key).Any(t => t == task.Id)
                                     select task.Id);

        foreach (var IndependentsTasksItem in IndependentsTasksList)//create deps for tasks that depend on start לבדוק כמויות
        {
            newDepsList.Add(new DO.Dependency(-1, IndependentsTasksItem, IdStartMilestone));
        }
        // create end milestone
        int IdEndMilestone = CreateEndMilestone(newDepsList, tasks, dependencies);

        //find tasks that no task depend on them

        return newDepsList;
    }
    private int CreateStartMilestone(List<Dependency> newDepsList)
    {
        int IdStartMilestone = _dal.Task.Create(new DO.Task(-1)
        {
            Description = "Start",
            Alias = "start",
            Milestone = true,
            CreateDate = DateTime.Now
        });

        newDepsList.Add(new DO.Dependency(-1, IdStartMilestone, null));
        return IdStartMilestone;
    }
    private int CreateEndMilestone(List<Dependency> newDepsList, List<DO.Task?> tasks, List<DO.Dependency?> dependencies)
    {
        int IdEndMilestone = _dal.Task.Create(new DO.Task(-1)
        {
            Description = "End",
            Alias = "end",
            Milestone = true,
            CreateDate = DateTime.Now
        });
        var endDependenceTask = (from task in tasks
                                 where !(from dep in dependencies
                                         select dep.DependenceOnTask).Distinct().Any(t => t == task.Id)
                                 select task.Id);
        foreach (var task in endDependenceTask)
        {
            newDepsList.Add(new DO.Dependency(-1, IdEndMilestone, task));
        }
        return IdEndMilestone;
    }
    private DateTime? UpdateDeadlines(int taskId, int endMilestoneId, List<DO.Dependency> depList)
    {
        if (taskId == endMilestoneId)//if this is the end milestone - stop.
            return _dal.EndProjectDate;
        DO.Task currentTask = _dal.Task.Read(taskId) ?? throw new BO.BlNullPropertyException($"Task with Id {taskId} does not exists");
        //get all the tasks that depent on this task
        List<int?> listOfDepentOnCurrentTask = (from dep in depList
                                                where dep.DependenceOnTask == taskId
                                                select dep.DependenceTask).ToList();

        DateTime? deadline = currentTask.DeadLine;//null?
        foreach (int task in listOfDepentOnCurrentTask)
        {
            DO.Task readTask = _dal.Task.Read(task)!;
            if (readTask.DeadLine is null)//set deadline for each task that depent on me by calling the recursion
                readTask = readTask with { DeadLine = UpdateDeadlines(task, endMilestoneId, depList) };
            if (deadline is null || readTask.DeadLine - readTask.TimeSpan < deadline)//set my deadline
                deadline = readTask.DeadLine - readTask.TimeSpan;
        }
        if (deadline > _dal.EndProjectDate)//if there is not enough time for the project
            throw new BO.BlInsufficientTime("There is insufficient time to complete this task\n");
        currentTask = currentTask with { DeadLine = deadline };//update the task with the deadline
        _dal.Task.Update(currentTask);
        return currentTask.DeadLine;//return the deadline for the recursion
    }
    private DateTime? UpdateScheduledDates(int taskId, int startMilestoneId, List<DO.Dependency> depList)
    {
        if (taskId == startMilestoneId)//if this is the start milestone - stop.
            return _dal.StartProjectDate;
        DO.Task currentTask = _dal.Task.Read(taskId) ?? throw new BO.BlNullPropertyException($"Task with Id {taskId} does not exists");
        //get all the tasks that this task depent on them
        List<int?> listOfTasksThatCurrentDepsOnThem = (from dep in depList
                                                       where dep.DependenceTask == taskId
                                                       select dep.DependenceOnTask).ToList();

        DateTime? scheduledDate = currentTask.SchedulableDate;
        foreach (int task in listOfTasksThatCurrentDepsOnThem)
        {
            DO.Task readTask = _dal.Task.Read(task)!;
            if (readTask.SchedulableDate is null)//set scheduled date for each task that i depent on it by calling the recursion
                readTask = readTask with { SchedulableDate = UpdateScheduledDates((int)task!, startMilestoneId, depList) };
            if (scheduledDate is null || readTask.SchedulableDate + readTask.TimeSpan > scheduledDate)//set my scheduled date
                scheduledDate = readTask.SchedulableDate + readTask.TimeSpan;
        }
        if (scheduledDate < _dal.StartProjectDate)//if there is not enough time for the project
            throw new BO.BlInsufficientTime("There is insufficient time to complete this task\n");
        currentTask = currentTask with { SchedulableDate = scheduledDate };//update the task with the scheduled date
        _dal.Task.Update(currentTask);
        return currentTask.SchedulableDate;//return the scheduled date for the recursion
    }
    /// <summary>
    /// Rename the milestones alias to be the ids of the dependent on tasks
    /// </summary>
    /// <param name="dependenciesList"></param>
    public void renameMilestonesAlias(List<DO.Dependency> dependenciesList)
    {
        if (dependenciesList.Count == 0) return;
        List<DO.Task?> allTasks = _dal.Task.ReadAll().ToList();
        foreach (var task in allTasks)
        {
            if (task?.Milestone is true)
            {
                string alias = "M";
                var dependsOnTaskList = dependenciesList.Where(dep => dep?.DependenceTask == task.Id)
                .Select(dep => dep?.DependenceOnTask).ToList();
                foreach (var dependsOnTask in dependsOnTaskList)
                {
                    alias += $" {dependsOnTask}";
                }
                task.Alias = alias;
                _dal.Task.Update(task);
            }

        }

    }
}



#endregion