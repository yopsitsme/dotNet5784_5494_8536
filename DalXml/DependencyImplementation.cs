﻿

using DalApi;
using DO;

namespace Dal;

internal class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        List <Dependency> xmlDependency = XMLTools.LoadListFromXMLSerializer<Dependency>("dependency");
        Dependency newDependency = item with { Id = Config.nextDependencyId };
        xmlDependency.Add(newDependency);
        XMLTools.SaveListToXMLSerializer<Dependency>(xmlDependency, "dependency");
        return newDependency.Id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
