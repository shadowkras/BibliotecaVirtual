using BibliotecaVirtual.Application.Resources;
using BibliotecaVirtual.Application.ViewModels;
using BibliotecaVirtual.Data.Entities;
using BibliotecaVirtual.Data.Extensions;
using BibliotecaVirtual.Data.Repositories;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookCategoryRepository _bookCategoryRepository;
        private string ModelError = string.Empty;
        public bool OperationSuccesful = false;

        #region Construtor

        public BookService(IBookRepository bookRepositorio,
                           IBookCategoryRepository bookCategoryRepository)
        {
            _bookRepository = bookRepositorio;
            _bookCategoryRepository = bookCategoryRepository;
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

            if (await _bookRepository.Exists(p => p.Title == viewModel.Title))
            {
                ModelError = string.Format(Criticas.Ja_Cadastrado_0, "Livro");
                return viewModel;
            }
            else if (string.IsNullOrEmpty(viewModel.CategoriesList) == true)
            {
                ModelError = "Nenhum gênero informado para o livro.";
                return viewModel;
            }

            #endregion

            var book = viewModel.AutoMapear<BookViewModel, Book>();

            //Convertendo a imagem de string para byte[].
            book.SetCoverImage(viewModel.CoverImageBase64);

            _bookRepository.Insert(book);

            #region Inserindo categorias

            if (string.IsNullOrEmpty(viewModel.CategoriesList) == false)
            {
                var categoryList = viewModel.CategoriesList.Split(',');
                foreach (var item in categoryList)
                {
                    var bookCategory = new BookCategory(book.BookId, int.Parse(item));
                    _bookCategoryRepository.Insert(bookCategory);
                }
            } 

            #endregion

            OperationSuccesful = await _bookRepository.Commit();

            //Recuperando o valor recebido pelo BookId.
            viewModel.BookId = book.BookId;

            return viewModel;
        }

        /// <summary>
        /// Altera informações de um livro cadastrado.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do livro.</param>
        /// <returns></returns>
        public async Task<BookViewModel> UpdateBook(BookViewModel viewModel)
        {
            #region Validação da regra de negócios

            if (await _bookRepository.Exists(p => p.Title == viewModel.Title && p.BookId != viewModel.BookId))
            {
                ModelError = string.Format(Criticas.Ja_Existe_0, "outro Livro com este título.");
                return viewModel;
            }
            else if (string.IsNullOrEmpty(viewModel.CategoriesList) == true)
            {
                ModelError = "Nenhum gênero informado para o livro.";
                return viewModel;
            }

            #endregion

            var book = viewModel.AutoMapear<BookViewModel, Book>();

            //Convertendo a imagem de string para byte[].
            book.SetCoverImage(viewModel.CoverImageBase64);

            _bookRepository.Update(book);

            #region Inserindo categorias

            if (string.IsNullOrEmpty(viewModel.CategoriesList) == false)
            {
                var categoryList = viewModel.CategoriesList.Split(',');
                foreach (var item in categoryList)
                {                    
                    var bookCategory = new BookCategory(book.BookId, int.Parse(item));

                    if (await _bookCategoryRepository.Exists(p => p.BookId == bookCategory.BookId &&
                                                           p.CategoryId == bookCategory.CategoryId) == false)
                    {
                        _bookCategoryRepository.Insert(bookCategory);
                    }
                    else
                    {
                        _bookCategoryRepository.Update(bookCategory);
                    }
                }
            }

            #endregion

            OperationSuccesful = await _bookRepository.Commit();

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
            _bookRepository.Delete(book);
            OperationSuccesful = await _bookRepository.Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Deleta um livro cadastrado.
        /// </summary>
        /// <param name="BookId">Identificador do livro.</param>
        /// <returns></returns>
        public async Task<bool> DeleteBook(int BookId)
        {
            _bookRepository.Delete(p => p.BookId == BookId);
            OperationSuccesful = await _bookRepository.Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Obtém uma lista com os livros cadastrados.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BookViewModel>> ObtainBooks()
        {
            var books = await _bookRepository.SelectList(null, p => new BookViewModel()
            {
                BookId = p.BookId,
                Title = p.Title,
                AuthorId = p.AuthorId,
                AuthorName = p.Author.Name, //Obtendo o nome do autor utilizando a propridade de navegação.
                PublishDate = p.PublishDate,
            });

            return books;
        }

        /// <summary>
        /// Obtém um livro a partir do seu identificador.
        /// </summary>
        /// <param name="bookId">Identificador do livro.</param>
        /// <returns></returns>
        public async Task<BookViewModel> ObtainBook(int bookId)
        {
            var books = await _bookRepository.Select(p => p.BookId == bookId);
            var categories = await _bookCategoryRepository.SelectList(p => p.BookId == bookId);

            var viewModel = books.AutoMapear<Book, BookViewModel>();

            //Criando uma lista de categorias separadas por virgula para levar até a view.
            foreach (var category in categories)
            {
                if (string.IsNullOrEmpty(viewModel.CategoriesList) == false)
                {
                    viewModel.CategoriesList += ",";
                }

                viewModel.CategoriesList += category.CategoryId.ToString();
            }

            //Convertendo a imagem de  byte[] para string.
            viewModel.CoverImageBase64 = books.CoverImage.AsStringBase64();

            return viewModel;
        }
    }
}
