using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace InsurancesGAP.Models
{
    public class Customer
    {
        public long ID { get; set; }

        [DisplayName("Nombre")]
        public string Name { get; set; }

        public virtual List<Policy> Policies { get; set; }
    }
}
