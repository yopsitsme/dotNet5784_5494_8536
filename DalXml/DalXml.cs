


namespace Dal;
using DalApi;
using System.Xml.Linq;


sealed public class DalXml : IDal
{
    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementationcs();

    public ITask Task => new TaskImplementation();
}
