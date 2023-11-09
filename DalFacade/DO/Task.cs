

namespace DO;

// decleration of the entity task
public record Task
    (
    int ID,
    DateTime StartDete,
    DateTime ScheduledDete,
    DateTime ForecasDate,
    DateTime CompleteDte,
    DateTime DeadLineDate,
    string Deliverables,
    string Remarks,
    int EngineerId,
    EngineerExperience‏ ComplexityLevl,
    string? Description=null,
    string? Ailas = null,
    bool? IsMilestone = null,
    DateTime? CreatedAtDete = null

    )


{ 

    //defult constructor
    public Task():this(0,DateTime.Now,DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now,"","",0, EngineerExperience.Novice) { }

}
