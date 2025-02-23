global using Shared.Individual;
global using Domain.Entities.Individual;

namespace Services.Abstraction.Individual_Services
{
    public interface ICalculationsService
    {
        public Task<Activity> LogActivityAsync(string UserId, ActivityDTO activitiesDTO);
        public Task<float> CalculateCarbonFootPrintAsync(ActivityDTO activitiesDTO);
    }
}
