

namespace DO;

public record Task
{
<<<<<<< HEAD
    public int Id { get; set; }
    string Description { get; set; }
    string Ailas { get; set; }
    bool IsMilestone { get; set; }
    DateTime CreatedAtDete { get; set; }
    DateTime StartDete { get; set; }
    DateTime? ScheduledDete { get; set; } = null;
    DateTime? ForecasDate { get; set; } = null;
    DateTime DeadLineDate { get; set; }
    DateTime? CompleteDate { get; set; } = null;
    string Deliverables { get; set; }
    string Remarks { get; set; }
    EngineerExperience‏ EngineerId { get; set; }
    int ComplexityLevl { get; set; }
=======
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
    /*לא גמור*/
    public EngineerExperience‏ ComplexityLevl { get; set; }
>>>>>>> e4bca0acc3730ef202b902a5c3157bf0662dff91

    public Task()
    {

    }
    public Task(string Description, string Ailas, bool IsMilestone, DateTime CreatedAtDete, DateTime StartDete, DateTime ScheduledDete, DateTime ForecasDate,
    DateTime DeadLineDate, DateTime CompleteDate, string Deliverables, string Remarks, int EngineerId, /*לא גמור*/int ComplexityLevl)
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
