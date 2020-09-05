using InsurancesGAP.Data.Interfaces;
using InsurancesGAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsurancesGAP.Tests.Mocks
{
    class MockRiskTypeRepository : IRepository<RiskType>
    {
        private List<RiskType> _riskTypes;

        public MockRiskTypeRepository()
        {
            _riskTypes = new List<RiskType>()
            {
                new RiskType() { ID = 1, Name = "Bajo" },
                new RiskType() { ID = 2, Name = "Medio" },
                new RiskType() { ID = 3, Name = "Medio-Alto" },
                new RiskType() { ID = 4, Name = "Alto" }
            };
        }

        public RiskType Create(RiskType riskType)
        {
            riskType.ID = _riskTypes.Max(c => c.ID) + 1;
            _riskTypes.Add(riskType);
            return riskType;
        }

        public RiskType Delete(object id)
        {
            RiskType customer = _riskTypes.Find(r => r.ID == (long)id);
            if (customer != null)
            {
                _riskTypes.Remove(customer);
            }
            return customer;
        }

        public IEnumerable<RiskType> Get()
        {
            return _riskTypes;
        }

        public RiskType Get(params object[] id)
        {
            return _riskTypes.FirstOrDefault(w => w.ID == (long)id.First());
        }

        public RiskType Update(RiskType entityToUpdate)
        {
            RiskType customer = _riskTypes.FirstOrDefault(w => w.ID == entityToUpdate.ID);
            if (customer != null)
            {
                customer.Name = entityToUpdate.Name;
            }
            return customer;
        }
    }
}
