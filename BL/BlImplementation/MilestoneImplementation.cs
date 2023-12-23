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

    public void Create()
    {
       List <DO.Dependency> newDependencies=Tools.CreateMileStone(_dal.Dependency.ReadAll()?.ToList());
        _dal.Dependency.Reset();
        foreach(DO.Dependency depent in newDependencies){
            _dal.Dependency.AddDependency(depent);
        }
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
                CompletionPercentage = Tools.completionPercentage(Tools.depndentTesks(doTask.Id)),
                Remarks = doTask.Remarks,
                Dependencies = Tools.depndentTesks(doTask.Id),
            };
        }
        catch (Exception ex)
        {
            throw new BlDoesNotExistException("", ex);
        }


    }

    public BO.Milestone Update(int id, string alias,string description,string remarks)
    {
        if(alias==""|| description==""|| remarks=="")
        { throw new BO.InvalidInputException(""); }
        try
        {
           DO.Task abc= _dal.Task.Read(id);
            

        }
    }
  
}
