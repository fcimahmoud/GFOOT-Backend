
namespace Services.Abstractions.Individual_Services.Abstraction
{
    public interface IRankService
    {
        public Task<int> GetCityRankAsync(string userId);
        public Task<int> GetCountryRankAsync(string userId);
        public Task<int> GetGlobalRankAsync(string userId);
    }
}
