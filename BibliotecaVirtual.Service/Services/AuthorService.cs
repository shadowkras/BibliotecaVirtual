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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private string ModelError = string.Empty;
        public bool OperationSuccesful = false;

        #region Construtor

        public AuthorService(IAuthorRepository repositorio)
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
        /// Cadastra um novo autor.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do autor.</param>
        /// <returns></returns>
        public async Task<AuthorViewModel> AddAuthor(AuthorViewModel viewModel)
        {
            if(await _repository.Exists(p=> p.Name == viewModel.Name))
            {
                ModelError = string.Format(Criticas.Ja_Cadastrado_0, "Autor(a)");
                return viewModel;
            }

            var author = viewModel.AutoMapear<AuthorViewModel, Author>();
            _repository.Insert(author);
            OperationSuccesful = await _repository.Commit();

            //Recuperando o valor recebido pelo AuthorId.
            viewModel = author.AutoMapear<Author, AuthorViewModel>();

            return viewModel;
        }

        /// <summary>
        /// Altera informações de um autor cadastrado.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do autor.</param>
        /// <returns></returns>
        public async Task<AuthorViewModel> UpdateAuthor(AuthorViewModel viewModel)
        {
            if (await _repository.Exists(p => p.Name == viewModel.Name))
            {
                ModelError = string.Format(Criticas.Ja_Existe_0, "outro(a) Autor(a) com este nome.");
                return viewModel;
            }

            var author = viewModel.AutoMapear<AuthorViewModel, Author>();
            _repository.Update(author);
            OperationSuccesful = await _repository.Commit();

            return viewModel;
        }

        /// <summary>
        /// Deleta um autor cadastrado.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do autor.</param>
        /// <returns></returns>
        public async Task<bool> DeleteAuthor(AuthorViewModel viewModel)
        {
            var author = viewModel.AutoMapear<AuthorViewModel, Author>();
            _repository.Delete(author);
            OperationSuccesful = await _repository.Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Deleta um autor cadastrado.
        /// </summary>
        /// <param name="authorId">Identificador do autor.</param>
        /// <returns></returns>
        public async Task<bool> DeleteAuthor(int authorId)
        {
            _repository.Delete(p=> p.AuthorId == authorId);
            OperationSuccesful = await _repository.Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Obtém uma lista com os autores cadastrados.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AuthorViewModel>> ObtainAuthors()
        {
            var authors = await _repository.SelectAll();
            var viewModel = authors.AutoMapearLista<Author, AuthorViewModel>();
            return viewModel;
        }

        /// <summary>
        /// Obtém um autor a partir do seu identificador.
        /// </summary>
        /// <param name="authorId">Identificador do autor.</param>
        /// <returns></returns>
        public async Task<AuthorViewModel> ObtainAuthor(int authorId)
        {
            var authors = await _repository.Select(p=> p.AuthorId == authorId);
            var viewModel = authors.AutoMapear<Author, AuthorViewModel>();
            return viewModel;
        }

        #endregion
    }
}
