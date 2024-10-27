namespace TV_Infrastructure.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly TVDBContext TV_DBContext;

        public GenericRepository(TVDBContext TV_DBContext)
        {
            this.TV_DBContext = TV_DBContext;
        }

        //public T Add(T entity)
        //{
        //    // إذا كان الكيان موجودًا بالفعل في DbContext، فصله
        //    var trackedEntity = TV_DBContext.Entry(entity).State;
        //    if (trackedEntity == EntityState.Modified || trackedEntity == EntityState.Unchanged)
        //    {
        //        TV_DBContext.Entry(entity).State = EntityState.Detached;
        //    }

        //    var newEntity = TV_DBContext.Add(entity);
        //    return newEntity.Entity;
        //}
        public T Add(T entity)
        {
            var newentity = TV_DBContext.Add(entity);
            return newentity.Entity;
        }

        public IList<T> All()
        {
            var All = TV_DBContext.Set<T>().ToList();
            return All;
        }

        public T? Get(Guid Id)
        {
            return TV_DBContext.Find<T>(Id);
        }

        public void SaveChange()
        {
            TV_DBContext.SaveChanges();
        }

        public T Update(T entity)
        {
            return TV_DBContext.Update(entity).Entity;
        }
    }
}