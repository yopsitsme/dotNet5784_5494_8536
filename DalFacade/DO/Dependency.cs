

namespace DO;

public record Dependency
{
    private int idTask1;
    private int idTask2;

    public int Id { get; set; }
    public int DependentTask {  get; set; }
    public int DependsTask {  get; set; }

    public Dependency() { }
    public Dependency( int DependentTask, int DependsTask) 
    { 
        
        this.DependentTask = DependentTask;     
        this.DependentTask = DependsTask;
    }

    public Dependency(int idTask1, int idTask2)
    {
        this.idTask1 = idTask1;
        this.idTask2 = idTask2;
    }
}
