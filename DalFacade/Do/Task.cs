

namespace DO;

// decleration of the entity task
public record Task
    (
    int ID=0,
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
    private DateTime start;
    private DateTime dedline;
    private int idTask;
    private string? v1;
    private string? v2;
    private bool? v3;
    private DateTime dateTime1;
    private DateTime dateTime2;
    private DateTime dateTime3;
    private DateTime dateTime4;
    private DateTime dateTime5;
    private string v4;
    private string v5;
    private int v6;
    private EngineerExperience engineerExperience;

    //defult constructor
    public Task() :this(0,DateTime.Now,DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now,"","",0, EngineerExperience.Novice) { }

}
