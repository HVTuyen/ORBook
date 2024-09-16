using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ORBook.Data;
using ORBook.Hubs;
using ORBook.Models;
using ORBook.Service;
using ORBook.Service.Books;
using ORBook.Service.BookUsers;
using ORBook.Service.Genres;
using ORBook.Service.Notifications;

namespace ORBook.Controllers
{
    public class VolumnsController : Controller
    {
        private readonly ICommonDataService<Volumn> _volumnService;
        private readonly INotificationService _notificationService;
        private readonly IBookUserService _bookUserService;
        public VolumnsController(
            ICommonDataService<Volumn> volumnService, 
            INotificationService notificationService, 
            IBookUserService bookUserService
            )
        {
            _volumnService = volumnService;
            _notificationService = notificationService;
            _bookUserService = bookUserService;
        }

        // GET: Volumns
        public async Task<IActionResult> Index()
        {
            return NotFound();
        }

        // GET: Volumns/Details/5
        public async Task<IActionResult> Details(int? id, int bookId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volumn = await _volumnService.GetByIdAsync(id.Value);

            if (volumn == null)
            {
                return NotFound();
            }
            ViewBag.BookId = bookId;
            return View(volumn);
        }

        [Authorize(Roles = "Admin")]
        // GET: Volumns/Create
        public IActionResult Create(int bookId)
        {
            ViewBag.BookId = bookId;
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Volumns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,BookId")] Volumn volumn)
        {
            if (ModelState.IsValid)
            {
                await _volumnService.CreateAsync(volumn);
                await _notificationService.NotifyUsersAsync(volumn);

                return RedirectToAction("Details", "Books", new { id = volumn.BookId });
            }
            return View(volumn);
        }

        [Authorize(Roles = "Admin")]
        // GET: Volumns/Edit/5
        public async Task<IActionResult> Edit(int? id, int bookId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volumn = await _volumnService.GetByIdAsync(id.Value);
            if (volumn == null)
            {
                return NotFound();
            }
            ViewBag.BookId = bookId;
            return View(volumn);
        }

        [Authorize(Roles = "Admin")]
        // POST: Volumns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CreatedTime,BookId")] Volumn volumn)
        {
            if (id != volumn.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var volumnUpdated = await _volumnService.UpdateAsync(volumn);
                if (volumnUpdated != null)
                {
                    return RedirectToAction("Details", "Books", new { id = volumn.BookId });
                }
                return NotFound();
            }
            return View(volumn);
        }

        [Authorize(Roles = "Admin")]
        // GET: Volumns/Delete/5
        public async Task<IActionResult> Delete(int? id, int bookId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volumn = await _volumnService.GetByIdAsync(id.Value);
            if (volumn == null)
            {
                return NotFound();
            }
            ViewBag.BookId = bookId;
            return View(volumn);
        }

        [Authorize(Roles = "Admin")]
        // POST: Volumns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int bookId)
        {
            var result = await _volumnService.DeleteAsync(id);
            if (result)
            {
                return RedirectToAction("Details", "Books", new { id = bookId });
            }
            return NotFound();
        }
    }
}
