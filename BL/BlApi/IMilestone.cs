

namespace BlApi;

public interface IMilestone
{
    public void Create();
    public BO.Milestone? Read(int id);
    public BO.Milestone Update(int id, string alias, string description, string? remarks);
}
