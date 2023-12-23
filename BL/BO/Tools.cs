

using DalApi;

namespace BO;

public static class Tools
{
    private  static IDal _dal = Factory.Get;

    public static List <BO.TaskInList> depndentTesks(int id)
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

    public static  double completionPercentage(List<TaskInList> listTask)
    {
        double sum = 0;
        for (int i = 0; i < listTask.Count; i++)
        {
            sum += (int)listTask[i].Status * 100 / 4;
        }
        return sum / listTask.Count;

    }
    public static BO.Task taskFromDoToBo(DO.Task doTask) {
        return new Task
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            CreatedAtDate = doTask.CreatedAtDate,
            Status = Tools.myStatus(doTask),
            //Milestone =is //פונקציה שתקבל את האבן דרך,
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
    public  static BO.EngineerInTask? EngineerInTask(int? id)
    {
        if (id == null)
            return null;
        var engineerInTask = _dal.Engineer.ReadAll()
                      .Where(e => e!.Id == id)
                      .Select(en => new EngineerInTask { Id = id ?? 0, Name = en?.Name }).First();
        return engineerInTask;
    }

}
