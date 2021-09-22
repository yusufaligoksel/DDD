using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Domain.Entities;
using Identity.Persistence.Context;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IdentityContext _postgreSqlExampleDbContext;

        public TestController(IdentityContext postgreSqlExampleDbContext)
        {
            _postgreSqlExampleDbContext = postgreSqlExampleDbContext;
        }

        [HttpPost("test")]
        public IActionResult Test()
        {
            //_postgreSqlExampleDbContext.Add(new Role { Name = "SuperAdmin", Description = "Tum yetkilere sahip."});
            var entity = _postgreSqlExampleDbContext.Roles.FirstOrDefault(x => x.Id == 2);
            entity.Name = "Admin";
            entity.Description = "SuperAdmine gore bazı yetkileri kısıtlı";
            _postgreSqlExampleDbContext.Roles.Update(entity);
            _postgreSqlExampleDbContext.SaveChanges();
            return Ok("Başarılı");
        }
    }
}
