

namespace DO;

// decleration of the entity dependency
public record Dependency

    (
     int Id,
     int DependentTask1,
     int DependsTask2
    )
{
    //defult constructor
    public Dependency():this(0,0,0) { }
}
