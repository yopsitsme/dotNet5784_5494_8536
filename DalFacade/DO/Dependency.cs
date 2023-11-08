

namespace DO;

public record Dependency
{
   

    public int Id { get; set; }
    public int DependentTask1 {  get; set; }
    public int DependsTask2 {  get; set; }

    public Dependency() { }
    public Dependency( int DependentTask, int DependsTask) 
    { 
        
        this.DependentTask1 = DependentTask;     
        this.DependsTask2 = DependsTask;
    }

}
