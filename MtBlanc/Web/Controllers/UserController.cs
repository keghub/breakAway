using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BreakAway.Domain.Intranet.Users;
using BreakAway.Models.Users;

namespace BreakAway.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public ActionResult Index()
        {
            var viewModel = new IndexViewModel();

            viewModel.Users = GetUsers();

            return View(viewModel);
        }

        private IReadOnlyList<UserModel> GetUsers()
        {
            var result = _userRepository.Items.Select(p => new UserModel
            {
                Id = p.Id,
                Name =  p.Name,
                Email =  p.Email,
                Site = p.Site.Domain,
                Roles = p.Roles.Select(r=>r.Name)
            });

            return result.ToList();
        }
    }
}