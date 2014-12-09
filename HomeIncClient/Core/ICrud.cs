using HomeIncClient.Core.Store;

namespace HomeIncClient.Core
{
    internal interface ICrud<TEntity> where TEntity : Entity
    {
        int Create(TEntity entity);
        TEntity Read(int id);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}