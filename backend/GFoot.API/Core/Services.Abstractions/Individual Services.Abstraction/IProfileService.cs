
using Shared.IndividualModels.ProfileModels;

namespace Services.Abstractions.Individual_Services.Abstraction
{
    public interface IProfileService
    {
        Task<ProfileUserDto> GetProfileByIdAsync(string appUserId);
        Task UpdateProfileAsync(string appUserId, UpdateProfileUserDto profile);
    }
}
