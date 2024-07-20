using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Intefaces
{
    public interface IDALGenerico<TEntity> where TEntity : class
    {


        bool Add(TEntity entity);


        bool Update(TEntity entity);
        bool Remove(TEntity entity);


        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();

    }
}
