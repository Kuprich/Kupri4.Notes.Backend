using IdentityServer4.Services;
using Kupri4.Notes.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kupri4.Notes.Identity.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IIdentityServerInteractionService interactionService) =>
            (_signInManager, _userManager, _interactionService) =
            (signInManager, userManager, interactionService);

        [HttpGet]
        public IActionResult Login(string returnUrl) =>
            View(new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
                return View(viewModel);

            var user = await _userManager.FindByNameAsync(viewModel.UserName);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(viewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);

            if (result.Succeeded)
                Redirect(viewModel.ReturnUrl);

            ModelState.AddModelError(string.Empty, "Login Error");
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl) =>
            View(new RegisterViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            if (await _userManager.FindByNameAsync(viewModel.UserName) != null)
            {
                ModelState.AddModelError(string.Empty, "User already exists");
                return View(viewModel);
            }

            AppUser user = new AppUser { UserName = viewModel.UserName };

            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Redirect(viewModel.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Error occured");
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }

    }
}
