namespace BlImplementation;
using System;
using System.Resources;
using BO;
using DalApi;


public class TaskImplementation : BlApi.ITask
{
    private IDal _dal = Factory.Get;
    public int Create(BO.Task boTask)
    {
        if (boTask.Alias==null)
            throw new BO.BlNullPropertyException("you can not send a null property");
        if (boTask.Id<0||boTask.Alias=="")
            throw new BO.InvalidInputException("Invalid input");
        try
        {
            DO.Task doTask = fromBoToDoTask(boTask);
            int taskId = _dal.Task.Create(doTask);
            return taskId;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        if (DependsTask(id))
           throw new BO.BlDeletionImpossible($"Their is atask(or more) who depent on the task with the ID={id}");
        try
        {
            _dal.Task.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} already exists", ex);
        }

    }

    public Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        return new Task
        {
            Id = id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            CreatedAtDate = doTask.CreatedAtDate,
            //Status = ,
            //Milestone = doTask.IsMilestone&&//פונקציה שתקבל את האבן דרך,
            //BaseLineStartDate = doTask.BaseLineStartDate,
            StartDate = doTask.StartDate,
            ScheduledStartDate = doTask.ScheduledDate,
            ForecastDate = doTask.ForecasDate,
            CompleteDate = doTask.CompleteDate,
            DeadLineDate = doTask.DeadLineDate,
            Deliverables = doTask.Deliverables,
            Remarks = doTask.Remarks,
            Engineer = EngineerInTask(doTask.EngineerId),
            ComplexityLevel = (BO.EngineerExperience)doTask.ComplexityLevel
        };
    }

    public IEnumerable<Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
       var listTask=_dal.Task.ReadAll()
                .Select(doTask=>new BO.Task
                {
                    Id = doTask!.Id,
                    Description = doTask.Description,
                    Alias = doTask.Alias,
                    CreatedAtDate = doTask.CreatedAtDate,
                    //Status = ,
                    //Milestone = doTask.IsMilestone&&//פונקציה שתקבל את האבן דרך,
                    //BaseLineStartDate = doTask.BaseLineStartDate,
                    StartDate = doTask.StartDate,
                    ScheduledStartDate = doTask.ScheduledDate,
                    ForecastDate = doTask.ForecasDate,
                    CompleteDate = doTask.CompleteDate,
                    DeadLineDate = doTask.DeadLineDate,
                    Deliverables = doTask.Deliverables,
                    Remarks = doTask.Remarks,
                    Engineer = EngineerInTask(doTask.EngineerId),
                    ComplexityLevel = (BO.EngineerExperience)doTask.ComplexityLevel
                });
        return filter==null? listTask: listTask.Where(filter);
    }

    public void Update(Task boTask)
    {
        if (boTask.Id < 0 || boTask.Alias == "")
            throw new BO.InvalidInputException("Invalid input");
        try
        {
            DO.Task doTask = fromBoToDoTask(boTask);
            _dal.Task.Update(doTask);
          
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={boTask.Id} already exists", ex);
        }
    }


    public DO.Task fromBoToDoTask(BO.Task boTask)
    {
    return new DO.Task
    {
        Id = boTask.Id,
        StartDate = boTask.StartDate,
        ScheduledDate = boTask.ScheduledStartDate,
        ForecasDate = boTask.ForecastDate,
        CompleteDate = boTask.CompleteDate,
        DeadLineDate = boTask.DeadLineDate,
        Deliverables = boTask.Deliverables,
        Remarks = boTask.Remarks,
        EngineerId = boTask.Engineer.Id, 
        ComplexityLevel = (DO.EngineerExperience)boTask.ComplexityLevel,
        Description = boTask.Description,
        Alias = boTask.Alias,
        IsMilestone =(boTask.Milestone.Alias==""|| boTask.Milestone.Alias==null) ?false:true,
        CreatedAtDate = boTask.CreatedAtDate,

    };
    }

    public BO.EngineerInTask  EngineerInTask(int id)
    {
        var engineerInTask = _dal.Engineer.ReadAll()
                      .Where(e => e!.Id==id)
                      .Select(en => new EngineerInTask { Id = id, Name = en?.Name }).First();
        return engineerInTask;
    }
    public bool DependsTask(int id)
    {
       var flagDependsTask= _dal.Dependency.ReadAll()
        .FirstOrDefault(d => d!.DependentTask == id);
        return(flagDependsTask!=null);

    }


}

