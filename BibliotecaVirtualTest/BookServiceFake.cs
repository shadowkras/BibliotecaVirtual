using BibliotecaVirtual.Application.Services;
using BibliotecaVirtual.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BibliotecaVirtualTest
{
    public class BookServiceFake : IBookService
    {
        private readonly List<BookViewModel> _books;

        public BookServiceFake()
        {
            _books = new List<BookViewModel>()
            {
                new BookViewModel()
                {
                    BookId = -1,
                    Title = "Orange Juice", Description="Orange Tree", },
                new BookViewModel()
                {
                    BookId = 0,
                    Title = "Diary Milk", Description="Cow", },
                new BookViewModel()
                {
                    BookId = 1,
                    Title = "Frozen Pizza", Description="Uncle Mickey", },
                new BookViewModel()
                {
                    BookId = int.MaxValue,
                    Title = "Frozen Water", Description="Donald Duck", }
            };
        }

        public bool IsSuccessful()
        {
            throw new NotImplementedException();
        }

        public string GetModelErrors()
        {
            throw new NotImplementedException();
        }

        public async Task<BookViewModel> AddBook(BookViewModel viewModel)
        {
            await Task.Run(() => _books.Add(viewModel));
            return viewModel;
        }

        public async Task<BookViewModel> UpdateBook(BookViewModel viewModel)
        {
            var existing = _books.First(a => a.BookId == viewModel.BookId);
            await Task.Run(() => existing = viewModel);

            return viewModel;
        }

        public async Task<bool> DeleteBook(BookViewModel viewModel)
        {
            var existing = _books.First(a => a.BookId == viewModel.BookId);
            await Task.Run(() => _books.Remove(existing));

            return _books.Exists(p => p.BookId == viewModel.BookId);
        }

        public async Task<bool> DeleteBook(int bookId)
        {
            var existing = _books.First(a => a.BookId == bookId);
            await Task.Run(() => _books.Remove(existing));

            return _books.Exists(p => p.BookId == bookId);
        }

        public async Task<IEnumerable<BookViewModel>> ObtainBooks()
        {
            var books = await Task.Run(() => _books);
            return books;
        }

        public async Task<BookViewModel> ObtainBook(int bookId)
        {
            var existing = await Task.Run(() => _books.First(a => a.BookId == bookId));
            return existing;
        }

        public async Task<IEnumerable<BookViewModel>> ObtainBooks(string title, string author)
        {
            var existing = await Task.Run(() => _books.FindAll(a => a.Title.Contains(title) || 
                                                                    a.Author.Name.Contains(author)));
            return existing;
        }

        public Task<IEnumerable<BookViewModel>> ObtainBooks(string filtro)
        {
            throw new NotImplementedException();
        }
    }
}
