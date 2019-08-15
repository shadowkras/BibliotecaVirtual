using BibliotecaVirtual.Data.Entities;

namespace BibliotecaVirtual.Data.Repositories
{
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        /// <summary>
        /// Construtor do repositório.
        /// </summary>
        /// <param name="dbContext">Contexto do banco para a entidade.</param>
        public PublisherRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        { }
    }
}
