using BibliotecaVirtual.Data.Entities;
using System;

namespace BibliotecaVirtual.Data.Repositories
{
    public interface IAuthorRepository : IBaseRepository<Author>, IDisposable
    { }
}
