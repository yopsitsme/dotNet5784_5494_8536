using BlApi;
using BO;

namespace BlImplementation;

// Business logic implementation class that implements the IBl interface.
internal class Bl : IBl
{
    // Property to access the engineer-related functionality in the system.
    public IEngineer Engineer => new EngineerImplementation();

    // Property to access the milestone-related functionality in the system.
    public IMilestone Milestone => new MilestoneImplementation();

    // Property to access the task-related functionality in the system.
    public ITask Task => new TaskImplementation();
}
