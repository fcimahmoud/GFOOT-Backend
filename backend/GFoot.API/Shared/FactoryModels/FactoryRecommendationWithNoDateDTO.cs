using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.FactoryModels
{
    public class FactoryRecommendationWithNoDateDTO
    {
        public required string Id { get; set; }
        public required string RecHeader { get; set; }
        public required string RecBody { get; set; }
    }
}
