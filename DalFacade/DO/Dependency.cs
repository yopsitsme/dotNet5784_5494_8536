

namespace DO;

// decleration of the entity dependency
public record Dependency
{

    public int Id { get; set; }
    public int DependentTask1 {  get; set; }
    public int DependsTask2 {  get; set; }

    //defult constructor
    public Dependency() { }

    //prametrise constructor 1
    public Dependency( int DependentTask, int DependsTask) 
    { 
        
        this.DependentTask1 = DependentTask;     
        this.DependsTask2 = DependsTask;
    }

    //prametrise constructor 1
    public Dependency(int ID,int DependentTask, int DependsTask)
    {
        this.Id= ID;
        this.DependentTask1 = DependentTask;
        this.DependsTask2 = DependsTask;
    }

}
