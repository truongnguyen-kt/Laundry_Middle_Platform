using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Implements;
using Repository.Interface;
using Repository.IRepository;
using System.Text.RegularExpressions;
using Validation;

namespace LaundryMidlePlatform.Pages.Register
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private Utils validation = new Utils();
        private IUserRepository _user = new UserRepository();
        private IRoleRepository _role = new RoleRepository();

        [BindProperty]
        public User User { get; set; } = default!;

        [BindProperty]
        public string Error { get; set; }
        [BindProperty]
        public string Noti { get; set; }

        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult OnGet()
        {
            var roleList = _role.GetAllRole().Where(x => x.RoleId == 2 || x.RoleId == 3).ToList();
            ViewData["RoleId"] = new SelectList(roleList, "RoleId", "RoleName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User == null)
            {
                OnGet();

            }
            var checkUser = _user.GetUserByEmail(User.Email);
            if (checkUser.Count > 0)
            {
                Error = "This email already exists!";
                return OnGet();

            }
            if (!ValidateInputs())
            {
                return OnGet();
            }
            _user.AddNewCustomer(User);

            return RedirectToPage("/Index");
        }

        private bool ValidateInputs()
        {
            Utils utils = new Utils();
            var isValid = true;
            if (!Regex.IsMatch(User.Phone, "^[0-9]+$"))
            {
                ModelState.AddModelError("User.Phone", "Phone must contain only numeric digits (0-9)");
                isValid = false;
            }else if (!utils.checkTelephoneFormat(User.Phone))
            {
                ModelState.AddModelError("User.Phone", "Phone must has length from 10 to 13!");
                isValid = false;
            }


            if (User.DateOfBirth == DateTime.MinValue)
            {
                ModelState.AddModelError("User.DateOfBirth", "DateOfBirth cannot null");
                isValid = false;
            }
            else
            {
                // Tính khoảng thời gian giữa ngày sinh và ngày hiện tại
                TimeSpan ageDifference = DateTime.Now - (DateTime)User.DateOfBirth;

                // Tính tuổi dựa trên khoảng thời gian
                int age = (int)(ageDifference.TotalDays / 365.25); // Sử dụng 365.25 để xem xét năm nhuận

                // Kiểm tra xem tuổi có lớn hơn 11 hay không
                if (age < 12)
                {
                    ModelState.AddModelError("User.DateOfBirth", "You must be at least 12 years old.");
                    isValid = false;
                }
            }
            return isValid;
        }
    }
}
