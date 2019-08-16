using BibliotecaVirtual.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Application.Services
{
    public interface ICategoryService
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
        /// Cadastra um novo gênero.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<CategoryViewModel> AddCategory(CategoryViewModel viewModel);

        /// <summary>
        /// Altera informações de um gênero cadastrado.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do gênero.</param>
        /// <returns></returns>
        Task<CategoryViewModel> UpdateCategory(CategoryViewModel viewModel);

        /// <summary>
        /// Deleta um gênero cadastrado.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do gênero.</param>
        /// <returns></returns>
        Task<bool> DeleteCategory(CategoryViewModel viewModel);

        /// <summary>
        /// Deleta um gênero cadastrado.
        /// </summary>
        /// <param name="CategoryId">Identificador do gênero.</param>
        /// <returns></returns>
        Task<bool> DeleteCategory(int CategoryId);

        /// <summary>
        /// Obtém uma lista com os gênero cadastrados.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CategoryViewModel>> ObtainCategorys();

        /// <summary>
        /// Obtém um gênero a partir do seu identificador.
        /// </summary>
        /// <param name="CategoryId">Identificador do gênero.</param>
        /// <returns></returns>
        Task<CategoryViewModel> ObtainCategory(int CategoryId);
    }
}
