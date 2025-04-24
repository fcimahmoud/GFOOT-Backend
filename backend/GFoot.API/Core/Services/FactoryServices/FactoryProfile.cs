using AutoMapper;
using Shared.FactoryModels.ProfileModels;

namespace Services.FactoryServices
{
    public class FactoryProfile(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager) : IFactoryProfile
    {
        public async Task<FactoryProfileDTO> GetFactoryProfileAsync(string appUserId)
        {
            var factoryUser = await unitOfWork.GetRepository<FactoryUser, string>()
                       .GetWithIncludesAsync(user => user.ApplicationUserId == appUserId, f => f.ApplicationUser!);

            if (factoryUser == null) throw new Exception("Factory User Not found");
            //var profile = mapper.Map<FactoryUser, FactoryProfileDTO>(factoryUser);
            var profile = new FactoryProfileDTO
            {
                Id = factoryUser.Id,
                DisplayName = factoryUser.ApplicationUser?.DisplayName ?? "",
                UserName = factoryUser.ApplicationUser?.UserName ?? "",
                Email = factoryUser.ApplicationUser?.Email ?? "",
                IndustryType = factoryUser.IndustryType,
                PhoneNumber = factoryUser.ApplicationUser?.PhoneNumber ?? "",
                Country = factoryUser.ApplicationUser?.Country ?? "",
                City = factoryUser.ApplicationUser?.City ?? ""
            };

            return profile;
        }

        public async Task UpdateFactoryProfileAsync(string appUserId, UpdateFactoryProfileDTO profile)
        {
            var appUser = userManager.Users.FirstOrDefault(a => a.Id == appUserId);
            if (appUser == null)
                throw new Exception("Application User not found.");

            var factoryUser = await unitOfWork.GetRepository<FactoryUser, string>()
                       .GetByConditionAsync(user => user.ApplicationUserId == appUserId);
            if (factoryUser == null)
                throw new Exception("Factory User not found.");


            factoryUser.IndustryType = profile.IndustryType;
            appUser.City = profile.City;
            appUser.Country = profile.Country;
            appUser.DisplayName = profile.DisplayName;


            unitOfWork.GetRepository<FactoryUser, string>().Update(factoryUser);
            await userManager.UpdateAsync(appUser);
            await unitOfWork.SaveChangesAsync();

        }
    }
}
