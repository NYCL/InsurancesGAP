using InsurancesGAP.Data.Interfaces;
using InsurancesGAP.Models;
using InsurancesGAP.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsurancesGAP.Tests
{
    [TestClass]
    public class PolicyTest
    {
        [TestMethod]
        public void MockTest()
        {
            IRepository<RiskType> repo = new MockRiskTypeRepository();

            var allRisk = repo.Get();

            Assert.AreEqual(4, allRisk.Count());
        }

        [TestMethod]
        public void CheckValid()
        {
            IRepository<RiskType> repo = new MockRiskTypeRepository();

            Policy policy = new Policy()
            {
                RiskTypeId = repo.Get().FirstOrDefault(r => r.Name.ToLower().Equals("alto")).ID,
                CoveragePercentage = 60.1
            };

            Assert.IsFalse(policy.Validate());
        }

        [TestMethod]
        public void CheckInvalid()
        {
            IRepository<RiskType> repo = new MockRiskTypeRepository();

            Policy policy = new Policy()
            {
                RiskTypeId = repo.Get().FirstOrDefault(r => r.Name.ToLower().Equals("bajo")).ID,
                CoveragePercentage = 60.1
            };

            Assert.IsTrue(policy.Validate());
        }

    }
}
