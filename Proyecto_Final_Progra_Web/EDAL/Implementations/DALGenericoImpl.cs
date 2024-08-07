using EDAL.Interfaces;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDAL.Implementations
{
    public class DALGenericoImpl<TEntity> : IDALGenerico<TEntity> where TEntity : class
    {
        private VeterinariaDbContext _veterinariaDbContext;

        public DALGenericoImpl(VeterinariaDbContext veterinariaDbContext)
        {
            _veterinariaDbContext = veterinariaDbContext;
        }

        public bool Add(TEntity entity)
        {
            try
            {
                _veterinariaDbContext.Add(entity);
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public TEntity Get(int id)
        {

            return _veterinariaDbContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _veterinariaDbContext.Set<TEntity>().ToList();
        }

        public bool Remove(TEntity entity)
        {
            try
            {
                _veterinariaDbContext.Set<TEntity>().Attach(entity);
                _veterinariaDbContext.Set<TEntity>().Remove(entity);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(TEntity entity)
        {
            try
            {
                _veterinariaDbContext.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
