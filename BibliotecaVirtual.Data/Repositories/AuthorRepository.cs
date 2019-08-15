using BibliotecaVirtual.Data.Entities;

namespace BibliotecaVirtual.Data.Repositories
{
    public class AuthorRepository : BaseRepository<Author>
    {
        /// <summary>
        /// Construtor do repositório.
        /// </summary>
        /// <param name="dbContext">Contexto do banco para a entidade.</param>
        public AuthorRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        { }
    }
}
