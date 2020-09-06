using InsurancesGAP.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InsurancesGAP.Models
{
    public class Policy
    {
        public long ID { get; set; }

        [Required]
        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Descripción")]
        public string Description { get; set; }

        [DisplayName("Tipo de cubrimiento(s)")]
        [EnsureMinimumElements(1, ErrorMessage = "Se debe seleccionar al menos 1 tipo de cubrimiento de la póliza")]
        public virtual List<PolicyCoverageType> PolicyCoverageTypes { get; set; }

        [Range(1.0, 100.0)]
        [DisplayName("Porcentaje de cobertura")]
        public double CoveragePercentage { get; set; }

        [DisplayName("Inicio de vigencia")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [Range(1, 60)]
        [DisplayName("Meses de cobertura")]
        public int? CoverageMonths { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [DisplayName("Precio")]
        [Range(1, double.MaxValue)]
        public decimal? Price { get; set; }

        [Required]
        [DisplayName("Tipo de Riesgo")]
        public long RiskTypeId { get; set; }

        [DisplayName("Tipo de Riesgo")]
        public virtual RiskType RiskType { get; set; }

        public long? CustomerId { get; set; }

        [DisplayName("Cliente")]
        public virtual Customer Customer { get; set; }

        public bool Validate()
        {
            if (RiskTypeId == 4L && CoveragePercentage > 50.0)
            {
                return false;
            }
            return true;
        }
    }
}
