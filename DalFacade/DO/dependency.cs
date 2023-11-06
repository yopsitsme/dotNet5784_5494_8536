

namespace DO;

public record Dependency
{
    int Id { get; set; }
    int DependentTask {  get; set; }
    int DependsTask {  get; set; }

    public Dependency() { }
    public Dependency(int Id, int DependentTask, int DependsTask) 
    { 
        this.Id = Id;   
        this.DependentTask = DependentTask;     
        this.DependentTask = DependsTask;
    }



}
