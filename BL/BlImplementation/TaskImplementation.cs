namespace BlImplementation;
using System;
using BO;
using DalApi;


public class TaskImplementation : BlApi.ITask
{
    private IDal _dal = Factory.Get;

    public int Create(Task boTask)
    {
        // כאן יש להוסיף בדיקות תקינות נוספות לפי הדרישות שלך
        // אם אין כלל בדיקות תקינות נוספות יתכן שהמימוש יפשט
        try
        {
            DO.Task doTask = new DO.Task
            {
                Id = boTask.Id,
                Description = boTask.Description,
                Ailas = boTask.Ailas,
                CreatedAtDete = boTask.CreatedAtDete,
                Milestone = (DO.MilestoneInTask)boTask.Milestone,
                BaseLineStartDate = boTask.BaseLineStartDate,
                StartDate = boTask.StartDate,
                ScheduledStartDate = boTask.ScheduledStartDate,
                ForecastDate = boTask.ForecastDate,
                CompleteDate = boTask.CompleteDate,
                DeadLineDate = boTask.DeadLineDate,
                Deliverables = boTask.Deliverables,
                Remarks = boTask.Remarks,
                Engineer = new DO.EngineerInTask
                {
                    EngineerId = boTask.Engineer.Id,
                    TaskId = boTask.Id
                },
                ComplexityLevl = (DO.EngineerExperience)boTask.ComplexityLevl
            };

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
        throw new NotImplementedException();
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
            Ailas = doTask.Ailas,
            CreatedAtDete = doTask.CreatedAtDete,
            Status = (BO.Status)doTask.Status,
            Milestone = (BO.MilestoneInTask)doTask.Milestone,
            BaseLineStartDate = doTask.BaseLineStartDate,
            StartDate = doTask.StartDate,
            ScheduledStartDate = doTask.ScheduledStartDate,
            ForecastDate = doTask.ForecastDate,
            CompleteDate = doTask.CompleteDate,
            DeadLineDate = doTask.DeadLineDate,
            Deliverables = doTask.Deliverables,
            Remarks = doTask.Remarks,
            Engineer = new BO.EngineerInTask
            {
                EngineerId = doTask.Engineer?.EngineerId ?? 0, // אם המהנדס יש ערך, יש להשיב אותו, אחרת להשיב 0
                TaskId = id
            },
            ComplexityLevl = (BO.EngineerExperience)doTask.ComplexityLevl
        };
    }

    public IEnumerable<Task> ReadAll()
    {
        return (from DO.Task doTask in _dal.Task.ReadAll()
                select new Task
                {
                    Id = doTask.Id,
                    Description = doTask.Description,
                    Ailas = doTask.Ailas,
                    CreatedAtDete = doTask.CreatedAtDete,
                    Status = (BO.Status)doTask.Status,
                    Milestone = (BO.MilestoneInTask)doTask.Milestone,
                    BaseLineStartDate = doTask.BaseLineStartDate,
                    StartDate = doTask.StartDate,
                    ScheduledStartDate = doTask.ScheduledStartDate,
                    ForecastDate = doTask.ForecastDate,
                    CompleteDate = doTask.CompleteDate,
                    DeadLineDate = doTask.DeadLineDate,
                    Deliverables = doTask.Deliverables,
                    Remarks = doTask.Remarks,
                    Engineer = new BO.EngineerInTask
                    {
                        EngineerId = doTask.Engineer?.EngineerId ?? 0, // אם המהנדס יש ערך, יש להשיב אותו, אחרת להשיב 0
                        TaskId = doTask.Id
                    },
                    ComplexityLevl = (BO.EngineerExperience)doTask.ComplexityLevl
                });
    }

    public void Update(Task boTask)
    {
        // כאן יש להוסיף בדיקות תקינות נוספות לפי הדרישות שלך
        // אם אין כלל בדיקות תקינות נוספות יתכן שהמימוש יפשט
        try
        {
            DO.Task do

