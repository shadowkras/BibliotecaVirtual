using BibliotecaVirtual.Data.Entities;
using System;

namespace BibliotecaVirtual.Data.Repositories
{
    public interface IBookCategoryRepository : IBaseRepository<BookCategory>, IDisposable
    { }
}
