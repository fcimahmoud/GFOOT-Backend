using Domain.Entities.Factory;
using Shared.FactoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.Factory_Services.Abstraction
{
    public interface IFactoryCalculationsService
    {
        public Task<float> CalculateCarbonFootPrintAsync(FactoryEmissionDTO factoryEmissionDTO);
        public Task<FactoryEmission> LogCarbonFootPrintAsync(string applicationUserId, FactoryEmissionDTO factoryEmissionDTO);
        public Task<float> GetFootPrintAsync(string appUserId);
    }
}
