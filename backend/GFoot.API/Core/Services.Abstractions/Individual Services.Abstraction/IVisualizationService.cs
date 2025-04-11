global using Shared.IndividualModels;

namespace Services.Abstractions.Individual_Services.Abstraction
{
    public interface IVisualizationService
    {
        public Task<IEnumerable<ActivityEmissionDTO>> GetVisualizedDataDailyAsync(string appUserId, bool ascending = true);
        public Task<IEnumerable<ActivityEmissionDTO>> GetVisualizedDataMonthlyAsync(string appUserId, bool ascending = true);
        public Task<IEnumerable<ActivityEmissionDTO>> GetVisualizedDataYearlyAsync(string appUserId, bool ascending = true);
    }
}
