using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InsurancesGAP.Models
{
    public class Policy
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<PolicyCoverageType> PolicyCoverageTypes { get; set; }

        public double CoveragePercentage { get; set; }

        public DateTime StartDate { get; set; }

        public int CoverageMonths { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public long RiskTypeId { get; set; }

        public virtual RiskType RiskType { get; set; }

        public long? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
