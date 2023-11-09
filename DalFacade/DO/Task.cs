

using System.Numerics;
using System.Threading.Tasks;

namespace DO;

// decleration of the entity task
public record Task
    (
    int Id=0,
    DateTime StartDete = new DateTime() ,
    DateTime ScheduledDete= new DateTime(),
    DateTime ForecasDate= new DateTime(),
    DateTime CompleteDate = new DateTime(),
    DateTime DeadLineDate = new DateTime(),
    string Deliverables="",
    string Remarks = "",
    int EngineerId = 0,
    EngineerExperience‏ ComplexityLevl= EngineerExperience.Novice,
    string? Description=null,
    string? Ailas = null,
    bool? IsMilestone = null,
    DateTime? CreatedAtDete = null

    )


{
  

    //defult constructor
    public Task() : this(0, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, "", "", 0, EngineerExperience.Novice) { }
    public Task(string description, string alias, bool isMilestone, DateTime start, DateTime deadline, string deliverables, EngineerExperience‏ complexityLevel) : this()
    {
        this.Ailas = alias;
        this.Description = description;
        this.IsMilestone = isMilestone;
        this.StartDete = start;
        this.DeadLineDate = deadline;
        this.Deliverables = deliverables;
        this.ComplexityLevl = complexityLevel;
    }


}
