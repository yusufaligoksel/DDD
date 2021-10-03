using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.API.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public IActionResult TestSuperAdmin()
        {
            return CreateActionResult("Sadece Superadminin erişebileceği bilgiler çekildi.", 200);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult TestAdmin()
        {
            return CreateActionResult("Sadece Adminin erişebileceği bilgiler çekildi.", 200);
        }
    }
}