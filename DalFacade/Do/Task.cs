

namespace DO;

public record Task
{
    public  int Id { get; set; } 
    public string Description { get; set; }
    public string Ailas { get; set; }
    public bool IsMilestone { get; set; }
    public DateTime CreatedAtDete { get; set; }
    public DateTime? StartDete { get; set; } = null;
    public DateTime? ScheduledDete { get; set; } = null;
    public DateTime? ForecasDate { get; set; } =  null;
    public DateTime DeadLineDate { get; set; }
    public DateTime? CompleteDate { get; set; } = null;
    public string Deliverables { get; set; }
    public string? Remarks { get; set; } = null;
    public int? EngineerId { get; set; } = null;
    public EngineerExperience ComplexityLevl { get; set; }
    public Task()
    {

    }
    public Task(string Description, string Ailas, bool IsMilestone, DateTime CreatedAtDete,
    DateTime DeadLineDate,  string Deliverables,  EngineerExperience‏ ComplexityLevl)
    {
     
        this.Description = Description;
        this.Ailas = Ailas;
        this.IsMilestone = IsMilestone;
        this.CreatedAtDete = CreatedAtDete;
        this.DeadLineDate = DeadLineDate;
        this.Deliverables = Deliverables;
        this.Remarks = Remarks;
        this.ComplexityLevl=ComplexityLevl;



    }
}
