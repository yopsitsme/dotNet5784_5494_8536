namespace Dal
{
    using DalApi;
    using DO;
    using System.Collections.Generic;

    //The implementation of the interface of the dependency entity between the tasks
    public class DependencyImplementation : IDependency
    {
        /// <summary>
        /// the method gets the ditails of creates a new dependency by using the cunstroctor
        /// </summary>
        /// <param name="item"></param>
        /// <returns>the new dependencys id</returns>
        public int Create(Dependency item)
        {
            Dependency newDependency = item with { Id = DataSource.Config.NextDependencyId };
            DataSource.Dependencies.Add(newDependency);
            return newDependency.Id;

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
            Dependency ?newDependency = Read(id);
            if (newDependency == null)
            { throw new Exception($"Dependency with ID={id} does Not exist"); }
            DataSource.Dependencies.Remove(newDependency);

        }


        /// <summary>
        /// the method checks if an item with the requierde id exsist and returns it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the item that was found or null if no matching item was found</returns>
        public Dependency? Read(int id)
        {
            return DataSource.Dependencies.Find(x => x.Id == id);
        }

        /// <summary>
        /// the method returns the list of items
        /// </summary>
        public List<Dependency> ReadAll()
        {
            return new List<Dependency>(DataSource.Dependencies);

        }

        /// <summary>
        /// the method checks if an item with such an id exsist in the list
        /// if yes, it deletes it(by calling delete method),
        /// now the method will create a new item wuth the updated detailes
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="Exception"></exception>
        public void Update(Dependency item)
        {
            Dependency ?newDependency = Read(item.Id);
            if (newDependency == null)
            { throw new Exception($"Dependency with ID={item.Id} does Not exist"); }
            Delete(item.Id);
            DataSource.Dependencies.Add(item);

        }
    }
}