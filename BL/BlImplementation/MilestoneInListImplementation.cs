using BO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace BlImplementation
{
    public class MilestoneInListImplementation : BlApi.IMilestoneInList
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;

        public IEnumerable<MilestoneInList> ReadAll(Func<MilestoneInList, bool>? filter = null)
        {
            var milestoneInLists = (from DO.Task doTask in _dal.Task.ReadAll()
                                select Tools.fromDoTaskToMilestonInList(doTask));
            if (filter != null)
            {
                return milestoneInLists.Where(filter);
            }
            else
            {
                return milestoneInLists;
            }
        }
    }
}
