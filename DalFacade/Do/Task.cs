

namespace DO;

public record Task
{
 public  int Id { get; set; } 
    public string Description { get; set; }
    public string Ailas { get; set; }
    public bool IsMilestone { get; set; }
    public DateTime CreatedAtDete { get; set; }
    public DateTime StartDete { get; set; }
    public DateTime? ScheduledDete { get; set; } = null;
    public DateTime? ForecasDate { get; set; } =  null;
    public DateTime DeadLineDate { get; set; }
    public DateTime? CompleteDate { get; set; } = null;
    public string Deliverables { get; set; }
    public string Remarks { get; set; }
    public int EngineerId { get; set; }
    public EngineerExperience‏ ComplexityLevl { get; set; }
    public Task()
    {

    }
    public Task(int Id,string Description, string Ailas, bool IsMilestone, DateTime CreatedAtDete, DateTime StartDete, DateTime ScheduledDete, DateTime ForecasDate,
    DateTime DeadLineDate, DateTime CompleteDate, string Deliverables, string Remarks, int EngineerId, EngineerExperience‏ ComplexityLevl)
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
}
