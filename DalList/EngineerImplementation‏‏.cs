

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

//The implementation of the interface of the engineers entity
internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        Engineer ?newEngineer = Read(item.Id);
        if (newEngineer != null)
        { throw new DalAlreadyExistsException($"Engineer with ID={newEngineer.Id} does exist"); }
        DataSource.Engineers.Add(item);
        return item.Id;
        
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
        Engineer? newEngineer = Read(id);
        if (newEngineer == null)
        { throw new DalDoesNotExistException($"Engineer with ID={id} does Not exist"); }
        DataSource.Engineers.Remove(newEngineer);
    
    }

    /// <summary>
    /// the method checks if an item with the requierde id exsist and returns it
    /// </summary>
    /// <param name="id"></param>
    /// <returns>the item that was found or null if no matching item was found</returns>
    public Engineer? Read(int id)
    {
        return DataSource.Engineers.Find(x => x.Id == id);
        
    }

    /// <summary>
    /// the method returns the list of items
    /// </summary>
    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
      
    }

    /// <summary>
    /// the method checks if an item with such an id exsist in the list
    /// if yes, it deletes it(by calling delete method),
    /// now the method will create a new item wuth the updated detailes
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Engineer item)
    {
        Engineer ?newEngineer= Read(item.Id);
        if(newEngineer == null) 
        { throw new DalDoesNotExistException($"Engineer with ID={item.Id} does Not exist"); }
        Delete(item.Id);
        DataSource.Engineers.Add(item);
       
    }
}
