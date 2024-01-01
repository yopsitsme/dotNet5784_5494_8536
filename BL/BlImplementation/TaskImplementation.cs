namespace BlImplementation;
using System;
using System.Collections.Generic;
using BlApi;
using BO;
using DalApi;


/// <summary>
/// Implementation of the <see cref="ITask"/> interface providing CRUD operations for tasks.
/// </summary>
public class TaskImplementation : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Creates a new task based on the provided business object.
    /// </summary>
    /// <param name="boTask">The business object representing the task to be created.</param>
    /// <returns>The ID of the newly created task.</returns>
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
    /// <summary>
    /// Deletes a task with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the task to be deleted.</param>
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

    /// <summary>
    /// Retrieves a task with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the task to be retrieved.</param>
    /// <returns>The business object representation of the retrieved task.</returns>
    public Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null|| doTask.IsMilestone)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        return Tools.TaskFromDoToBo(doTask);


    }

    /// <summary>
    /// Retrieves all tasks, optionally filtered by a specified condition.
    /// </summary>
    /// <param name="filter">The filter condition to apply to the tasks.</param>
    /// <returns>The collection of business object representations of tasks.</returns>

    public IEnumerable<Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {

        var listTask = _dal.Task.ReadAll()
                 .Where(doTask=> doTask?.IsMilestone??false)
                .Select(doTask => Tools.TaskFromDoToBo(doTask));
        return filter == null ? listTask : listTask.Where(filter);
    }

    /// <summary>
    /// Updates an existing task with the provided business object.
    /// </summary>
    /// <param name="boTask">The business object representing the task to be updated.</param>
    public void Update(Task boTask)
    {
        if (boTask.Id < 0 || boTask.Alias == ""|| _dal.Engineer.Read(boTask?.Engineer?.Id??0) == null)
            throw new BO.InvalidInputException("Invalid input");
        try
        {
            if (boTask != null)
            {
                DO.Task doTask = Tools.TaskfromBoToDo(boTask);
                _dal.Task.Update(doTask);
            }

        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={boTask?.Id} already exists", ex);
        }
    }

    /// <summary>
    /// Creates sample tasks and dependencies for testing purposes.
    /// </summary>
    public void creatD()
    {
        Random s_rand = new();

        for (int i = 0; i < 5; i++)
        {
          
            DO.Task doTask = new DO.Task(0,$"{i}","cfgv",DateTime.Now, TimeSpan.FromDays(s_rand.Next(10,50)));
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

    /// <summary>
    /// Prints all dependencies to the console for testing purposes.
    /// </summary>
    public void printd()
    {
      var d=  _dal.Dependency.ReadAll();
        foreach (var item in d.ToList())
        {
            Console.WriteLine(item);
        }
    }




}

