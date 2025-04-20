using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.FactoryModels
{
    public class FactoryProfileDTO
    {
        public required string DisplayName { get; set; }
        public string? IndustryType { get; set; }
        public string? IndustryDescription { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public int Phone { get; set; }

    }
}
