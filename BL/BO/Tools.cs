

using BlApi;
using DalApi;
using System;
using System.Reflection;
using System.Collections;
using System.Text;

namespace BO;

/// <summary>
/// Utility class containing various static methods for handling tasks and dependencies.
/// </summary>
public static class Tools
{
    private static DalApi.IDal _dal = DalApi.Factory.Get;
    static BlApi.IBl s_bl = BlApi.Factory.Get();
    // <summary>
    /// Retrieves a list of dependent tasks for a given task ID.
    /// </summary>
    /// <param name="id">The ID of the task.</param>
    /// <returns>A list of dependent tasks represented as <see cref="TaskInList"/>.</returns>
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

    /// <summary>
    /// Calculates and returns the status of a task based on its various date properties.
    /// </summary>
    /// <param name="task">The task for which to determine the status.</param>
    /// <returns>The status of the task as <see cref="BO.Status"/>.</returns>
    public static Status myStatus(DO.Task task)
    {
        return (BO.Status)(task.ScheduledDate == null ? 0
                                                     : task.DeadLineDate?.AddDays(-5) == DateTime.Now ? 2
                                                     : task.StartDate == null ? 1
                                                    : task.CompleteDate == null ? 3
                                                    : 4);
    }
    public static List<DO.Dependency> CreateMileStone(List<DO.Dependency> ? dependencies)
    {
        if (dependencies == null)
            dependencies = new List<DO.Dependency>();
        List<DO.Dependency> newDependencies = new List<DO.Dependency>();
        int count = 0;
      
        DO.Task firstMilestone = new(
         0,
         $"M{count++}",
         $"MStart",
         DateTime.Now,
         new TimeSpan(0),
         true//ismilestone
         );
        int idfirstMilestone = _dal.Task.Create(firstMilestone);

        var list = dependencies.GroupBy(d => d.DependentTask).ToList();
        var sortedList = list.OrderBy(comparer => comparer.Key);
        foreach (DO.Task task in _dal.Task.ReadAll())
        {

            var isTaskIdInList = sortedList.Any(group => group.Any(item => item.DependentTask == task.Id));
            if (!isTaskIdInList && task.Id != idfirstMilestone)
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
            bool flagExsistMileStone = false;
            int id = 0;
            var allMilestones = _dal.Task.ReadAll(task => task.IsMilestone).ToList();

            foreach (var m in allMilestones)
            {
                List<DO.Dependency> allDependencies = newDependencies?.Where(d => d!.DependentTask == m.Id)?.ToList();
                flagExsistMileStone = AreGroupsEqual(allDependencies, group.ToList());
                if (flagExsistMileStone)
                {
                    id = m!.Id;
                    break;

                }
            }
            if (!flagExsistMileStone)
            {

                DO.Task milestone = new DO.Task(
                  0,
                $"M{count++}",
                $"M{count}",
                DateTime.Now,
               TimeSpan.Zero,
                true);
                id = _dal.Task.Create(milestone);

                foreach (var depend in group)
                {
                    newDependencies.Add(new DO.Dependency()
                    {
                        DependentTask = id,
                        DependsTask = depend.DependsTask
                    });
                }
            }
            newDependencies.Add(new DO.Dependency()
            {
                DependentTask = group.Key,
                DependsTask = id
            });

        }
        DO.Task endMilestone = new(
                                 0,
                                  $"M{count++}",
                                   $"MEnd",
    DateTime.Now,
    new TimeSpan(0),
    true
    );
        int idendMilestone = _dal.Task.Create(endMilestone);

        var DependonsonEnd = (from DO.Task t in _dal.Task.ReadAll()
                              where ((_dal.Dependency.Read((de) => de.DependsTask == t.Id) == null ? true : false) && !t.IsMilestone)
                              select t.Id).ToList();
        foreach (var tId in DependonsonEnd)
        {
            newDependencies.Add(new DO.Dependency()
            {
                DependentTask = idendMilestone,
                DependsTask = tId,
            });
        }
        return newDependencies;
    }

