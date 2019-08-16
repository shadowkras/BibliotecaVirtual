﻿using BibliotecaVirtual.Application.Services;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(BookViewModel viewModel)
        {
            var Book = await _bookService.AddBook(viewModel);

            if (_bookService.IsSuccessful() == false)
            {
                AddModelError(_bookService.GetModelErrors());
                return View(nameof(Edit), Book);
            }

            return RedirectToAction(nameof(Edit), new { Book.BookId});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int BookId)
        {
            var Book = await _bookService.ObtainBook(BookId);
            return View(nameof(Edit), Book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookViewModel viewModel)
        {
            var Book = await _bookService.UpdateBook(viewModel);

            if (_bookService.IsSuccessful() == false)
            {
                AddModelError(_bookService.GetModelErrors());
                return View(nameof(Edit), Book);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int BookId)
        {
            await _bookService.DeleteBook(BookId);

            if (_bookService.IsSuccessful() == false)
            {
                AddModelError(_bookService.GetModelErrors());
            }

            return RedirectToAction(nameof(Index));
        }
    }
}