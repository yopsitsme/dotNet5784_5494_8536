

namespace DO;

// decleration of the entity task
public record Task
    (
    int Id = 0,
    DateTime StartDate = new DateTime(),
    DateTime ScheduledDate = new DateTime(),
    DateTime ForecasDate = new DateTime(),
    DateTime CompleteDate = new DateTime(),
    DateTime DeadLineDate = new DateTime(),
    string Deliverables = "",
    string Remarks = "",
    int EngineerId = 0,
    EngineerExperience‏ ComplexityLevel = EngineerExperience.Novice,
    string? Description = null,
    string? Ailas = null,
    bool? IsMilestone = null,
    DateTime? CreatedAtDate = null

    )


{

    //defult constructor
    public Task() : this(0, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, "", "", 0, EngineerExperience.Novice) { }
    public Task(string description, string alias, bool isMilestone, DateTime start, DateTime deadline, string deliverables, EngineerExperience‏ complexityLevel) : this()
    {
        this.Ailas = alias;
        this.Description = description;
        this.IsMilestone = isMilestone;
        this.StartDate = start;
        this.DeadLineDate = deadline;
        this.Deliverables = deliverables;
        this.ComplexityLevel = complexityLevel;
    }
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
