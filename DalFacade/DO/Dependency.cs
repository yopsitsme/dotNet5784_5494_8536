

namespace DO;

public record Dependency
{
   public int Id { get; set; }
    public int? DependentTask { get; set; } = null;
    public int? DependsTask { get; set; } = null;

    public Dependency() { }
    public Dependency(int Id, int DependentTask, int DependsTask) 
    { 
        this.Id = Id;   
        this.DependentTask = DependentTask;     
        this.DependentTask = DependsTask;
    }



}
