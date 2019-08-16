using BibliotecaVirtual.Application.Services;
using BibliotecaVirtual.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Areas.Biblioteca.Controllers
{
    [Authorize]
    [Area("Biblioteca")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        #region Construtor

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        } 

        #endregion

        #region Métodos privados

        /// <summary>
        /// Adiciona uma mensagem de erro ao model state da view model.
        /// </summary>
        /// <param name="message"></param>
        private void AddModelError(string message)
        {
            ModelState.AddModelError(string.Empty, message);
        } 

        #endregion

        public async Task<IActionResult> Index()
        {
            var Books = await _bookService.ObtainBooks();
            return View(nameof(Index), Books);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(nameof(Edit), new BookViewModel());
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(BookViewModel viewModel)
        {
            var book = await _bookService.AddBook(viewModel);

            if (_bookService.IsSuccessful() == false)
            {
                AddModelError(_bookService.GetModelErrors());
                return View(nameof(Edit), book);
            }

            return RedirectToAction(nameof(Edit), new { book.BookId});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int bookId)
        {
            var book = await _bookService.ObtainBook(bookId);
            return View(nameof(Edit), book);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookViewModel viewModel)
        {
            var book = await _bookService.UpdateBook(viewModel);

            if (_bookService.IsSuccessful() == false)
            {
                AddModelError(_bookService.GetModelErrors());
                return View(nameof(Edit), book);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int bookId)
        {
            await _bookService.DeleteBook(bookId);

            if (_bookService.IsSuccessful() == false)
            {
                AddModelError(_bookService.GetModelErrors());
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
