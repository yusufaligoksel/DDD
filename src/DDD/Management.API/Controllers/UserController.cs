using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Management.Domain.Settings;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Management.API.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        // GET: api/<UserController>

        [HttpGet]
        public IActionResult GetUserInfo()
        {
            var user = $"{CurrentUser.Id} {CurrentUser.Name} {CurrentUser.Surname} {CurrentUser.Email}";
            return CreateActionResult(user, 200);
        }


        [HttpGet]
        public IActionResult GetMenu()
        {
            List<string> menus = new List<string>();
            menus.Add("Anasayfa");
            menus.Add("Kullanıcı Yönetimi");
            menus.Add("Ürün Yönetimi");
            menus.Add("Stok Yönetimi");
            return CreateActionResult(menus, 200);
        }
    }
}
