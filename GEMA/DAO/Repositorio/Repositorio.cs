using System;
using System.Linq;
using System.Data.Entity;
using GEMA.DAO.Repositorio.Base;

namespace GEMA.DAO.Repositorio
{
    public abstract class Repositorio<TEntity> : IDisposable, IRepositorio<TEntity> where TEntity : class
    {
        Contexto.Dao dao = new Contexto.Dao();

        public System.Linq.IQueryable<TEntity> GetAll()
        {
            return dao.Set<TEntity>();
        }

        public System.Linq.IQueryable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return GetAll().Where(predicate).AsQueryable();
        }

        public TEntity Find(params object[] key)
        {
            return dao.Set<TEntity>().Find(key);
        }

        public void Atualizar(TEntity obj)
        {
            dao.Entry(obj).State = EntityState.Modified;
        }

        public void SalvarTodos()
        {
            dao.SaveChanges();
        }

        public void Adicionar(TEntity obj)
        {
            dao.Set<TEntity>().Add(obj);
        }

        public void Excluir(Func<TEntity, bool> predicate)
        {
            dao.Set<TEntity>()
               .Where(predicate).ToList()
               .ForEach(del => dao.Set<TEntity>().Remove(del));
        }
        public void Dispose()
        {
            dao.Dispose();
        }
    }
}
