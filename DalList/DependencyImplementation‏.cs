namespace Dal
{
    using DalApi;
    using DO;
    using System.Collections.Generic;

    public class DependencyImplementation : IDependency‏
    {
        public int Create(Dependency item)
        {
            Dependency newDependency = item with { Id = DataSource.Config.NextDependencyId };‏
            DataSource.Dependencies.Add(newDependency);
            return newDependency.Id;
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            Dependency newDependency = Read(id);
            if (newDependency == null)
            { throw new Exception($"Dependency with ID={newDependency.Id} does Not exist"); }
            DataSource.Dependencies.Remove(newDependency);
            throw new NotImplementedException();
        }

        public Dependency? Read(int id)
        {
            return DataSource.Dependencies.Find(x => x.Id == id);
            throw new NotImplementedException();
        }

        public List<Dependency> ReadAll()
        {
            return new List<Dependency>(DataSource.Dependencies);
            throw new NotImplementedException();
        }

        public void Update(Dependency item)
        {
            Dependency newDependency = Read(item.Id);
            if (newDependency == null)
            { throw new Exception($"Dependency with ID={newDependency.Id} does Not exist"); }
            Delete(item.Id);
            DataSource.Dependencies.Add(newDependency);
            throw new NotImplementedException();
        }
    }
}