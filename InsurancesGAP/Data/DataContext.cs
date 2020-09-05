using InsurancesGAP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsurancesGAP.Data
{
    public class DataContext : DbContext
    {
        public EFRepository<Customer> Customer { get; set; }
        public EFRepository<CoverageType> CoverageTypes { get; set; }
        public EFRepository<RiskType> RiskTypes { get; set; }
        public EFRepository<Policy> Policies { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PolicyCoverageType>().HasKey(pct => new { pct.CoverageTypeId, pct.PolicyId });

            modelBuilder.Entity<PolicyCoverageType>()
                .HasOne(p => p.Policy)
                .WithMany(pct => pct.PolicyCoverageTypes)
                .HasForeignKey(p => p.PolicyId);

            modelBuilder.Entity<PolicyCoverageType>()
                .HasOne(ct => ct.CoverageType)
                .WithMany(pct => pct.PolicyCoverageTypes)
                .HasForeignKey(ct => ct.CoverageTypeId);

            modelBuilder.Entity<RiskType>().HasData(
                new RiskType() { ID = 1, Name = "Bajo" },
                new RiskType() { ID = 2, Name = "Medio" },
                new RiskType() { ID = 3, Name = "Medio-Alto" },
                new RiskType() { ID = 4, Name = "Alto" });

            modelBuilder.Entity<CoverageType>().HasData(
                new RiskType() { ID = 1, Name = "Terremoto" },
                new RiskType() { ID = 2, Name = "Incendio" },
                new RiskType() { ID = 3, Name = "Robo" },
                new RiskType() { ID = 4, Name = "Pérdida" });

        }
    }
}
