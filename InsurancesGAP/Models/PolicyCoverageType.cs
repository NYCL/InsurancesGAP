using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsurancesGAP.Models
{
    public class PolicyCoverageType
    {
        public long CoverageTypeId { get; set; }

        public virtual CoverageType CoverageType { get; set; }

        public long PolicyId { get; set; }

        public virtual Policy Policy { get; set; }
    }
}
