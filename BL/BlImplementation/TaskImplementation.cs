namespace BlImplementation;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Resources;
using BlApi;
using BO;
using DalApi;


public class TaskImplementation : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Task boTask)
    {
        if (boTask.Alias == null)
            throw new BO.BlNullPropertyException("you can not send a null property");
        if (boTask.Id < 0 || boTask.Alias == "")
            throw new BO.InvalidInputException("Invalid input");
        try
        {
            DO.Task doTask = Tools.TaskfromBoToDo(boTask);
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
        if (Tools.DependsTask(id))
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

        return Tools.TaskFromDoToBo(doTask);


    }

    public IEnumerable<Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {

        var listTask = _dal.Task.ReadAll()
                .Select(doTask => Tools.TaskFromDoToBo(doTask));
        return filter == null ? listTask : listTask.Where(filter);
    }

    public void Update(Task boTask)
    {
        if (boTask.Id < 0 || boTask.Alias == "")
            throw new BO.InvalidInputException("Invalid input");
        try
        {
            DO.Task doTask = Tools.TaskfromBoToDo(boTask);
            _dal.Task.Update(doTask);

        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={boTask.Id} already exists", ex);
        }
    }




}

