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

            var Book = viewModel.AutoMapear<BookViewModel, Book>();
            _repository.Insert(Book);
            OperationSuccesful = await _repository.Commit();

            //Recuperando o valor recebido pelo BookId.
            viewModel = Book.AutoMapear<Book, BookViewModel>();

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

            var Book = viewModel.AutoMapear<BookViewModel, Book>();
            _repository.Update(Book);
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
            var Book = viewModel.AutoMapear<BookViewModel, Book>();
            _repository.Delete(Book);
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
            var Books = await _repository.SelectAll();
            var viewModel = Books.AutoMapearLista<Book, BookViewModel>();
            return viewModel;
        }

        /// <summary>
        /// Obtém um livro a partir do seu identificador.
        /// </summary>
        /// <param name="BookId">Identificador do livro.</param>
        /// <returns></returns>
        public async Task<BookViewModel> ObtainBook(int BookId)
        {
            var Books = await _repository.Select(p=> p.BookId == BookId);
            var viewModel = Books.AutoMapear<Book, BookViewModel>();
            return viewModel;
        }
    }
}
