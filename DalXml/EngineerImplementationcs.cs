using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class EngineerImplementationcs : IEngineer
{
    public int Create(Engineer item)
    {
        List<Engineer> xmlEngineer = XMLTools.LoadListFromXMLSerializer<Engineer>("engineer");
        Engineer? newEngineer = Read(item.Id);
        if (newEngineer != null)
        { throw new DalAlreadyExistsException($"Engineer with ID={newEngineer.Id} does exist"); }
        xmlEngineer.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(xmlEngineer, "engineer");

        return item.Id;
    }
    /// <summary>
    /// the method checks if an item with such an id exsist.
    /// if it does it will be deleted from the xml,
    /// other ways an Exception will be throwen
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        List<Engineer> xmlEngineer = XMLTools.LoadListFromXMLSerializer<Engineer>("engineer");
        Engineer? newEngineer = Read(id);
        if (newEngineer == null)
        { throw new DalDoesNotExistException($"Engineer with ID={id} does Not exist"); }
        xmlEngineer.Remove(newEngineer);
        XMLTools.SaveListToXMLSerializer<Engineer>(xmlEngineer, "engineer");
    }

    public Engineer? Read(int id)
    {
        List<Engineer> xmlEngineer = XMLTools.LoadListFromXMLSerializer<Engineer>("engineer");
        Engineer? engineer = xmlEngineer.FirstOrDefault(Engineer=> Engineer.Id== id);
        return engineer != null ? engineer : null;
    }

    /// <summary>
    /// a method that gets a condition and returns the first item who matches the condition 
    /// if ther is no matching item
    /// </summary>
    /// <param name="filter">a function with a condtion</param>
    /// <returns> the first item who matches the condition</returns>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer> xmlEngineer = XMLTools.LoadListFromXMLSerializer<Engineer>("engineer");
        Engineer? engineer = xmlEngineer.FirstOrDefault(Engineer=> filter(Engineer));
        return engineer != null ? engineer : null;
    }
    /// <summary>
    /// the method returns the list of items We met the condition
    /// </summary>

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer> xmlEngineer = XMLTools.LoadListFromXMLSerializer<Engineer>("engineer");

        if (filter != null)
        {
            return from item in xmlEngineer
                   where filter(item)
                   select item;
        }
        return from item in xmlEngineer
               select item;
    }

    public void Update(Engineer item)
    {
        List<Engineer> xmlEngineer = XMLTools.LoadListFromXMLSerializer<Engineer>("engineer");
        Engineer? newEngineer = Read(item.Id);
        if (newEngineer == null)
        { throw new DalDoesNotExistException($"Engineer with ID={item.Id} does Not exist"); }
        Delete(item.Id);
        xmlEngineer.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(xmlEngineer, "engineer");

    }
}
