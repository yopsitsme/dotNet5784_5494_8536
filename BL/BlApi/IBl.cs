using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IBl
{
    public IEngineer Engineer { get; }  
    public IMilestone Milestone { get; }
    public ITask Task { get; }
    public IEngineerInTask EngineerInTask {  get; }
    public IMilestoneInList MilestoneInList {  get; }
    public IMilestoneInTask MilestoneInTask {  get; }
    public ITaskInList TaskInList {  get; }
}
