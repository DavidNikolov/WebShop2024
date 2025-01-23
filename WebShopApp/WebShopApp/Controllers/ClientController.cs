using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShopApp.Core.Contracts;
using WebShopApp.Infrastructure.Data.Domain;
using WebShopApp.Models.Client;

namespace WebShopApp.Controllers;

public class ClientController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IOrderService _orderService;

        public ClientController(UserManager<ApplicationUser> userManager, IOrderService orderService)
        {
            this._userManager = userManager;
            _orderService = orderService;
        }

        public async Task<ActionResult> Index()
        {
            var allUsers = this._userManager.Users.Select(u => new ClientIndexVM
            {
                Id = u.Id,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Address = u.Address,
                Email = u.Email,
            })
            .ToList();

            var adminIds = (await _userManager.GetUsersInRoleAsync("Admin")).Select(a => a.Id).ToList();

            foreach (var user in allUsers)
            {
                user.IsAdmin = adminIds.Contains(user.Id);
            }

            var users = allUsers.Where(x => x.IsAdmin == false).OrderBy(x => x.UserName).ToList();

            return this.View(users);
        }
        public ActionResult Delete(string id)
        {
            var user = this._userManager.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            ClientDeleteVM userToDelete = new ClientDeleteVM()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                UserName = user.UserName
            };
            return View(userToDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(ClientDeleteVM bindingModel)
        {
            string id = bindingModel.Id;
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (_orderService.GetOrdersByUser(id).Count() == 0)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Success");
                }
            }
            else
            {
                return RedirectToAction("Denied");
            }
            return NotFound();
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Denied()
        {
            return View();
        }
}