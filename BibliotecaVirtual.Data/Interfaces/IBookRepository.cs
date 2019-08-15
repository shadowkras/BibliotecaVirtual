using BibliotecaVirtual.Data.Entities;
using System;

namespace BibliotecaVirtual.Data.Repositories
{
    public interface IBookRepository : IBaseRepository<Book>, IDisposable
    { }
}