    /// <summary>
    /// Calculates the completion percentage of a list of tasks.
    /// </summary>
    /// <param name="listTask">The list of tasks for which to calculate the completion percentage.</param>
    /// <returns>The completion percentage as a double.</returns>
    public static double completionPercentage(List<TaskInList> listTask)
    {
        double sum = 0;
        for (int i = 0; i < listTask.Count; i++)
        {
            sum += (int)listTask[i].Status * 100 / 4;
        }
        return sum / listTask.Count;

    }
    /// <summary>
    /// Converts a business object representation of a task to a data object representation.
    /// </summary>
    /// <param name="boTask">The business object task to convert.</param>
    /// <returns>The data object representation of the task.</returns>
    public static DO.Task TaskfromBoToDo(BO.Task boTask)
    {
        //return new DO.Task
        //{
        //   Id=  boTask.Id,
        //   Alias= boTask.Alias,
        //  Description= boTask.Description,
        //  CreatedAtDate=  boTask.CreatedAtDate,
        //   RequierdEffortTime= boTask.RequierdEffortTime,
        // IsMilestone=   false,
        //  CompleteDate=  boTask.CompleteDate,
        //  StartDate=   boTask.StartDate,
        //   ScheduledDate=  boTask.ScheduledStartDate,
        //    DeadLineDate= boTask.DeadLineDate,
        //   Deliverables=  boTask.Deliverables,
        //  Remarks=   boTask.Remarks,
        //   EngineerId= boTask.Engineer.Id,
        //   ComplexityLevel=  (DO.EngineerExperience)boTask.ComplexityLevel
        //};
        return new DO.Task
        (
           boTask.Id,
            boTask.Alias,
            boTask.Description,
            boTask.CreatedAtDate,
            boTask.RequierdEffortTime,
            false,
            boTask.StartDate,
            boTask.ScheduledStartDate,
            boTask.DeadLineDate,
            boTask.CompleteDate,
            boTask.Deliverables,
            boTask.Remarks,
            boTask.Engineer?.Id ?? null,
           boTask.ComplexityLevel == null ? null : (DO.EngineerExperience)boTask.ComplexityLevel
        );
    }
    /// <summary>
    /// Converts a data object representation of a task to a business object representation.
    /// </summary>
    /// <param name="doTask">The data object task to convert.</param>
    /// <returns>The business object representation of the task.</returns>
    public static BO.Task TaskFromDoToBo(DO.Task doTask)
    {
        return new Task()
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
            ComplexityLevel = doTask.ComplexityLevel == null ? null : (BO.EngineerExperience)doTask.ComplexityLevel
        };
    }
    /// <summary>
    /// Retrieves information about an engineer associated with a task.
    /// </summary>
    /// <param name="id">The ID of the engineer.</param>
    /// <returns>The <see cref="EngineerInTask"/> information if available; otherwise, null.</returns>
    public static BO.EngineerInTask? EngineerInTask(int? id)
    {
        if (id == null)
            return null;
        var engineerInTask = _dal.Engineer.ReadAll()
                      .Where(e => e!.Id == id)
                      .Select(en => new EngineerInTask { Id = id ?? 0, Name = en?.Name }).First();
        return engineerInTask;
    }

    /// <summary>
    /// Converts a business object representation of an engineer to a data object representation.
    /// </summary>
    /// <param name="boEngineer">The business object engineer to convert.</param>
    /// <returns>The data object representation of the engineer.</returns>
    public static DO.Engineer EngineerfromBoToDo(BO.Engineer boEngineer)
    {
        return new DO.Engineer(
        boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);
    }
    /// <summary>
    /// Converts a data object representation of an engineer to a business object representation.
    /// </summary>
    /// <param name="doEngineer">The data object engineer to convert.</param>
    /// <returns>The business object representation of the engineer.</returns>
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
                                }).FirstOrDefault();

        return milestoneInTask;

    }
    /// <summary>
    /// Converts a business object representation of a milestone to a data object representation.
    /// </summary>
    /// <param name="doTask">The data object task representing the milestone.</param>
    /// <returns>The business object representation of the milestone.</returns>
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


    /// <summary>
    /// Generates a string representation of an entity using its properties.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="entity">The entity for which to generate the string representation.</param>
    /// <returns>The string representation of the entity properties.</returns>
    public static string ToStringProperty<T>(this T entity)
    {
        StringBuilder sb = new StringBuilder();

        // Get all properties of the entity using reflection
        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            // Check if the property is a collection
            if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string))
            {
                // Handle collections
                IEnumerable collection = (IEnumerable)property.GetValue(entity);
                if (collection != null)
                {
                    sb.AppendLine($"{property.Name}:");
                    foreach (var item in collection)
                    {
                        sb.AppendLine($"- {item}");
                    }
                }
            }
            else
            {
                // Handle regular properties
                sb.AppendLine($"{property.Name}: {property.GetValue(entity)}");
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Calculates and updates task deadlines based on project end date.
    /// </summary>
    /// <param name="endPro">The project end date.</param>
    /// <param name="startPro">The project start date.</param>
    /// <param name="tasks">The list of tasks.</param>
    internal static void CalculationTimes(DateTime endPro, DateTime startPro, List<Task> tasks)
    {

        List<BO.Task> endtask = (s_bl.Task.ReadAll((t) => t.Description == "MEnd")).ToList();
        foreach (var deptask in depndentTesks(endtask[0].Id))
        {
            BO.Task task = s_bl.Task.Read(deptask.Id)!;
            task.DeadLineDate = endPro;
            s_bl.Task.Update(task);

        }


    }

    /// <summary>
    /// Recursively calculates and updates task deadlines based on milestones and dependencies.
    /// </summary>
    /// <param name="task">The task for which to calculate the deadline.</param>
    /// <param name="milestone">The milestone associated with the task.</param>
    /// <param name="dateToDeadLine">The date to the project's deadline.</param>
    private static void RecursionCalculationTimes(Task? task, Milestone? milestone, DateTime dateToDeadLine)
    {


        try
        {
            if (milestone != null)
            {
                s_bl.Milestone.Read(task.Id);

            }
        }
        catch (Exception ex)
        {
            DateTime? updateDate = dateToDeadLine;
            if (task.DeadLineDate != null)
            {
                updateDate = dateToDeadLine > (task.DeadLineDate) ? dateToDeadLine : task.DeadLineDate;
            }
            task.DeadLineDate = updateDate;
            s_bl.Task.Update(task);
            RecursionCalculationTimes(null, s_bl.Milestone.Read(task.Milestone.Id), task.DeadLineDate?.Add(task.RequierdEffortTime ?? new TimeSpan(0)) ?? DateTime.Now);
        }

    }

    /// <summary>
    /// Checks whether two lists of dependencies are equal.
    /// </summary>
    /// <param name="list1">The first list of dependencies to compare.</param>
    /// <param name="list2">The second list of dependencies to compare.</param>
    /// <returns>True if the lists are equal, false otherwise.</returns>
    public static bool AreGroupsEqual(List<DO.Dependency>? list1, List<DO.Dependency>? list2)
    {
        // Check if both lists are null
        if (list1 == null && list2 == null)
        {
            return true;
        }

        // Check if one list is null while the other is not
        if ((list1 == null && list2 != null) || (list2 == null && list1 != null))
        {
            return false;
        }

        // Check if both lists are the same instance or both are null
        if (ReferenceEquals(list1, list2))
        {
            return true;
        }

        // Check if any of the lists is null or their counts are different
        if (list1 == null || list2 == null || list1.Count != list2.Count)
        {
            return false;
        }

        // Sort both lists to ensure the order of elements is the same for comparison
        list1.Sort((d1, d2) => d1.Id.CompareTo(d2.Id));
        list2.Sort((d1, d2) => d1.Id.CompareTo(d2.Id));

        // Compare each element in both lists
        for (int i = 0; i < list1.Count; i++)
        {
            // Return false if any elements are not equal
            if (list1[i].DependsTask != list2[i].DependsTask)
            {
                return false;
            }
        }

        // All elements are equal
        return true;
    }

}






