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
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void Create()
    {
        DateTime ?startProject = _dal.StartProject;
        DateTime? endProject = _dal.EndProject;
        if (startProject != null && endProject != null)
        {
            List<DO.Dependency> newDependencies = Tools.CreateMileStone(_dal?.Dependency.ReadAll().ToList(), startProject, endProject);
            _dal.Dependency.Reset();
            foreach (DO.Dependency depent in newDependencies)
            {
                _dal.Dependency.Create(depent);
            }
            Tools.CalculationTimes(_dal.Dependency.ReadAll()?.ToList(), startProject ?? DateTime.Now, endProject??DateTime.Now);
        }
        else { throw new BlNoDatesForProject("the program is missing a start or end date for the project"); }

    }

    public BO.Milestone? Read(int id)
    {
        try
        {
            DO.Task? doTask = _dal!.Task.Read(id);
            if (doTask == null || doTask.IsMilestone != true)
                throw new BO.BlDoesNotExistException($"Milestone with ID={id} does Not exist");

            return Tools.fromDoTaskToMilestone(doTask);
        }
        catch (Exception ex)
        {
            throw new BlDoesNotExistException("", ex);
        }


    }

    public BO.Milestone Update(int id, string alias, string description, string? remarks)
    {
        if (alias == null || alias == "" || description == null || description == "" || remarks == "")
        { throw new BO.InvalidInputException("InvalidInput"); }

        DO.Task? task = _dal.Task.Read(id);
        if (task == null)
            throw new BO.BlDoesNotExistException($"DoesNotExist milestone whith{id}");
        //DO.Task newtask = new DO.Task
        //{
        //    Id = id,
        //    Alias = alias,
        //    Description = description,
        //    CreatedAtDate = task.CreatedAtDate,
        //    RequierdEffortTime = task.RequierdEffortTime,
        //    IsMilestone = task.IsMilestone,
        //    StartDate = task.StartDate,
        //    ScheduledDate = task.ScheduledDate,
        //    DeadLineDate = task.DeadLineDate,
        //    CompleteDate = task.CompleteDate,
        //    Deliverables = task.Deliverables,
        //    Remarks = remarks,
        //    EngineerId = task.EngineerId,
        //    ComplexityLevel = task.ComplexityLevel,
        //};
        //_dal.Task.Update(newtask);//יצליח בטוח לעדכן כי בדקתי בread ש ה id  הזה כבר קיים
        //return Tools.fromDoTaskToMilestone(newtask);
        return new BO.Milestone();
    }

}
