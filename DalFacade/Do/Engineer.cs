

namespace DO;

// decleration of the entity engineer
public record Engineer
 (
    int Id,
    string? Name = null,
    string? Email = null,
    EngineerExperience‏? Level=null,
    double? Cost = null


 )
{
    //defult constructor
    public Engineer():this(0) { }

}
