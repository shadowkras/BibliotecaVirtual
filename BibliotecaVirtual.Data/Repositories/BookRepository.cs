using BibliotecaVirtual.Data.Entities;

namespace BibliotecaVirtual.Data.Repositories
{
    public class BookRepository : BaseRepository<Book>
    {
        /// <summary>
        /// Construtor do repositório.
        /// </summary>
        /// <param name="dbContext">Contexto do banco para a entidade.</param>
        public BookRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        { }
    }
}
