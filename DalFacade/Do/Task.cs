

namespace DO;

// decleration of the entity task
public record Task
    (
    int Id,
    string Alias,
    string Description,
    DateTime CreatedAtDate,
    TimeSpan? RequierdEffortTime = null,
    bool IsMilestone = false,
    DateTime? StartDate = null,
    DateTime? ScheduledDate = null,
    DateTime? DeadLineDate = null,
    DateTime? CompleteDate = null,
    string? Deliverables = null,
    string? Remarks = null,
    int? EngineerId = null,
    EngineerExperience‏? ComplexityLevel = EngineerExperience.Novice

    )


{

    //defult constructor
    // public Task():this(0," ","",DateTime.Now) { }

    //public Task() : this(0, "", "", DateTime.Now, null, false, null, null, null, null, null, null, null, EngineerExperience.Novice) { }

    //public Task(string description, string alias, bool isMilestone, DateTime start, DateTime deadline, string deliverables, EngineerExperience‏ complexityLevel) : this()
    //{
    //    this.Alias = alias;
    //    this.Description = description;
    //    this.IsMilestone = isMilestone;
    //    this.StartDate = start;
    //    this.DeadLineDate = deadline;
    //    this.Deliverables = deliverables;
    //    this.ComplexityLevel = complexityLevel;
    //}
    //public Task(int Id, DateTime StartDate, DateTime ScheduledDate,
    //            DateTime CompleteDate,
    //            DateTime ForecasDate,
    //            DateTime DeadLineDate,
    //            string Deliverables,
    //            string Remarks,
    //            int EngineerId,
    //            EngineerExperience‏ ComplexityLevel,
    //            string? Description,
    //            string? Ailas,
    //            bool? IsMilestone,
    //            DateTime? CreatedAtDate):this() 
    //{
    //    this.Id = Id;
    //    this.StartDate = StartDate;
    //    this.ScheduledDate = ScheduledDate;
    //    this.ForecasDate = ForecasDate;
    //    this.CompleteDate = CompleteDate;
    //    this.DeadLineDate = DeadLineDate;
    //    this.Deliverables = Deliverables;
    //    this.ComplexityLevel = ComplexityLevel; 
    //    this.Remarks = Remarks;
    // this.EngineerId = EngineerId;  

    //    this.Description = Description;
    //    this.Ailas = Ailas;
    //    this.IsMilestone = IsMilestone;
    //this.CreatedAtDate = CreatedAtDate;
    //}
}
