

using BlApi;
using DalApi;

namespace BO;

public static class Tools
{
    private static DalApi.IDal _dal = DalApi.Factory.Get;

    public static List<BO.TaskInList> depndentTesks(int id)
    {
        var listId = _dal!.Dependency.ReadAll((d) => d.DependentTask == id);
        var dependencies = (from d in listId.ToList()
                            let task = _dal!.Task.Read(d.DependsTask)
                            select new TaskInList
                            {
                                Id = task.Id,
                                Ailas = task.Alias,
                                Description = task.Description,
                                Status = myStatus(task)
                            }).ToList();
        return dependencies;
    }


    public static Status myStatus(DO.Task task)
    {
        return (BO.Status)(task.ScheduledDate == null ? 0
                                                     : task.DeadLineDate?.AddDays(-5) == DateTime.Now ? 2
                                                     : task.StartDate == null ? 1
                                                    : task.CompleteDate == null ? 3
                                                    : 4);
    }
    public static List<DO.Dependency>? CreateMileStone(List<DO.Dependency>? dependencies)
    {
        int count = 0;
        DO.Task firstMilestone = new()
        {
            Alias = $"M{count++}",
            Description = $"MStart",
            CreatedAtDate = DateTime.Now,
            IsMilestone = true,//ismilestone
        };
        int idfirstMilestone = _dal.Task.Create(firstMilestone);

        List<DO.Dependency> newDependencies = new List<DO.Dependency>();
        var list = dependencies.GroupBy(d => d.DependentTask).ToList();
        var sortedList = list.OrderBy(comparer => comparer);
        foreach (DO.Task task in _dal.Task.ReadAll())
        {
            var isTaskIdInList = sortedList.Any(group => group.Key == task.Id);

            if (!isTaskIdInList)
            {
                newDependencies.Add(new DO.Dependency()
                {
                    DependentTask = task.Id,
                    DependsTask = idfirstMilestone
                });
            }

        }
        foreach (var group in list)
        {
            DO.Task milestone = new()
            {
                Alias = $"M{count++}",
                Description = $"M{count++}",
                CreatedAtDate = DateTime.Now,
                IsMilestone = true,
            };
            int id = _dal.Task.Create(milestone);
            foreach (var depend in group)
            {
                newDependencies.Add(new DO.Dependency()
                {
                    DependentTask = id,
                    DependsTask = depend.DependsTask
                });
            }
            newDependencies.Add(new DO.Dependency()
            {
                DependentTask = group.Key,
                DependsTask = id
            });


        }


        return newDependencies;
    }

    public static double completionPercentage(List<TaskInList> listTask)
    {
        double sum = 0;
        for (int i = 0; i < listTask.Count; i++)
        {
            sum += (int)listTask[i].Status * 100 / 4;
        }
        return sum / listTask.Count;

    }
    public static DO.Task TaskfromBoToDo(BO.Task boTask)
    {
        return new DO.Task
        {
            Id = boTask.Id,
            Alias = boTask.Alias,
            Description = boTask.Description,
            CreatedAtDate = boTask.CreatedAtDate,
            RequierdEffortTime = boTask.RequierdEffortTime,
            IsMilestone = (boTask.Milestone?.Alias == null || boTask.Milestone.Alias == "") ? false : true,
            CompleteDate = boTask.CompleteDate,
            StartDate = boTask.StartDate,
            ScheduledDate = boTask.ScheduledStartDate,
            DeadLineDate = boTask.DeadLineDate,
            Deliverables = boTask.Deliverables,
            Remarks = boTask.Remarks,
            EngineerId = boTask.Engineer.Id,
            ComplexityLevel = (DO.EngineerExperience)boTask.ComplexityLevel,
        };
    }
    public static BO.Task TaskFromDoToBo(DO.Task doTask)
    {
        return new Task
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            CreatedAtDate = doTask.CreatedAtDate,
            Status = Tools.myStatus(doTask),
            Milestone = milestoneInTask(doTask.Id),
            StartDate = doTask.StartDate,
            ScheduledStartDate = doTask.ScheduledDate,
            ForeCastDate = doTask.StartDate?.Add(doTask?.RequierdEffortTime ?? new TimeSpan(0)),
            CompleteDate = doTask!.CompleteDate,
            DeadLineDate = doTask.DeadLineDate,
            Deliverables = doTask.Deliverables,
            Remarks = doTask.Remarks,
            Engineer = EngineerInTask(doTask.EngineerId),
            ComplexityLevel = (BO.EngineerExperience)doTask.ComplexityLevel
        };
    }
    public static BO.EngineerInTask? EngineerInTask(int? id)
    {
        if (id == null)
            return null;
        var engineerInTask = _dal.Engineer.ReadAll()
                      .Where(e => e!.Id == id)
                      .Select(en => new EngineerInTask { Id = id ?? 0, Name = en?.Name }).First();
        return engineerInTask;
    }
    public static DO.Engineer EngineerfromBoToDo(BO.Engineer boEngineer)
    {
        return new DO.Engineer(
        boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);
    }

    public static BO.Engineer EngineerfromDoToBo(DO.Engineer doEngineer)
    {
        return new BO.Engineer
        {
            Id = doEngineer.Id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            Task = TaskinEngineer(doEngineer.Id)
        };
    }
    public static TaskinEngineer? TaskinEngineer(int Id)
    {
        return (from DO.Task doTask in _dal.Task.ReadAll()
                where doTask.EngineerId == Id
                select new TaskinEngineer
                {
                    Id = doTask.Id,
                    Alias = doTask.Alias
                }).FirstOrDefault();// FirstOrDefaultכתוב בתיאור הכללי שמהנדס יכול לעבוד על משימה אחת בו אבל גם יתכן שךא מוגדרת לו אף משימה ולכן ניתן לןהשתמש בפונקציה 
    }
    public static bool DependsTask(int id)
    {
        var flagDependsTask = _dal.Dependency.ReadAll()
         .FirstOrDefault(d => d!.DependentTask == id);
        return (flagDependsTask != null);
    }
    public static BO.MilestoneInTask? milestoneInTask(int id)
    {
        BO.MilestoneInTask? milestoneInTask = Tools.depndentTesks(id)
                                .Where(d => _dal.Task!.Read(d.Id)!.IsMilestone)
                                .Select(d => new BO.MilestoneInTask()
                                {
                                    Id = d.Id,
                                    Alias = d.Ailas
                                }) .FirstOrDefault();

        return milestoneInTask;

    }
    public static BO.Milestone fromDoTaskToMilestone(DO.Task doTask)
    {
        return new BO.Milestone
        {
            Id = doTask.Id,
            Description = doTask.Description,
            CreatedAtDate = doTask.CreatedAtDate,
            Status = Tools.myStatus(doTask),
            ForeCastDate = doTask.StartDate?.Add(doTask?.RequierdEffortTime ?? new TimeSpan(0)),
            CompleteDate = doTask!.CompleteDate,
            DeadLineDate = doTask.DeadLineDate ?? DateTime.Now,
            CompletionPercentage = Tools.completionPercentage(Tools.depndentTesks(doTask.Id)),
            Remarks = doTask.Remarks,
            Dependencies = Tools.depndentTesks(doTask.Id),
        };
    }
}
