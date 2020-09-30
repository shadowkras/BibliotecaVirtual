using BibliotecaVirtual.Application.Services;
using BibliotecaVirtual.Application.ViewModels;
using BibliotecaVirtual.Areas.Biblioteca.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BibliotecaVirtualTest
{
    public class BookControllerTest
    {
        private readonly BookController _controller;
        private readonly IBookService _bookService;

        public BookControllerTest()
        {
            _bookService = new BookServiceFake();
            _controller = new BookController(_bookService);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Add();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Index() as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<BookViewModel>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Edit(999);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testBookId = int.MaxValue;

            // Act
            var okResult = _controller.Edit(testBookId).Result;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testBookId = int.MaxValue;

            // Act
            var okResult = _controller.Edit(testBookId).Result as OkObjectResult;

            // Assert
            Assert.IsType<BookViewModel>(okResult.Value);
            Assert.Equal(testBookId, (okResult.Value as BookViewModel).BookId);
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingSinopsis = new BookViewModel()
            {
                Title = "Guinness",
                Description = "Some random record to be recorded."
            };

            _controller.ModelState.AddModelError(nameof(BookViewModel.Sinopsis), "Required");

            // Act
            var badResponse = _controller.Edit(nameMissingSinopsis).Result;

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            BookViewModel testItem = new BookViewModel()
            {
                Title = "Guinness Original 6 Pack",
                Description = "Guinness",
                Pages = int.MaxValue,
            };

            // Act
            var createdResponse = _controller.Edit(testItem).Result;

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new BookViewModel()
            {
                Title = "Guinness Original 6 Pack",
                Description = "Guinness",
                Pages = int.MaxValue,
            };

            // Act
            var createdResponse = _controller.Edit(testItem).Result as CreatedAtActionResult;
            var item = createdResponse.Value as BookViewModel;

            // Assert
            Assert.IsType<BookViewModel>(item);
            Assert.Equal("Guinness Original 6 Pack", item.Title);
        }

        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingGuid = 99999;

            // Act
            var badResponse = _controller.Delete(notExistingGuid).Result;

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var existingBookId = int.MaxValue;

            // Act
            var okResponse = _controller.Delete(existingBookId).Result;

            // Assert
            Assert.IsType<OkResult>(okResponse);
        }
        [Fact]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            // Arrange
            var existingBookId = int.MaxValue;

            // Act
            var okResponse = _controller.Delete(existingBookId).Result;
            var bookList = _bookService.ObtainBooks().Result;

            // Assert
            Assert.Equal(4, bookList.Count());
        }
    }
}
