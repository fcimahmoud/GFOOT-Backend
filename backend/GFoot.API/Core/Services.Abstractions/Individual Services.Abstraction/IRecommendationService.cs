
using Shared.IndividualModels;

namespace Services.Abstractions.Individual_Services.Abstraction
{
    public interface IRecommendationService
    {
        public Task CreateRecommendationAsync(string userId, ActivityDTO activityDTO, float emission);
        public Task<IEnumerable<RecommendationDTO>> GetAllRecommendationsAsync(string userId);
        public Task<RecommendationDTO> GetRecommendationAsync(string recId);
    }
}
