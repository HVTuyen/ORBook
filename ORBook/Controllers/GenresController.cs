using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ORBook.Data;
using ORBook.Models;
using ORBook.Service;

namespace ORBook.Controllers
{
    public class GenresController : Controller
    {
        private readonly ICommonDataService<Genre> _genreService;
        private readonly ORBookContext _context;

        public GenresController(ICommonDataService<Genre> genreService, ORBookContext context)
        {
            _genreService = genreService;
            _context = context;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            return View(await _genreService.GetAllAsync());
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _genreService.GetByIdAsync(id.Value);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        [Authorize(Roles = "Admin")]
        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                await _genreService.CreateAsync(genre);
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        [Authorize(Roles = "Admin")]
        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _genreService.GetByIdAsync(id.Value);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        [Authorize(Roles = "Admin")]
        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, byte[] rowVersion)
        {
            //if (id != genre.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    var genreUpdated = await _genreService.UpdateAsync(genre);
            //    if (genreUpdated != null)
            //    {
            //        return RedirectToAction(nameof(Index));
            //    }
            //    return NotFound();
            //}
            //return View(genre);
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Genre.FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null)
            {
                Genre deletedGenre = new Genre();
                await TryUpdateModelAsync(deletedGenre);
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The department was deleted by another user.");

                return View(deletedGenre);
            }
            _context.Entry(genre).Property("RowVersion").OriginalValue = rowVersion;

            if (await TryUpdateModelAsync<Genre>(
                genre,
                "",
                s => s.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Genre)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The department was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Genre)databaseEntry.ToObject();

                        if (databaseValues.Name != clientValues.Name)
                        {
                            ModelState.AddModelError("Name", $"Giá trị hiện tại: {databaseValues.Name}");
                        }

                        ModelState.AddModelError(string.Empty, "Bản ghi bạn đã cố gắng chỉnh sửa "
                                + "đã được sửa đổi bởi một người dùng khác. Thao tác chỉnh sửa đã bị hủy "
                                + "và các giá trị hiện tại trong cơ sở dữ liệu đã được hiển thị. "
                                + "Nếu bạn vẫn muốn chỉnh sửa bản ghi này, hãy bấm lại vào nút Lưu. "
                                + "Nếu không, hãy bấm vào siêu kết nối Quay lại Danh sách.");
                        genre.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
            }
            return View(genre);
        }

        [Authorize(Roles = "Admin")]
        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _genreService.GetByIdAsync(id.Value);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        [Authorize(Roles = "Admin")]
        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result =  await _genreService.DeleteAsync(id);
            if(result)
            {
                return RedirectToAction(nameof(Index));
            }
            var genre = await _genreService.GetByIdAsync(id);
            ModelState.AddModelError("Error", "Không thể xóa thể loại này!");
            return View(genre);
        }
    }
}
