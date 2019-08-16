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
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private string ModelError = string.Empty;
        public bool OperationSuccesful = false;

        #region Construtor

        public BookService(IBookRepository repositorio)
        {
            _repository = repositorio;
        }

        #endregion

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
        /// Cadastra um novo livro.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do livro.</param>
        /// <returns></returns>
        public async Task<BookViewModel> AddBook(BookViewModel viewModel)
        {
            #region Validação da regra de negócios

            if (await _repository.Exists(p => p.Title == viewModel.Title))
            {
                ModelError = string.Format(Criticas.Ja_Cadastrado_0, "Livro");
                return viewModel;
            } 

            #endregion

            var book = viewModel.AutoMapear<BookViewModel, Book>();
            _repository.Insert(book);
            OperationSuccesful = await _repository.Commit();

            //Recuperando o valor recebido pelo BookId.
            viewModel = book.AutoMapear<Book, BookViewModel>();

            return viewModel;
        }

        /// <summary>
        /// Altera informações de um livro cadastrado.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do livro.</param>
        /// <returns></returns>
        public async Task<BookViewModel> UpdateBook(BookViewModel viewModel)
        {
            if (await _repository.Exists(p => p.Title == viewModel.Title))
            {
                ModelError = string.Format(Criticas.Ja_Existe_0, "outro Livro com este título.");
                return viewModel;
            }

            var book = viewModel.AutoMapear<BookViewModel, Book>();
            _repository.Update(book);
            OperationSuccesful = await _repository.Commit();

            return viewModel;
        }

        /// <summary>
        /// Remove um livro.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do livro.</param>
        /// <returns></returns>
        public async Task<bool> DeleteBook(BookViewModel viewModel)
        {
            var book = viewModel.AutoMapear<BookViewModel, Book>();
            _repository.Delete(book);
            OperationSuccesful = await _repository.Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Deleta um livro cadastrado.
        /// </summary>
        /// <param name="BookId">Identificador do livro.</param>
        /// <returns></returns>
        public async Task<bool> DeleteBook(int BookId)
        {
            _repository.Delete(p=> p.BookId == BookId);
            OperationSuccesful = await _repository.Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Obtém uma lista com os livros cadastrados.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BookViewModel>> ObtainBooks()
        {
            var books = await _repository.SelectAll();
            var viewModel = books.AutoMapearLista<Book, BookViewModel>();
            return viewModel;
        }

        /// <summary>
        /// Obtém um livro a partir do seu identificador.
        /// </summary>
        /// <param name="BookId">Identificador do livro.</param>
        /// <returns></returns>
        public async Task<BookViewModel> ObtainBook(int BookId)
        {
            var books = await _repository.Select(p=> p.BookId == BookId);
            var viewModel = books.AutoMapear<Book, BookViewModel>();
            return viewModel;
        }
    }
}
