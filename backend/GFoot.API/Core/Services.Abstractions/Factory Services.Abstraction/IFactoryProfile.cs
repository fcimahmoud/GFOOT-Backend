using Shared.FactoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.Factory_Services.Abstraction
{
    public interface IFactoryProfile
    {
        public Task<FactoryProfileDTO> GetFactoryProfileAsync(string applicationUserId);
        public Task UpdateFactoryProfileAsync(string appUserId, FactoryProfileDTO profile);
    }
}
