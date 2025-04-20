using Shared.FactoryModels;
using Shared.IndividualModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.Factory_Services.Abstraction
{
    public interface IFactoryRecommendationService
    {
        public Task CreateRecommendationAsync(string userId, FactoryEmissionDTO factoryEmissionDTO, float emission);
        public Task<IEnumerable<FactoryRecommendationDTO>> GetAllRecommendationsAsync(string userId);
        public Task<FactoryRecommendationDTO> GetRecommendationAsync(string recId);
    }
}
