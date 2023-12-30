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
        if (doTask == null|| doTask.IsMilestone)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        return Tools.TaskFromDoToBo(doTask);


    }

    public IEnumerable<Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {

        var listTask = _dal.Task.ReadAll()
                 .Where(doTask=> !doTask.IsMilestone)
                .Select(doTask => Tools.TaskFromDoToBo(doTask));
        return filter == null ? listTask : listTask.Where(filter);
    }

    public void Update(Task boTask)
    {
        if (boTask.Id < 0 || boTask.Alias == ""|| _dal.Engineer.Read(boTask.Engineer.Id) == null)
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
    public void creatD()
    {
        for (int i = 0; i < 5; i++)
        {
            DO.Task doTask = new DO.Task(0,$"{i}","cfgv",DateTime.Now);
            _dal.Task.Create(doTask);
        }
        DO.Dependency doDependency = new DO.Dependency(0, 1002, 1001);
        _dal.Dependency.Create(doDependency);
        DO.Dependency doDependency1 = new DO.Dependency(0, 1002, 1000);
        _dal.Dependency.Create(doDependency1);
        DO.Dependency doDependency2 = new DO.Dependency(0, 1003, 1002);
        _dal.Dependency.Create(doDependency2);
        DO.Dependency doDependency3 = new DO.Dependency(0, 1004, 1002);
        _dal.Dependency.Create(doDependency3);

    }
    public void printd()
    {
      var d=  _dal.Dependency.ReadAll();
        foreach (var item in d.ToList())
        {
            Console.WriteLine(item);
        }
    }




}

