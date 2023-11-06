namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        Task newtTask = item with { Id = DataSource.Config.NextEngineerId };
        DataSource.Tasks.Add(newtTask);
        return newtTask.Id;
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        Task newtTask=Read(id);
        if (newtTask != null) 
        { throw new NotImplementedException(); }
        DataSource.Tasks.Remove(newtTask);
        throw new NotImplementedException();
    }

    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(x => x.Id == id);
        throw new NotImplementedException();
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        Task newTask = Read(item.Id);
        if (newTask == null)
        { throw new NotImplementedException(); }
        Delete(item.Id);
        DataSource.Tasks.Add(newTask);
        throw new NotImplementedException();
    }
}
