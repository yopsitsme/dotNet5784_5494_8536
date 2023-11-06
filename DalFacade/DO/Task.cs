

namespace DO;

 public record Task
{
    int Id;
    string Description;
    string Ailas;
    bool IsMilestone;
    DateTime CreatedAtDete;
    DateTime StartDete;
    DateTime ScheduledDete;
    DateTime ForecasDate;
    DateTime DeadLineDate;
    DateTime CompleteDate;
    string Deliverables;
    string Remarks;
    int EngineerId;
    /*לא גמור*/
    int ComplexityLevl;

    public Task()
    {

    }
    public Task(int Id,string Description,string Ailas,bool IsMilestone ,DateTime CreatedAtDete, DateTime StartDete ,DateTime ScheduledDete,DateTime ForecasDate,
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
