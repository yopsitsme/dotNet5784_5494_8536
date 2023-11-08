namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        Task newtTask = item with { Id = DataSource.Config.NextTaskId };
        DataSource.Tasks.Add(newtTask);
        return newtTask.Id;
       
    }

    public void Delete(int id)
    {
        Task newTask=Read(id);
        if (newTask == null) 
        { throw new Exception($"Task with ID={newTask.Id} does Not exist"); }
        DataSource.Tasks.Remove(newTask);
       
    }

    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(x => x.Id == id);
      
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
       
    }

    public void Update(Task item)
    {
        Task newTask = Read(item.Id);
        if (newTask == null)
        { throw new Exception($"Task with ID={newTask.Id} does Not exist"); }
        Delete(item.Id);
        DataSource.Tasks.Add(item);
       
    }
}
