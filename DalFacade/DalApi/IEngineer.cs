
namespace DalApi;
using DO;
    public interface IEngineer
    {
    int Create(Engineer item); //Creates new engineer object in DAL
    Engineer? Read(int id); //Reads engineer object by its ID
    List<Engineer> ReadAll(); //Reads all engineer objects
    void Update(Engineer item); //Updates engineer object
    void Delete(int id); //Deletes an object by its Id

}

