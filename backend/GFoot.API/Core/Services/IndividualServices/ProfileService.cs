
using AutoMapper;
using Domain.Contracts;
using Shared.IndividualModels.ProfileModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services.IndividualServices
{
    internal class ProfileService (IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        : IProfileService
    {
        public async Task<ProfileUserDto> GetProfileByIdAsync(string appUserId)
        {
            var individualUser = await unitOfWork.GetRepository<IndividualUser, string>()
           .GetWithIncludesAsync(user => user.ApplicationUserId == appUserId, f => f.ApplicationUser!);

            if (individualUser == null) throw new Exception("Individual User Not found");
            var profile = new ProfileUserDto
            {
                Id = individualUser.Id,
                Email = individualUser.ApplicationUser?.Email! ?? "",
                UserName = individualUser.ApplicationUser?.UserName ?? "",
                DisplayName = individualUser.ApplicationUser?.DisplayName ?? "",
                City = individualUser.ApplicationUser?.City ?? "",
                Country = individualUser.ApplicationUser?.Country ?? "",
                PhoneNumber = individualUser.ApplicationUser?.PhoneNumber ?? "",
            };

            return profile;
        }

        public async Task UpdateProfileAsync(string appUserId, UpdateProfileUserDto profile)
        {
            var appUser = userManager.Users.FirstOrDefault(a => a.Id == appUserId);
            if (appUser == null)
                throw new Exception("Application User not found.");

            appUser.PhoneNumber = profile.PhoneNumber;
            appUser.City = profile.City;
            appUser.Country = profile.Country;
            appUser.DisplayName = profile.DisplayName;

            await userManager.UpdateAsync(appUser);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
