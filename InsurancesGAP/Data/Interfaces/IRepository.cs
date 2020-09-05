using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsurancesGAP.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity entity);

        IEnumerable<TEntity> Get();

        TEntity Get(params object[] id);

        TEntity Update(TEntity entityToUpdate);

        TEntity Delete(object id);
    }
}
