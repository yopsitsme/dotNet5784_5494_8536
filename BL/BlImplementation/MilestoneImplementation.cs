using BlApi;
using BO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

public class MilestoneImplementation : IMilestone
{
    private IDal _dal = Factory.Get;

    public int Create()
    {
        throw new NotImplementedException();
    }

    public BO.Milestone? Read(int id)
    {
        try
        {
            DO.Task? doTask = _dal!.Task.Read(id);
            if (doTask == null || doTask.IsMilestone != true)
                throw new BO.BlDoesNotExistException($"Milestone with ID={id} does Not exist");
            var listId = _dal!.Dependency.ReadAll((d) => d.DependentTask == id);
            var listTask = (from d in listId.ToList()
                            let task = _dal!.Task.Read(d.DependsTask)
                            select new TaskInList
                            {
                                Id = task.Id,
                                Ailas = task.Alias,
                                Description = task.Description,
                                Status = (BO.Status)(task.ScheduledDate == null ? 0
                                                     : task.StartDate == null ? 1
                                                     :task.DeadLineDate?.AddDays(-5)==DateTime.Now?2
                                                    : task.CompleteDate == null ? 3
                                                    : 4)
                            }).ToList();

            return new BO.Milestone
            {
                Id = doTask.Id,
                Description = doTask.Description,
                CreatedAtDate = doTask.CreatedAtDate,
                Status = (BO.Status)1,
               // ForecastDate =
                CompleteDate = doTask.CompleteDate,
                DeadLineDate = doTask.DeadLineDate??DateTime.Now,
                CompletionPercentage = completionPercentage(listTask),
                Remarks = doTask.Remarks,
                Dependencies = listTask,
            };
        }
        catch (Exception ex)
        {
            throw new BlDoesNotExistException("", ex);
        }


    }

    public void Update(Milestone item)
    {
        throw new NotImplementedException();
    }
    private double completionPercentage(List<TaskInList> listTask)
    {
        double sum = 0;
        for (int i = 0; i < listTask.Count; i++)
        {
            sum += (int)listTask[i].Status * 100 / 4;
        }
        return sum / listTask.Count;

    }
}
