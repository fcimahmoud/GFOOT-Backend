
namespace Services.FactoryServices
{
    public class FactoryVisualizationService(IUnitOfWork unitOfWork) : IFactoryVisualizationService
    {
        public async Task<IEnumerable<FactoryActivityEmissionDTO>> GetVisualizedDataMonthlyAsync(string appUserId, bool ascending = true)
        {
            var factoryUser = await unitOfWork.GetRepository<FactoryUser, string>()
               .GetByConditionAsync(u => u.ApplicationUserId == appUserId);

            if (factoryUser == null) throw new Exception("Factory User Not Found");
            var userId = factoryUser.Id;

            var emissions = await unitOfWork.GetRepository<FactoryEmission, string>()
               .GetAllByConditionSortedAsync(u => u.FactoryUserId == userId, o => o.Date, ascending);

            var data = emissions.Select(a => new FactoryActivityEmissionDTO
            {
                Id = a.Id,
                Date = a.Date,
                CarbonEmission = a.CarbonEmission,
            });

            return data;
        }

        public async Task<IEnumerable<FactoryActivityEmissionDTO>> GetVisualizedDataYearlyAsync(string appUserId, bool ascending = true)
        {
            var factoryUser = await unitOfWork.GetRepository<FactoryUser, string>()
               .GetByConditionAsync(u => u.ApplicationUserId == appUserId);

            if (factoryUser == null) throw new Exception("Factory User Not Found");
            var userId = factoryUser.Id;

            var data = await unitOfWork.GetRepository<FactoryEmission, string>()
                                       .GetAllByConditionGroupedSortedAsync
                                           (u => u.FactoryUserId == userId,
                                             g => g.Date.Year,
                                             o => o.Key,
                                             g => new FactoryActivityEmissionDTO
                                             {
                                                 Id = g.Key.ToString(),
                                                 Date = new DateOnly(g.Key, 1, 1),
                                                 CarbonEmission = g.Sum(a => a.CarbonEmission)
                                             },
                                             ascending);

            return data;
        }
    }
}
