namespace Dal;
using DalApi;
using DO;


internal class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        List<Task> xmlTask = XMLTools.LoadListFromXMLSerializer<Task>("task");
        Task newTask = item with { Id = Config.nextTaskId };
        xmlTask.Add(newTask);
        XMLTools.SaveListToXMLSerializer<Task>(xmlTask, "task");
        return newTask.Id;
    }

    public void Delete(int id)
    {
        List<Task> xmlTask = XMLTools.LoadListFromXMLSerializer<Task>("task");
        Task? newTask = Read(id);
        if (newTask == null)
        { throw new DalDoesNotExistException($"Task with ID={id} does Not exist"); }
        xmlTask.Remove(newTask);
        XMLTools.SaveListToXMLSerializer<Task>(xmlTask, "task");
    }

    public Task? Read(int id)
    {
        List<Task> xmlTask = XMLTools.LoadListFromXMLSerializer<Task>("task");
        Task? task = (from item in xmlTask
                      where item.Id == id
                      select item).FirstOrDefault();
        return task != null ? task : null;
    }

    public Task? Read(Func<Task, bool> filter)
    {
        List<Task> xmlTask = XMLTools.LoadListFromXMLSerializer<Task>("task");
        Task? task = (from item in xmlTask
                      where filter(item)
                      select item).FirstOrDefault();
        return task != null ? task : null;
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        List<Task> xmlTask = XMLTools.LoadListFromXMLSerializer<Task>("task");
        if (filter != null)
        {
            return from item in xmlTask
                   where filter(item)
                   select item;
        }
        return from item in xmlTask
               select item;
    }

    public void Update(Task item)
    {
        List<Task> xmlTask = XMLTools.LoadListFromXMLSerializer<Task>("task");
        Task? newTask = Read(item.Id);
        if (newTask == null)
        { throw new DalDoesNotExistException($"Task with ID={item.Id} does Not exist"); }
        Delete(item.Id);
        xmlTask.Add(item);
        XMLTools.SaveListToXMLSerializer<Task>(xmlTask, "task");
    }
}
