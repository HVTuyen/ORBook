using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ORBook.Data;
using ORBook.Models;
using ORBook.Models.ViewModels;
using ORBook.Service;
using ORBook.Service.Users;
using ORBook.Service.Genres;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using ORBook.Service.BookUsers;

namespace ORBook.Controllers
{
    public class UsersController : Controller
    {
        private readonly ICommonDataService<User> _commonDataService;
        private readonly IUserService _userService;
        private readonly IBookUserService _bookUserService;

        public UsersController(ICommonDataService<User> userCommonDataService, IUserService userService, IBookUserService bookUserService)
        {
            _commonDataService = userCommonDataService;
            _userService = userService;
            _bookUserService = bookUserService;
        }

        [Authorize(Roles = "Admin")]
        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _commonDataService.GetAllAsync());
        }

        [Authorize(Roles = "Admin")]
        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _commonDataService.GetByIdAsync(id.Value);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [Authorize(Roles = "Admin")]
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _commonDataService.GetByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,role")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userUpdated = await _commonDataService.UpdateAsync(user);
                if (userUpdated != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _commonDataService.GetByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _commonDataService.DeleteAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            var genre = await _commonDataService.GetByIdAsync(id);
            ModelState.AddModelError("Error", "Không thể xóa thể loại này!");
            return View(genre);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckLogin(LoginView login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.Login(login.Email, login.Password);
                if (user == null)
                {
                    ModelState.AddModelError("Login", "Email hoặc mật khẩu không đúng");
                    return View(login);
                }
                //HttpContext.Session.SetString("UserId", user.Id.ToString());
                //HttpContext.Session.SetString("UserRole", user.role);

                // Tạo các Claims
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Role, user.role) // Vai trò từ User.Role
                    };

                // Tạo ClaimsIdentity và ClaimsPrincipal
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Đăng ký ClaimsPrincipal vào HttpContext
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);


                return RedirectToAction("Index", "Home");
            }
            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear(); // Xóa session nếu có
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Users");
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost, ActionName("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmRegister(RegisterView register)
        {
            if (ModelState.IsValid)
            {
                if (!register.Password.Equals(register.ConfirmPassword))
                {
                    ModelState.AddModelError("ConfirmPassWord", "Mật khẩu không trùng nhau");
                    return View(register);
                }
                if(await _userService.CheckEmail(register.Email)) {
                    var user = new User
                    {
                        Name = register.Name,
                        Email = register.Email,
                        Password = register.Password,
                    };
                    await _commonDataService.CreateAsync(user);
                    //HttpContext.Session.SetString("UserId", user.Id.ToString());
                    //HttpContext.Session.SetString("UserRole", user.role);

                    // Tạo các Claims
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim(ClaimTypes.Role, user.role) // Vai trò từ User.Role
                        };

                    // Tạo ClaimsIdentity và ClaimsPrincipal
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    // Đăng ký ClaimsPrincipal vào HttpContext
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Email", "Email đã được sử dụng");
                return View(register);
            }
            return View(register);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Follow(int id, int bookId)
        {
            if(await _bookUserService.Follow(id, bookId))
            {
                return RedirectToAction("Details", "Books", new { id = bookId });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UnFollow(int id, int bookId)
        {
            if (await _bookUserService.UnFollow(id, bookId))
            {
                return RedirectToAction("Details", "Books", new { id = bookId });
            }
            return NotFound();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
