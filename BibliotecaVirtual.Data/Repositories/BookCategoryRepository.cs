using BibliotecaVirtual.Data.Entities;

namespace BibliotecaVirtual.Data.Repositories
{
    public class BookCategoryRepository : BaseRepository<BookCategory>
    {
        /// <summary>
        /// Construtor do repositório.
        /// </summary>
        /// <param name="dbContext">Contexto do banco para a entidade.</param>
        public BookCategoryRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        { }
    }
}
