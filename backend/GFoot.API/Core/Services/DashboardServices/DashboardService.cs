
global using Services.Abstractions.Dashboard_Services.Abstraction;
using Shared.DashboardModels;

namespace Services.DashboardServices
{
    public class DashboardService (IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        : IDashboardService
    {

        public async Task DeleteFactoryAsync(string factoryId)
        {
            var appUser = await userManager.FindByIdAsync(factoryId);
            if (appUser == null)
                throw new Exception("AppUser not found");

            var factoryRepo = unitOfWork.GetRepository<FactoryUser, string>();
            var factoryUser = await factoryRepo.GetByConditionAsync(f => f.ApplicationUserId == factoryId);
            if (factoryUser == null)
                throw new Exception("Factory not found");

            await userManager.DeleteAsync(appUser);
            factoryRepo.Delete(factoryUser);
            await unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateFactoryAsync(UpdateFactoryDto updateFactoryDto, string factoryId)
        {
            var appUser = await userManager.FindByIdAsync(factoryId);
            if(appUser == null)
                throw new Exception("AppUser not found");
            appUser.DisplayName = updateFactoryDto.DisplayName;
            appUser.PhoneNumber = updateFactoryDto.PhoneNumber;
            appUser.Country = updateFactoryDto.Country;
            appUser.City = updateFactoryDto.City;


            var factoryRepo = unitOfWork.GetRepository<FactoryUser, string>();
            var factoryUser = await factoryRepo.GetByConditionAsync(f => f.ApplicationUserId == factoryId);
            if (factoryUser == null)
                throw new Exception("Factory not found");
            factoryUser.IndustryType = updateFactoryDto.IndustryType;

            await userManager.UpdateAsync(appUser);
            factoryRepo.Update(factoryUser);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAgentAsync(string agentId)
        {
            var appUser = await userManager.FindByIdAsync(agentId);
            if (appUser == null)
                throw new Exception("AppUser not found");

            var agentRepo = unitOfWork.GetRepository<EnvironmentalAgent, string>();
            var agentUser = await agentRepo.GetByConditionAsync(e => e.ApplicationUserId == agentId);
            if (agentUser == null)
                throw new Exception("Environmental Agent not found");

            await userManager.DeleteAsync(appUser);
            agentRepo.Delete(agentUser);
            await unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateAgentAsync(UpdateAgentDto updateAgentDto, string agentId)
        {
            var appUser = await userManager.FindByIdAsync(agentId);
            if (appUser == null)
                throw new Exception("AppUser not found");
            appUser.DisplayName = updateAgentDto.DisplayName;
            appUser.PhoneNumber = updateAgentDto.PhoneNumber;
            appUser.Country = updateAgentDto.Country;
            appUser.City = updateAgentDto.City;

            await userManager.UpdateAsync(appUser);
        }
    }
}
