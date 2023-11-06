

namespace DO;

public record Task
{
    int? Id { get; set; } = null;
    string Description { get; set; }
    string Ailas { get; set; }
    bool IsMilestone { get; set; }
    DateTime CreatedAtDete { get; set; }
    DateTime StartDete { get; set; }
    DateTime? ScheduledDete { get; set; } = null;
    DateTime? ForecasDate { get; set; } =  null;
    DateTime DeadLineDate { get; set; }
    DateTime? CompleteDate { get; set; } = null;
    string Deliverables { get; set; }
    string Remarks { get; set; }
    int EngineerId { get; set; }
    /*לא גמור*/
    int ComplexityLevl { get; set; }

    public Task()
    {

    }
    public Task(string Description,string Ailas,bool IsMilestone ,DateTime CreatedAtDete, DateTime StartDete ,DateTime ScheduledDete,DateTime ForecasDate,
    DateTime DeadLineDate,DateTime CompleteDate,string Deliverables,string Remarks,int EngineerId, /*לא גמור*/int ComplexityLevl)
    {
        this.Id = Id;
        this.Description = Description;
        this.Ailas = Ailas;     
        this.IsMilestone = IsMilestone;
        this.CreatedAtDete= CreatedAtDete;
        this.StartDete = StartDete;
        this.ScheduledDete= ScheduledDete;
        this.ForecasDate = ForecasDate;
        this.DeadLineDate = DeadLineDate;
        this.Deliverables = Deliverables; 
        this.Remarks = Remarks;
        this.EngineerId = EngineerId;
        this.CompleteDate = CompleteDate;

    }
}
