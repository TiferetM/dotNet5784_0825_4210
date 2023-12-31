using System.Runtime.Intrinsics.Arm;
using BlApi;
using BO;
using DalApi;
using DO;

namespace BlImplementation
{
    internal class MilestoneImplementation : IMilestone
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        #region functions     public Milestone? ReadMilestoneData(int id)
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
                return new BO.Milestone()
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


            List<DO.Task?> allTasks = _dal.Task.ReadAll().ToList();
            int startMilestoneId = allTasks.Where(task => task!.Alias == "start").Select(task => task!.Id).First();

            DO.Task startMilestone = _dal.Task.Read(startMilestoneId)!;
            if (startMilestone is not null)
                startMilestone = startMilestone with { SchedulableDate = _dal.StartProjectDate };

            int endMilestoneId = allTasks.Where(task => task!.Alias == "end").Select(task => task!.Id).First();

            DO.Task endMilestone = _dal.Task.Read(endMilestoneId)!;
            if (endMilestone is not null)
                endMilestone = endMilestone with { DeadLine = _dal.EndProjectDate };

            startMilestone = startMilestone! with { DeadLine = UpdateDeadlines(startMilestoneId, endMilestoneId, newDepsList) };
            _dal.Task.Update(startMilestone!);

            endMilestone = endMilestone! with { SchedulableDate = UpdateScheduledDates(endMilestoneId, startMilestoneId, newDepsList) };
            _dal.Task.Update(end
        #region  help fuctions
        //recommend to move to tools;)o tools;)
        private List<DO.Dependency> CreateMilestones(List<DO.Dependency?> dependencies)//2->0,1->0, 3->[1,2], 4->3, 5->3
        {
            var groupDependencies = (from dep in dep                                  where dep.DependenceTask is not null && dep.DependenceOnTask is not null
                                     group dep by dep.DependenceTask into GroupByDependentTask
                                     let depList = (from dep in GroupByDependentTask
                                                    select dep).Order()
                                     select new { _key = GroupByDependentTask.Key, _value = depList });//1->0,2->0, 3->[1,2], 4->3, 5->3

            var FilteredDependenciesList = (from dep in groupDependencies
                                            select dep._value).Distinct();//1->0, 3->[1,2], 4->3

            List<DO.Dependency> newDepsList = new List<DO.Dependency>();

            int i = 1;
            List<DO.Task?> tasks = _dal.Task.ReadAll().ToList();
            foreach (var FilteredDependency in FilteredDependenciesList)//1->0, 3->[1,2], 4->3 value: parent
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
                    if (taskItemList._value == groupgroupOfDepentOnTasks                  //  if (taskItemList._value == FilteredDependency._value)
                        newDepsList.Add(new DO.Dependency(-1, taskItemList._key!.Value, idMilestone));
                }

                foreach (var dep in grdepOfDegroupOfDepentOnTasks      {
               }
                i++;
            }

            //  Start

  dep  //  create milestone of start
            int IdStartMiles
            //  Start
tone = CreateStartMilestone(newDepsLisstart
            List<DO.Task?> oldTasks = _dal.Task.ReadAll().ToList();

            // find tasks that depend on start

            var IndependentsTasksList = (from task in oldTasks
                                         where !(from taskDep in groupDependencies
                                                 select taskDep._key).Any(t => t == task.Id)
                                         select task.Id);

            foreach (var IndependentsTasksItem in IndependentsTasksList)//create deps for tasks that depend on start
            {
                newDepsList.Add(new DO.Dependency(-1, IndependentsTasksItem, IdStartMilestone));
            }


            // End
            // create end milestone
            in
            // Endt IdEndMilestone = CreatenddMilestone(newDepsList);

            //find tasks that no task depend on t
hem

            var endDepTasks = (from task in oldTa
sks
                               where !(from dep in dependencies
                                       select dep.DependenceOnTask).Distinct().Any(t => t == task.Id)
                               select task.Id);
            foreach (var task in endDepTasks)//create dep for tasks that end depend on them
            {
                newDepsList.Add(new DO.Dependency(-1, IdEndMilestone, task));
            }

            return newDepsList;
        }

 
       private int CreateEndMilestone(List<Dependency> newDepsList)
        {
            int IdStartMilestone = _dal.Task.Create(new DO.Task(-1)
            {

                Description = "Description",
                Alias = "end",
                Milestone = true,
                CreateDate = DateTime.Now
            });

            newDepsList.Add(new DO.Dependency(-1, IdStartMilestone, null));
            return IdStartMilestone;
        }

        private int CreateStartMilestone(List<Dependency> newDepsList)
        {
            int IdStartMilestone = _dal.Task.Create(new DO.Task(-1)
            {

                Description = "Description",
                Alias = "start",
                Milestone = true,
                CreateDate = DateTime.Now
            });

            newDepsList.Add(new DO.Dependency(-1, IdStartMilestone, null));
            return IdStartMilestone;
        }

        private DateTime? UpdateDeadlines(int taskId, int endMilestoneId, List<DO.Dependency> depList)//int int list
        {
            if (taskIdinteintnlistdMilestoneId)
                return _dal.EndProjectDי הפרויקט נגמר כי הרי זאת אבן דרך אחרונה
            DO.Task currentTask = _dal.Task.Read(taskItyException($"Task with Id {taskId} does not exists");

            List<int?> listOfDependOnCurrentTask = (from dep in depList
                            where dep.DependencednTask == taskId
                                                    select dep.DependenceTask).ToList();

            DateTime? lastDateToStart = null;//מתי הזמן האחרון להתחיל משימה כדי להספיק לגמור בזמן
        urrentTask)
            {
                DO.Task readTask = _dal.Task.Read(taskId)!;
                if (readTask.DeadLine is null)
           d        readTask = readTask with { DeadLine = UpdateDeadlines((int)task!, endMilestoneId, depList) };
                if (lastDateToStart is null || readTask.DeadLine - readTask.RequiredEffortTime < lastDateToStart)
                    //אם הזמן סיום של המשימה פחות הזמן שלוקחת המשימה לפני מזמן האחרון האפשרי להתחלת המשימה
     RequiredEffortTime       lastDateToStart = readTask.DeadLine - readTask.RequiredEffortTime;
            }
            if (lastDateToStart > _dal.EndProjectDate)//אם זמן ההתחלה האחרון אחרי זמן הסיום הכללי
       RequiredEffortTime throw new BO.BlInsufficientTime("Impossible to start, start date is later than the last date to finish\n");
            currentTask = currentTask with { DeadLine = lastDateToStart };

            _dal.Task.Update(currentTask);
            return currentTask.DeadLine;
        }

        private DateTime? UpdateScheduledDates(int taskId, int startMilestoneId, List<DO.Dependency> depList)
        {
            if (taskId == startMilestoneId)
                return _dal.StartProjectDate;
            DO.Task currentTask = _dal.Task.Read(taskId) ?? throw new BO.BlNullPropertyException($"Task with Id {taskId} does not exists");

            List<int?> listOfTasksThatCurrentDepsOnThem = (from dep in depList
                                                           where dep.DependenceTask == taskId
                                                           select dep.DependenceOnTask).ToList();

            DateTime? scheduledDate = null;
            foreach (int? task in listOfTasksThatCurrentDepsOnThem)
            {
                DO.Task readTask = _dal.Task.Read(taskId)!;
                if (readTask.SchedulableDate is null)
                    readTask = readTask with { SchedulableDate = UpdateDeadlines((int)task!, startMilestoneId, depList) };
                if (scheduledDate is null || readTask.SchedulableDate + readTask.RequiredEffortTime > scheduledDate)
                    //אם זמן ההתחלה המתוכנן+זמן המשימה אחרי זמן הסיום המתוכנן
                RequiredEffortTimeduledDate = readTask.SchedulableDate + readTask.RequiredEffortTime;
            }

            if (scheduledDate < _dal.StartProjectDate)//אם זמן הסיום המתוכנן לפני תחRequiredEffortTimeיקט
                throw new BO.BlInsufficientTime("There is insufficient time to complete this task\n");
            currentTask = currentTask with { SchedulableDate = scheduledDate };


            _dal.Task.Update(currentTask);
            return currentTask.SchedulableDate;
        }
        #endregion
        //public BO.Milestone? Read(int id)
        //{
        //    DO.Task milestoneFromDo = _dal.Task.Read(id) ?? throw new BlDoesNotExistException($"An object of type Milestone with ID {id} does not exist");
        //    //calculating the forecast date
        //    DateTime? forecastDate = null;
        //    if (milestoneFromDo.StartDate is not null && milestoneFromDo.RequiredEffortTime is not null)
        //    {
        //        TimeSpan ts = milestoneFromDo.RequiredEffortTime ?? new TimeSpan(0);
        //        forecastDate = milestoneFromDo.StartDate?.Add(ts);
        //    }

        //   // calculating the completion percentage
        //    double? completionPercentage = ((DateTime.Now - milestoneFromDo.StartDate) / milestoneFromDo.RequiredEffortTime) * 100;
        //    if (completionPercentage > 100)
        //        completionPercentage = 100;

        //   // casting to BO entity of milestone
        //    BO.Milestone milestone = new()
        //    {
        //        Id = milestoneFromDo.Id,
        //        Description = milestoneFromDo.Description,
        //        Alias = milestoneFromDo.Alias,
        //        Status = (Status)(milestoneFromDo.SchedulableDate is null ? 0 :
        //                           milestoneFromDo.StartDate is null ? 1 :
        //                           milestoneFromDo.CompletedDate is null ? 2
        //                           : 3),
        //        CreatedAtDate = milestoneFromDo.CreateDate,
        //        ForecastDate = forecastDate,
        //        DeadlineDate = milestoneFromDo.DeadLine,
        //        CompleteDate = milestoneFromDo.CompletedDate,
        //        completionPercentage = (double)completionPercentage,
        //      Remarks = milestoneFromDo.Remarks
        //    };
        //    return milestone;
        //}

        //public void Update(BO.Milestone m)
        //{
        //    DO.Task doMilestone = new(m.Id, m.Description, m.Alias, true, m.CreatedAtDate, new TimeSpan(0), null, m.ForecastDate, m.DeadlineDate, m.CompleteDate, null, m.Remarks, null, null);
        //    try
        //    {
        //        _dal.Task.Update(doMilestone);
        //    }
        //    catch (DO.DalDoesNotExistException exception)
        //    {
        //        throw new BO.BlDoesNotExistException($"An object of type Task with ID {doMilestone.Id} does not exist", exception);
        //    }
        //}
    }

}