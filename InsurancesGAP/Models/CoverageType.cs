using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsurancesGAP.Models
{
    public class CoverageType
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public virtual List<PolicyCoverageType> PolicyCoverageTypes { get; set; }
    }
}
