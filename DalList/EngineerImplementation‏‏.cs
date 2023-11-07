

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        Engineer newEngineer = Read(item.Id);
        if (newEngineer == null)
        { throw new Exception($"Engineer with ID={newEngineer.Id} does exist"); }
        DataSource.Engineers.Add(item);
        return newEngineer.Id;
        
    }

    public void Delete(int id)
    {
        Engineer newEngineer = Read(id);
        if (newEngineer == null)
        { throw new Exception($"Engineer with ID={newEngineer.Id} does Not exist"); }
        DataSource.Engineers.Remove(newEngineer);
    
    }

    public Engineer? Read(int id)
    {
        return DataSource.Engineers.Find(x => x.Id == id);
        
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
      
    }

    public void Update(Engineer item)
    {
        Engineer newEngineer= Read(item.Id);
        if(newEngineer == null) 
        { throw new Exception($"Engineer with ID={newEngineer.Id} does Not exist"); }
        Delete(item.Id);
        DataSource.Engineers.Add(newEngineer);
       
    }
}
