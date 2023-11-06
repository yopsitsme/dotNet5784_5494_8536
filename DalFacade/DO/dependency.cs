

namespace DO;

public record dependency
{
    int Id { get; set; }
    int DependentTask {  get; set; }
    int DependsTask {  get; set; }

    public dependency() { }
    public dependency(int Id, int DependentTask, int DependsTask) 
    { 
        this.Id = Id;   
        this.DependentTask = DependentTask;     
        this.DependentTask = DependsTask;
    }



}
