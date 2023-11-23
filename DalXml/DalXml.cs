


namespace Dal;
using DalApi;


sealed public class DalXml : IDal
{
    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementationcs();

    public ITask Task => new TaskImplementation();
}
