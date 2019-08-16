using BibliotecaVirtual.Application.Resources;
using BibliotecaVirtual.Application.ViewModels;
using BibliotecaVirtual.Data.Entities;
using BibliotecaVirtual.Data.Extensions;
using BibliotecaVirtual.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private string ModelError = string.Empty;
        public bool OperationSuccesful = false;

        #region Construtor

        public CategoryService(ICategoryRepository repositorio)
        {
            _repository = repositorio;
        }

        /// <summary>
        /// Retorna se a operação foi bem sucedida.
        /// </summary>
        /// <returns></returns>
        public bool IsSuccessful()
        {
            return OperationSuccesful;
        }

        /// <summary>
        /// Retorna as mensagens de erro da validação da regra de negócios.
        /// </summary>
        /// <returns></returns>
        public string GetModelErrors()
        {
            return ModelError;
        }

        /// <summary>
        /// Cadastra um novo gênero.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do gênero.</param>
        /// <returns></returns>
        public async Task<CategoryViewModel> AddCategory(CategoryViewModel viewModel)
        {
            if(await _repository.Exists(p=> p.Description == viewModel.Description))
            {
                ModelError = string.Format(Criticas.Ja_Cadastrado_0, "Gênero");
                return viewModel;
            }

            var Category = viewModel.AutoMapear<CategoryViewModel, Category>();
            _repository.Insert(Category);
            OperationSuccesful = await _repository.Commit();

            //Recuperando o valor recebido pelo CategoryId.
            viewModel = Category.AutoMapear<Category, CategoryViewModel>();

            return viewModel;
        }

        /// <summary>
        /// Altera informações de um gênero cadastrado.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do gênero.</param>
        /// <returns></returns>
        public async Task<CategoryViewModel> UpdateCategory(CategoryViewModel viewModel)
        {
            if (await _repository.Exists(p => p.Description == viewModel.Description))
            {
                ModelError = string.Format(Criticas.Ja_Existe_0, "outro Gênero com esta descrição.");
                return viewModel;
            }

            var Category = viewModel.AutoMapear<CategoryViewModel, Category>();
            _repository.Update(Category);
            OperationSuccesful = await _repository.Commit();

            return viewModel;
        }

        /// <summary>
        /// Deleta um gênero cadastrado.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do gênero.</param>
        /// <returns></returns>
        public async Task<bool> DeleteCategory(CategoryViewModel viewModel)
        {
            var Category = viewModel.AutoMapear<CategoryViewModel, Category>();
            _repository.Delete(Category);
            OperationSuccesful = await _repository.Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Deleta um gênero cadastrado.
        /// </summary>
        /// <param name="CategoryId">Identificador do gênero.</param>
        /// <returns></returns>
        public async Task<bool> DeleteCategory(int CategoryId)
        {
            _repository.Delete(p=> p.CategoryId == CategoryId);
            OperationSuccesful = await _repository.Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Obtém uma lista com os gêneroes cadastrados.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryViewModel>> ObtainCategorys()
        {
            var Categorys = await _repository.SelectAll();
            var viewModel = Categorys.AutoMapearLista<Category, CategoryViewModel>();
            return viewModel;
        }

        /// <summary>
        /// Obtém um gênero a partir do seu identificador.
        /// </summary>
        /// <param name="CategoryId">Identificador do gênero.</param>
        /// <returns></returns>
        public async Task<CategoryViewModel> ObtainCategory(int CategoryId)
        {
            var Categorys = await _repository.Select(p=> p.CategoryId == CategoryId);
            var viewModel = Categorys.AutoMapear<Category, CategoryViewModel>();
            return viewModel;
        }

        #endregion
    }
}
