using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ORBook.Data;
using ORBook.Models;
using ORBook.Models.ViewModels;
using ORBook.Service;
using ORBook.Service.BookGenres;
using ORBook.Service.Genres;

namespace ORBook.Controllers
{
    public class BooksController : Controller
    {
        private const int PAGESIZE = 3;

        private readonly ICommonDataService<Book> _bookService;
        private readonly ICommonDataService<Genre> _genreService;
        private readonly IBookGenreService _bookGenreService;
        private readonly ORBookContext _orbookcontext;

        public BooksController(
            ICommonDataService<Book> bookService, 
            ICommonDataService<Genre> genreService, 
            IBookGenreService bookGenreService,
            ORBookContext orbookcontext
            )
        {
            _orbookcontext = orbookcontext;
            _bookService = bookService;
            _genreService = genreService;
            _bookGenreService = bookGenreService;
        }

        // GET: Books
        
        public async Task<IActionResult> Index(int GenreId, string searchString, int Page = 1)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                HttpContext.Session.SetString("searchString", searchString);
                ViewBag.searchString = searchString;
            }
            else
            {
                HttpContext.Session.Remove("searchString");
            }
            if (GenreId > 0)
            {
                HttpContext.Session.SetInt32("genreId", GenreId);
                ViewBag.GenreId = GenreId;
            }
            else
            {
                HttpContext.Session.Remove("genreId");
            }

            ViewBag.Page = Page;

            var books = await _bookGenreService.GetSearchPage(GenreId, searchString, 0, PAGESIZE);
            var BookGenre = new BookGenreView
            {
                Books = await _bookGenreService.GetSearchPage(GenreId, searchString, Page, PAGESIZE),
                Genres = new SelectList(await _genreService.GetAllAsync(), "Id", "Name"),
            };
            ViewBag.PageCount = ((int)Math.Ceiling(books.Count() / (double)PAGESIZE));
            return View(BookGenre);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetByIdAsync(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles = "Admin")]
        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            var genres = await _genreService.GetAllAsync();
            var model = new BookEditView
            {
                Genres = genres
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookEditView model)
        {
            BookValidator validator = new BookValidator();

            ModelState.Clear();

            ValidationResult results = validator.Validate(model);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                model.Genres = await _genreService.GetAllAsync();
                return View(model);
            }
            //if (ModelState.IsValid)
            //{
                // Bắt đầu transaction
                using (var transaction = await _orbookcontext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var book = await _bookService.CreateAsync(model.Book);

                        if (book != null)
                        {
                            var bookGenre = await _bookGenreService.CreateAsync(book.Id, model.SelectedGenreIds.ToList() ?? new List<int>());
                            if (bookGenre)
                            {
                                // Commit transaction nếu tất cả thao tác thành công
                                await transaction.CommitAsync();
                                return RedirectToAction(nameof(Index));
                            }

                            // Nếu việc tạo thể loại thất bại, rollback transaction
                            await transaction.RollbackAsync();
                            return NotFound();
                        }

                        // Nếu việc tạo sách thất bại, rollback transaction
                        await transaction.RollbackAsync();
                        return NotFound();
                    }
                    catch (Exception)
                    {
                        // Nếu có bất kỳ lỗi nào, rollback transaction
                        await transaction.RollbackAsync();
                        return StatusCode(500, "Đã xảy ra lỗi khi tạo sách và thể loại.");
                    }
                }
            //}

            // Nếu ModelState không hợp lệ, load lại danh sách thể loại và trả về view với dữ liệu hiện tại
            //model.Genres = await _genreService.GetAllAsync();
            //return View(model);
        }

        [Authorize(Roles = "Admin")]
        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetByIdAsync(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            var genres = await _genreService.GetAllAsync();
            var model = new BookEditView
            {
                Book = book,
                Genres = genres,
                SelectedGenreIds = book.BookGenres.Select(bg => bg.GenreId).ToList()
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookEditView model)
        {

            if (ModelState.IsValid)
            {
                var bookUpdated = await _bookService.UpdateAsync(model.Book);
                var bookUpdatedGenre = await _bookGenreService.UpdateAsync(model.Book.Id, model.SelectedGenreIds.ToList() ?? new List<int>());
                if (bookUpdated != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            var book = await _bookService.GetByIdAsync(model.Book.Id);
            model.Genres = await _genreService.GetAllAsync();
            model.SelectedGenreIds = book.BookGenres.Select(bg => bg.GenreId).ToList();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetByIdAsync(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles = "Admin")]
        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookGenre = await _bookGenreService.DeleteAsync(id);
            if(bookGenre)
            {
                var book = await _bookService.DeleteAsync(id);
                if(book)
                {
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            return NotFound();
        }
    }
}
