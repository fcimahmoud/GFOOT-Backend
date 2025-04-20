using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.FactoryModels
{
    public class FactoryActivityEmissionDTO
    {
        public string Id { get; set; }
        public DateOnly Date;
        public float CarbonEmission { get; set; }
    }
}
