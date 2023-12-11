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
    public string? Ailas { get; set; }
    public DateTime? CreatedAtDete { get; set; }
    public BO.Status Status { get; set; }
    public BO.MilestoneInTask Milestone {  get; set; }
    public DateTime BaseLineStartDate { get; set; }
    public DateTime StartDate { get; set; }
   public DateTime ScheduledStartDate { get; set; }
   public DateTime ForecastDate { get; set; }
   public DateTime CompleteDate { get; set; }
   public DateTime DeadLineDate { get; set; }
   public string Deliverables { get; set; }
   public string Remarks { get; set; }
   public BO.EngineerInTask Engineer { get; set; }
    public EngineerExperience‏ ComplexityLevl { get; set; }
 
}

