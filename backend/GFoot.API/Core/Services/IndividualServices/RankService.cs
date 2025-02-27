
namespace Services.IndividualServices
{
    internal class RankService (
        IUnitOfWork unitOfWork
        ): IRankService
    {
        public async Task<int> GetCityRankAsync(string appUserId)
        {
            var individualUser = await unitOfWork.GetRepository<IndividualUser, string>().GetByConditionAsync(user => user.ApplicationUserId == appUserId);

            var user = await unitOfWork.GetRepository<IndividualUser, string>()
                .GetWithIncludesAsync(u => u.Id == individualUser!.Id, u => u.ApplicationUser!);
            if (user?.ApplicationUser?.City == null) return -1;

            var latestActivity = await GetLatestActivityAsync(individualUser!.Id);
            if (latestActivity == null) return -1;

            var cityUsers = await unitOfWork.GetRepository<IndividualUser, string>()
                .GetAllWithIncludesAsync(
                u => u.ApplicationUser!.City == user.ApplicationUser.City,
                u => u.ApplicationUser!
            );

            var cityUserIds = cityUsers.Select(u => u.Id).ToList();
            var cityUsersLatestEmissions = await GetAllUsersLatestEmissionsAsync(cityUserIds);

            var rank = cityUsersLatestEmissions.OrderBy(e => e.CarbonEmission)
                                               .Select((e, index) => new { e.UserId, Rank = index + 1 })
                                               .FirstOrDefault(e => e.UserId == individualUser!.Id);

            return rank?.Rank ?? -1;
        }

        public async Task<int> GetCountryRankAsync(string appUserId)
        {
            var individualUser = await unitOfWork.GetRepository<IndividualUser, string>().GetByConditionAsync(user => user.ApplicationUserId == appUserId);

            var user = await unitOfWork.GetRepository<IndividualUser, string>()
                .GetWithIncludesAsync(u => u.Id == individualUser!.Id, u => u.ApplicationUser!);
            if (user?.ApplicationUser?.Country == null) return -1;

            var latestActivity = await GetLatestActivityAsync(individualUser!.Id);
            if (latestActivity == null) return -1;

            var cityUsers = await unitOfWork.GetRepository<IndividualUser, string>()
                .GetAllWithIncludesAsync(
                u => u.ApplicationUser!.Country == user.ApplicationUser.Country,
                u => u.ApplicationUser!
            );

            var countryUserIds = cityUsers.Select(u => u.Id).ToList();
            var countryUsersLatestEmissions = await GetAllUsersLatestEmissionsAsync(countryUserIds);

            var rank = countryUsersLatestEmissions.OrderBy(e => e.CarbonEmission)
                                               .Select((e, index) => new { e.UserId, Rank = index + 1 })
                                               .FirstOrDefault(e => e.UserId == individualUser!.Id);

            return rank?.Rank ?? -1;
        }

        public async Task<int> GetGlobalRankAsync(string appUserId)
        {
            var individualUser = await unitOfWork.GetRepository<IndividualUser, string>().GetByConditionAsync(user => user.ApplicationUserId == appUserId);

            var user = await unitOfWork.GetRepository<IndividualUser, string>()
                .GetWithIncludesAsync(u => u.Id == individualUser!.Id, u => u.ApplicationUser!);

            var latestActivity = await GetLatestActivityAsync(individualUser!.Id);
            if (latestActivity == null) return -1;

            var allUsersLatestEmissions = await GetAllUsersLatestEmissionsAsync();

            var rank = allUsersLatestEmissions.OrderBy(e => e.CarbonEmission)
                                               .Select((e, index) => new { e.UserId, Rank = index + 1 })
                                               .FirstOrDefault(e => e.UserId == individualUser!.Id);

            return rank?.Rank ?? -1;
        }

        private async Task<IEnumerable<(string UserId, float CarbonEmission)>> GetAllUsersLatestEmissionsAsync(List<string>? userIds = null)
        {
            var allActivities = userIds == null
                ? await unitOfWork.GetRepository<Activity, string>().GetAllAsync()
                : await unitOfWork.GetRepository<Activity, string>().GetAllByConditionAsync(a => userIds.Contains(a.UserId!));

            return allActivities.GroupBy(a => a.UserId)
                                .Select(g => g.OrderByDescending(a => a.Date).First())
                                .Select(a => (a.UserId, a.CarbonEmission))!;
        }
        private async Task<Activity?> GetLatestActivityAsync(string userId)
        {
            var userActivities = await unitOfWork
                .GetRepository<Activity, string>()
                .GetAllByConditionAsync(a => a.UserId == userId);

            return userActivities
                .OrderByDescending(a => a.Date)
                .FirstOrDefault();
        }
    }
}
