using BlApi;
using BO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

            return new BO.Milestone
            {
                Id = doTask.Id,
                Description = doTask.Description,
                CreatedAtDate = doTask.CreatedAtDate,
                Status = Tools.myStatus(doTask),
                ForeCastDate = doTask.StartDate?.Add(doTask?.RequierdEffortTime??new TimeSpan(0)),
                CompleteDate = doTask!.CompleteDate,
                DeadLineDate = doTask.DeadLineDate??DateTime.Now,
                CompletionPercentage = completionPercentage(Tools.depndentTesks(doTask.Id)),
                Remarks = doTask.Remarks,
                Dependencies = Tools.depndentTesks(doTask.Id),
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
