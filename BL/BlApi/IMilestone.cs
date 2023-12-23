

namespace BlApi;

public interface IMilestone
{
    public void Create();
    public void Update(BO.Milestone item);
    public BO.Milestone? Read(int id);
}
