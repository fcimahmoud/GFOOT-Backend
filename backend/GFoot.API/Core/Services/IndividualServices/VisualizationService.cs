

namespace Services.IndividualServices
{
    internal class VisualizationService(IUnitOfWork unitOfWork) : IVisualizationService
    {
        public async Task<IEnumerable<ActivityEmissionDTO>> GetVisualizedDataDailyAsync(string appUserId, bool ascending = true)
        {
            var individualUser = await unitOfWork.GetRepository<IndividualUser, string>()
                .GetByConditionAsync(u => u.ApplicationUserId == appUserId);
            if (individualUser == null) throw new NotFoundException("Individual User Not Found");
            var userId = individualUser.Id;

            var userActivities = await unitOfWork.GetRepository<Activity, string>()
                .GetAllByConditionSortedAsync(a => a.UserId == userId, o => o.Date, ascending);

            var data = userActivities.Select(a => new ActivityEmissionDTO
            {
                Date = a.Date,
                CarbonEmission = a.CarbonEmission,
            });

            return data;
        }

        public async Task<IEnumerable<ActivityEmissionDTO>> GetVisualizedDataMonthlyAsync(string appUserId, bool ascending = true)
        {
            var individualUser = await unitOfWork.GetRepository<IndividualUser, string>()
                .GetByConditionAsync(u => u.ApplicationUserId == appUserId);
            if (individualUser == null) throw new NotFoundException("Individual User Not Found");
            var userId = individualUser.Id;

            var data = await unitOfWork.GetRepository<Activity, string>()
                                       .GetAllByConditionGroupedSortedAsync
                                          (u => u.UserId == userId,
                                             g => new { g.Date.Year, g.Date.Month },
                                             o => o.Key.Year * 100 + o.Key.Month
                                             /*o => new { o.Key.Year , o.Key.Month}*/,
                                             g => new ActivityEmissionDTO
                                             {
                                                 Date = new DateOnly(g.Key.Year, g.Key.Month, 1),
                                                 CarbonEmission = g.Average(a => a.CarbonEmission)
                                             },
                                             ascending);

            return data;
        }

        public async Task<IEnumerable<ActivityEmissionDTO>> GetVisualizedDataYearlyAsync(string appUserId, bool ascending = true)
        {
            var individualUser = await unitOfWork.GetRepository<IndividualUser, string>()
                .GetByConditionAsync(u => u.ApplicationUserId == appUserId);
            if (individualUser == null) throw new NotFoundException("Individual User Not Found");
            var userId = individualUser.Id;

            var data = await unitOfWork.GetRepository<Activity, string>()
                                       .GetAllByConditionGroupedSortedAsync
                                           (u => u.UserId == userId,
                                             g => g.Date.Year,
                                             o => o.Key,
                                             g => new ActivityEmissionDTO
                                             {
                                                 Date = new DateOnly(g.Key, 1, 1),
                                                 CarbonEmission = g.Average(a => a.CarbonEmission)
                                             },
                                             ascending);

            return data;
        }
    }
}
