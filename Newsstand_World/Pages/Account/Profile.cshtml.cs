using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newsstand_World.Data;
using Newsstand_World.Model;
using System.Security.Claims;

namespace Newsstand_World.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfileModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Role { get; set; } = string.Empty;

        [BindProperty]
        public string CurrentPassword { get; set; } = string.Empty;

        [BindProperty]
        public string NewPassword { get; set; } = string.Empty;

        [BindProperty]
        public string ConfirmNewPassword { get; set; } = string.Empty;

        public AuthUser? CurrentUser { get; set; }
        public string SuccessMessage { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            var email = User.Identity?.Name;
            if (string.IsNullOrEmpty(email))
                return RedirectToPage("/Account/Login");

            CurrentUser = _context.AuthUsers.FirstOrDefault(u => u.Email == email);
            if (CurrentUser == null)
                return RedirectToPage("/Account/Login");

            Email = CurrentUser.Email;
            Role = CurrentUser.Role;

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateProfileAsync()
        {
            var email = User.Identity?.Name;
            if (string.IsNullOrEmpty(email))
                return RedirectToPage("/Account/Login");

            CurrentUser = _context.AuthUsers.FirstOrDefault(u => u.Email == email);
            if (CurrentUser == null)
                return RedirectToPage("/Account/Login");

            if (!string.IsNullOrEmpty(NewPassword))
            {
                if (NewPassword != ConfirmNewPassword)
                {
                    ErrorMessage = "Пароли не совпадают";
                    return Page();
                }

                if (NewPassword.Length < 6)
                {
                    ErrorMessage = "Пароль должен быть не менее 6 символов";
                    return Page();
                }

                CurrentUser.Password = NewPassword;
                await _context.SaveChangesAsync();
                SuccessMessage = "Пароль успешно изменен!";
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUploadAvatarAsync(IFormFile? avatarFile)
        {
            var email = User.Identity?.Name;
            if (string.IsNullOrEmpty(email))
                return RedirectToPage("/Account/Login");

            CurrentUser = _context.AuthUsers.FirstOrDefault(u => u.Email == email);
            if (CurrentUser == null)
                return RedirectToPage("/Account/Login");

            if (avatarFile == null || avatarFile.Length == 0)
            {
                ErrorMessage = "Необходимо выбрать файл для загрузки.";
                return Page();
            }

            if (avatarFile.Length > 2 * 1024 * 1024)
            {
                ErrorMessage = "Размер файла не должен превышать 2MB.";
                return Page();
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(avatarFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                ErrorMessage = "Допустимые форматы: JPG, PNG, GIF.";
                return Page();
            }

            if (!string.IsNullOrEmpty(CurrentUser.AvatarPath))
            {
                var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, CurrentUser.AvatarPath.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "avatars");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await avatarFile.CopyToAsync(stream);
            }

            CurrentUser.AvatarPath = $"/uploads/avatars/{fileName}";
            await _context.SaveChangesAsync();

            SuccessMessage = "Аватар успешно обновлен!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            var email = User.Identity?.Name;
            if (string.IsNullOrEmpty(email))
                return RedirectToPage("/Account/Login");

            CurrentUser = _context.AuthUsers.FirstOrDefault(u => u.Email == email);
            if (CurrentUser == null)
                return RedirectToPage("/Account/Login");

            if (string.IsNullOrEmpty(CurrentPassword))
            {
                ErrorMessage = "Введите текущий пароль";
                return Page();
            }

            if (string.IsNullOrEmpty(NewPassword))
            {
                ErrorMessage = "Введите новый пароль";
                return Page();
            }

            if (CurrentUser.Password != CurrentPassword)
            {
                ErrorMessage = "Неверный текущий пароль";
                return Page();
            }

            if (NewPassword != ConfirmNewPassword)
            {
                ErrorMessage = "Пароли не совпадают";
                return Page();
            }

            if (NewPassword.Length < 6)
            {
                ErrorMessage = "Пароль должен быть не менее 6 символов";
                return Page();
            }

            CurrentUser.Password = NewPassword;
            await _context.SaveChangesAsync();

            SuccessMessage = "Пароль успешно изменен!";
            return RedirectToPage();
        }
    }
}