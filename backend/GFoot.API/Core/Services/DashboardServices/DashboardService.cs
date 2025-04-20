
global using Services.Abstractions.Dashboard_Services.Abstraction;
global using Shared.DashboardModels;
global using System.Data;

namespace Services.DashboardServices
{
    public class DashboardService (IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IAuthenticationService authenticationService)
        : IDashboardService
    {
        public async Task<FactoryUserDto> GetFactoryByIdAsync(string factoryId)
        {
            var factoryRepo = unitOfWork.GetRepository<FactoryUser, string>();
            var factoryUser = await factoryRepo.GetAsync(factoryId);
            if (factoryUser == null) throw new Exception("Factory User Not Found");
            var appUser = userManager.Users.FirstOrDefault(a => a.Id == factoryUser.ApplicationUserId);
            if (appUser == null) throw new Exception("Application User Not Found");

            return new FactoryUserDto
            {
                Id = factoryUser.Id,
                Email = appUser.Email!,
                DisplayName = appUser.DisplayName,
                IndustryType = factoryUser.IndustryType!,
                UserType = appUser.UserType,
                City = appUser.City,
                Country = appUser.Country,
            };
        }

        public async Task<IEnumerable<FactoryUserDto>> GetAllFactoryUsersAsync()
        {
            var factoryRepo = unitOfWork.GetRepository<FactoryUser, string>();
            var factories = await factoryRepo.GetAllWithIncludesAsync(f => true, f => f.ApplicationUser!);
            return factories
                .Where(f => f.ApplicationUser != null)
                .Select(f => new FactoryUserDto
            {
                Id = f.Id,
                Email = f.ApplicationUser!.Email!,
                DisplayName = f.ApplicationUser.DisplayName,
                IndustryType = f.IndustryType!,
                UserType = f.ApplicationUser.UserType,
                City = f.ApplicationUser.City,
                Country = f.ApplicationUser.Country,
            });
        }
        public async Task<UserResultDTO> AddFactoryAsync(RegisterDTO registerModel)
        {
            return await authenticationService.RegisterAsync(registerModel);
        }
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


        public async Task<AgentUserDto> GetAgentByIdAsync(string agentId)
        {
            var agentRepo = unitOfWork.GetRepository<EnvironmentalAgent, string>();
            var agentUser = await agentRepo.GetAsync(agentId);
            if (agentUser == null) throw new Exception("Environmental Agent User Not Found");
            var appUser = userManager.Users.FirstOrDefault(a => a.Id == agentUser.ApplicationUserId);
            if (appUser == null) throw new Exception("Application User Not Found");

            return new AgentUserDto
            {
                Id = agentUser.Id,
                Email = appUser.Email!,
                DisplayName = appUser.DisplayName,
                UserType = appUser.UserType,
                City = appUser.City,
                Country = appUser.Country,
            };
        }

        public async Task<IEnumerable<AgentUserDto>> GetAllAgentUsersAsync()
        {
            var agentRepo = unitOfWork.GetRepository<EnvironmentalAgent, string>();
            var agents = await agentRepo.GetAllWithIncludesAsync(f => true, f => f.ApplicationUser!);
            return agents
                .Where(f => f.ApplicationUser != null)
                .Select(f => new AgentUserDto
            {
                Id = f.Id,
                Email = f.ApplicationUser!.Email!,
                DisplayName = f.ApplicationUser.DisplayName,
                UserType = f.ApplicationUser.UserType,
                City = f.ApplicationUser.City,
                Country = f.ApplicationUser.Country,
            });
        }
        public async Task<UserResultDTO> AddAgentAsync(RegisterDTO registerModel)
        {
            return await authenticationService.RegisterAsync(registerModel);
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
