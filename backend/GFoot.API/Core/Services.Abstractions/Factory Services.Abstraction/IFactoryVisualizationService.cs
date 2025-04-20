using Shared.FactoryModels;
using Shared.IndividualModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.Factory_Services.Abstraction
{
    public interface IFactoryVisualizationService
    {
        public Task<IEnumerable<FactoryActivityEmissionDTO>> GetVisualizedDataMonthlyAsync(string appUserId, bool ascending = true);
        public Task<IEnumerable<FactoryActivityEmissionDTO>> GetVisualizedDataYearlyAsync(string appUserId, bool ascending = true);
    }
}
