

namespace DO;

// decleration of the entity task
public record Task
{
    public int Id { get; set; } 
    public string Description { get; set; }
    public string Ailas { get; set; }
    public bool IsMilestone { get; set; }
    public DateTime CreatedAtDete { get; set; }
    public DateTime StartDete { get; set; }
    public DateTime? ScheduledDete { get; set; } = null;
    public DateTime? ForecasDate { get; set; } = null;
    public DateTime DeadLineDate { get; set; }
    public DateTime? CompleteDate { get; set; } = null;
    public string Deliverables { get; set; }
    public string? Remarks { get; set; }
    public int? EngineerId { get; set; }
    public EngineerExperience‏ ComplexityLevl { get; set; }

    //defult constructor
    public Task() { }

    //prametrise constructor 1
    public Task(int Id, string Description, string Ailas, bool IsMilestone, DateTime CreatedAtDete, DateTime StartDete, DateTime? ScheduledDete, DateTime? ForecasDate,
   DateTime DeadLineDate, DateTime? CompleteDate, string Deliverables, string Remarks, int? EngineerId, EngineerExperience‏ ComplexityLevl)
    {
        this.Id = Id;
        this.Description = Description;
        this.Ailas = Ailas;
        this.IsMilestone = IsMilestone;
        this.CreatedAtDete = CreatedAtDete;
        this.StartDete = StartDete;
        this.ScheduledDete = ScheduledDete;
        this.ForecasDate = ForecasDate;
        this.DeadLineDate = DeadLineDate;
        this.Deliverables = Deliverables;
        this.Remarks = Remarks;
        this.EngineerId = EngineerId;
        this.CompleteDate = CompleteDate;

    }


    //prametrise constructor 2
    public Task(string description, string alias, bool isMilestone, DateTime start, DateTime dedline, string deliverables, EngineerExperience complexityLevl)
    {
        this.Id = 0;
        this.Description = Description;
        this.Ailas = Ailas;
        this.IsMilestone = IsMilestone;
        this.CreatedAtDete = CreatedAtDete;
        this.DeadLineDate = DeadLineDate;
        this.Deliverables = Deliverables;
        this.Remarks = Remarks;
        this.ComplexityLevl = ComplexityLevl;
    }


}
