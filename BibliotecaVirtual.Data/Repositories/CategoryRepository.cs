﻿using BibliotecaVirtual.Data.Entities;

namespace BibliotecaVirtual.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        /// <summary>
        /// Construtor do repositório.
        /// </summary>
        /// <param name="dbContext">Contexto do banco para a entidade.</param>
        public CategoryRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        { }
    }
}