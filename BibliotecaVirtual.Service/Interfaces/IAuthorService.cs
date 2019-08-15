using BibliotecaVirtual.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Application.Services
{
    public interface IAuthorService
    {
        /// <summary>
        /// Retorna se a operação foi bem sucedida.
        /// </summary>
        /// <returns></returns>
        bool IsSuccessful();

        /// <summary>
        /// Retorna as mensagens de erro da validação da regra de negócios.
        /// </summary>
        /// <returns></returns>
        string GetModelErrors();

        /// <summary>
        /// Cadastra um novo autor.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<AuthorViewModel> AddAuthor(AuthorViewModel viewModel);

        /// <summary>
        /// Altera informações de um autor cadastrado.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do autor.</param>
        /// <returns></returns>
        Task<AuthorViewModel> UpdateAuthor(AuthorViewModel viewModel);

        Task<bool> DeleteAuthor(AuthorViewModel viewModel);

        /// <summary>
        /// Deleta um autor cadastrado.
        /// </summary>
        /// <param name="authorId">Identificador do autor.</param>
        /// <returns></returns>
        Task<bool> DeleteAuthor(int authorId);

        /// <summary>
        /// Obtém uma lista com os autores cadastrados.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AuthorViewModel>> ObtainAuthors();

        /// <summary>
        /// Obtém um autor a partir do seu identificador.
        /// </summary>
        /// <param name="authorId">Identificador do autor.</param>
        /// <returns></returns>
        Task<AuthorViewModel> ObtainAuthor(int authorId);
    }
}
