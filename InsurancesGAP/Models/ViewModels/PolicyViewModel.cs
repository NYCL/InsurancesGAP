using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsurancesGAP.Models.ViewModels
{
    public class PolicyViewModel
    {
        public List<RiskType> RiskTypes { get; set; }
        public List<CoverageType> CoverageTypes { get; set; }
        public List<Customer> Customers { get; set; }
        public Policy Policy { get; set; }

        public PolicyViewModel()
        {
            Policy = new Policy();
        }
    }
}
