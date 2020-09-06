using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsurancesGAP.Data;
using InsurancesGAP.Models;
using InsurancesGAP.Helpers;

namespace InsurancesGAP.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        private readonly DataContext _context;

        public PoliciesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Policy>> GetPolicy()
        {
            return _context.Policies.Get(includeProperties : new string[] { "Customer", "RiskType", "PolicyCoverageTypes.CoverageType" }).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Policy> GetPolicy(long id)
        {
            var policy = _context.Policies.Get(p=> p.ID == id, null, new string[] { "PolicyCoverageTypes" });

            if (policy == null)
            {
                return NotFound();
            }

            return policy.First();
        }

        [HttpPost]
        public ActionResult<Policy> PostPolicy(Policy policy)
        {
            if (!policy.Validate())
            {
                ModelState.AddModelError(string.Empty, "Una línea de riesgo alto, el porcentaje de cubrimiento no puede ser superior al 50%.");
                return BadRequest(ModelState);
            }
            _context.Policies.Create(policy);
            _context.Policies.Save();

            return CreatedAtAction("GetPolicy", new { id = policy.ID }, policy);
        }

        [HttpPut("{id}")]
        public ActionResult<Policy> PutPolicy(long id, Policy policy)
        {
            if (id != policy.ID)
            {
                return BadRequest();
            }

            if (!policy.Validate())
            {
                ModelState.AddModelError(string.Empty, "Una línea de riesgo alto, el porcentaje de cubrimiento no puede ser superior al 50%.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Policies.Update(policy);
                _context.Policies.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return policy;
        }

        [HttpDelete("{id}")]
        public ActionResult<Policy> DeletePolicy(long id)
        {
            var policy = _context.Policies.Get(id);
            if (policy == null)
            {
                return NotFound();
            }

            _context.Policies.Delete(policy);
            _context.Policies.Save();

            return policy;
        }

        private bool PolicyExists(long id)
        {
            return _context.Policies.Get().Any(e => e.ID == id);
        }
    }
}
