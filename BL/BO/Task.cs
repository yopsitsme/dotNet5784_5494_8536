using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Task
{
    public int Id { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public DateTime? CreatedAtDate { get; set; }
    public BO.Status Status { get; set; }
    public BO.MilestoneInTask ? Milestone {  get; set; }//מכיוון שכשאשר המשימה שלי היא לא אבן דרך אז לא תקבל כאן ערכים
    public DateTime? StartDate { get; set; }
   public DateTime? ScheduledStartDate { get; set; }
   public DateTime? ForeCastDate { get; set; }
   public DateTime? CompleteDate { get; set; }
   public DateTime? DeadLineDate { get; set; }
   public string Deliverables { get; set; }
   public string Remarks { get; set; }
   public BO.EngineerInTask Engineer { get; set; }
    public EngineerExperience‏ ComplexityLevel { get; set; }

  
}

