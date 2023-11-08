
namespace DalApi;
using DO;

    public interface IDependency
    {
        int Create(Dependency item); //Creates new dependency object in DAL
        Dependency? Read(int id); //Reads dependency object by its ID
    List<Dependency> ReadAll(); // Reads all dependency objects
    void Update(Dependency item); //Updates dependency object
    void Delete(int id); //Deletes an object by its Id

    }
