using AutoMapper;

namespace Services.FactoryServices
{
    public class FactoryProfile(IUnitOfWork unitOfWork, IMapper mapper) : IFactoryProfile
    {
        public async Task<FactoryProfileDTO> GetFactoryProfileAsync(string appUserId)
        {
            var factoryUser = await unitOfWork.GetRepository<FactoryUser, string>()
                       .GetByConditionAsync(user => user.ApplicationUserId == appUserId);

            if (factoryUser == null) throw new Exception("Factory User Not found");
            var profile = mapper.Map<FactoryUser, FactoryProfileDTO>(factoryUser);

            //var profile = new FactoryProfileDTO
            //{
            //    DisplayName = factoryUser!.ApplicationUser!.DisplayName,
            //    IndustryType = factoryUser.IndustryType,
            //    IndustryDescription = factoryUser.IndustryDescription,
            //    Phone = factoryUser.Phone,
            //    Country = factoryUser.ApplicationUser.Country,
            //    City = factoryUser.ApplicationUser.City
            //};

            return profile;
        }

        public async Task UpdateFactoryProfileAsync(string appUserId, FactoryProfileDTO profile)
        {
            var factoryUser = await unitOfWork.GetRepository<FactoryUser, string>()
                       .GetByConditionAsync(user => user.ApplicationUserId == appUserId);
            if (factoryUser == null)
                throw new Exception("User not found.");

            var updatedProfile = mapper.Map<FactoryProfileDTO, FactoryUser>(profile);
            unitOfWork.GetRepository<FactoryUser, string>().Update(updatedProfile);
            await unitOfWork.SaveChangesAsync();


            //factoryUser.ApplicationUser!.DisplayName = profile.DisplayName;
            //factoryUser.ApplicationUser.Country = profile.Country;
            //factoryUser.ApplicationUser.City = profile.City;
            //factoryUser.IndustryType = profile.IndustryType;
            //factoryUser.IndustryDescription = profile.IndustryDescription;
            //factoryUser.Phone = profile.Phone;
            //unitOfWork.GetRepository<FactoryUser, string>().Update(updatedProfile);
            //await unitOfWork.SaveChangesAsync();

        }
    }
}
