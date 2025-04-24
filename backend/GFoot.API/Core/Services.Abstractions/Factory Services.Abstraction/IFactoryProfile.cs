using Shared.FactoryModels;
using Shared.FactoryModels.ProfileModels;

namespace Services.Abstractions.Factory_Services.Abstraction
{
    public interface IFactoryProfile
    {
        public Task<FactoryProfileDTO> GetFactoryProfileAsync(string applicationUserId);
        public Task UpdateFactoryProfileAsync(string appUserId, UpdateFactoryProfileDTO profile);
    }
}
