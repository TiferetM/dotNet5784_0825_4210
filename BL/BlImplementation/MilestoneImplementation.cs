using BlApi;
using BO;
using DalApi;

namespace BlImplementation
{
    internal class MilestoneImplemantaion : IMilestone
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;

        public Engineer MilestoneDetailsRequest(int id)
        {
           throw new NotImplementedException();
        }

        public void UpdateMilestoneData(Milestone mileStone)
        {
            throw new NotImplementedException();
        }
    }
}
