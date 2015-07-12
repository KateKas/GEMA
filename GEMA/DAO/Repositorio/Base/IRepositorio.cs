using System;
using System.Linq;

namespace GEMA.DAO.Repositorio.Base
{
    interface IRepositorio<TEntity> where TEntity : class
    {
        //Retorna todos os dados como IQueryable, ou seja, você pode retornar a lista e aplicar expressões Lambda para filtrar e classificar dados.
        IQueryable<TEntity> GetAll();

        //Retona todos os dados que atenderem a um critério passado em tempo de execução através de uma expressão Lambda. 
        //Neste caso, o Func é um delegate que será criado dinâmicamente, aplica-se o predicate para verificar se o dado atende ao critério. 
        IQueryable<TEntity> Get(Func<TEntity, bool> predicate);

        //Irá aplicar um filtro pela chave primária da classe em si.
        TEntity Find(params object[] key);

        //Recebe o objeto TEntity para efetuar o Update no banco.
        void Atualizar(TEntity obj);

        //Chama o método SaveChanges para efetivar todas as alterações no contexto.
        void SalvarTodos();

        //Recebe o objeto TEntity para efetuar o Insert no banco.
        void Adicionar(TEntity obj);

        //Este método irá excluir registros, sendo que a condição é dinâmica através de uma expressão Lambda e aplica-se o predicate para a condição passada.
        void Excluir(Func<TEntity, bool> predicate);
    }
}
