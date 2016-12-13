namespace Recruitment.Data
{
    public static class DataRepositoryFactory
    {
        public static IRepository CurrentRepository
        {
            get
            {
                var repository = DataRepositoryStore.CurrentDataStore[DataRepositoryStore.KEY_DATACONTEXT] as IRepository;
                if (repository == null)
                {
                    repository = new EFGenericRepository(new recruitmentEntities());
                    DataRepositoryStore.CurrentDataStore[DataRepositoryStore.KEY_DATACONTEXT] = repository;
                }

                return repository;

            }
        }

        public static void CloseCurrentRepository()
        {
            var repository = DataRepositoryStore.CurrentDataStore[DataRepositoryStore.KEY_DATACONTEXT] as IRepository;
            if (repository != null)
            {
                repository.Dispose();
                DataRepositoryStore.CurrentDataStore[DataRepositoryStore.KEY_DATACONTEXT] = null;
            }
        }
    }
}
