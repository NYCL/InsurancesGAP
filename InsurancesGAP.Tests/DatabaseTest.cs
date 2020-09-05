using InsurancesGAP.Data;
using InsurancesGAP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InsurancesGAP.Tests
{
    [TestClass]
    public class DatabaseTest : IDisposable
    {
        DataContext _context;

        
        public DatabaseTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<DataContext>();

            builder.UseSqlServer($"Server=NYCL;Database=test_db_{Guid.NewGuid()};Trusted_Connection=True;MultipleActiveResultSets=true")
                   .UseInternalServiceProvider(serviceProvider);

            _context = new DataContext(builder.Options);
            _context.Database.Migrate();

        }

        [TestMethod]
        public void CheckSeed()
        {
            Customer client = _context.Customers.Create(new Customer() { Name = "Rudolf" });
            _context.Policies.Create(new Policy() { 
                Name = "All Risks", 
                Description = "Simple Policy",
                PolicyCoverageTypes = new List<PolicyCoverageType>() { 
                    new PolicyCoverageType() { CoverageTypeId = 1 }
                },
                CoveragePercentage = 40.5,
                StartDate = DateTime.Now,
                CoverageMonths = 12,
                Price = 780000,
                RiskTypeId = 2,
            });
            _context.SaveChanges();

            Assert.AreEqual(4, _context.RiskTypes.Get().Count());
            Assert.AreEqual(4, _context.CoverageTypes.Get().Count());
            Assert.AreEqual(client.Name, _context.Customers.Get(client.ID).Name);
            Assert.AreEqual(1, _context.Policies.Get().Count());
        }

        [TestCleanup]
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
