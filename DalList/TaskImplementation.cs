namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

//The interface implementation of the tasks entity
public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        Task newtTask = item with { Id = DataSource.Config.NextTaskId };
        DataSource.Tasks.Add(newtTask);
        return newtTask.Id;
       
    }
    /// <summary>
    /// the method checks if an item with such an id exsist.
    /// if it does it will be deleted from the list,
    /// other ways an Exception will be throwen
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        Task? newTask=Read(id);
        if (newTask == null) 
        { throw new Exception($"Task with ID={id} does Not exist"); }
        DataSource.Tasks.Remove(newTask);
       
    }

    /// <summary>
    /// the method checks if an item with the requierde id exsist and returns it
    /// </summary>
    /// <param name="id"></param>
    /// <returns>the item that was found or null if no matching item was found</returns>
    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(x => x.Id == id);
      
    }

    /// <summary>
    /// the method returns the list of items
    /// </summary>
    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
       
    }
    /// <summary>
    /// the method checks if an item with such an id exsist in the list
    /// if yes, it deletes it(by calling delete method),
    /// now the method will create a new item wuth the updated detailes
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Task item)
    {
        Task ?newTask = Read(item.Id);
        if (newTask == null)
        { throw new Exception($"Task with ID={item.Id} does Not exist"); }
        Delete(item.Id);
        DataSource.Tasks.Add(item);
       
    }
}
