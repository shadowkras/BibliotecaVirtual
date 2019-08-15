using BibliotecaVirtual.Data.Entities;
using System;

namespace BibliotecaVirtual.Data.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>, IDisposable
    { }
}
