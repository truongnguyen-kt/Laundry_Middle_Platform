using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Implement;
using Repository.Interface;
using Repository.IRepository;
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
                OnGet();

            }
            _user.AddNewCustomer(User);

            return RedirectToPage("/Index");
        }
    }
}
