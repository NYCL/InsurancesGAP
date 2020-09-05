using InsurancesGAP.Data.Interfaces;
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
        private EFRepository<Customer> _customer { get; set; }
        private EFRepository<CoverageType> _coverageTypes { get; set; }
        private EFRepository<RiskType> _riskTypes { get; set; }
        private EFRepository<Policy> _policies { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public EFRepository<Customer> Customers => _customer ?? (_customer = new EFRepository<Customer>(this));
        public EFRepository<CoverageType> CoverageTypes => _coverageTypes ?? (_coverageTypes = new EFRepository<CoverageType>(this));
        public EFRepository<RiskType> RiskTypes => _riskTypes ?? (_riskTypes = new EFRepository<RiskType>(this));
        public EFRepository<Policy> Policies => _policies ?? (_policies = new EFRepository<Policy>(this));

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
